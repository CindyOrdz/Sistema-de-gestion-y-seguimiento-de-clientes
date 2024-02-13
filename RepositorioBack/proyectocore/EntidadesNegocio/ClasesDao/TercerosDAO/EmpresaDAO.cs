using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;
using MySqlConnector;
namespace EntidadesNegocio.ClasesDao.TercerosDAO
{
    public class EmpresaDAO
    {
        private readonly MySqlConnection _conexion;
        public EmpresaDAO(MySqlConnection conexion)
        {
            _conexion = conexion;

        }

        public bool InsertarEmpresa(TerceroInterfazGraficaDTO tercero)
        {
            string sqlnombre = string.Empty;
            string sqlParametrosNombre = string.Empty;
            if (!string.IsNullOrWhiteSpace(tercero.Nombre1))
            {
                sqlnombre = ",NOMBRE1,NOMBRE2,APELLIDO1,APELLIDO2";
                sqlParametrosNombre = ",@NOMBRE1,@NOMBRE2,@APELLIDO1,@APELLIDO2";
            }
            

            string sqlInfoTercero = "INSERT INTO terceros(IDENTIFICACION,DIGITO,TIPO,RAZON_SOCIAL"+sqlnombre+",PAIS,DEPARTAMENTO,MUNICIPIO,DIRECCION,TELEFONO,EMAIL,TIPO_TERCERO,celular) " +
                " VALUES (@IDENTIFICACION, @DIGITO, @TIPO, @RAZON_SOCIAL"+sqlParametrosNombre+",@PAIS, @DEPARTAMENTO, @MUNICIPIO, @DIRECCION, @TELEFONO, @EMAIL, @TIPO_TERCERO, @celular)";


            string sqlInfoEmpresa = "insert into empresas(identificacion_empresa) values(@id_empresa)";
            _conexion.Open();
            var MysqlConnection = _conexion;
            int filasTercero = 0;
            int filasEmpresa = 0;
            int filasSedes = 0;
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
                if (!string.IsNullOrWhiteSpace(tercero.Nombre1))
                {
                    commandoInsercionInfoTercero.Parameters.AddWithValue("@NOMBRE1", tercero.Nombre1);
                    commandoInsercionInfoTercero.Parameters.AddWithValue("@NOMBRE2", tercero.Nombre2);
                    commandoInsercionInfoTercero.Parameters.AddWithValue("@APELLIDO1", tercero.Apellido1);
                    commandoInsercionInfoTercero.Parameters.AddWithValue("@APELLIDO2", tercero.Apellido2);
                }
                commandoInsercionInfoTercero.Parameters.AddWithValue("@PAIS", tercero.Ubicacion.Pais.Codigo);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@DEPARTAMENTO", tercero.Ubicacion.DepartamentoProvincia.Codigo);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@MUNICIPIO", tercero.Ubicacion.Ciudad.Codigo);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@DIRECCION", tercero.Direccion);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@TELEFONO", tercero.Telefono);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@EMAIL", tercero.Email);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@TIPO_TERCERO", tercero.TipoTercero);
                commandoInsercionInfoTercero.Parameters.AddWithValue("@celular", tercero.Celular);
                filasTercero = commandoInsercionInfoTercero.ExecuteNonQuery();

                var commandoInsercionInfoEmpresa = MysqlConnection.CreateCommand();
                commandoInsercionInfoEmpresa.Transaction = transaction;
                commandoInsercionInfoEmpresa.CommandText = sqlInfoEmpresa;
                commandoInsercionInfoEmpresa.Parameters.AddWithValue("@id_empresa", tercero.Identificacion);
                filasEmpresa = commandoInsercionInfoEmpresa.ExecuteNonQuery(); 

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

                if (filasEmpresa == 1 && filasTercero == 1 && filasSedes == sedes.Count)
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

        public List<TerceroInterfazGraficaDTO> FiltrarEmpresasPorNombre(string nombre)
        {
            string sql = "select IDENTIFICACION,DIGITO,TIPO,tip.nombre as tipo_documento,RAZON_SOCIAL,NOMBRE1,NOMBRE2," +
                "APELLIDO1,APELLIDO2,PAIS,pa.nombre as nombre_pais, DEPARTAMENTO,dpto.nombre as nombre_departamento," +
                "MUNICIPIO, ciu.nombre as nombre_ciudad,DIRECCION,TELEFONO,EMAIL,TIPO_TERCERO,celular" +
                " from terceros inner join tipo_documentos tip on TIPO = tip.codigo inner join " +
                " paises pa on PAIS= pa.codigo inner join departamentos dpto on DEPARTAMENTO = dpto.codigo " +
                " inner join ciudades ciu on MUNICIPIO = ciu.codigo " +
                " where (LOWER(RAZON_SOCIAL) LIKE CONCAT('%', @nombre, '%') OR LOWER(NOMBRE1) LIKE CONCAT('%', @nombre, '%') OR LOWER(NOMBRE2) LIKE CONCAT('%', @nombre, '%') OR LOWER(APELLIDO1) LIKE CONCAT('%', @nombre, '%') OR LOWER(APELLIDO2) LIKE CONCAT('%', @nombre, '%')) " +
                "and TIPO_TERCERO='EMPRESA'";

            var listadeEmpresas = new List<TerceroInterfazGraficaDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@nombre", nombre.ToLower());
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var empresaDTO = new TerceroInterfazGraficaDTO();
                    empresaDTO.Identificacion = (long)reader.GetDouble("IDENTIFICACION");
                    empresaDTO.Digito = reader.GetInt32("DIGITO");
                    empresaDTO.TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                    {
                        Codigo = reader.GetInt32("TIPO"),
                        Nombre = reader.GetString("tipo_documento"),
                    };
                    empresaDTO.RazonSocial = reader.GetString("RAZON_SOCIAL");
                    bool tieneNombre1= !reader.IsDBNull(5);
                    bool tieneNombre2= !reader.IsDBNull(6);
                    bool tieneApellido1 = !reader.IsDBNull(7);
                    bool tieneApellido2= !reader.IsDBNull(8);

                    if (tieneNombre1 && tieneNombre2 && tieneApellido1 && tieneApellido2)
                    {
                        empresaDTO.Nombre1 = reader.GetString("NOMBRE1");
                        empresaDTO.Nombre2 = reader.GetString("NOMBRE2");
                        empresaDTO.Apellido1 = reader.GetString("APELLIDO1");
                        empresaDTO.Apellido2 = reader.GetString("APELLIDO2");
                    }
                    
                    empresaDTO.Ubicacion = new UbicacionInterfazGraficaVentaDTO
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
                    empresaDTO.Direccion = reader.GetString("DIRECCION");
                    empresaDTO.Telefono = reader.GetString("TELEFONO");
                    empresaDTO.Email = reader.GetString("EMAIL");
                    empresaDTO.TipoTercero = reader.GetString("TIPO_TERCERO");
                    empresaDTO.Celular = reader.GetString("celular");

                    listadeEmpresas.Add(empresaDTO);

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


            return listadeEmpresas;


        }

        public TerceroInterfazGraficaDTO FiltrarEmpresasPorIdentificacion(long identificacion)
        {
            string sql = "select IDENTIFICACION,DIGITO,TIPO,tip.nombre as tipo_documento,RAZON_SOCIAL,NOMBRE1,NOMBRE2," +
                "APELLIDO1,APELLIDO2,PAIS,pa.nombre as nombre_pais, DEPARTAMENTO,dpto.nombre as nombre_departamento," +
                "MUNICIPIO, ciu.nombre as nombre_ciudad,DIRECCION,TELEFONO,EMAIL,TIPO_TERCERO,celular " +
                " from terceros inner join tipo_documentos tip on TIPO = tip.codigo inner join " +
                " paises pa on PAIS= pa.codigo inner join departamentos dpto on DEPARTAMENTO = dpto.codigo " +
                " inner join ciudades ciu on MUNICIPIO = ciu.codigo " +
                " where IDENTIFICACION = @identificacion and TIPO_TERCERO='EMPRESA'";

            var empresaDTO = new TerceroInterfazGraficaDTO();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@identificacion", identificacion);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    empresaDTO.Identificacion = (long)reader.GetDouble("IDENTIFICACION");
                    empresaDTO.Digito = reader.GetInt32("DIGITO");
                    empresaDTO.TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                    {
                        Codigo = reader.GetInt32("TIPO"),
                        Nombre = reader.GetString("tipo_documento"),
                    };
                    empresaDTO.RazonSocial = reader.GetString("RAZON_SOCIAL");
                    bool tieneNombre1 = !reader.IsDBNull(5);
                    bool tieneNombre2 = !reader.IsDBNull(6);
                    bool tieneApellido1 = !reader.IsDBNull(7);
                    bool tieneApellido2 = !reader.IsDBNull(8);

                    if (tieneNombre1 && tieneNombre2 && tieneApellido1 && tieneApellido2)
                    {
                        empresaDTO.Nombre1 = reader.GetString("NOMBRE1");
                        empresaDTO.Nombre2 = reader.GetString("NOMBRE2");
                        empresaDTO.Apellido1 = reader.GetString("APELLIDO1");
                        empresaDTO.Apellido2 = reader.GetString("APELLIDO2");
                    }

                    empresaDTO.Ubicacion = new UbicacionInterfazGraficaVentaDTO
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
                    empresaDTO.Direccion = reader.GetString("DIRECCION");
                    empresaDTO.Telefono = reader.GetString("TELEFONO");
                    empresaDTO.Email = reader.GetString("EMAIL");
                    empresaDTO.TipoTercero = reader.GetString("TIPO_TERCERO");
                    empresaDTO.Celular = reader.GetString("celular");

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

            return empresaDTO;

        }

        public EmpresaInterfazGraficaVentaDTO ObtenerEmpresaPorIdentificacion(long identificacion)
        {
            string sql = "select IDENTIFICACION,TIPO,tip.nombre as tipo_documento,RAZON_SOCIAL,NOMBRE1,NOMBRE2," +
                "APELLIDO1,APELLIDO2,PAIS,pa.nombre as nombre_pais, DEPARTAMENTO,dpto.nombre as nombre_departamento," +
                "MUNICIPIO, ciu.nombre as nombre_ciudad,DIRECCION,TELEFONO,EMAIL,celular " +
                " from terceros inner join tipo_documentos tip on TIPO = tip.codigo inner join " +
                " paises pa on PAIS= pa.codigo inner join departamentos dpto on DEPARTAMENTO = dpto.codigo " +
                " inner join ciudades ciu on MUNICIPIO = ciu.codigo " +
                " where IDENTIFICACION = @identificacion and TIPO_TERCERO='EMPRESA'";

            var empresaDTO = new EmpresaInterfazGraficaVentaDTO();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@identificacion", identificacion);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    empresaDTO.Identificacion = (long)reader.GetDouble("IDENTIFICACION");
                    empresaDTO.TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                    {
                        Codigo = reader.GetInt32("TIPO"),
                        Nombre = reader.GetString("tipo_documento"),
                    };
                    empresaDTO.RazonSocial = reader.GetString("RAZON_SOCIAL");
                    bool tieneNombre1 = !reader.IsDBNull(5);
                    bool tieneNombre2 = !reader.IsDBNull(6);
                    bool tieneApellido1 = !reader.IsDBNull(7);
                    bool tieneApellido2 = !reader.IsDBNull(8);

                    if (tieneNombre1 && tieneNombre2 && tieneApellido1 && tieneApellido2)
                    {
                        empresaDTO.Nombre1 = reader.GetString("NOMBRE1");
                        empresaDTO.Nombre2 = reader.GetString("NOMBRE2");
                        empresaDTO.Apellido1 = reader.GetString("APELLIDO1");
                        empresaDTO.Apellido2 = reader.GetString("APELLIDO2");
                    }

                    empresaDTO.Ubicacion = new UbicacionInterfazGraficaVentaDTO
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
                    empresaDTO.Direccion = reader.GetString("DIRECCION");
                    empresaDTO.Telefono = reader.GetString("TELEFONO");
                    empresaDTO.Email = reader.GetString("EMAIL");
                    empresaDTO.Celular = reader.GetString("celular");

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

            return empresaDTO;


        }

        public bool ActualizarEmpresa(TerceroInterfazGraficaDTO tercero)
        {
            string sqlnombre = string.Empty;
            if (!string.IsNullOrWhiteSpace(tercero.Nombre1))
            {
                sqlnombre = "NOMBRE1=@NOMBRE1, NOMBRE2=@NOMBRE2, APELLIDO1=@APELLIDO1, APELLIDO2=@APELLIDO2,";
            }

            string sqlInfoEmpresa = "update terceros set DIGITO=@DIGITO, TIPO=@TIPO, RAZON_SOCIAL=@RAZON_SOCIAL,"+sqlnombre+" PAIS=@PAIS, " +
                "DEPARTAMENTO=@DEPARTAMENTO, MUNICIPIO=@MUNICIPIO, DIRECCION=@DIRECCION, TELEFONO=@TELEFONO, EMAIL=@EMAIL, TIPO_TERCERO=@TIPO_TERCERO," +
                "celular=@celular where IDENTIFICACION=@IDENTIFICACION";
            
            _conexion.Open();
            var MysqlConnection = _conexion;
            int filasEmpresa = 0;
            bool validacionTransaccion = false;
            using MySqlTransaction transaction = MysqlConnection.BeginTransaction();

            try
            {
                var commandoInsercionInfoEmpresa = MysqlConnection.CreateCommand();
                commandoInsercionInfoEmpresa.Transaction = transaction;
                commandoInsercionInfoEmpresa.CommandText = sqlInfoEmpresa;
                commandoInsercionInfoEmpresa.Parameters.AddWithValue("@IDENTIFICACION", tercero.Identificacion);
                commandoInsercionInfoEmpresa.Parameters.AddWithValue("@DIGITO", tercero.Digito);
                commandoInsercionInfoEmpresa.Parameters.AddWithValue("@TIPO", tercero.TipoDocumento.Codigo);
                commandoInsercionInfoEmpresa.Parameters.AddWithValue("@RAZON_SOCIAL", tercero.RazonSocial);
                commandoInsercionInfoEmpresa.Parameters.AddWithValue("@NOMBRE1", tercero.Nombre1);
                commandoInsercionInfoEmpresa.Parameters.AddWithValue("@NOMBRE2", tercero.Nombre2);
                commandoInsercionInfoEmpresa.Parameters.AddWithValue("@APELLIDO1", tercero.Apellido1);
                commandoInsercionInfoEmpresa.Parameters.AddWithValue("@APELLIDO2", tercero.Apellido2);
                commandoInsercionInfoEmpresa.Parameters.AddWithValue("@PAIS", tercero.Ubicacion.Pais.Codigo);
                commandoInsercionInfoEmpresa.Parameters.AddWithValue("@DEPARTAMENTO", tercero.Ubicacion.DepartamentoProvincia.Codigo);
                commandoInsercionInfoEmpresa.Parameters.AddWithValue("@MUNICIPIO", tercero.Ubicacion.Ciudad.Codigo);
                commandoInsercionInfoEmpresa.Parameters.AddWithValue("@DIRECCION", tercero.Direccion);
                commandoInsercionInfoEmpresa.Parameters.AddWithValue("@TELEFONO", tercero.Telefono);
                commandoInsercionInfoEmpresa.Parameters.AddWithValue("@EMAIL", tercero.Email);
                commandoInsercionInfoEmpresa.Parameters.AddWithValue("@TIPO_TERCERO", tercero.TipoTercero);
                commandoInsercionInfoEmpresa.Parameters.AddWithValue("@celular", tercero.Celular);
                filasEmpresa = commandoInsercionInfoEmpresa.ExecuteNonQuery();

                
                if (filasEmpresa == 1 )
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
