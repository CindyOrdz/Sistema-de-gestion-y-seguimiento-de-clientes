using MySqlConnector;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.Terceros;

namespace EntidadesNegocio.ClasesDao.TercerosDAO
{
    public  class SedeDAO
    {
        private readonly MySqlConnection _conexion;
        public SedeDAO(MySqlConnection conexion)
        {
            _conexion = conexion;
        }

        public List<SedeInterfazGraficaTerceroDTO> ObtenerSedesDeTercero(long idEmpresa)
        {
            string sqlSedes = "select id, responsable, email1, email2, telefono, pais, pa.nombre as nombre_pais, departamento," +
                                      "dpto.nombre as nombre_departamento, municipio, ciu.nombre as nombre_ciudad,  direccion " +
                                      "from sedes inner join paises pa on pais = pa.codigo inner join departamentos dpto on departamento = dpto.codigo " +
                                      "inner join ciudades ciu on municipio = ciu.codigo where id_tercero = @id_empresa";
            var listadeSedes = new List<SedeInterfazGraficaTerceroDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand commandoSedes = new MySqlCommand(sqlSedes, _conexion);
                commandoSedes.Parameters.AddWithValue("@id_empresa", idEmpresa);
                using MySqlDataReader readersedes = commandoSedes.ExecuteReader();
                while (readersedes.Read())
                {
                    var sedeDTO = new SedeInterfazGraficaTerceroDTO
                    {
                        Id = readersedes.GetInt64("id"),
                        Responsable = readersedes.GetString("responsable"),
                        Email1 = readersedes.GetString("email1"),
                        Email2 = readersedes.GetString("email2"),
                        Telefono = readersedes.GetString("telefono"),
                        Ubicacion = new UbicacionInterfazGraficaVentaDTO
                        {
                            Pais = new PaisInterfazGraficaVentaDTO
                            {
                                Codigo = readersedes.GetString("pais"),
                                Nombre = readersedes.GetString("nombre_pais")
                            },
                            DepartamentoProvincia = new DepartamentoProvinciaInterfazGraficaVentaDTO
                            {
                                Codigo = readersedes.GetString("departamento"),
                                Nombre = readersedes.GetString("nombre_departamento")
                            },
                            Ciudad = new CiudadInterfazGraficaVentaDTO
                            {
                                Codigo = readersedes.GetString("municipio"),
                                Nombre = readersedes.GetString("nombre_ciudad")
                            }
                        },
                        Direccion = readersedes.GetString("direccion")
                    };
                    listadeSedes.Add(sedeDTO);
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
            

            return listadeSedes;
        }

        public int ActualizarSede(SedeInterfazGraficaTerceroDTO sedeDTO)
        {
            int filas = 0;
            string sql = "update sedes set responsable=@responsable, email1=@email1, email2=@email2," +
                    " telefono=@telefono, pais=@pais, departamento=@departamento, municipio=@municipio," +
                    " direccion=@direccion where id=@id_sede";
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@id_sede", sedeDTO.Id);
                command.Parameters.AddWithValue("@responsable", sedeDTO.Responsable);
                command.Parameters.AddWithValue("@email1", sedeDTO.Email1);
                command.Parameters.AddWithValue("@email2", sedeDTO.Email2);
                command.Parameters.AddWithValue("@telefono", sedeDTO.Telefono);
                command.Parameters.AddWithValue("@pais", sedeDTO.Ubicacion.Pais.Codigo);
                command.Parameters.AddWithValue("@departamento", sedeDTO.Ubicacion.DepartamentoProvincia.Codigo);
                command.Parameters.AddWithValue("@municipio", sedeDTO.Ubicacion.Ciudad.Codigo);
                command.Parameters.AddWithValue("@direccion", sedeDTO.Direccion);

                filas = command.ExecuteNonQuery();
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

            return filas;
        }


        public int InsertarSede(SedeInterfazGraficaTerceroDTO sedeDTO, long identificacionTercero)
        {
            int filas = 0;
            string sql = "insert into sedes(id_tercero, responsable, email1, email2, telefono, pais, departamento, municipio, direccion) " +
                " values(@id_tercero, @responsable, @email1, @email2, @telefono, @pais, @departamento, @municipio, @direccion)";
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@id_tercero", identificacionTercero);
                command.Parameters.AddWithValue("@id_sede", sedeDTO.Id);
                command.Parameters.AddWithValue("@responsable", sedeDTO.Responsable);
                command.Parameters.AddWithValue("@email1", sedeDTO.Email1);
                command.Parameters.AddWithValue("@email2", sedeDTO.Email2);
                command.Parameters.AddWithValue("@telefono", sedeDTO.Telefono);
                command.Parameters.AddWithValue("@pais", sedeDTO.Ubicacion.Pais.Codigo);
                command.Parameters.AddWithValue("@departamento", sedeDTO.Ubicacion.DepartamentoProvincia.Codigo);
                command.Parameters.AddWithValue("@municipio", sedeDTO.Ubicacion.Ciudad.Codigo);
                command.Parameters.AddWithValue("@direccion", sedeDTO.Direccion);

                filas = command.ExecuteNonQuery();
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

            return filas;
        }




    }
}
