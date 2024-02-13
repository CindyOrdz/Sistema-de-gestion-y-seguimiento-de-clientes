using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;
using MySqlConnector;

namespace EntidadesNegocio.ClasesDao.ProcedenciaDAO
{
    public class PaisDAO
    {
        private readonly MySqlConnection _conexion;
        public PaisDAO(MySqlConnection conexion)
        {
            _conexion= conexion;
        }

        public List<PaisInterfazGraficaVentaDTO> ObtenerPaises()
        {
            string sql = "select codigo, nombre from paises";

            var listaPaises = new List<PaisInterfazGraficaVentaDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var paisDTO = new PaisInterfazGraficaVentaDTO
                    {
                        Codigo = reader.GetString("codigo"),
                        Nombre = reader.GetString("nombre"),

                    };

                    listaPaises.Add(paisDTO);

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


            return listaPaises;


        }
    }
}
