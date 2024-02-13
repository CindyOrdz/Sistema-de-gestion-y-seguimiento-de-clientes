using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;
using MySqlConnector;
namespace EntidadesNegocio.ClasesDao.ProcedenciaDAO
{
    
    public class MunicipioDAO
    {
        private readonly MySqlConnection _conexion;

        public MunicipioDAO(MySqlConnection conexion)
        {
            _conexion = conexion;
        }

        public List<CiudadInterfazGraficaVentaDTO> ObtenerCiudadesDeDepartamento(string codigoDepartamento)
        {
            string sql = "select codigo, nombre from ciudades where codigo_departamento = @codigo_dpto";

            var listaCiudades = new List<CiudadInterfazGraficaVentaDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@codigo_dpto", codigoDepartamento);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var ciudadDTO = new CiudadInterfazGraficaVentaDTO
                    {
                        Codigo = reader.GetString("codigo"),
                        Nombre = reader.GetString("nombre"),

                    };

                    listaCiudades.Add(ciudadDTO);

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


            return listaCiudades;

        }
    }
}
