using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;
using EntidadesNegocio.Terceros;
using MySqlConnector;
namespace EntidadesNegocio.ClasesDao.ProcedenciaDAO
{
    public class DepartamentoDAO
    {
        private readonly MySqlConnection _conexion;
        public DepartamentoDAO(MySqlConnection conexion)
        {
            _conexion= conexion;    
        }

        public List<DepartamentoProvinciaInterfazGraficaVentaDTO> ObtenerDepartamentosDePais(string codigoPais)
        {
            string sql = "select codigo, nombre from departamentos where codigo_pais = @codigo_pais";

            var listaDepartamentos = new List<DepartamentoProvinciaInterfazGraficaVentaDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@codigo_pais", codigoPais);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var departamentoDTO = new DepartamentoProvinciaInterfazGraficaVentaDTO
                    {
                        Codigo = reader.GetString("codigo"),
                        Nombre = reader.GetString("nombre"),

                    };

                    listaDepartamentos.Add(departamentoDTO);

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


            return listaDepartamentos;

        }
    }
}
