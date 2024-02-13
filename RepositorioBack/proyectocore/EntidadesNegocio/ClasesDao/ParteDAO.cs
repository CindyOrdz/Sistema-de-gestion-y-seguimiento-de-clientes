using EntidadesNegocio.EntidadesDto;
using EntidadesNegocio.InformacionVisita;
using MySqlConnector;
using System.Numerics;


namespace EntidadesNegocio.ClasesDao
{
    public class ParteDAO
    {
        private readonly MySqlConnection conexion_;

        public ParteDAO(MySqlConnection conexion)
        {
            conexion_ = conexion;
        }

        public async Task AgregarCondicionInicial(CondicionInicialDTO condicion, BigInteger idParte)
        {
            string query = "INSERT INTO condicionoperativainicial (descripcion, id_parte, id_responsable) VALUES (@descripcion, @idParte, @idResponsable);";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);

            cmd.Parameters.AddWithValue("@descripcion", condicion.descripcion);
            cmd.Parameters.AddWithValue("@idParte", idParte);
            cmd.Parameters.AddWithValue("@idResponsable", condicion.idResponsable);
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al agregar la condicion inicial: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public List<CondicionInicialDTO> ListarCondicionesIniciales(BigInteger id)
        {
            string query = "SELECT c.id,c.descripcion, CONCAT(COALESCE(t.NOMBRE1, ''),' ', COALESCE(t.NOMBRE2, ''),' ', COALESCE(t.APELLIDO1, ''),' ', COALESCE(t.APELLIDO2, '')) as completo FROM condicionoperativainicial c JOIN terceros t ON c.id_responsable = t.IDENTIFICACION where c.id_parte = @id";

            List<CondicionInicialDTO> listaCondIniciales = new List<CondicionInicialDTO>();

            try
            {
                conexion_.Open();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader rdr = (MySqlDataReader) cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        CondicionInicialDTO condicion = new CondicionInicialDTO();
                        condicion.id= rdr.GetInt32(0);
                        condicion.descripcion= rdr.GetString(1);
                        condicion.nombreResponsable = rdr.GetString(2);
                        listaCondIniciales.Add(condicion);
                    }
                }

                return listaCondIniciales;
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

        public async Task BorrarCondicionInicial(BigInteger id)
        {
            string query = "DELETE FROM condicionoperativainicial WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al eliminar la condicion incial: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task EditarCondicionInicial(CondicionOperativaInicial incial)
        {
            string query = "UPDATE condicionoperativainicial SET descripcion = @descripcion WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", incial.ObtenerId());
            cmd.Parameters.AddWithValue("@descripcion", incial.ObtenerDescripcion());
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al actualizar la condicion inicial: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task<CondicionOperativaInicial> InformacionCondicionInicial(BigInteger idCondicion, BigInteger idParte)
        {
            string query = "SELECT * FROM condicionoperativainicial WHERE id = @idCondicion and id_parte = @idParte";

            CondicionOperativaInicial condicionInicial = null;

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@idParte", idParte);
                cmd.Parameters.AddWithValue("@idCondicion", idCondicion);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    if (await rdr.ReadAsync())
                    {
                        condicionInicial = new CondicionOperativaInicial(
                            rdr.GetInt32(0),
                            rdr.GetString(1));
                    }
                }

                return condicionInicial;
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

        public async Task<List<ParteDTO>> BuscarPartesParaRegistrarCondicionesEnVisita(String nombreParte, BigInteger idPlanta)
        {
            string query = "SELECT p.id as idParte, e.nombre as nombreParte, o.nombre as nombreEquipo, c.nombre as nombreComponente,a.nombre as nombreArea FROM parte p INNER JOIN elementos e ON e.codigo=p.id_elemento INNER JOIN equipodelcomponente o ON p.id_equipodelcomponente=o.id INNER JOIN componente c ON c.id=o.id_componente INNER JOIN areasproceso a ON a.id=c.id_areasproceso where e.nombre like @nombreParte AND a.id_plantaempresacliente= @idPlanta;";

            List<ParteDTO> listaInfoPartes = new List<ParteDTO>();

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@idPlanta", idPlanta);
                cmd.Parameters.AddWithValue("@nombreParte","%"+ nombreParte +"%");

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        ParteDTO parte = new ParteDTO();

                        parte.Id= rdr.GetInt32(0);
                        parte.NombreElemento= rdr.GetString(1);
                        parte.NombreEquipo = rdr.GetString(2);
                        parte.NombreComponente = rdr.GetString(3);
                        parte.NombreArea = rdr.GetString(4);

                        listaInfoPartes.Add(parte);
                    }
                }

                return listaInfoPartes;
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

        public List<CondicionOperativaDTO> ObtenerCondicionesOperativas(BigInteger id)
        {
            string query = "SELECT c.id, c.nombre, c.valorFijo, CONCAT(c.rangoInicial, ' - ', c.rangoFinal) AS rango,c.rangoInicial, c.rangoFinal, u.unidad FROM condicionoperativa c JOIN unidad u ON c.id_unidad= u.id where id_condicionoperativainicial = @id;";

            List<CondicionOperativaDTO> listaCondiciones = new List<CondicionOperativaDTO>();

            try
            {
                conexion_.Open();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@id", id);
                
                using (MySqlDataReader rdr = (MySqlDataReader) cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        CondicionOperativaDTO condicion = new CondicionOperativaDTO();
                        condicion.id = rdr.GetInt32(0);
                        condicion.nombre = rdr.GetString(1);
                        condicion.valorFijo = rdr.GetDouble(2);
                        condicion.rangoCompleto = rdr.GetString(3);
                        condicion.rangoInicial = rdr.GetDouble(4);
                        condicion.rangoFinal = rdr.GetDouble(5);
                        condicion.codigoUnidad = rdr.GetString(6);


                        listaCondiciones.Add(condicion);
                    }
                }

                return listaCondiciones;
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

        //Condiciones reales
        public async Task AgregarCondicionReal(CondicionRealDTO condicion, BigInteger idParte)
        {
            string query = "INSERT INTO condicionoperativareal (descripcion, id_parte, id_responsable, id_visita) VALUES (@descripcion, @idParte, @idResponsable, @idVisita);";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);

            cmd.Parameters.AddWithValue("@descripcion", condicion.descripcion);
            cmd.Parameters.AddWithValue("@idParte", idParte);
            cmd.Parameters.AddWithValue("@idResponsable", condicion.idResponsable);
            cmd.Parameters.AddWithValue("@idVisita", condicion.idVisita);

            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al agregar la condicion real: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public List<CondicionRealDTO> ListarCondicionesReales(BigInteger id)
        {
            string query = "SELECT c.id,c.descripcion, CONCAT(COALESCE(t.NOMBRE1, ''),' ', COALESCE(t.NOMBRE2, ''),' ', COALESCE(t.APELLIDO1, ''),' ', COALESCE(t.APELLIDO2, '')) as completo, c.id_visita FROM condicionoperativareal c JOIN terceros t ON c.id_responsable = t.IDENTIFICACION where c.id_parte = @id";

            List<CondicionRealDTO> listaCondIniciales = new List<CondicionRealDTO>();

            try
            {
                conexion_.Open();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader rdr = (MySqlDataReader)cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        CondicionRealDTO condicion = new CondicionRealDTO();
                        condicion.id= rdr.GetInt32(0);
                        condicion.descripcion= rdr.GetString(1);
                        condicion.nombreResponsable = rdr.GetString(2);
                        condicion.idVisita = rdr.GetInt32(3);

                        listaCondIniciales.Add(condicion);
                    }
                }

                return listaCondIniciales;
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

        public async Task BorrarCondicionReal(BigInteger id)
        {
            string query = "DELETE FROM condicionoperativareal WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al eliminar la condicion real: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task EditarCondicionReal(CondicionOperativaReal real)
        {
            string query = "UPDATE condicionoperativareal SET descripcion = @descripcion WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", real.ObtenerId());
            cmd.Parameters.AddWithValue("@descripcion", real.ObtenerDescripcion());
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al actualizar la condicion real: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task<CondicionOperativaReal> InformacionCondicionReal(BigInteger idCondicion, BigInteger idParte)
        {
            string query = "SELECT * FROM condicionoperativareal WHERE id = @idCondicion and id_parte = @idParte";

            CondicionOperativaReal condicionReal = null;

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@idParte", idParte);
                cmd.Parameters.AddWithValue("@idCondicion", idCondicion);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    if (await rdr.ReadAsync())
                    {
                        condicionReal = new CondicionOperativaReal(
                            rdr.GetInt32(0),
                            rdr.GetString(1));
                    }
                }

                return condicionReal;
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

        public List<CondicionOperativaDTO> ObtenerCondicionesOperativasReales(BigInteger id)
        {
            string query = "SELECT c.id, c.nombre, c.valorFijo, CONCAT(c.rangoInicial, ' - ', c.rangoFinal) AS rango,c.rangoInicial, c.rangoFinal, u.unidad FROM condicionoperativa c JOIN unidad u ON c.id_unidad= u.id where id_condicionoperativareal = @id";

            List<CondicionOperativaDTO> listaCondiciones = new List<CondicionOperativaDTO>();

            try
            {
                conexion_.Open();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader rdr = (MySqlDataReader) cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        CondicionOperativaDTO condicion = new CondicionOperativaDTO();
                        condicion.id = rdr.GetInt32(0);
                        condicion.nombre = rdr.GetString(1);
                        condicion.valorFijo = rdr.GetDouble(2);
                        condicion.rangoCompleto = rdr.GetString(3);
                        condicion.rangoInicial = rdr.GetDouble(4);
                        condicion.rangoFinal = rdr.GetDouble(5);
                        condicion.codigoUnidad = rdr.GetString(6);


                        listaCondiciones.Add(condicion);
                    }
                }

                return listaCondiciones;
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