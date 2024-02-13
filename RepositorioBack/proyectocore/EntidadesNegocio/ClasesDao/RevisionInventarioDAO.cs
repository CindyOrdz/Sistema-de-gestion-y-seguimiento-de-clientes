using EntidadesNegocio.EntidadesDto;
using EntidadesNegocio.GestionInventario;
using EntidadesNegocio.InformacionVisita;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Inventario;
using EntidadesNegocio.Terceros;
using MySqlConnector;
using System.Numerics;

namespace EntidadesNegocio.ClasesDao
{
    public class RevisionInventarioDAO
    {
        private readonly MySqlConnection conexion_;

        public RevisionInventarioDAO(MySqlConnection conexion)
        {
            conexion_ = conexion;
        }

        public List<ElementoInventarioDTO> ListarInventario(long IdEmpresa,int paginaActual, int elementosPorPagina)
        {
            var limiteInferior = (paginaActual - 1) * elementosPorPagina;

            string query = "SELECT el.codigo,el.tipo_articulo,el.nombre,m.marca, CONCAT(el.ref1,' ',el.ref2,' ',el.ref3,' ',el.ref4, ' ', el.ref5) as referencia, el.sett,el.cantidad_disponible, el.cantidad_en_almacen ,el.cantidad_vendidos FROM elementos el INNER JOIN marcas m ON el.marca = m.id where el.idempresa = @IdEmpresa limit @limite_inferior,@elementos_pagina";

            List<ElementoInventarioDTO> Inventario = new List<ElementoInventarioDTO>();

            try
            {
                conexion_.Open();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
                cmd.Parameters.AddWithValue("@limite_inferior", limiteInferior);
                cmd.Parameters.AddWithValue("@elementos_pagina", elementosPorPagina);

                using (MySqlDataReader rdr = (MySqlDataReader) cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        ElementoInventarioDTO inventarioDTO = new ElementoInventarioDTO();
                        inventarioDTO.CodigoElemento = rdr.GetInt64(0);
                        inventarioDTO.TipoElemento = rdr.GetString(1);
                        inventarioDTO.Nombre = rdr.GetString(2);
                        inventarioDTO.Marca = rdr.GetString(3);
                        inventarioDTO.Referencia = rdr.GetString(4);
                        inventarioDTO.Sett = rdr.IsDBNull(5) ? "" : rdr.GetString(5);
                        inventarioDTO.CantidadDisponible = rdr.GetDouble(6);
                        inventarioDTO.CantidadAlmacen = rdr.GetDouble(7);
                        inventarioDTO.CantidadVendidos = rdr.GetDouble(8); 

                        Inventario.Add(inventarioDTO);
                    }
                }

                return Inventario;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public int ObtenerTotalElementosInventarioPorEmpresa(long idEmpresa)
        {
            string sql = "SELECT COUNT(el.codigo) as cantidad FROM elementos el where el.idempresa = @idEmpresa";
            int CantidadDatos = 0;
            try
            {
                conexion_.Open();
                using MySqlCommand command = new MySqlCommand(sql, conexion_);
                command.Parameters.AddWithValue("@idEmpresa", idEmpresa);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadDatos = reader.GetInt32("cantidad");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
            return CantidadDatos;

        }

        public async Task ProgramarRevision(RevisionInventario revision)
        {
            try
            {
                await conexion_.OpenAsync();

                // Insertar en la tabla revision inventario
                string insertVisitaQuery = "INSERT INTO revision_inventario (fechaInicio, fechaCreacion, idResponsable, descripcion) VALUES (@fechaInicio, @fechaCreacion, @idResponsable, @descripcion); SELECT LAST_INSERT_ID();";

                MySqlCommand cmdInsertRevision = new MySqlCommand(insertVisitaQuery, conexion_);

                cmdInsertRevision.Parameters.AddWithValue("@fechaCreacion", DateTime.Now);
                cmdInsertRevision.Parameters.AddWithValue("@fechaInicio", revision.ObtenerFechaInicio());
                cmdInsertRevision.Parameters.AddWithValue("@idResponsable", revision.ObtenerIdResponsable());
                cmdInsertRevision.Parameters.AddWithValue("@descripcion", revision.ObtenerDescripcion());

                int nuevaRevisionId = Convert.ToInt32(await cmdInsertRevision.ExecuteScalarAsync());

                string insertElementosRevisionQuery = "INSERT INTO elementos_por_revision (idRevision, idElemento) VALUES (@idRevision, @idElemento);";
                MySqlCommand cmdInsertElementosRevision = new MySqlCommand(insertElementosRevisionQuery, conexion_);

                foreach (ElementoInterfazGraficaVentaDTO elemento in revision.ObtenerListaElementosRevisados())
                {
                    cmdInsertElementosRevision.Parameters.Clear(); // Limpia los parámetros anteriores

                    cmdInsertElementosRevision.Parameters.AddWithValue("@idRevision", nuevaRevisionId);
                    cmdInsertElementosRevision.Parameters.AddWithValue("@idElemento", elemento.CodigoArticulo);

                    //Inserta en elementos_por_revision para cada elemento en la lista
                    await cmdInsertElementosRevision.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al programar la revisión: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public List<RevisionInventarioDTO> ListarRevisionesInventario(long idEmpresaLogueada, int paginaActual, int elementosPorPagina)
        {
            var limiteInferior = (paginaActual - 1) * elementosPorPagina;
            string query = "select i.idRevision, i.fechaInicio, i.fechaCreacion, i.idResponsable, CONCAT(COALESCE(t.NOMBRE1, ''),' ', COALESCE(t.NOMBRE2, ''),' ', COALESCE(t.APELLIDO1, ''),' ', COALESCE(t.APELLIDO2, '')) as completo, i.descripcion, i.estado from revision_inventario i JOIN terceros t on t.IDENTIFICACION = i.idResponsable JOIN empleados e on i.idResponsable =e.identificacion_empleado where e.identificacion_empresa = @idEmpresaLogueada order by fechaCreacion DESC limit @limite_inferior,@elementos_pagina;";

            List<RevisionInventarioDTO> listRevision = new List<RevisionInventarioDTO>();

            try
            {
                conexion_.Open();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@idEmpresaLogueada", idEmpresaLogueada);
                cmd.Parameters.AddWithValue("@limite_inferior", limiteInferior);
                cmd.Parameters.AddWithValue("@elementos_pagina", elementosPorPagina);

                using (MySqlDataReader rdr = (MySqlDataReader) cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        RevisionInventarioDTO revision = new RevisionInventarioDTO();
                        revision.id = rdr.GetInt32(0);
                        revision.fechaInicio = rdr.GetDateTime(1);
                        revision.fechaCreacion = rdr.GetDateTime(2);
                        revision.idResponsable = rdr.GetDouble(3);
                        revision.nombreResponsable = rdr.GetString(4);
                        revision.descripcion = rdr.GetString(5);
                        revision.estado = rdr.GetString(6);

                        listRevision.Add(revision);
                    }
                }

                return listRevision;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public int ObtenerTotalRevisionesAgendadas(BigInteger idEmpresa)
        {
            string sql = "select COUNT(i.idRevision) as cantidad from revision_inventario i JOIN empleados e on i.idResponsable =e.identificacion_empleado where e.identificacion_empresa = @idEmpresa;";
            int CantidadDatos = 0;
            try
            {
                conexion_.Open();
                using MySqlCommand command = new MySqlCommand(sql, conexion_);
                command.Parameters.AddWithValue("@idEmpresa", idEmpresa);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadDatos = reader.GetInt32("cantidad");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
            return CantidadDatos;

        }

        public async Task BorrarRevisionProgramada(BigInteger id)
        {
            string query = "DELETE FROM revision_inventario WHERE idRevision = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al borrar la revision programda: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task EditarRevisionConResponsable(RevisionInventario revision)
        {
            string query = "UPDATE revision_inventario SET fechaInicio = @fechaInicio, idResponsable = @idResponsable, descripcion = @descripcion WHERE idRevision = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", revision.ObtenerId());
            cmd.Parameters.AddWithValue("@fechaInicio", revision.ObtenerFechaInicio());
            cmd.Parameters.AddWithValue("@idResponsable", revision.ObtenerIdResponsable());
            cmd.Parameters.AddWithValue("@descripcion", revision.ObtenerDescripcion());
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al actualizar los detalles de la revision: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task EditarRevisionSinResponsable(RevisionInventario revision)
        {
            string query = "UPDATE revision_inventario SET fechaInicio = @fechaInicio, descripcion = @descripcion WHERE idRevision = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", revision.ObtenerId());
            cmd.Parameters.AddWithValue("@fechaInicio", revision.ObtenerFechaInicio());
            cmd.Parameters.AddWithValue("@descripcion", revision.ObtenerDescripcion());
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al actualizar los detalles de la revision: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task<List<RevisionInventarioDTO>> InformacionRevisionPorFecha(DateOnly fecha, long idResponsable)
        {
            string query = "SELECT i.idRevision, i.fechaInicio, i.fechaFinalizacion, i.fechaCreacion, i.idResponsable, CONCAT(COALESCE(t.NOMBRE1, ''),' ', COALESCE(t.NOMBRE2, ''),' ', COALESCE(t.APELLIDO1, ''),' ', COALESCE(t.APELLIDO2, '')) as completo, i.descripcion, i.estado from revision_inventario i JOIN terceros t on t.IDENTIFICACION = i.idResponsable where i.idResponsable =@idResponsable AND DATE(i.fechaInicio) =@fecha order by fechaCreacion DESC";

            List<RevisionInventarioDTO> listRevisiones = new List<RevisionInventarioDTO>();

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@fecha", fecha.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@idResponsable", idResponsable);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        RevisionInventarioDTO revision = new RevisionInventarioDTO();
                        revision.id = rdr.GetInt32(0);
                        revision.fechaInicio = rdr.GetDateTime(1);
                        revision.fechaFinalizacion = rdr.IsDBNull(2) ? DateTime.MinValue : rdr.GetDateTime(2);
                        revision.fechaCreacion = rdr.GetDateTime(3);
                        revision.idResponsable = rdr.GetDouble(4);
                        revision.nombreResponsable = rdr.GetString(5);
                        revision.descripcion = rdr.GetString(6);
                        revision.estado = rdr.GetString(7);

                        listRevisiones.Add(revision);
                    }
                }

                return listRevisiones;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task<List<RevisionInventarioDTO>> InformacionRevisionPorResponsable(String responsable)
        {
            string query = "SELECT i.idRevision, i.fechaInicio, i.fechaFinalizacion, i.fechaCreacion, i.idResponsable, CONCAT(t.NOMBRE1,' ',t.NOMBRE2,' ',t.APELLIDO1,' ',t.APELLIDO2) as completo, i.descripcion, i.estado from revision_inventario i JOIN terceros t on t.IDENTIFICACION = i.idResponsable JOIN empleados e on i.idResponsable =e.identificacion_empleado where e.identificacion_empresa = 5839382737 HAVING completo LIKE @responsable order by fechaInicio DESC";

            List<RevisionInventarioDTO> listRevision = new List<RevisionInventarioDTO>();

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@responsable", "%"+ responsable +"%");

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        RevisionInventarioDTO revision = new RevisionInventarioDTO();
                        revision.id = rdr.GetInt32(0);
                        revision.fechaInicio = rdr.GetDateTime(1);
                        revision.fechaFinalizacion = rdr.IsDBNull(2) ? DateTime.MinValue : rdr.GetDateTime(2);
                        revision.fechaCreacion = rdr.GetDateTime(3);
                        revision.idResponsable = rdr.GetDouble(4);
                        revision.nombreResponsable = rdr.GetString(5);
                        revision.descripcion = rdr.GetString(6);
                        revision.estado = rdr.GetString(7);

                        listRevision.Add(revision);
                    }
                }

                return listRevision;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        
        public async Task<List<ElementoInventarioDTO>> ListarElementosRevision(BigInteger idRevision)
        {
            string query = "SELECT r.idElemento, e.nombre, m.marca,  CONCAT(e.ref1,' ',e.ref2,' ',e.ref3,' ',e.ref4, ' ', e.ref5) as referencia, e.cantidad_disponible  from elementos_por_revision  r JOIN elementos e ON r.idElemento = e.codigo JOIN marcas m ON e.marca = m.id where r.idRevision = @idRevision";

            List<ElementoInventarioDTO> Elementos = new List<ElementoInventarioDTO>();

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@idRevision", idRevision);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        ElementoInventarioDTO elementoDTO = new ElementoInventarioDTO();
                        elementoDTO.CodigoElemento = rdr.GetInt32(0);
                        elementoDTO.Nombre = rdr.GetString(1);
                        elementoDTO.Marca = rdr.GetString(2);
                        elementoDTO.Referencia = rdr.GetString(3);
                        elementoDTO.CantidadDisponible = rdr.GetDouble(4);

                        Elementos.Add(elementoDTO);
                    }
                }

                return Elementos;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task EditarCantidadDisponible(BigInteger codigo, Double nuevaCantidad)
        {
            string query = "UPDATE elementos SET cantidad_disponible = @nuevaCantidad WHERE codigo = @codigo";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@codigo", codigo);
            cmd.Parameters.AddWithValue("@nuevaCantidad", nuevaCantidad);

            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al actualizar cantidad disponible: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task RegistrarBitacora(BitacoraInventarioDTO bitacora)
        {
            string query = "INSERT INTO bitacora_revision_inventario (idRevision, idElemento, cantidadAnterior,nuevaCantidad, observaciones,fecha) VALUES (@idRevision, @idElemento, @cantidadAnterior, @nuevaCantidad, @observaciones,@fecha) ";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);

            cmd.Parameters.AddWithValue("@idRevision", bitacora.IdRevision);
            cmd.Parameters.AddWithValue("@idElemento", bitacora.IdElemento);
            cmd.Parameters.AddWithValue("@cantidadAnterior", bitacora.CantidadAnterior);
            cmd.Parameters.AddWithValue("@nuevaCantidad", bitacora.NuevaCantidad);
            cmd.Parameters.AddWithValue("@observaciones", bitacora.Observaciones);
            cmd.Parameters.AddWithValue("@fecha", DateTime.Now);

            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al agregar registro a bitacora: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task<int> RegistrarAnomalia(AnomaliaRevisionInventario anomalia)
        {
            string query = "INSERT INTO anomalias_inventario (descripcion, idRevision, idElemento) VALUES (@descripcion, @idRevision, @idElemento); SELECT LAST_INSERT_ID();";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);

            cmd.Parameters.AddWithValue("@descripcion", anomalia.ObtenerDescripcion());
            cmd.Parameters.AddWithValue("@idRevision", anomalia.ObtenerIdRevision());
            cmd.Parameters.AddWithValue("@idElemento", anomalia.ObtenerIdElemento());

            try
            {
                await conexion_.OpenAsync();
                int idInsertado = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                return idInsertado;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al registrar anomalia: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task<List<AnomaliaInventarioDTO>> ListarEvidenciaAnomaliasPorRevision(BigInteger idRevision)
        {
            string query = "SELECT a.idAnomalia, a.descripcion, a.idElemento, r.nombreArchivo, r.tipoArchivo,r.fechaRegistro,r.fechaCaptura, r.rutaRelativa, r.idEvidencia from anomalias_inventario a INNER JOIN registro_evidencia r on  a.idAnomalia = r.idTipoEvidencia where a.idRevision =@idRevision and r.tipoEvidencia ='anomalia'";

            List<AnomaliaInventarioDTO> listAnomalias = new List<AnomaliaInventarioDTO>();

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@idRevision", idRevision);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        AnomaliaInventarioDTO anomalia = new AnomaliaInventarioDTO();
                        anomalia.IdAnomalia = rdr.GetInt32(0);
                        anomalia.Descripcion = rdr.GetString(1);
                        anomalia.IdElemento = rdr.GetInt32(2);
                        anomalia.NombreArchivo = rdr.GetString(3);
                        anomalia.TipoArchivo = rdr.GetString(4);
                        anomalia.FechaRegistro = rdr.GetDateTime(5);
                        anomalia.FechaCaptura = rdr.GetDateTime(6);
                        anomalia.Ruta = rdr.GetString(7);
                        anomalia.IdEvidencia = rdr.GetInt32(8);

                        listAnomalias.Add(anomalia);
                    }
                }

                return listAnomalias;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task BorrarAnomalia(BigInteger id)
        {
            string query = "DELETE FROM anomalias_inventario WHERE idAnomalia = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al eliminar la anomalia: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public List<BitacoraInventarioDTO> ObtenerBitacoraRevisionesPorEmpresa(long idEmpresa, int paginaActual, int elementosPorPagina)
        {
            var limiteInferior = (paginaActual - 1) * elementosPorPagina;
            string query = "SELECT b.idRevision, r.idResponsable, CONCAT(COALESCE(t.NOMBRE1, ''),' ', COALESCE(t.NOMBRE2, ''),' ', COALESCE(t.APELLIDO1, ''),' ', COALESCE(t.APELLIDO2, '')) as completo, b.idElemento,el.nombre, b.cantidadAnterior, b.nuevaCantidad, b.observaciones, b.fecha from bitacora_revision_inventario b INNER JOIN revision_inventario r ON r.idRevision=b.idRevision INNER JOIN empleados e ON e.identificacion_empleado=r.idResponsable INNER JOIN elementos el ON el.codigo=b.idElemento INNER JOIN terceros t ON t.IDENTIFICACION = r.idResponsable where e.identificacion_empresa=@idEmpresa ORDER BY b.fecha DESC limit @limite_inferior,@elementos_pagina;";

            List<BitacoraInventarioDTO> listBitacora = new List<BitacoraInventarioDTO>();

            try
            {
                conexion_.Open();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                cmd.Parameters.AddWithValue("@limite_inferior", limiteInferior);
                cmd.Parameters.AddWithValue("@elementos_pagina", elementosPorPagina);

                using (MySqlDataReader rdr = (MySqlDataReader) cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        BitacoraInventarioDTO bitacora = new BitacoraInventarioDTO();
                        bitacora.IdRevision = rdr.GetInt32(0);
                        bitacora.IdResponsable = rdr.GetDouble(1);
                        bitacora.NombreResponsable = rdr.GetString(2);
                        bitacora.IdElemento = rdr.GetInt32(3);
                        bitacora.NombreElemento = rdr.GetString(4);
                        bitacora.CantidadAnterior = rdr.GetDouble(5);
                        bitacora.NuevaCantidad = rdr.GetDouble(6);
                        bitacora.Observaciones = rdr.GetString(7);
                        bitacora.fecha = rdr.GetDateTime(8);

                        listBitacora.Add(bitacora);
                    }
                }

                return listBitacora;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public int ObtenerTotalDatosBitacora(long idEmpresa)
        {
            string sql = "SELECT COUNT(b.idRevision) as cantidad from bitacora_revision_inventario b INNER JOIN revision_inventario r ON r.idRevision=b.idRevision INNER JOIN empleados e ON e.identificacion_empleado=r.idResponsable where e.identificacion_empresa= @idEmpresa";
            int CantidadDatos = 0;
            try
            {
                conexion_.Open();
                using MySqlCommand command = new MySqlCommand(sql, conexion_);
                command.Parameters.AddWithValue("@idEmpresa", idEmpresa);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadDatos = reader.GetInt32("cantidad");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
            return CantidadDatos;

        }

        public List<CatalogoInterfazGraficaVentaDTO> ObtenerCatalogoPorEmpresa(long idEmpresa, int paginaActual, int elementosPorPagina)
        {
            var limiteInferior = (paginaActual - 1) * elementosPorPagina;
            string query = "SELECT id, clasificacion, descripcion FROM catalogos where idempresa = @idEmpresa ORDER BY clasificacion ASC limit @limite_inferior,@elementos_pagina;";

            List<CatalogoInterfazGraficaVentaDTO> listaCatologos = new List<CatalogoInterfazGraficaVentaDTO>();

            try
            {
                conexion_.Open();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                cmd.Parameters.AddWithValue("@limite_inferior", limiteInferior);
                cmd.Parameters.AddWithValue("@elementos_pagina", elementosPorPagina);

                using (MySqlDataReader rdr = (MySqlDataReader)cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        CatalogoInterfazGraficaVentaDTO catalogo = new CatalogoInterfazGraficaVentaDTO();
                        catalogo.id = rdr.GetInt64(0);
                        catalogo.clasificacion = rdr.GetString(1);
                        catalogo.descripcion = rdr.GetString(2);


                        listaCatologos.Add(catalogo);
                    }
                }

                return listaCatologos;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public int ObtenerTotalCatalogoPorEmpresa(long idEmpresa)
        {
            string sql = "SELECT COUNT(id) as cantidad FROM catalogos where idempresa = @idEmpresa";
            int CantidadDatos = 0;
            try
            {
                conexion_.Open();
                using MySqlCommand command = new MySqlCommand(sql, conexion_);
                command.Parameters.AddWithValue("@idEmpresa", idEmpresa);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadDatos = reader.GetInt32("cantidad");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
            return CantidadDatos;

        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosCatalogo(long idCatalogo, int paginaActual, int elementosPorPagina)
        {
            var limiteInferior = (paginaActual - 1) * elementosPorPagina;
            string query = "SELECT el.codigo,el.nombre,m.marca, CONCAT(el.ref1,' ',el.ref2,' ',el.ref3,' ',el.ref4, ' ', el.ref5) as referencia FROM elementos el INNER JOIN marcas m ON el.marca = m.id INNER JOIN catalogos_elementos c ON c.id_elemento=el.codigo where c.id_catalogo =@idCatalogo ORDER BY el.codigo limit @limite_inferior,@elementos_pagina;";

            List<ElementoInterfazGraficaVentaDTO> listaElementos = new List<ElementoInterfazGraficaVentaDTO>();

            try
            {
                conexion_.Open();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@idCatalogo", idCatalogo);
                cmd.Parameters.AddWithValue("@limite_inferior", limiteInferior);
                cmd.Parameters.AddWithValue("@elementos_pagina", elementosPorPagina);

                using (MySqlDataReader rdr = (MySqlDataReader)cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        ElementoInterfazGraficaVentaDTO elemento = new ElementoInterfazGraficaVentaDTO();
                        elemento.CodigoArticulo = rdr.GetInt64(0);
                        elemento.Nombre = rdr.GetString(1);
                        elemento.Marca = rdr.GetString(2);
                        elemento.Referencia = rdr.GetString(3);

                        listaElementos.Add(elemento);
                    }
                }

                return listaElementos;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public int ObtenerTotalElementosPorCatalogo(long idCatalogo)
        {
            string sql = "SELECT COUNT(id_elemento) as cantidad from catalogos_elementos where id_catalogo = @idCatalogo;";
            int CantidadDatos = 0;
            try
            {
                conexion_.Open();
                using MySqlCommand command = new MySqlCommand(sql, conexion_);
                command.Parameters.AddWithValue("@idCatalogo", idCatalogo);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadDatos = reader.GetInt32("cantidad");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
            return CantidadDatos;

        }

        public RevisionInventarioDTO InformacionRevisionPorID(BigInteger idRevision)
        {
            string query = "SELECT r.fechaInicio,r.fechaFinalizacion,r.fechaCreacion,r.idResponsable,CONCAT(COALESCE(t.NOMBRE1, ''),' ', COALESCE(t.NOMBRE2, ''),' ', COALESCE(t.APELLIDO1, ''),' ', COALESCE(t.APELLIDO2, '')) as nombreResponsable, r.descripcion FROM revision_inventario r INNER JOIN terceros t ON t.IDENTIFICACION=r.idResponsable where idRevision=@idRevision;";

            RevisionInventarioDTO revision = new RevisionInventarioDTO();

            try
            {
                conexion_.Open();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@idRevision", idRevision);

                using (MySqlDataReader rdr = (MySqlDataReader) cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        revision.fechaInicio = rdr.GetDateTime(0);
                        revision.fechaFinalizacion = rdr.GetDateTime(1);
                        revision.fechaCreacion = rdr.GetDateTime(2);
                        revision.idResponsable = rdr.GetDouble(3);
                        revision.nombreResponsable = rdr.GetString(4);
                        revision.descripcion = rdr.GetString(5);
                    }
                }

                return revision;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }
        //INFORMES

        public (long, long) ObtenerDatosParaGrafica(long IdEmpresa)
        {
            string consultaRevisionesConAnomalias = "SELECT COUNT(DISTINCT a.idRevision) FROM anomalias_inventario a INNER JOIN revision_inventario r ON r.idRevision = a.idRevision JOIN empleados e ON r.idResponsable =e.identificacion_empleado where e.identificacion_empresa=@IdEmpresa AND MONTH(r.fechaInicio) = MONTH(NOW()) AND YEAR(r.fechaInicio) = YEAR(NOW()) AND r.estado='Finalizada';";

            string consultaRevisionesSinAnomalias = "SELECT COUNT(*) FROM revision_inventario r JOIN empleados e ON r.idResponsable =e.identificacion_empleado where e.identificacion_empresa=@IdEmpresa AND r.estado='Finalizada' AND MONTH(r.fechaInicio) = MONTH(NOW()) AND YEAR(r.fechaInicio) = YEAR(NOW()) AND r.idRevision NOT IN (SELECT DISTINCT a.idRevision FROM anomalias_inventario a);";

            long cantidadRevisionesConAnomalias = 0;
            long cantidadRevisionesSinAnomalias = 0;

            try
            {
                conexion_.Open();

                // Consulta 1: Obtener cantidad de revisiones con anomalías
                MySqlCommand cmd1 = new MySqlCommand(consultaRevisionesConAnomalias, conexion_);
                cmd1.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
                object result1 = cmd1.ExecuteScalar();
                cantidadRevisionesConAnomalias = result1 == null ? 0 : Convert.ToInt64(result1);

                // Consulta 2: Obtener cantidad de revisiones sin anomalías
                MySqlCommand cmd2 = new MySqlCommand(consultaRevisionesSinAnomalias, conexion_);
                cmd2.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
                object result2 = cmd2.ExecuteScalar();
                cantidadRevisionesSinAnomalias = result2 == null ? 0 : Convert.ToInt64(result2);

                return (cantidadRevisionesConAnomalias, cantidadRevisionesSinAnomalias);
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task<List<ElementoInventarioDTO>> ElementosConAnomaliasMesActual(long idEmpresa)
        {
            string query = "SELECT a.idElemento, e.nombre, CONCAT(e.ref1,' ',e.ref2,' ',e.ref3,' ',e.ref4, ' ', e.ref5) as referencia, r.idRevision from anomalias_inventario a INNER JOIN elementos e ON e.codigo=a.idElemento INNER JOIN revision_inventario r ON r.idRevision=a.idRevision INNER JOIN empleados em ON em.identificacion_empleado=r.idResponsable WHERE MONTH(r.fechaInicio) = MONTH(NOW()) AND YEAR(r.fechaInicio) = YEAR(NOW()) AND em.identificacion_empresa=@idEmpresa order by a.idElemento;";

            List<ElementoInventarioDTO> listElementos = new List<ElementoInventarioDTO>();

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@idEmpresa", idEmpresa);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        ElementoInventarioDTO elemento = new ElementoInventarioDTO();
                        elemento.CodigoElemento = rdr.GetInt32(0);
                        elemento.Nombre = rdr.GetString(1);
                        elemento.Referencia= rdr.GetString(2);
                        elemento.IdRevision = rdr.GetInt32(3);

                        listElementos.Add(elemento);
                    }
                }

                return listElementos;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task<List<RevisionInventarioDTO>> CantidadRevisionesPorResponsable(long IdEmpresa)
        {
            string query = "SELECT r.idResponsable, concat(t.NOMBRE1, ' ',COALESCE(t.NOMBRE2, ''), ' ', t.APELLIDO1, ' ', t.APELLIDO2) as nombre, COUNT(r.idRevision) AS cantidad_revisiones FROM revision_inventario r INNER JOIN terceros t ON t.IDENTIFICACION=r.idResponsable INNER JOIN empleados e ON e.identificacion_empleado=r.idResponsable WHERE MONTH(r.fechaInicio) = MONTH(NOW()) AND YEAR(r.fechaInicio) = YEAR(NOW()) AND e.identificacion_empresa= @IdEmpresa GROUP BY r.idResponsable;";

            List<RevisionInventarioDTO> listRevisiones = new List<RevisionInventarioDTO>();

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        RevisionInventarioDTO revision = new RevisionInventarioDTO();
                        revision.idResponsable= rdr.GetDouble(0);
                        revision.nombreResponsable = rdr.GetString(1);
                        revision.cantidad = rdr.GetInt32(2);


                        listRevisiones.Add(revision);
                    }
                }

                return listRevisiones;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task FinalizarRevision(BigInteger id)
        {
            string query = "UPDATE revision_inventario SET fechaFinalizacion = @fechaFinalizacion, estado = 'Finalizada' WHERE idRevision = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@fechaFinalizacion", DateTime.Now);
            
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al finalizar la revision: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

    }
}
