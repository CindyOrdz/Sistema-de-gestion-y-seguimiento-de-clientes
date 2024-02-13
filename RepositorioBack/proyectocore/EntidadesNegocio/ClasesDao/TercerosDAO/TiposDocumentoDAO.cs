using MySqlConnector;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;

namespace EntidadesNegocio.ClasesDao.TercerosDAO
{
    public class TiposDocumentoDAO
    {
        private readonly MySqlConnection _conexion;   
        public TiposDocumentoDAO(MySqlConnection connection)
        {
            _conexion = connection;
        }

        public List<TipoDocumentoInterfazGraficaTercerosDTO> ObtenerTiposDocumento()
        {
            string sql = "select codigo, nombre from tipo_documentos";

            var listaTiposDocumentos = new List<TipoDocumentoInterfazGraficaTercerosDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var documentoDTO = new TipoDocumentoInterfazGraficaTercerosDTO
                    {
                        Codigo = reader.GetInt32("codigo"),
                        Nombre = reader.GetString("nombre"),

                    };

                    listaTiposDocumentos.Add(documentoDTO);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _conexion.Close();
            }


            return listaTiposDocumentos;

        }
    }
}
