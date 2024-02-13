using EntidadesNegocio.InformacionVisita;
using MySqlConnector;
using System.Numerics;


namespace EntidadesNegocio.ClasesDao
{
    public class ComponenteDAO
    {
        private readonly MySqlConnection conexion_;

        public ComponenteDAO(MySqlConnection conexion)
        {
            conexion_ = conexion;
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

        public async Task BorrarEquipos(BigInteger id)
        {
            string query = "DELETE FROM equipodelcomponente WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al eliminar el equipo: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task EditarEquipo(EquipoDelComponente equipo)
        {
            string query = "UPDATE equipodelcomponente SET nombre = @nombre, descripcion = @descripcion WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", equipo.ObtenerId());
            cmd.Parameters.AddWithValue("@nombre", equipo.ObtenerNombre());
            cmd.Parameters.AddWithValue("@descripcion", equipo.ObtenerDescripcion());
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al actualizar el equipo: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task AgregarEquipo(EquipoDelComponente equipo)
        {
            string query = "INSERT INTO equipodelcomponente (nombre, descripcion, id_componente) VALUES (@nombre, @descripcion, @idComponente)";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);

            cmd.Parameters.AddWithValue("@nombre", equipo.ObtenerNombre());
            cmd.Parameters.AddWithValue("@descripcion", equipo.ObtenerDescripcion());
            cmd.Parameters.AddWithValue("@idComponente", equipo.ObtenerIdComponente());

            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al agregar el equipo: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task<EquipoDelComponente> InformacionEquipo(BigInteger idEquipo, BigInteger idComponente)
        {
            string query = "SELECT * FROM equipodelcomponente WHERE id = @idEquipo and  id_componente = @idComponente";

            EquipoDelComponente equipo = null;

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@idComponente", idComponente);
                cmd.Parameters.AddWithValue("@idEquipo", idEquipo);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    if (await rdr.ReadAsync())
                    {
                        equipo = new EquipoDelComponente(
                            rdr.GetInt32(0),
                            rdr.GetString(1),
                            rdr.GetString(2));
                    }
                }

                return equipo;
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
