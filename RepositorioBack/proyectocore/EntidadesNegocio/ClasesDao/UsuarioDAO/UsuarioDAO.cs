
using EntidadesNegocio.ElementosInventario;
using EntidadesNegocio.InterfazGraficaBlazorDTO;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.InterfazGraficaBlazorDTO.SesionDTO;
using MySqlConnector;
namespace EntidadesNegocio.ClasesDao.UsuarioDAO
{
    public class UsuarioDAO
    {

        private readonly MySqlConnection _conexion;
        public UsuarioDAO(MySqlConnection conexion)
        {
            _conexion = conexion;
        }

        public int VerificarUsuario(string emailUsuario)
        {
            string sql = "select exists(select id from usuarios " +
                "where email = @email_usuario and fecha_activacion is not null) as resultado";
            int id = 0;
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@email_usuario", emailUsuario);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    id = reader.GetInt32("resultado");

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


            return id;


        }

        public string ObtenerHashPasswd(string emailUsuario)
        {
            string sql = "select password_hash from usuarios " +
                "where email = @email_usuario";
            string hashPasswd = "";
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@email_usuario", emailUsuario);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    hashPasswd = reader.GetString("password_hash");

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


            return hashPasswd;
        }

        public List<string> ObtenerRolesUsuario(string emailUsuario)
        {
            List<string> roles = new List<string>();
            string sql = "select rol.nombre as nombre_rol from usuarios us " +
                         "inner join usuario_roles us_rol on us.id= us_rol.id_usuario " +
                         "inner join roles rol on rol.id = us_rol.id_rol " +
                         "where us.email = @email_usuario";

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql,_conexion);
                command.Parameters.AddWithValue("@email_usuario", emailUsuario);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    roles.Add(reader.GetString("nombre_rol"));
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

            return roles;
        }


        public List<string> ObtenerPoliticasAccesoDeUsuario(string emailUsuario)
        {
            List<string> politicasAcceso = new List<string>();
            string sql = "select pol_acc.nombre as nombre_politica  " +
                         "from usuarios inner join usuario_politicas_acceso us_pol " +
                         "on us_pol.id_usuario = id inner join politicas_acceso pol_acc  " +
                         "on pol_acc.id = us_pol.id_politica_acceso " +
                         "where email = @email_usuario";

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@email_usuario", emailUsuario);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    politicasAcceso.Add(reader.GetString("nombre_politica"));
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

            return politicasAcceso;
        }

        public List<PoliticasAccesoDTO> ObtenerPoliticasAccesoDeUsuarioParaConfiguracion(string emailUsuario)
        {
            List<PoliticasAccesoDTO> politicasAcceso = new List<PoliticasAccesoDTO>();
            string sql = "select pol_acc.id as id_politica ,pol_acc.nombre as nombre_politica  " +
                         "from usuarios inner join usuario_politicas_acceso us_pol " +
                         "on us_pol.id_usuario = id inner join politicas_acceso pol_acc  " +
                         "on pol_acc.id = us_pol.id_politica_acceso " +
                         "where email = @email_usuario";

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@email_usuario", emailUsuario);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var politica = new PoliticasAccesoDTO
                    {
                        Id = reader.GetInt32("id_politica"),
                        Nombre = reader.GetString("nombre_politica"),
                        PoliticaExistente = true,
                        Seleccionada = true,
                    };
                    politicasAcceso.Add(politica);
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

            return politicasAcceso;
        }

        public long ObtenerIdentificacionEmpresaPorUsuario(string emailUsuario)
        {
            string sql = "select IDENTIFICACION from terceros inner join tercero_usuarios ter_us " +
                "on ter_us.identificacion_tercero=IDENTIFICACION inner join usuarios us " +
                "on us.id=ter_us.id_usuario where us.email=@email_usuario";

            long identificacionEmpresa = 0;
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@email_usuario", emailUsuario);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    identificacionEmpresa = (long)reader.GetDouble("IDENTIFICACION");
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


            return identificacionEmpresa;
        }


        public UsuarioInfoDTO ObtenerInformacionPorUsuario(string emailUsuario)
        {
            string sql = "select IDENTIFICACION,TIPO,tip.nombre as tipo_documento,RAZON_SOCIAL,NOMBRE1,NOMBRE2," +
                "APELLIDO1,APELLIDO2,PAIS,pa.nombre as nombre_pais, DEPARTAMENTO,dpto.nombre as nombre_departamento," +
                "MUNICIPIO, ciu.nombre as nombre_ciudad,DIRECCION,TELEFONO " +
                "from terceros inner join tipo_documentos tip on TIPO = tip.codigo inner join  " +
                "paises pa on PAIS= pa.codigo inner join departamentos dpto on DEPARTAMENTO = dpto.codigo " +
                "inner join ciudades ciu on MUNICIPIO = ciu.codigo inner join tercero_usuarios ter_us " +
                "on ter_us.identificacion_tercero=IDENTIFICACION inner join usuarios us " +
                "on us.id = ter_us.id_usuario where us.email = @email_usuario ";

            var usuarioDTO = new UsuarioInfoDTO();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@email_usuario", emailUsuario);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    usuarioDTO.Identificacion = (long)reader.GetDouble("IDENTIFICACION");
                    usuarioDTO.TipoDocumento = new TipoDocumentoInterfazGraficaTercerosDTO
                    {
                        Codigo = reader.GetInt32("TIPO"),
                        Nombre = reader.GetString("tipo_documento"),
                    };
                    bool tieneRazonsocial = !reader.IsDBNull(3);
                    bool tieneNombre1 = !reader.IsDBNull(4);
                    bool tieneNombre2 = !reader.IsDBNull(5);
                    bool tieneApellido1 = !reader.IsDBNull(6);
                    bool tieneApellido2 = !reader.IsDBNull(7);

                    if (tieneRazonsocial)
                    {
                        usuarioDTO.RazonSocial = reader.GetString("RAZON_SOCIAL");
                    }

                    if (tieneNombre1)
                    {
                        usuarioDTO.Nombre1 = reader.GetString("NOMBRE1");

                    }

                    if (tieneNombre2)
                    {
                        usuarioDTO.Nombre2 = reader.GetString("NOMBRE2");
                    }

                    if (tieneApellido1)
                    {
                        usuarioDTO.Apellido1 = reader.GetString("APELLIDO1");
                        usuarioDTO.Apellido2 = reader.GetString("APELLIDO2");
                    }

                    usuarioDTO.Ubicacion = new UbicacionInterfazGraficaVentaDTO
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
                    usuarioDTO.Direccion = reader.GetString("DIRECCION");
                    usuarioDTO.Telefono = reader.GetString("TELEFONO");
                    
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

            return usuarioDTO;
        }

        public long ObtenerIdentificacionEmpresaPorUsuarioEmpleado(string emailUsuario)
        {
            string sql = "select emp.identificacion_empresa from terceros  " +
                "inner join tercero_usuarios ter_us on ter_us.identificacion_tercero=IDENTIFICACION " +
                "inner join usuarios us on us.id=ter_us.id_usuario inner join empleados emp " +
                "on emp.identificacion_empleado=IDENTIFICACION " +
                "where us.email=@email_usuario";

            long identificacionEmpresa = 0;
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@email_usuario", emailUsuario);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    identificacionEmpresa = (long)reader.GetDouble("identificacion_empresa");
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


            return identificacionEmpresa;
        }


        public DetalleUsuarioDTO ObtenerUsuarioPorIdentificacionDelTercero(long  identificacionTercero)
        {
            string sql = "select us.id as id_usuario, us.email as email_usuario from usuarios us " +
                "inner join tercero_usuarios ter_us " +
                "on us.id= ter_us.id_usuario inner join terceros ter " +
                "on ter.IDENTIFICACION = ter_us.identificacion_tercero " +
                "where ter.IDENTIFICACION = @identificacion";

            DetalleUsuarioDTO usuarioDTO = new DetalleUsuarioDTO();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@identificacion", identificacionTercero);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    usuarioDTO.Id = reader.GetInt64("id_usuario");
                    usuarioDTO.Email = reader.GetString("email_usuario");
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


            return usuarioDTO;
        }


        public List<PoliticasAccesoDTO> ObtenerPoliticasDeAccesoParaUsuarioEmpresa()
        {
            List<PoliticasAccesoDTO> politicasAcceso = new List<PoliticasAccesoDTO>();
            string sql = "select id, nombre from politicas_acceso where id=1 or id=2 or id=3 or id=4 " +
                "or id=5 or id=6 or id=10 or id=11 or id=12 or id=13 or id=19 or id=20 or id=21 or id=22 " +
                "or id=27 or id=29 or id=31 order by id";

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var politicaDTO = new PoliticasAccesoDTO();
                    politicaDTO.Id = reader.GetInt32("id");
                    politicaDTO.Nombre = reader.GetString("nombre");

                    politicasAcceso.Add(politicaDTO);
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

            return politicasAcceso;
        }

        public List<PoliticasAccesoDTO> ObtenerPoliticasDeAccesoParaUsuarioEmpleado()
        {
            List<PoliticasAccesoDTO> politicasAcceso = new List<PoliticasAccesoDTO>();
            string sql = "select id, nombre from politicas_acceso where id=1 or id=2 or id=3 or id=10 or id=11 " +
                "or id=12 or id=15 or id=16 or id=17 or id=18 or id=19 or id=20 or id=21 or id=23 or id=24 " +
                "or id=25 or id=26 or id=28 or id=29 or id=30 or id=31 order by id";

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var politicaDTO = new PoliticasAccesoDTO();
                    politicaDTO.Id = reader.GetInt32("id");
                    politicaDTO.Nombre = reader.GetString("nombre");

                    politicasAcceso.Add(politicaDTO);
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

            return politicasAcceso;
        }


        public bool ConfigurarPoliticasDeUsuario(DetalleUsuarioDTO detalleUsuario, List<PoliticasAccesoDTO> politicasUsuario)
        {

            string sqlNuevaPolitica = "insert into usuario_politicas_acceso(id_usuario, id_politica_acceso) " +
                                    "values (@id_usuario, @id_politica)";

            string sqlEliminarPolitica = "delete from usuario_politicas_acceso where " +
                "id_usuario=@id_usuario and  id_politica_acceso=@id_politica";

            _conexion.Open();
            var MysqlConnection = _conexion;
            int filasNuevaPolitica = 0;
            int filasEliminarPolitica = 0;
            bool validacionTransaccion = false;
            using MySqlTransaction transaction = MysqlConnection.BeginTransaction();

            try
            {

                var politicasNuevas = politicasUsuario.Where(pol => pol.PoliticaNueva && pol.Seleccionada).ToList();

                politicasNuevas.ForEach(politica =>
                {
                    var camandoNuevaPolitica = MysqlConnection.CreateCommand();
                    camandoNuevaPolitica.Transaction = transaction;
                    camandoNuevaPolitica.CommandText = sqlNuevaPolitica;
                    camandoNuevaPolitica.Parameters.AddWithValue("@id_politica", politica.Id);
                    camandoNuevaPolitica.Parameters.AddWithValue("@id_usuario", detalleUsuario.Id);
                    filasNuevaPolitica += camandoNuevaPolitica.ExecuteNonQuery();

                });


                var politicasParaEliminar = politicasUsuario.Where(pol => pol.PoliticaExistente && !pol.Seleccionada && !pol.PoliticaNueva ).ToList();

                politicasParaEliminar.ForEach(politica =>
                {
                    var commandoEliminarPolitica = MysqlConnection.CreateCommand();
                    commandoEliminarPolitica.Transaction = transaction;
                    commandoEliminarPolitica.CommandText = sqlEliminarPolitica;
                    commandoEliminarPolitica.Parameters.AddWithValue("@id_politica", politica.Id);
                    commandoEliminarPolitica.Parameters.AddWithValue("@id_usuario", detalleUsuario.Id);
                    filasEliminarPolitica += commandoEliminarPolitica.ExecuteNonQuery();

                });



                if (filasNuevaPolitica == politicasNuevas.Count && filasEliminarPolitica == politicasParaEliminar.Count)
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

        public bool InsertarUsuario(UsuarioParaInsertarDTO usuarioDTO, long identificacionTercero, int idRol)
        {
            string sqlUsuario = "insert into usuarios(email, password_hash,fecha_activacion) " +
                                 "values (@email,@password_hash,@fecha_activacion)";

            string sqlIdUsuario = "select id as id_usuario from usuarios where email=@email";

            string sqlTerceroUsuarios = "insert into tercero_usuarios(identificacion_tercero, id_usuario) " +
                                    "values (@identificacion_tercero, @id_usuario)";

            string sqlRolUsuario = "insert into usuario_roles(id_usuario,id_rol)" +
                                   "values(@id_usuario,@id_rol)";

            _conexion.Open();
            var MysqlConnection = _conexion;
            int filasNuevoUsuario = 0;
            int filasUsuarioTercero = 0;
            int filasRolUsuario = 0;
            long idUsuarioInsertado = 0;
            bool validacionTransaccion = false;
            using MySqlTransaction transaction = MysqlConnection.BeginTransaction();

            try
            {

                var comandoNuevaUsuario = MysqlConnection.CreateCommand();
                comandoNuevaUsuario.Transaction = transaction;
                comandoNuevaUsuario.CommandText = sqlUsuario;
                comandoNuevaUsuario.Parameters.AddWithValue("@email", usuarioDTO.Email);
                comandoNuevaUsuario.Parameters.AddWithValue("@password_hash", usuarioDTO.HashPassword);
                comandoNuevaUsuario.Parameters.AddWithValue("@fecha_activacion", DateOnly.FromDateTime(DateTime.Now).ToString("yyyy/MM/dd"));
                filasNuevoUsuario = comandoNuevaUsuario.ExecuteNonQuery();

                var comandoIdUsuarioInsertado = MysqlConnection.CreateCommand();
                comandoIdUsuarioInsertado.Transaction = transaction;
                comandoIdUsuarioInsertado.CommandText = sqlIdUsuario;
                comandoIdUsuarioInsertado.Parameters.AddWithValue("@email", usuarioDTO.Email);
                using MySqlDataReader reader = comandoIdUsuarioInsertado.ExecuteReader();
                while (reader.Read())
                {
                    idUsuarioInsertado = reader.GetInt64("id_usuario");
                }
                reader.Close();

                var comandoUsuarioTercero= MysqlConnection.CreateCommand();
                comandoUsuarioTercero.Transaction = transaction;
                comandoUsuarioTercero.CommandText = sqlTerceroUsuarios;
                comandoUsuarioTercero.Parameters.AddWithValue("@identificacion_tercero", identificacionTercero);
                comandoUsuarioTercero.Parameters.AddWithValue("@id_usuario", idUsuarioInsertado);
                filasUsuarioTercero = comandoUsuarioTercero.ExecuteNonQuery();

                var comandoRolUsuario = MysqlConnection.CreateCommand();
                comandoRolUsuario.Transaction = transaction;
                comandoRolUsuario.CommandText = sqlRolUsuario;
                comandoRolUsuario.Parameters.AddWithValue("@id_rol", idRol);
                comandoRolUsuario.Parameters.AddWithValue("@id_usuario", idUsuarioInsertado);
                filasRolUsuario = comandoRolUsuario.ExecuteNonQuery();


                if (filasNuevoUsuario == 1 && filasUsuarioTercero == 1 && filasRolUsuario == 1)
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
