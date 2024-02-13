using EntidadesNegocio.EntidadesDto;
using EntidadesNegocio.InformacionVisita;
using MySqlConnector;
using System.Numerics;

namespace EntidadesNegocio.ClasesDao
{
    public class AreaDAO
    {
        private readonly MySqlConnection conexion_;

        public AreaDAO(MySqlConnection conexion)
        {
            conexion_ = conexion;
        }

        public async Task<List<Componente>> ListarComponentes(BigInteger id)
        {
            string query = "SELECT * FROM componente WHERE id_areasproceso = @id";

            List<Componente> listComponentes = new List<Componente>();

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@id",id);

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

        public async Task BorrarComponentes(BigInteger id)
        {
            string query = "DELETE FROM componente WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al eliminar el componente: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task EditarComponentes(Componente componente)
        {
            string query = "UPDATE componente SET nombre = @nombre, descripcion = @descripcion WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", componente.ObtenerId());
            cmd.Parameters.AddWithValue("@nombre", componente.ObtenerNombre());
            cmd.Parameters.AddWithValue("@descripcion", componente.ObtenerDescripcion());
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al actualizar el componente: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task AgregarComponente(Componente componente)
        {
            string query = "INSERT INTO componente (nombre, descripcion, id_areasproceso) VALUES (@nombre, @descripcion, @idArea)";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);

            cmd.Parameters.AddWithValue("@nombre", componente.ObtenerNombre());
            cmd.Parameters.AddWithValue("@descripcion", componente.ObtenerDescripcion());
            cmd.Parameters.AddWithValue("@idArea", componente.ObtenerIdArea());

            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al agregar el componente: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task<Componente> InformacionComponente(BigInteger idComponente, BigInteger idArea)
        {
            string query = "SELECT * FROM componente WHERE id = @idComponente and  id_areasproceso = @idArea";

            Componente componente = null;

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@idArea", idArea);
                cmd.Parameters.AddWithValue("@idComponente", idComponente);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    if (await rdr.ReadAsync())
                    {
                        componente = new Componente(
                            rdr.GetInt32(0),
                            rdr.GetString(1),
                            rdr.GetString(2));
                    }
                }

                return componente;
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
