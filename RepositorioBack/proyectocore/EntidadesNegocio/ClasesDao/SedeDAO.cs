using EntidadesNegocio.EntidadesDto;
using EntidadesNegocio.InformacionVisita;
using EntidadesNegocio.Terceros;
using MySqlConnector;
using System.Numerics;


namespace EntidadesNegocio.ClasesDao
{
    public class SedeDAO
    {
        private readonly MySqlConnection conexion_;

        public SedeDAO(MySqlConnection conexion)
        {
            conexion_ = conexion;
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

        public async Task BorrarPlantas(BigInteger id)
        {
            string query = "DELETE FROM plantaempresacliente WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al eliminar la planta: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task EditarPlantas(PlantaEmpresaCliente planta)
        {
            string query = "UPDATE plantaempresacliente SET nombre = @nombre,direccion = @direccion, contacto = @contacto, telefonoContacto1 = @telefono1, telefonoContacto2 = @telefono2, emailContacto = @emailContacto, emailInforme = @emailinforme WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", planta.ObtenerId());
            cmd.Parameters.AddWithValue("@nombre", planta.ObtenerNombre());
            cmd.Parameters.AddWithValue("@direccion", planta.ObtenerDireccion());
            cmd.Parameters.AddWithValue("@contacto", planta.ObtenerContacto());
            cmd.Parameters.AddWithValue("@telefono1", planta.ObtenerTelefono1());
            cmd.Parameters.AddWithValue("@telefono2", planta.ObtenerTelefono2());
            cmd.Parameters.AddWithValue("@emailContacto", planta.ObtenerEmailContacto());
            cmd.Parameters.AddWithValue("@emailInforme", planta.ObtenerEmailParaInforme());
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al actualizar la planta: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task AgregarPlanta(PlantaEmpresaCliente planta)
        {
            string query = "INSERT INTO plantaempresacliente (nombre, direccion,contacto, telefonoContacto1, telefonoContacto2, emailContacto, emailinforme, id_sede,id_empresa)  VALUES (@nombre,@direccion, @contacto, @telefono1, @telefono2, @emailContacto, @emailinforme, @idSede,@idEmpresa) ";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);

            cmd.Parameters.AddWithValue("@nombre", planta.ObtenerNombre());
            cmd.Parameters.AddWithValue("@direccion", planta.ObtenerDireccion());
            cmd.Parameters.AddWithValue("@contacto", planta.ObtenerContacto());
            cmd.Parameters.AddWithValue("@telefono1", planta.ObtenerTelefono1());
            cmd.Parameters.AddWithValue("@telefono2", planta.ObtenerTelefono2());
            cmd.Parameters.AddWithValue("@emailContacto", planta.ObtenerEmailContacto());
            cmd.Parameters.AddWithValue("@emailinforme", planta.ObtenerEmailParaInforme());
            cmd.Parameters.AddWithValue("@idSede", planta.ObtenerIdSede());
            cmd.Parameters.AddWithValue("@idEmpresa", planta.ObtenerIdEmpresa());

            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al agregar la planta: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task<PlantaEmpresaCliente> InformacionPlanta(BigInteger idPlanta, BigInteger idSede)
        {
            string query = "SELECT * FROM plantaempresacliente WHERE id = @idPlanta and  id_sede = @idSede";

            PlantaEmpresaCliente planta = null;

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@idSede", idSede);
                cmd.Parameters.AddWithValue("@idPlanta", idPlanta);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    if (await rdr.ReadAsync())
                    {
                        planta = new PlantaEmpresaCliente(
                            rdr.GetInt32(0),
                            rdr.GetString(1),
                            rdr.GetString(2),
                            rdr.GetString(3),
                            rdr.GetString(4),
                            rdr.GetString(5),
                            rdr.GetString(6),
                            rdr.GetString(7));
                    }
                }

                return planta;
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
