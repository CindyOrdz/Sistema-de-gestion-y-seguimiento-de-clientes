using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using MySqlConnector;
namespace EntidadesNegocio.ClasesDao.TercerosDAO
{
    public class EmpleadoDAO
    {
        private readonly MySqlConnection _conexion;

        public EmpleadoDAO(MySqlConnection conexion)
        {
            _conexion= conexion;
        }

        public List<CargoEmpleadoDTO> ObtenerCargosDeEmpleado()
        {
            string sql = "select id,nombre from cargos";

            var listaCargos = new List<CargoEmpleadoDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var cargoDTO = new CargoEmpleadoDTO
                    {
                        Id = reader.GetInt32("id"),
                        Nombre = reader.GetString("nombre"),

                    };

                    listaCargos.Add(cargoDTO);

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


            return listaCargos;

        }

        public bool InsertarEmpleado(TerceroInterfazGraficaDTO tercero, long identificacionEmpresa)
        {
            string sqlNombre2 = string.Empty;
            string sqlParametrosNombre2 = string.Empty;

            if (!string.IsNullOrWhiteSpace(tercero.Nombre2))
            {
                sqlNombre2 = "NOMBRE2,";
                sqlParametrosNombre2 = "@NOMBRE2,";
            }
            
            string sqlInfoTercero = $"INSERT INTO terceros(IDENTIFICACION,DIGITO,TIPO,NOMBRE1, {sqlNombre2} APELLIDO1,APELLIDO2,PAIS,DEPARTAMENTO,MUNICIPIO,DIRECCION,TELEFONO,EMAIL,TIPO_TERCERO,celular) " +
                $" VALUES (@IDENTIFICACION, @DIGITO, @TIPO,@NOMBRE1, { sqlParametrosNombre2} @APELLIDO1,@APELLIDO2,@PAIS, @DEPARTAMENTO, @MUNICIPIO, @DIRECCION, @TELEFONO, @EMAIL, @TIPO_TERCERO, @celular)";


            string sqlInfoEmpleado = "insert into empleados(identificacion_empleado, identificacion_empresa, cargo, fecha_inicio, fecha_fin) " +
                "values(@identificacion_empleado, @identificacion_empresa, @cargo, @fecha_inicio, @fecha_fin)";
            _conexion.Open();
            var MysqlConnection = _conexion;
            int filasTercero = 0;
            int filasEmpleado = 0;
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

                var commandoInsercionInfoEmpleado = MysqlConnection.CreateCommand();
                commandoInsercionInfoEmpleado.Transaction = transaction;
                commandoInsercionInfoEmpleado.CommandText = sqlInfoEmpleado;
                commandoInsercionInfoEmpleado.Parameters.AddWithValue("@identificacion_empleado", tercero.Identificacion);
                commandoInsercionInfoEmpleado.Parameters.AddWithValue("@identificacion_empresa", identificacionEmpresa);
                commandoInsercionInfoEmpleado.Parameters.AddWithValue("@cargo", tercero.Cargo.Id);
                commandoInsercionInfoEmpleado.Parameters.AddWithValue("@fecha_inicio", tercero.FechaInicio.ToString("yyyy/MM/dd"));
                commandoInsercionInfoEmpleado.Parameters.AddWithValue("@fecha_fin", tercero.FechaFin.ToString("yyyy/MM/dd"));
                filasEmpleado = commandoInsercionInfoEmpleado.ExecuteNonQuery();

                

                if (filasEmpleado == 1 && filasTercero == 1 )
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

        public List<TerceroInterfazGraficaDTO> FiltrarEmpleadosPorNombre(string nombre, long identificacionEmpresa)
        {
            string sql = "select IDENTIFICACION,DIGITO,TIPO,tip.nombre as tipo_documento,NOMBRE1,NOMBRE2," +
                "APELLIDO1,APELLIDO2,PAIS,pa.nombre as nombre_pais, DEPARTAMENTO,dpto.nombre as nombre_departamento," +
                "MUNICIPIO, ciu.nombre as nombre_ciudad,DIRECCION,TELEFONO,EMAIL,TIPO_TERCERO, celular, carg.id as id_cargo," +
                "carg.nombre as nombre_cargo, emp.fecha_inicio, emp.fecha_fin " +
                "from terceros inner join tipo_documentos tip on TIPO = tip.codigo inner join " +
                "paises pa on PAIS= pa.codigo inner join departamentos dpto on DEPARTAMENTO = dpto.codigo " +
                "inner join ciudades ciu on MUNICIPIO = ciu.codigo inner join " +
                "empleados emp on emp.identificacion_empleado = IDENTIFICACION inner join " +
                "cargos carg on carg.id = emp.cargo " +
                "where (LOWER(NOMBRE1) LIKE CONCAT('%', @nombre, '%') OR LOWER(NOMBRE2) LIKE CONCAT('%', @nombre, '%') " +
                "OR LOWER(APELLIDO1) LIKE CONCAT('%',@nombre, '%') " +
                "OR LOWER(APELLIDO2) LIKE CONCAT('%', @nombre, '%')) and emp.identificacion_empresa =@identificacion_empresa " +
                "and TIPO_TERCERO='EMPLEADO'";

            var listadeEmpleados = new List<TerceroInterfazGraficaDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@nombre", nombre.ToLower());
                command.Parameters.AddWithValue("@identificacion_empresa", identificacionEmpresa);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var empleadoDTO = new TerceroInterfazGraficaDTO();
                    empleadoDTO.Identificacion = (long)reader.GetDouble("IDENTIFICACION");
                    empleadoDTO.Digito = reader.GetInt32("DIGITO");
                    empleadoDTO.TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                    {
                        Codigo = reader.GetInt32("TIPO"),
                        Nombre = reader.GetString("tipo_documento"),
                    };
                    empleadoDTO.Nombre1 = reader.GetString("NOMBRE1");
                    bool tieneNombre2 = !reader.IsDBNull(5);

                    if (tieneNombre2)
                    {
                        empleadoDTO.Nombre2 = reader.GetString("NOMBRE2");
                    }

                    empleadoDTO.Apellido1 = reader.GetString("APELLIDO1");
                    empleadoDTO.Apellido2 = reader.GetString("APELLIDO2");

                    empleadoDTO.Ubicacion = new UbicacionInterfazGraficaVentaDTO
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
                    empleadoDTO.Direccion = reader.GetString("DIRECCION");
                    empleadoDTO.Telefono = reader.GetString("TELEFONO");
                    empleadoDTO.Email = reader.GetString("EMAIL");
                    empleadoDTO.TipoTercero = reader.GetString("TIPO_TERCERO");
                    empleadoDTO.Celular = reader.GetString("celular");
                    empleadoDTO.Cargo = new CargoEmpleadoDTO
                    {
                        Id = reader.GetInt32("id_cargo"),
                        Nombre = reader.GetString("nombre_cargo")

                    };
                    empleadoDTO.FechaInicio = reader.GetDateOnly("fecha_inicio");
                    empleadoDTO.FechaFin = reader.GetDateOnly("fecha_fin");

                    listadeEmpleados.Add(empleadoDTO);

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


            return listadeEmpleados;


        }

        public TerceroInterfazGraficaDTO FiltrarEmpleadoPorIdentificacion(long identificacion, long identificacionEmpresa)
        {
            string sql = "select IDENTIFICACION,DIGITO,TIPO,tip.nombre as tipo_documento,RAZON_SOCIAL,NOMBRE1,NOMBRE2," +
                "APELLIDO1,APELLIDO2,PAIS,pa.nombre as nombre_pais, DEPARTAMENTO,dpto.nombre as nombre_departamento," +
                "MUNICIPIO, ciu.nombre as nombre_ciudad,DIRECCION,TELEFONO,EMAIL,TIPO_TERCERO, celular, carg.id as id_cargo," +
                "carg.nombre as nombre_cargo, emp.fecha_inicio, emp.fecha_fin " +
                "from terceros inner join tipo_documentos tip on TIPO = tip.codigo inner join " +
                "paises pa on PAIS= pa.codigo inner join departamentos dpto on DEPARTAMENTO = dpto.codigo " +
                "inner join ciudades ciu on MUNICIPIO = ciu.codigo inner join " +
                "empleados emp on emp.identificacion_empleado = IDENTIFICACION inner join " +
                "cargos carg on carg.id = emp.cargo " +
                "where IDENTIFICACION =@IDENTIFICACION and emp.identificacion_empresa = @identificacion_empresa " +
                "and TIPO_TERCERO='EMPLEADO'";

            var empleadoDTO = new TerceroInterfazGraficaDTO();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@IDENTIFICACION", identificacion);
                command.Parameters.AddWithValue("@identificacion_empresa", identificacionEmpresa);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    empleadoDTO.Identificacion = (long)reader.GetDouble("IDENTIFICACION");
                    empleadoDTO.Digito = reader.GetInt32("DIGITO");
                    empleadoDTO.TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                    {
                        Codigo = reader.GetInt32("DIGITO"),
                        Nombre = reader.GetString("tipo_documento"),
                    };
                    empleadoDTO.Nombre1 = reader.GetString("NOMBRE1");
                    bool tieneNombre2 = !reader.IsDBNull(6);

                    if (tieneNombre2)
                    {
                        empleadoDTO.Nombre2 = reader.GetString("NOMBRE2");
                    }
                    empleadoDTO.Apellido1 = reader.GetString("APELLIDO1");
                    empleadoDTO.Apellido2 = reader.GetString("APELLIDO2");
                    empleadoDTO.Ubicacion = new UbicacionInterfazGraficaVentaDTO
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
                    empleadoDTO.Direccion = reader.GetString("DIRECCION");
                    empleadoDTO.Telefono = reader.GetString("TELEFONO");
                    empleadoDTO.Email = reader.GetString("EMAIL");
                    empleadoDTO.TipoTercero = reader.GetString("TIPO_TERCERO");
                    empleadoDTO.Celular = reader.GetString("celular");
                    empleadoDTO.Cargo = new CargoEmpleadoDTO
                    {
                        Id = reader.GetInt32("id_cargo"),
                        Nombre = reader.GetString("nombre_cargo")

                    };
                    empleadoDTO.FechaInicio = reader.GetDateOnly("fecha_inicio");
                    empleadoDTO.FechaFin = reader.GetDateOnly("fecha_fin");

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

            return empleadoDTO;
        }

        public bool ActualizarEmpleado(TerceroInterfazGraficaDTO tercero, long identificacionEmpresa)
        {
            string sqlNombre2 = string.Empty;

            if (!string.IsNullOrWhiteSpace(tercero.Nombre2))
            {
                sqlNombre2 = "NOMBRE2=@NOMBRE2,";
            }

            string sqlInfoTercero = $"update terceros set DIGITO=@DIGITO, TIPO=@TIPO, NOMBRE1=@NOMBRE1, {sqlNombre2} APELLIDO1=@APELLIDO1, APELLIDO2=@APELLIDO2, PAIS=@PAIS, " +
                "DEPARTAMENTO=@DEPARTAMENTO, MUNICIPIO=@MUNICIPIO, DIRECCION=@DIRECCION, TELEFONO=@TELEFONO, EMAIL=@EMAIL, TIPO_TERCERO=@TIPO_TERCERO," +
                "celular=@celular  where IDENTIFICACION=@IDENTIFICACION";
            string sqlInfoCliente = "update empleados set cargo=@cargo, fecha_inicio=@fecha_inicio, fecha_fin=@fecha_fin " +
                "where identificacion_empleado=@identificacion_empleado and identificacion_empresa=@identificacion_empresa";

            _conexion.Open();
            var MysqlConnection = _conexion;
            int filasEmpleado = 0;
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

                var commandoInsercionInfoEmpleado = MysqlConnection.CreateCommand();
                commandoInsercionInfoEmpleado.Transaction = transaction;
                commandoInsercionInfoEmpleado.CommandText = sqlInfoCliente;
                commandoInsercionInfoEmpleado.Parameters.AddWithValue("@identificacion_empleado", tercero.Identificacion);
                commandoInsercionInfoEmpleado.Parameters.AddWithValue("@identificacion_empresa", identificacionEmpresa);
                commandoInsercionInfoEmpleado.Parameters.AddWithValue("@cargo", tercero.Cargo.Id);
                commandoInsercionInfoEmpleado.Parameters.AddWithValue("@fecha_inicio", tercero.FechaInicio.ToString("yyyy/MM/dd"));
                commandoInsercionInfoEmpleado.Parameters.AddWithValue("@fecha_fin", tercero.FechaFin.ToString("yyyy/MM/dd"));
                filasEmpleado = commandoInsercionInfoEmpleado.ExecuteNonQuery();

                if (filasTercero == 1 && filasEmpleado == 1)
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

        public List<EmpleadoInterfazGraficaVentaDTO> ObtenerEmpleadosDeEmpresa(long identificacionEmpresa)
        {
            string sql = "select IDENTIFICACION,TIPO,tip.nombre as tipo_documento,NOMBRE1,NOMBRE2," +
                "APELLIDO1,APELLIDO2,PAIS,pa.nombre as nombre_pais, DEPARTAMENTO,dpto.nombre as nombre_departamento," +
                "MUNICIPIO, ciu.nombre as nombre_ciudad,DIRECCION,TELEFONO,EMAIL, celular " +
                "from terceros inner join tipo_documentos tip on TIPO = tip.codigo inner join " +
                "paises pa on PAIS= pa.codigo inner join departamentos dpto on DEPARTAMENTO = dpto.codigo " +
                "inner join ciudades ciu on MUNICIPIO = ciu.codigo inner join " +
                "empleados emp on emp.identificacion_empleado = IDENTIFICACION inner join " +
                "cargos carg on carg.id = emp.cargo " +
                "where emp.identificacion_empresa =@identificacion_empresa " +
                "and TIPO_TERCERO='EMPLEADO'";

            var listadeEmpleados = new List<EmpleadoInterfazGraficaVentaDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@identificacion_empresa", identificacionEmpresa);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var empleadoDTO = new EmpleadoInterfazGraficaVentaDTO();
                    empleadoDTO.Identificacion = (long)reader.GetDouble("IDENTIFICACION");
                    empleadoDTO.TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                    {
                        Codigo = reader.GetInt32("TIPO"),
                        Nombre = reader.GetString("tipo_documento"),
                    };
                    empleadoDTO.Nombre1 = reader.GetString("NOMBRE1");
                    bool tieneNombre2 = !reader.IsDBNull(4);

                    if (tieneNombre2)
                    {
                        empleadoDTO.Nombre2 = reader.GetString("NOMBRE2");
                    }

                    empleadoDTO.Apellido1 = reader.GetString("APELLIDO1");
                    empleadoDTO.Apellido2 = reader.GetString("APELLIDO2");

                    empleadoDTO.Ubicacion = new UbicacionInterfazGraficaVentaDTO
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
                    empleadoDTO.Direccion = reader.GetString("DIRECCION");
                    empleadoDTO.Telefono = reader.GetString("TELEFONO");
                    empleadoDTO.Email = reader.GetString("EMAIL");
                    empleadoDTO.Celular = reader.GetString("celular");

                    listadeEmpleados.Add(empleadoDTO);

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


            return listadeEmpleados;


        }


    }

    
}
