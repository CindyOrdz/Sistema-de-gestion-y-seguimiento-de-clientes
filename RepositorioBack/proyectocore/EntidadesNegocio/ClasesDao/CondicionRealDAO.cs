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
    public class CondicionRealDAO
    {
        private readonly MySqlConnection conexion_;

        public CondicionRealDAO(MySqlConnection conexion)
        {
            conexion_ = conexion;
        }

        public async Task<List<CondicionOperativaDTO>> ListarCondicionesOperativas(BigInteger id)
        {
            string query = "SELECT c.id, c.nombre, c.valorFijo, CONCAT(c.rangoInicial, ' - ', c.rangoFinal) AS rango,c.rangoInicial, c.rangoFinal, u.unidad FROM condicionoperativa c JOIN unidad u ON c.id_unidad= u.id where id_condicionoperativareal = @id";

            List<CondicionOperativaDTO> listaCondiciones = new List<CondicionOperativaDTO>();

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        CondicionOperativaDTO condicion = new CondicionOperativaDTO();
                        condicion.id = rdr.GetInt32(0);
                        condicion.nombre = rdr.GetString(1);
                        condicion.valorFijo = rdr.GetDouble(2);
                        condicion.rangoCompleto = rdr.GetString(3);
                        condicion.rangoInicial = rdr.GetDouble(4);
                        condicion.rangoFinal = rdr.GetDouble(5);
                        condicion.codigoUnidad = rdr.GetString(6);


                        listaCondiciones.Add(condicion);
                    }
                }

                return listaCondiciones;
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

        public async Task AgregarCondicionOperativa(CondicionOperativa condicion, BigInteger id, String unidad)
        {
            string query = "INSERT INTO condicionoperativa (nombre, valorFijo, rangoInicial, rangoFinal, id_unidad,id_condicionoperativareal)  VALUES (@nombre, @valorFijo, @rangoInicial, @rangoFinal, @idUnidad, @idReal)";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);

            cmd.Parameters.AddWithValue("@nombre", condicion.ObtenerNombre());
            cmd.Parameters.AddWithValue("@valorFijo", condicion.ObtenerValorFijoMedido());
            cmd.Parameters.AddWithValue("@rangoInicial", condicion.ObtenerRangoInicial());
            cmd.Parameters.AddWithValue("@rangoFinal", condicion.ObtenerRangoFinal());
            cmd.Parameters.AddWithValue("@idUnidad", unidad);
            cmd.Parameters.AddWithValue("@idReal", id);

            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al agregar la condicion operativa: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }


    }
}
