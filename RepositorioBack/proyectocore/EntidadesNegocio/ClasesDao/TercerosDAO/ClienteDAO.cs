
using MySqlConnector;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.Terceros;
using EntidadesNegocio.EntidadesDto;

namespace EntidadesNegocio.ClasesDao.TercerosDAO
{
    public  class ClienteDAO
    {
        private readonly MySqlConnection _conexion;
        public ClienteDAO(MySqlConnection conexion)
        {
            _conexion = conexion;

        }

        public bool InsertarCliente(TerceroInterfazGraficaDTO tercero, long identificacionEmpresa)
        {
            string sqlRazonSocial = string.Empty;
            string sqlParametroRazonsocial = string.Empty;
            string sqlNombre = string.Empty;
            string sqlParametroNombre = string.Empty;
            if (!string.IsNullOrWhiteSpace(tercero.RazonSocial))
            {
                sqlRazonSocial = "RAZON_SOCIAL,";
                sqlParametroRazonsocial = "@RAZON_SOCIAL,";
            }
            if (!string.IsNullOrWhiteSpace(tercero.Nombre1))
            {
                sqlNombre = "NOMBRE1,NOMBRE2,APELLIDO1,APELLIDO2,";
                sqlParametroNombre = "@NOMBRE1, @NOMBRE2, @APELLIDO1, @APELLIDO2,";
            }

            string sqlInfoTercero = $"INSERT INTO terceros(IDENTIFICACION,DIGITO,TIPO,{sqlRazonSocial} {sqlNombre} PAIS,DEPARTAMENTO,MUNICIPIO,DIRECCION,TELEFONO,EMAIL,TIPO_TERCERO,celular) " +
                $" VALUES (@IDENTIFICACION, @DIGITO, @TIPO, {sqlParametroRazonsocial} {sqlParametroNombre} @PAIS, @DEPARTAMENTO, @MUNICIPIO, @DIRECCION, @TELEFONO, @EMAIL, @TIPO_TERCERO, @celular)";
            string sqlInfoCliente = "insert into clientes(identificacion_cliente, identificacion_empresa, naturaleza, actividad_economica) " +
                "values(@identificacion_cliente, @identificacion_empresa, @naturaleza, @actividad_economica)";

            _conexion.Open();
            var MysqlConnection = _conexion;
            int filasCliente = 0;
            int filasSedes = 0;
            int filasTercero = 0;
            bool validacionTransaccion = false;
            using MySqlTransaction transaction = MysqlConnection.BeginTransaction();

            try
            {
                var commandoInsercionInfoTercero = MysqlConnection.CreateCommand();
                commandoInsercionInfoTercero.Transaction = transaction;
                commandoInsercionInfoTercero.CommandText = sqlInfoTercero;
                commandoInsercionInfoTercero.Parameters.AddWithValue("@IDENTIFICACION", tercero.Identificacion);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@DIGITO", tercero.Digito);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@TIPO", tercero.TipoDocumento.Codigo);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@RAZON_SOCIAL", tercero.RazonSocial);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@NOMBRE1", tercero.Nombre1);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@NOMBRE2", tercero.Nombre2);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@APELLIDO1", tercero.Apellido1);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@APELLIDO2", tercero.Apellido2);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@PAIS", tercero.Ubicacion.Pais.Codigo);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@DEPARTAMENTO", tercero.Ubicacion.DepartamentoProvincia.Codigo);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@MUNICIPIO", tercero.Ubicacion.Ciudad.Codigo);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@DIRECCION", tercero.Direccion);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@TELEFONO", tercero.Telefono);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@EMAIL", tercero.Email);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@TIPO_TERCERO", tercero.TipoTercero);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@celular", tercero.Celular);
                filasTercero = commandoInsercionInfoTercero.ExecuteNonQuery();

                var commandoInsercionInfoCliente = MysqlConnection.CreateCommand();
                commandoInsercionInfoCliente.Transaction = transaction;
                commandoInsercionInfoCliente.CommandText = sqlInfoCliente;
                commandoInsercionInfoCliente.Parameters.AddWithValue("@identificacion_cliente", tercero.Identificacion);
                commandoInsercionInfoCliente.Parameters.AddWithValue("@identificacion_empresa", identificacionEmpresa);
                commandoInsercionInfoCliente.Parameters.AddWithValue("@naturaleza", tercero.Naturaleza);
                commandoInsercionInfoCliente.Parameters.AddWithValue("@actividad_economica", tercero.ActividadEconomica);
                filasCliente = commandoInsercionInfoCliente.ExecuteNonQuery();

                var sedes = tercero.Sedes;

                if (sedes.Count > 0)
                {
                    sedes.ForEach(sede =>
                    {
                        var commandoInsercionInfoSede = MysqlConnection.CreateCommand();
                        commandoInsercionInfoSede.Transaction = transaction;
                        commandoInsercionInfoSede.CommandText = "INSERT INTO sedes(id_tercero, responsable, email1, email2, telefono, pais, departamento, municipio, direccion) " +
                                                                         " VALUES (@id_tercero, @responsable, @email1, @email2, @telefono, @pais, @departamento, @municipio, @direccion)";
                        commandoInsercionInfoSede.Parameters.AddWithValue("@id_tercero", tercero.Identificacion);
                        commandoInsercionInfoSede.Parameters.AddWithValue("@responsable", sede.Responsable);
                        commandoInsercionInfoSede.Parameters.AddWithValue("@email1", sede.Email1);
                        commandoInsercionInfoSede.Parameters.AddWithValue("@email2", sede.Email2);
                        commandoInsercionInfoSede.Parameters.AddWithValue("@telefono", sede.Telefono);
                        commandoInsercionInfoSede.Parameters.AddWithValue("@pais", sede.Ubicacion.Pais.Codigo);
                        commandoInsercionInfoSede.Parameters.AddWithValue("@departamento", sede.Ubicacion.DepartamentoProvincia.Codigo);
                        commandoInsercionInfoSede.Parameters.AddWithValue("@municipio", sede.Ubicacion.Ciudad.Codigo);
                        commandoInsercionInfoSede.Parameters.AddWithValue("@direccion", sede.Direccion);
                        filasSedes += commandoInsercionInfoSede.ExecuteNonQuery();

                    });
                }

                if (filasTercero == 1 && filasCliente == 1 && filasSedes == sedes.Count)
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

        public List<TerceroInterfazGraficaDTO> FiltrarClientesPorNombre(string nombre, long identificacionEmpresa)
        {
            string sql = "select IDENTIFICACION,DIGITO,TIPO,tip.nombre as tipo_documento,RAZON_SOCIAL,NOMBRE1,NOMBRE2," +
                "APELLIDO1,APELLIDO2,PAIS,pa.nombre as nombre_pais, DEPARTAMENTO,dpto.nombre as nombre_departamento," +
                "MUNICIPIO, ciu.nombre as nombre_ciudad,DIRECCION,TELEFONO,EMAIL,TIPO_TERCERO," +
                "celular,cli.naturaleza,cli.actividad_economica, cli.con_monto_ley, cli.con_monto_personalizado, cli.monto_personalizado " +
                "from terceros inner join tipo_documentos tip on TIPO = tip.codigo inner join " +
                "paises pa on PAIS= pa.codigo inner join departamentos dpto on DEPARTAMENTO = dpto.codigo  " +
                "inner join ciudades ciu on MUNICIPIO = ciu.codigo inner join " +
                "clientes cli on cli.identificacion_cliente = IDENTIFICACION " +
                "where (LOWER(RAZON_SOCIAL) LIKE CONCAT('%', @nombre, '%') OR LOWER(NOMBRE1) LIKE CONCAT('%', @nombre, '%') " +
                "OR LOWER(NOMBRE2) LIKE CONCAT('%', @nombre, '%') OR LOWER(APELLIDO1) LIKE CONCAT('%',@nombre, '%') " +
                "OR LOWER(APELLIDO2) LIKE CONCAT('%', @nombre, '%')) and cli.identificacion_empresa = @identificacion_empresa " +
                "and TIPO_TERCERO='CLIENTE'";

            var listadeClientes = new List<TerceroInterfazGraficaDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@nombre", nombre.ToLower());
                command.Parameters.AddWithValue("@identificacion_empresa", identificacionEmpresa);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var clienteDTO = new TerceroInterfazGraficaDTO();
                    clienteDTO.Identificacion = (long)reader.GetDouble("IDENTIFICACION");
                    clienteDTO.Digito = reader.GetInt32("DIGITO");
                    clienteDTO.TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                    {
                        Codigo = reader.GetInt32("TIPO"),
                        Nombre = reader.GetString("tipo_documento"),
                    };
                    
                    bool tieneRazonsocial = !reader.IsDBNull(4);
                    bool tieneNombre1 = !reader.IsDBNull(5);
                    bool tieneNombre2 = !reader.IsDBNull(6);
                    bool tieneApellido1 = !reader.IsDBNull(7);
                    bool tieneApellido2 = !reader.IsDBNull(8);

                    if (tieneRazonsocial)
                    {
                        clienteDTO.RazonSocial = reader.GetString("RAZON_SOCIAL");
                    }

                    if (tieneNombre1 && tieneNombre2 && tieneApellido1 && tieneApellido2)
                    {
                        clienteDTO.Nombre1 = reader.GetString("NOMBRE1");
                        clienteDTO.Nombre2 = reader.GetString("NOMBRE2");
                        clienteDTO.Apellido1 = reader.GetString("APELLIDO1");
                        clienteDTO.Apellido2 = reader.GetString("APELLIDO2");
                    }

                    clienteDTO.Ubicacion = new UbicacionInterfazGraficaVentaDTO
                    {
                        Pais = new PaisInterfazGraficaVentaDTO
                        {
                            Codigo = reader.GetString("PAIS"),
                            Nombre = reader.GetString("nombre_pais"),
                        },
                        DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                        {
                            Codigo = reader.GetString("DEPARTAMENTO"),
                            Nombre = reader.GetString("nombre_departamento"),
                        },
                        Ciudad = new CiudadInterfazGraficaVentaDTO
                        {
                            Codigo = reader.GetString("MUNICIPIO"),
                            Nombre = reader.GetString("nombre_ciudad"),
                        }
                    };
                    clienteDTO.Direccion = reader.GetString("DIRECCION");
                    clienteDTO.Telefono = reader.GetString("TELEFONO");
                    clienteDTO.Email = reader.GetString("EMAIL");
                    clienteDTO.TipoTercero = reader.GetString("TIPO_TERCERO");
                    clienteDTO.Celular = reader.GetString("celular");
                    clienteDTO.Naturaleza = reader.GetString("naturaleza");
                    clienteDTO.ActividadEconomica = reader.GetString("actividad_economica");
                    clienteDTO.ConMontoDeLey = reader.GetString("con_monto_ley");
                    clienteDTO.ConMontoPersonalizado = reader.GetString("con_monto_personalizado");
                    clienteDTO.MontoPersonalizado = reader.GetDouble("monto_personalizado");
                    listadeClientes.Add(clienteDTO);

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


            return listadeClientes;


        }

        public TerceroInterfazGraficaDTO FiltrarClientesPorIdentificacion(long identificacion, long identificacionEmpresa)
        {
            string sql = "select IDENTIFICACION,DIGITO,TIPO,tip.nombre as tipo_documento,RAZON_SOCIAL,NOMBRE1,NOMBRE2," +
                "APELLIDO1,APELLIDO2,PAIS,pa.nombre as nombre_pais, DEPARTAMENTO,dpto.nombre as nombre_departamento," +
                "MUNICIPIO, ciu.nombre as nombre_ciudad,DIRECCION,TELEFONO,EMAIL,TIPO_TERCERO," +
                "celular,naturaleza,actividad_economica, cli.con_monto_ley, cli.con_monto_personalizado, cli.monto_personalizado " +
                "from terceros inner join tipo_documentos tip on TIPO = tip.codigo inner join " +
                "paises pa on PAIS= pa.codigo inner join departamentos dpto on DEPARTAMENTO = dpto.codigo " +
                "inner join ciudades ciu on MUNICIPIO = ciu.codigo inner join clientes cli " +
                "on cli.identificacion_cliente= IDENTIFICACION " +
                "where IDENTIFICACION = @IDENTIFICACION and cli.identificacion_empresa = @identificacion_empresa " +
                "and TIPO_TERCERO='CLIENTE'";

            var clienteDTO = new TerceroInterfazGraficaDTO();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@IDENTIFICACION", identificacion);
                command.Parameters.AddWithValue("@identificacion_empresa", identificacionEmpresa);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    clienteDTO.Identificacion = (long)reader.GetDouble("IDENTIFICACION");
                    clienteDTO.Digito = reader.GetInt32("DIGITO");
                    clienteDTO.TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                    {
                        Codigo = reader.GetInt32("TIPO"),
                        Nombre = reader.GetString("tipo_documento"),
                    };
                    bool tieneRazonsocial = !reader.IsDBNull(4);
                    bool tieneNombre1 = !reader.IsDBNull(5);
                    bool tieneNombre2 = !reader.IsDBNull(6);
                    bool tieneApellido1 = !reader.IsDBNull(7);
                    bool tieneApellido2 = !reader.IsDBNull(8);

                    if (tieneRazonsocial)
                    {
                        clienteDTO.RazonSocial = reader.GetString("RAZON_SOCIAL");
                    }

                    if (tieneNombre1 && tieneNombre2 && tieneApellido1 && tieneApellido2)
                    {
                        clienteDTO.Nombre1 = reader.GetString("NOMBRE1");
                        clienteDTO.Nombre2 = reader.GetString("NOMBRE2");
                        clienteDTO.Apellido1 = reader.GetString("APELLIDO1");
                        clienteDTO.Apellido2 = reader.GetString("APELLIDO2");
                    }
                    clienteDTO.Ubicacion = new UbicacionInterfazGraficaVentaDTO
                    {
                        Pais = new PaisInterfazGraficaVentaDTO
                        {
                            Codigo = reader.GetString("PAIS"),
                            Nombre = reader.GetString("nombre_pais"),
                        },
                        DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                        {
                            Codigo = reader.GetString("DEPARTAMENTO"),
                            Nombre = reader.GetString("nombre_departamento"),
                        },
                        Ciudad = new CiudadInterfazGraficaVentaDTO
                        {
                            Codigo = reader.GetString("MUNICIPIO"),
                            Nombre = reader.GetString("nombre_ciudad"),
                        }
                    };
                    clienteDTO.Direccion = reader.GetString("DIRECCION");
                    clienteDTO.Telefono = reader.GetString("TELEFONO");
                    clienteDTO.Email = reader.GetString("EMAIL");
                    clienteDTO.TipoTercero = reader.GetString("TIPO_TERCERO");
                    clienteDTO.Celular = reader.GetString("celular");
                    clienteDTO.Naturaleza = reader.GetString("naturaleza");
                    clienteDTO.ActividadEconomica = reader.GetString("actividad_economica");
                    clienteDTO.ConMontoDeLey = reader.GetString("con_monto_ley");
                    clienteDTO.ConMontoPersonalizado = reader.GetString("con_monto_personalizado");
                    clienteDTO.MontoPersonalizado = reader.GetDouble("monto_personalizado");

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

            return clienteDTO;
        }

        public bool ActualizarCliente(TerceroInterfazGraficaDTO tercero, long identificacionEmpresa)
        {
            string sqlRazonSocial = string.Empty;
            string sqlNombre = string.Empty;
            if (!string.IsNullOrWhiteSpace(tercero.RazonSocial))
            {
                sqlRazonSocial = "RAZON_SOCIAL=@RAZON_SOCIAL,";
            }

            if (!string.IsNullOrWhiteSpace(tercero.Nombre1))
            {
                sqlNombre = "NOMBRE1=@NOMBRE1, NOMBRE2=@NOMBRE2, APELLIDO1=@APELLIDO1, APELLIDO2=@APELLIDO2,";
            }

            string sqlInfoTercero = $"update terceros set DIGITO=@DIGITO, TIPO=@TIPO, {sqlRazonSocial} {sqlNombre} PAIS=@PAIS, " +
                "DEPARTAMENTO=@DEPARTAMENTO, MUNICIPIO=@MUNICIPIO, DIRECCION=@DIRECCION, TELEFONO=@TELEFONO, EMAIL=@EMAIL, TIPO_TERCERO=@TIPO_TERCERO," +
                "celular=@celular  where IDENTIFICACION=@IDENTIFICACION";
            string sqlInfoCliente = "update clientes set naturaleza=@naturaleza, actividad_economica=@actividad_economica " +
                "where identificacion_cliente=@identificacion_cliente and identificacion_empresa=@identificacion_empresa";

            _conexion.Open();
            var MysqlConnection = _conexion;
            int filasCliente = 0;
            int filasTercero = 0;
            bool validacionTransaccion = false;
            using MySqlTransaction transaction = MysqlConnection.BeginTransaction();

            try
            {
                var commandoInsercionInfoTercero = MysqlConnection.CreateCommand();
                commandoInsercionInfoTercero.Transaction = transaction;
                commandoInsercionInfoTercero.CommandText = sqlInfoTercero;
                commandoInsercionInfoTercero.Parameters.AddWithValue("@IDENTIFICACION", tercero.Identificacion);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@DIGITO", tercero.Digito);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@TIPO", tercero.TipoDocumento.Codigo);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@RAZON_SOCIAL", tercero.RazonSocial);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@NOMBRE1", tercero.Nombre1);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@NOMBRE2", tercero.Nombre2);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@APELLIDO1", tercero.Apellido1);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@APELLIDO2", tercero.Apellido2);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@PAIS", tercero.Ubicacion.Pais.Codigo);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@DEPARTAMENTO", tercero.Ubicacion.DepartamentoProvincia.Codigo);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@MUNICIPIO", tercero.Ubicacion.Ciudad.Codigo);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@DIRECCION", tercero.Direccion);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@TELEFONO", tercero.Telefono);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@EMAIL", tercero.Email);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@TIPO_TERCERO", tercero.TipoTercero);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@celular", tercero.Celular);
                filasTercero = commandoInsercionInfoTercero.ExecuteNonQuery();

                var commandoInsercionCliente = MysqlConnection.CreateCommand();
                commandoInsercionCliente.Transaction = transaction;
                commandoInsercionCliente.CommandText = sqlInfoCliente;
                commandoInsercionCliente.Parameters.AddWithValue("@identificacion_cliente", tercero.Identificacion);
                commandoInsercionCliente.Parameters.AddWithValue("@identificacion_empresa", identificacionEmpresa);
                commandoInsercionCliente.Parameters.AddWithValue("@naturaleza", tercero.Naturaleza);
                commandoInsercionCliente.Parameters.AddWithValue("@actividad_economica", tercero.ActividadEconomica);
                filasCliente = commandoInsercionCliente.ExecuteNonQuery();

                if ( filasTercero == 1 && filasCliente == 1)
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
