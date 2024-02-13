using EntidadesNegocio.EntidadesDto;
using EntidadesNegocio.InformacionVisita;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.Terceros;
using MySqlConnector;
using System.Numerics;


namespace EntidadesNegocio.ClasesDao
{
    public class MapaDeProcesosDAO
    {
        private readonly MySqlConnection conexion_;

        public MapaDeProcesosDAO(MySqlConnection conexion)
        {
            conexion_ = conexion;
        }

        public async Task<List<AreasProceso>> ListarAreas(BigInteger id)
        {
            string query = "SELECT * FROM areasproceso WHERE id_plantaempresacliente = @id";

            List<AreasProceso> listAreas = new List<AreasProceso>();

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        AreasProceso area = new AreasProceso(
                            rdr.GetInt32(0),
                            rdr.GetString(1),
                            rdr.GetString(2),
                            rdr.GetString(3),
                            rdr.GetString(4),
                            rdr.GetString(5));

                        listAreas.Add(area);
                    }
                }

                return listAreas;
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


        public async Task<List<PlantaEmpresaCliente>> ListarPlantas(BigInteger id)
        {
            string query = "SELECT * FROM plantaempresacliente where id_sede = @id";

            List<PlantaEmpresaCliente> listPlantas = new List<PlantaEmpresaCliente>();

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        PlantaEmpresaCliente planta = new PlantaEmpresaCliente(
                            rdr.GetInt32(0),
                            rdr.GetString(1),
                            rdr.GetString(2),
                            rdr.GetString(3),
                            rdr.GetString(4),
                            rdr.GetString(5),
                            rdr.GetString(6),
                            rdr.GetString(7));


                        listPlantas.Add(planta);
                    }
                }

                return listPlantas;
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


        public async Task<List<Sede>> ListarSedes(long id)
        {
            string query = "SELECT id, responsable, telefono, email1, email2 FROM sedes where id_tercero = @id"; 

            List<Sede> listPlantas = new List<Sede>();

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        Sede sede = new Sede(
                            rdr.GetInt64(0),
                            rdr.GetString(1),
                            rdr.GetString(2),
                            rdr.GetString(3),
                            rdr.GetString(4));


                        listPlantas.Add(sede);
                    }
                }

                return listPlantas;
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

        public async Task<List<Componente>> ListarComponentes(BigInteger id)
        {
            string query = "SELECT * FROM componente WHERE id_areasproceso = @id";

            List<Componente> listComponentes = new List<Componente>();

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        Componente componente = new Componente(
                            rdr.GetInt32(0),
                            rdr.GetString(1),
                            rdr.GetString(2));

                        listComponentes.Add(componente);
                    }
                }

                return listComponentes;
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


        public async Task<List<EquipoDelComponente>> ListarEquipos(BigInteger id)
        {
            string query = "SELECT * FROM equipodelcomponente where id_componente = @id";

            List<EquipoDelComponente> listaEquipos = new List<EquipoDelComponente>();

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        EquipoDelComponente equipo = new EquipoDelComponente(
                            rdr.GetInt32(0),
                            rdr.GetString(1),
                            rdr.GetString(2));

                        listaEquipos.Add(equipo);
                    }
                }

                return listaEquipos;
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

        public async Task<TerceroInterfazGraficaDTO> InformacionEmpresa(BigInteger idEmpresaCliente, BigInteger idEmpresaLogueada)
        {
            string query = "SELECT t.IDENTIFICACION,t.RAZON_SOCIAL,t.DIRECCION,t.TELEFONO FROM clientes c JOIN terceros t ON c.identificacion_cliente = t.IDENTIFICACION WHERE c.identificacion_empresa = @idEmpresaLogueada and t.IDENTIFICACION = @idEmpresaCliente";

            TerceroInterfazGraficaDTO empresa = null;

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@idEmpresaCliente", idEmpresaCliente);
                cmd.Parameters.AddWithValue("@idEmpresaLogueada", idEmpresaLogueada);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    if (await rdr.ReadAsync())
                    {
                        empresa = new TerceroInterfazGraficaDTO
                        {
                            Identificacion = (long)rdr.GetDouble(0),
                            RazonSocial = rdr.GetString(1),
                            Direccion = rdr.GetString(2),
                            Telefono = rdr.GetString(3)
                        };
                    }
                }

                return empresa;
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

        public async Task<List<ParteDTO>> ListarPartes(BigInteger id)
        {
            string query = "SELECT p.id, p.id_elemento, e.nombre,p.id_equipodelcomponente FROM parte p JOIN elementos e ON p.id_elemento=e.codigo WHERE id_equipodelcomponente = @id;";

            List<ParteDTO> listParte = new List<ParteDTO>();

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        ParteDTO parte = new ParteDTO();
                        parte.Id =  rdr.GetInt32(0);
                        parte.IdElemento =  rdr.GetInt32(1);
                        parte.NombreElemento = rdr.GetString(2);
                        parte.IdEquipo =  rdr.GetInt32(3);

                        listParte.Add(parte);
                    }
                }

                return listParte;
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
