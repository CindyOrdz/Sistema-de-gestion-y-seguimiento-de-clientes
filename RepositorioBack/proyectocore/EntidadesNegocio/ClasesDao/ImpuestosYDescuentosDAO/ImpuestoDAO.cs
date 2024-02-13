using EntidadesNegocio.Impuestos;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.ImpuestosYDescuentos;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.Terceros;
using MySqlConnector;
using System.Drawing;

namespace EntidadesNegocio.ClasesDao.ImpuestosYDescuentosDAO
{
    public class ImpuestoDAO
    {
        private readonly MySqlConnection _conexion;
        public ImpuestoDAO(MySqlConnection conexion)
        {
            _conexion = conexion;
        }

        public List<ImpuestoInterfazGraficaVentaDTO> ObtenerImpuestos()
        {
            string sql = "select identificador, nombre from impuestos";

            var listaImpuestos = new List<ImpuestoInterfazGraficaVentaDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var impuestoDTO = new ImpuestoInterfazGraficaVentaDTO
                    {
                        Id = reader.GetString("identificador"),
                        Nombre = reader.GetString("nombre"),

                    };

                    listaImpuestos.Add(impuestoDTO);

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


            return listaImpuestos;
        }

        public List<ImpuestoInterfazGraficaVentaDTO> ObtenerImpuestosParaClientes()
        {
            string sql = "select identificador, nombre from impuestos where identificador='05' or identificador='06'or identificador='07'";

            var listaImpuestos = new List<ImpuestoInterfazGraficaVentaDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var impuestoDTO = new ImpuestoInterfazGraficaVentaDTO
                    {
                        Id = reader.GetString("identificador"),
                        Nombre = reader.GetString("nombre"),

                    };

                    listaImpuestos.Add(impuestoDTO);

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


            return listaImpuestos;
        }

        public List<ImpuestoInterfazGraficaVentaDTO> ObtenerImpuestosDelCliente(long identificacionCliente, long identificacionEmpresa)
        {
            string sql = "select imp.identificador, imp.nombre, cli_imp.vigente from impuestos imp " +
                "inner join cliente_impuestos cli_imp on cli_imp.id_impuesto = imp.identificador " +
                "where cli_imp.id_cliente = @id_cliente and cli_imp.id_empresa = @id_empresa";

            var listaImpuestosEmpresa = new List<ImpuestoInterfazGraficaVentaDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@id_cliente", identificacionCliente);
                command.Parameters.AddWithValue("@id_empresa", identificacionEmpresa);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var impuestoDTO = new ImpuestoInterfazGraficaVentaDTO
                    {
                        Id = reader.GetString("identificador"),
                        Nombre = reader.GetString("nombre"),
                        Vigente = reader.GetString("vigente"),
                        ImpuestoClienteExiste = true

                    };

                    listaImpuestosEmpresa.Add(impuestoDTO);

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


            return listaImpuestosEmpresa;
        }


        public ParametrosImpuestosInterfazGraficaVentaDTO ObtenerParametrosImpuestos()
        {
            string sql = "select UVT, valor_UVT, porcentaje_reteiva, porcentaje_retefuente from parametros_impuesto";

            var parametrosDTO = new ParametrosImpuestosInterfazGraficaVentaDTO();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    parametrosDTO.UVT = reader.GetInt32("UVT");
                    parametrosDTO.ValorUVT = reader.GetDouble("valor_UVT");
                    parametrosDTO.PorcentajeReteIva = reader.GetDouble("porcentaje_reteiva");
                    parametrosDTO.PorcentajeReteFuente = reader.GetDouble("porcentaje_retefuente");

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


            return parametrosDTO;
        }

        public bool ConfigurarImpuestosDelCliente(long identificacionEmpresa, TerceroInterfazGraficaDTO cliente)
        {
            string sqlMontosCliente = "update clientes set con_monto_ley=@con_monto_ley, con_monto_personalizado=@con_monto_personalizado, monto_personalizado=@monto_personalizado " +
                                       "where identificacion_cliente=@identificacion_cliente and identificacion_empresa=@identificacion_empresa";

            string sqlNuevoImpuesto = "insert into cliente_impuestos(id_cliente, id_empresa, id_impuesto, vigente) " +
                                    "values (@id_cliente, @id_empresa, @id_impuesto, @vigente)";

            string sqlActualaizarImpuesto = "update cliente_impuestos set vigente=@vigente where id_cliente=@id_cliente " +
                " and id_empresa=@id_empresa and id_impuesto=@id_impuesto";

            _conexion.Open();
            var MysqlConnection = _conexion;
            int filasMontosCliente = 0;
            int filasNuevoImpuesto = 0;
            int filasActualizarImpuesto = 0;
            bool validacionTransaccion = false;
            using MySqlTransaction transaction = MysqlConnection.BeginTransaction();

            try
            {

                var comandoMontosCliente= MysqlConnection.CreateCommand();
                comandoMontosCliente.Transaction = transaction;
                comandoMontosCliente.CommandText = sqlMontosCliente;
                comandoMontosCliente.Parameters.AddWithValue("@identificacion_cliente", cliente.Identificacion);
                comandoMontosCliente.Parameters.AddWithValue("@identificacion_empresa", identificacionEmpresa);
                comandoMontosCliente.Parameters.AddWithValue("@con_monto_ley", cliente.ConMontoDeLey);
                comandoMontosCliente.Parameters.AddWithValue("@con_monto_personalizado", cliente.ConMontoPersonalizado);
                comandoMontosCliente.Parameters.AddWithValue("@monto_personalizado", cliente.MontoPersonalizado);
                filasMontosCliente = comandoMontosCliente.ExecuteNonQuery();


                var impuestosNuevos = cliente.Impuestos.Where(imp => imp.NuevoImpuesto == true && imp.CambiarVigencia == false && imp.ImpuestoClienteExiste== false).ToList();

                impuestosNuevos.ForEach(impuesto =>
                {
                    var comandoNuevoimpuesto = MysqlConnection.CreateCommand();
                    comandoNuevoimpuesto.Transaction = transaction;
                    comandoNuevoimpuesto.CommandText = sqlNuevoImpuesto;
                    comandoNuevoimpuesto.Parameters.AddWithValue("@id_cliente", cliente.Identificacion);
                    comandoNuevoimpuesto.Parameters.AddWithValue("@id_empresa", identificacionEmpresa);
                    comandoNuevoimpuesto.Parameters.AddWithValue("@id_impuesto", impuesto.Id);
                    comandoNuevoimpuesto.Parameters.AddWithValue("@vigente", impuesto.Vigente);
                    filasNuevoImpuesto += comandoNuevoimpuesto.ExecuteNonQuery();

                });


                var impuestosParaActualizar = cliente.Impuestos.Where(imp => imp.CambiarVigencia == true && imp.ImpuestoClienteExiste == true && imp.NuevoImpuesto == false).ToList();

                impuestosParaActualizar.ForEach(impuesto =>
                {
                    var commandoActualizarImpuesto = MysqlConnection.CreateCommand();
                    commandoActualizarImpuesto.Transaction = transaction;
                    commandoActualizarImpuesto.CommandText = sqlActualaizarImpuesto;
                    commandoActualizarImpuesto.Parameters.AddWithValue("@id_cliente", cliente.Identificacion);
                    commandoActualizarImpuesto.Parameters.AddWithValue("@id_empresa", identificacionEmpresa);
                    commandoActualizarImpuesto.Parameters.AddWithValue("@id_impuesto", impuesto.Id);
                    commandoActualizarImpuesto.Parameters.AddWithValue("@vigente", impuesto.Vigente);
                    filasActualizarImpuesto += commandoActualizarImpuesto.ExecuteNonQuery();

                });
                

              
                if (filasNuevoImpuesto == impuestosNuevos.Count && filasActualizarImpuesto == impuestosParaActualizar.Count && filasMontosCliente == 1)
                {
                    transaction.Commit();
                    validacionTransaccion = true;
                }
                else
                {
                    transaction.Rollback();
                }

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _conexion.Close();
            }

            return validacionTransaccion;
        }





    }
}
