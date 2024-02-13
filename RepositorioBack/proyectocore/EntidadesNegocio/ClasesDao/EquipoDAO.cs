using EntidadesNegocio.EntidadesDto;
using EntidadesNegocio.InformacionVisita;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.ClasesDao
{
    public class EquipoDAO
    {
        private readonly MySqlConnection conexion_;

        public EquipoDAO(MySqlConnection conexion)
        {
            conexion_ = conexion;
        }

        //CRUD PARTE
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

        public async Task BorrarParte(BigInteger id)
        {
            string query = "DELETE FROM parte WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al eliminar la parte: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }
      
        public async Task AgregarParte(BigInteger idEquipo, long idElemento)
        {
            string query = "INSERT INTO parte (id_elemento, id_equipodelcomponente) VALUES (@idElemento, @idEquipo);";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);

            cmd.Parameters.AddWithValue("@idElemento", idElemento);
            cmd.Parameters.AddWithValue("@idEquipo", idEquipo);

            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al agregar la parte: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task<ParteDTO> InformacionParte(BigInteger idParte)
        {
            string query = "SELECT p.id, p.id_elemento,e.nombre FROM parte p JOIN elementos e ON p.id_elemento=e.codigo WHERE p.id = @idParte;";

            ParteDTO parte = null;

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@idParte", idParte);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    if (await rdr.ReadAsync())
                    {
                        parte = new ParteDTO();
                        parte.Id =  rdr.GetInt32(0);
                        parte.IdElemento =  rdr.GetInt32(1);
                        parte.NombreElemento = rdr.GetString(2);
                    }
                }

                return parte;
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
