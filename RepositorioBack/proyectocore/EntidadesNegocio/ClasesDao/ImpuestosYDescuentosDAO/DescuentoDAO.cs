
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.ImpuestosYDescuentos;
using MySqlConnector;

namespace EntidadesNegocio.ClasesDao.ImpuestosYDescuentosDAO
{
    public class DescuentoDAO
    {
        private readonly MySqlConnection _conexion;
        public DescuentoDAO(MySqlConnection conexion)
        {
            _conexion = conexion;
        }

        public List<DescuentoInterfazGraficaVentaDTO> ObtenerDescuentosParaClientes()
        {
            string sql = "select codigo, nombre from descuentos where codigo='01' or codigo='04' or codigo='06'or codigo='09'";

            var listaDescuentos = new List<DescuentoInterfazGraficaVentaDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var impuestoDTO = new DescuentoInterfazGraficaVentaDTO
                    {
                        Codigo = reader.GetString("codigo"),
                        Nombre = reader.GetString("nombre"),

                    };

                    listaDescuentos.Add(impuestoDTO);

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


            return listaDescuentos;
        }
    }


}
