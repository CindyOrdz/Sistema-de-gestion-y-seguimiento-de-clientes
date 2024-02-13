using EntidadesNegocio.EntidadesDto;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Venta;
using MySqlConnector;
using System.Numerics;

namespace EntidadesNegocio.ClasesDao
{
    public class VisitaDAO
    {
        private readonly MySqlConnection conexion_;

        public VisitaDAO(MySqlConnection conexion)
        {
            conexion_ = conexion;
        }

        public List<PlantaEmpresaClienteDTO> ListarPlantasPorIdSede(BigInteger idSede)
        {
            string query = "SELECT id,nombre FROM plantaempresacliente where id_sede= @idSede";

            List<PlantaEmpresaClienteDTO> listaPlantas = new List<PlantaEmpresaClienteDTO>();

            try
            {
                conexion_.Open();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@idSede", idSede);

                using (MySqlDataReader rdr = (MySqlDataReader) cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        PlantaEmpresaClienteDTO planta = new PlantaEmpresaClienteDTO();
                        planta.id = rdr.GetInt32(0);
                        planta.nombre = rdr.GetString(1);

                        listaPlantas.Add(planta);
                    }
                }

                return listaPlantas;
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

        public List<CabeceraHistorialDTO> ObtenerHistorial(long idEmpresa, double idCliente, int paginaActual, int elementosPorPagina)
        {
            var limiteInferior = (paginaActual - 1) * elementosPorPagina;

            string sql = "select vent.id_venta_transaccion, vent.tipo_venta, vent.fecha, vent.hora,  vent.total, vent.sub_total, vent.iva, " +
                "concat(ter.NOMBRE1, ' ',COALESCE(ter.NOMBRE2, ''), ' ', ter.APELLIDO1, ' ', ter.APELLIDO2) as nombre_responsable " +
                "from ventas vent inner join terceros ter on vent.responsable= ter.IDENTIFICACION " +
                "where vent.idempresa = @idEmpresa and vent.cliente= @idCliente and (vent.tipo_venta ='FACTURA' or  vent.tipo_venta ='ORDEN_PEDIDO') order by vent.fecha desc, vent.hora desc " +
                "limit @limite_inferior,@elementos_pagina";

            var listaVentas = new List<CabeceraHistorialDTO>();
            try
            {
                conexion_.Open();
                using MySqlCommand command = new MySqlCommand(sql, conexion_);
                command.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                command.Parameters.AddWithValue("@idCliente", idCliente);
                command.Parameters.AddWithValue("@limite_inferior", limiteInferior);
                command.Parameters.AddWithValue("@elementos_pagina", elementosPorPagina);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var ventaDTO = new CabeceraHistorialDTO();


                    ventaDTO.IdVenta = reader.GetInt64("id_venta_transaccion");
                    ventaDTO.FechaVenta = reader.GetDateOnly("fecha");
                    ventaDTO.Hora = reader.GetTimeOnly("hora");
                    ventaDTO.TipoVenta = reader.GetString("tipo_venta");
                    ventaDTO.Total = reader.GetDouble("total");
                    ventaDTO.SubTotal = reader.GetDouble("sub_total");
                    ventaDTO.NombreResponsable = reader.GetString("nombre_responsable");
                    ventaDTO.IVA = reader.GetDouble("iva");

                    listaVentas.Add(ventaDTO);

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


            return listaVentas;

        }


        public CabeceraHistorialDTO ObtenerCabecerVentaAsociada(long idVenta, long idEmpresa)
        {
            string sql = "select vent.id_venta_transaccion, vent.tipo_venta, vent.fecha, vent.hora,  vent.total, vent.sub_total,vent.iva, concat(ter.NOMBRE1, ' ',COALESCE(ter.NOMBRE2, ''), ' ', ter.APELLIDO1, ' ', ter.APELLIDO2) as nombre_responsable, vent.cliente, t.RAZON_SOCIAL from ventas vent inner join terceros ter on vent.responsable= ter.IDENTIFICACION inner join terceros t on vent.cliente= t.IDENTIFICACION  where vent.id_venta_transaccion=@idVenta and vent.idempresa=@idEmpresa;";

            var ventaDTO = new CabeceraHistorialDTO();
            try
            {
                conexion_.Open();
                using MySqlCommand command = new MySqlCommand(sql, conexion_);
                command.Parameters.AddWithValue("@idVenta", idVenta);
                command.Parameters.AddWithValue("@idEmpresa", idEmpresa);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    ventaDTO.IdVenta = reader.GetInt64("id_venta_transaccion");
                    ventaDTO.FechaVenta = reader.GetDateOnly("fecha");
                    ventaDTO.Hora = reader.GetTimeOnly("hora");
                    ventaDTO.TipoVenta = reader.GetString("tipo_venta");
                    ventaDTO.Total = reader.GetDouble("total");
                    ventaDTO.SubTotal = reader.GetDouble("sub_total");
                    ventaDTO.NombreResponsable = reader.GetString("nombre_responsable");
                    ventaDTO.IVA = reader.GetDouble("iva");
                    ventaDTO.IdCliente =reader.GetDouble("cliente");
                    ventaDTO.NombreCliente =  reader.GetString("RAZON_SOCIAL");
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


            return ventaDTO;

        }

        public List<DetalleVentaHistorialClienteDTO> ObtenerDetallesVenta(long idVenta)
        {
            string sql = "select  det.cantidad, elmt.codigo, elmt.nombre, " +
                "concat(elmt.ref1,' ', elmt.ref2, ' ', elmt.ref3, ' ', elmt.ref4,' ', elmt.ref5) as referencia, " +
                "elmt.valor, mrc.marca " +
                "from venta_detalles det inner join elementos elmt on det.id_elemento = elmt.codigo " +
                "inner join marcas mrc on mrc.id = elmt.marca " +
                "where det.id_venta =@id_venta";

            var listaDetalles = new List<DetalleVentaHistorialClienteDTO>();
            try
            {
                conexion_.Open();
                using MySqlCommand command = new MySqlCommand(sql, conexion_);
                command.Parameters.AddWithValue("@id_venta", idVenta);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var DetalleVentaDTO = new DetalleVentaHistorialClienteDTO { Elemento = new ElementoHistorialClienteDTO() };

                    DetalleVentaDTO.Cantidad = reader.GetDouble("cantidad");
                    DetalleVentaDTO.Elemento.Codigo = reader.GetInt64("codigo");
                    DetalleVentaDTO.Elemento.Nombre = reader.GetString("nombre");
                    DetalleVentaDTO.Elemento.Referencia = reader.GetString("referencia");
                    DetalleVentaDTO.Elemento.Valor = reader.GetDouble("valor");
                    DetalleVentaDTO.Elemento.Marca = reader.GetString("marca");
                    listaDetalles.Add(DetalleVentaDTO);

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


            return listaDetalles;


        }

        
        public int ObtenerTotalVentasCliente(long idEmpresa, double idCliente)
        {
            string sql = "SELECT COUNT(vent.id_venta_transaccion) as cantidad FROM ventas vent " +
                "where vent.idempresa = @id_empresa and vent.cliente=@id_cliente and (vent.tipo_venta ='FACTURA' or  vent.tipo_venta ='ORDEN_PEDIDO')";
            int CantidadventasDelCliente = 0;
            try
            {
                conexion_.Open();
                using MySqlCommand command = new MySqlCommand(sql, conexion_);
                command.Parameters.AddWithValue("@id_empresa", idEmpresa);
                command.Parameters.AddWithValue("@id_cliente", idCliente);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadventasDelCliente = reader.GetInt32("cantidad");
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
            return CantidadventasDelCliente;

        }


        public int VerificarVenta(long idVenta)
        {
            string sql = "SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS result FROM ventas WHERE id_venta_transaccion = @idVenta AND anulado ='N' AND (tipo_venta='FACTURA' OR tipo_venta='ORDEN_PEDIDO');";
            int verificacion = 0;
            try
            {
                conexion_.Open();
                using MySqlCommand command = new MySqlCommand(sql, conexion_);
                command.Parameters.AddWithValue("@idVenta", idVenta);
                
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    verificacion = reader.GetInt32(0);
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
            return verificacion;

        }

        public void AsociarVentaAVisita(long idVenta, BigInteger idVisita)
        {
            string query = "UPDATE visita SET idVentaAsociada = @idVenta WHERE id = @idVisita;";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@idVenta", idVenta);
            cmd.Parameters.AddWithValue("@idVisita", idVisita);

            try
            {
                conexion_.Open();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al asociar la venta a la visita: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task FinalizarVisita(BigInteger id)
        {
            string query = "UPDATE visita SET estado = 'Finalizada' WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", id);
            
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al finalizar la visita: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        //INFORMES

        public (long, long) ObtenerDatosParaGrafica(long IdEmpresa)
        {
            string consultaVisitasProgramadas = "SELECT COUNT(id) FROM visita where idEmpresaPrestaServicio = @IdEmpresa AND MONTH(fechaInicio) = MONTH(NOW()) AND YEAR(fechaInicio) = YEAR(NOW()) AND estado='Programada';";

            string consultaVisitasFinalizadas = "SELECT COUNT(id) FROM visita where idEmpresaPrestaServicio = @IdEmpresa AND MONTH(fechaInicio) = MONTH(NOW()) AND YEAR(fechaInicio) = YEAR(NOW()) AND estado='Finalizada';";

            long cantidadProgramadas = 0;
            long cantidadFinalizadas = 0;

            try
            {
                conexion_.Open();

                // Consulta 1: Obtener cantidad de visitas programadas
                MySqlCommand cmd1 = new MySqlCommand(consultaVisitasProgramadas, conexion_);
                cmd1.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
                object result1 = cmd1.ExecuteScalar();
                cantidadProgramadas = result1 == null ? 0 : Convert.ToInt64(result1);

                // Consulta 2: Obtener cantidad de visitas finalizadas
                MySqlCommand cmd2 = new MySqlCommand(consultaVisitasFinalizadas, conexion_);
                cmd2.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
                object result2 = cmd2.ExecuteScalar();
                cantidadFinalizadas = result2 == null ? 0 : Convert.ToInt64(result2);

                return (cantidadProgramadas, cantidadFinalizadas);
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

        public async Task<List<VisitaDTO>> CantidadVisitasPorResponsable(long IdEmpresa)
        {
            string query = "SELECT v.idResponsable,concat(t.NOMBRE1, ' ',COALESCE(t.NOMBRE2, ''), ' ', t.APELLIDO1, ' ', t.APELLIDO2) as nombre, count(v.id) as cantidad_visitas FROM visita v INNER JOIN terceros t ON t.IDENTIFICACION=v.idResponsable INNER JOIN empleados e ON e.identificacion_empleado=v.idResponsable WHERE MONTH(v.fechaInicio) = MONTH(NOW()) AND YEAR(v.fechaInicio) = YEAR(NOW()) AND e.identificacion_empresa= @IdEmpresa GROUP BY v.idResponsable;";

            List<VisitaDTO> listVisitas = new List<VisitaDTO>();

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        VisitaDTO visita = new VisitaDTO();
                        visita.idResponsable= rdr.GetDouble(0);
                        visita.nombreResponsable = rdr.GetString(1);
                        visita.cantidad = rdr.GetInt32(2);


                        listVisitas.Add(visita);
                    }
                }

                return listVisitas;
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

        public VisitaDTO InformacionVisitaPorID(BigInteger idVisita)
        {
            string query = "SELECT v.id, v.fechaCreacion, v.fechaInicio, v.fechaFinaliza, v.cantidadDias, v.contactoEmpresa,t.RAZON_SOCIAL, p.nombre, s.id, t.IDENTIFICACION,v.idPlantaCliente, v.idVentaAsociada, v.estado FROM visita v INNER JOIN plantaempresacliente p ON v.idPlantaCliente = p.id INNER JOIN sedes s ON p.id_sede = s.id INNER JOIN terceros t ON s.id_tercero = t.IDENTIFICACION WHERE v.id = @idVisita;";

            VisitaDTO visita = new VisitaDTO();

            try
            {
                conexion_.Open();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@idVisita", idVisita);

                using (MySqlDataReader rdr = (MySqlDataReader)cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        visita.id = rdr.GetInt32(0);
                        visita.fechaCreacion = rdr.GetDateTime(1);
                        visita.fechaInicio = rdr.GetDateTime(2);
                        visita.fechaFinaliza = rdr.GetDateTime(3);
                        visita.cantidadDias = rdr.GetInt32(4);
                        visita.contactoEmpresa = rdr.GetString(5);
                        visita.empresaCliente = rdr.GetString(6);
                        visita.plantaCliente = rdr.GetString(7);
                        visita.sedeCliente = rdr.GetInt64(8);
                        visita.idEmpresaCliente = rdr.GetDouble(9);
                        visita.idPlantaCliente = rdr.GetInt32(10);
                        visita.idVentaAsociada = rdr.IsDBNull(11) ? 0 : rdr.GetInt64(11);
                        visita.estado = rdr.GetString(12);

                    }
                }

                return visita;
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

        public async Task<List<ElementoInventarioDTO>> ElementosRevisadosMesActual(long idEmpresa)
        {
            string query = "SELECT DISTINCT c.id_parte,CONCAT(e.codigo,' - ',e.nombre) as elemento, CONCAT(e.ref1,' ',e.ref2,' ',e.ref3,' ',e.ref4, ' ', e.ref5) as referencia,c.id_visita from condicionoperativareal c INNER JOIN parte p ON p.id = c.id_parte INNER JOIN elementos e ON e.codigo= p.id_elemento INNER JOIN visita v ON v.id=c.id_visita WHERE MONTH(v.fechaInicio) = MONTH(NOW()) AND YEAR(v.fechaInicio) = YEAR(NOW()) AND v.idEmpresaPrestaServicio =@idEmpresa AND v.estado='Finalizada' order by c.id_parte;";

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

    }
}
