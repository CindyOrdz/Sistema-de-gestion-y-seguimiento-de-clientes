using EntidadesNegocio.InformacionVisita;
using MySqlConnector;
using System.Numerics;
using RegistroEvidencia;

namespace EntidadesNegocio.ClasesDao
{
    public class RegistroEvidenciaDAO
    {
        private readonly MySqlConnection conexion_;

        public RegistroEvidenciaDAO(MySqlConnection conexion)
        {
            conexion_ = conexion;
        }

        public async Task RegistrarEvidencia(RegistroArchivo archivo, BigInteger id, String tipoEvidencia)
        {
            string query = "INSERT INTO registro_evidencia (rutaRelativa, extension, tipoArchivo, nombreArchivo, fechaRegistro, fechaCaptura, tipoEvidencia, idTipoEvidencia) VALUES (@rutaRelativa, @extension, @tipoArchivo, @nombreArchivo, @fechaRegistro, @fechaCaptura, @tipoEvidencia, @idTipoEvidencia)";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);

            cmd.Parameters.AddWithValue("@rutaRelativa", archivo.ObtenerRutaRelativaArchivo());
            cmd.Parameters.AddWithValue("@extension", archivo.ObtenerExtensionArchivo());
            cmd.Parameters.AddWithValue("@tipoArchivo", archivo.ObtenerTipoArchivo());
            cmd.Parameters.AddWithValue("@nombreArchivo", archivo.ObtenerNombreArchivo());
            cmd.Parameters.AddWithValue("@fechaRegistro", DateTime.Now);
            cmd.Parameters.AddWithValue("@fechaCaptura", archivo.ObtenerFechaCaptura());
            cmd.Parameters.AddWithValue("@tipoEvidencia", tipoEvidencia);
            cmd.Parameters.AddWithValue("@idTipoEvidencia", id);

            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al registrar la evidencia: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task<List<RegistroArchivo>> ListarEvidencia(BigInteger id, String tipoEvidencia)
        {
            string query = "SELECT idEvidencia,rutaRelativa, extension, tipoArchivo, nombreArchivo, fechaRegistro, fechaCaptura FROM registro_evidencia where idTipoEvidencia = @id and tipoEvidencia = @tipoEvidencia";

            List<RegistroArchivo> listaArchivos = new List<RegistroArchivo>();

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@tipoEvidencia", tipoEvidencia);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        RegistroArchivo archivo = new RegistroArchivo(
                            rdr.GetInt64(0),
                            rdr.GetString(1),
                            rdr.GetString(2),
                            rdr.GetString(3),
                            rdr.GetString(4),
                            rdr.GetDateTime(5),
                            rdr.GetDateTime(6));

                        listaArchivos.Add(archivo);
                    }
                }

                return listaArchivos;
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

        public async Task BorrarEvidencia(BigInteger id)
        {
            string query = "DELETE FROM registro_evidencia WHERE idEvidencia = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al eliminar la evidencia: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

    }
}
