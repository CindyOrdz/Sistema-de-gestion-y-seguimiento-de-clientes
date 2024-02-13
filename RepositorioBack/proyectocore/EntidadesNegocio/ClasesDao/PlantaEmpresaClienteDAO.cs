using EntidadesNegocio.InformacionVisita;
using EntidadesNegocio.EntidadesDto;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Data.Common;

namespace EntidadesNegocio.ClasesDao
{
    public class PlantaEmpresaClienteDAO
    {
        private readonly MySqlConnection conexion_;

        public PlantaEmpresaClienteDAO(MySqlConnection conexion)
        {
            conexion_ = conexion;
        }

        public async Task<List<AreasProceso>> ListarAreas(BigInteger id)
        {
            string query = "SELECT * FROM areasproceso WHERE id_plantaempresacliente = @id";

            List<AreasProceso> listAreas = new List<AreasProceso>();

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        AreasProceso area = new AreasProceso(
                            rdr.GetInt32(0),
                            rdr.GetString(1),
                            rdr.GetString(2),
                            rdr.GetString(3),
                            rdr.GetString(4),
                            rdr.GetString(5));

                        listAreas.Add(area);
                    }
                }

                return listAreas;
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

        public async Task BorrarAreas(BigInteger id)
        {
            string query = "DELETE FROM areasproceso WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al eliminar el área de proceso: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task EditarAreas(AreasProceso area)
        {
            string query = "UPDATE areasproceso SET nombre = @nombre, responsable = @responsable, telefonoContacto = @telefonoContacto, emailContacto = @emailContacto, descripcion = @descripcion WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", area.ObtenerId());
            cmd.Parameters.AddWithValue("@nombre", area.ObtenerNombre());
            cmd.Parameters.AddWithValue("@responsable", area.ObtenerResponsable());
            cmd.Parameters.AddWithValue("@telefonoContacto", area.ObtenerTelefonoContacto());
            cmd.Parameters.AddWithValue("@emailContacto", area.ObtenerEmailContacto());
            cmd.Parameters.AddWithValue("@descripcion", area.ObtenerDescripcion());
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al actualizar el área de proceso: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task AgregarArea(AreasProceso area)
        {
            string query = "INSERT INTO areasproceso (nombre, responsable, telefonoContacto, emailContacto, descripcion, id_plantaempresacliente) VALUES (@nombre, @responsable, @telefono, @email, @descripcion, @idPlanta)";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);

            cmd.Parameters.AddWithValue("@nombre", area.ObtenerNombre());
            cmd.Parameters.AddWithValue("@responsable", area.ObtenerResponsable());
            cmd.Parameters.AddWithValue("@telefono", area.ObtenerTelefonoContacto());
            cmd.Parameters.AddWithValue("@email", area.ObtenerEmailContacto());
            cmd.Parameters.AddWithValue("@descripcion", area.ObtenerDescripcion());
            cmd.Parameters.AddWithValue("@idPlanta",area.ObtenerIdPlanta());

            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al agregar el área de proceso: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task<AreasProceso> InformacionArea(BigInteger idArea, BigInteger idPlanta)
        {
            string query = "SELECT * FROM areasproceso WHERE id = @idArea and id_plantaempresacliente = @idPlanta";
            
            AreasProceso area = null;

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@idArea", idArea);
                cmd.Parameters.AddWithValue("@idPlanta", idPlanta);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    if (await rdr.ReadAsync())
                    {
                        area = new AreasProceso(
                            rdr.GetInt32(0),
                            rdr.GetString(1),
                            rdr.GetString(2),
                            rdr.GetString(3),
                            rdr.GetString(4),
                            rdr.GetString(5));
                    }
                }

                return area;
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

        //MÉTODOS PARA AGENDAMIENTO DE UNA VISITA A LA PLANTA

        public List<VisitaDTO> ListarVisitasAgendadas(BigInteger id,int paginaActual, int elementosPorPagina)
        {
            var limiteInferior = (paginaActual - 1) * elementosPorPagina;
            string query = "SELECT v.id,v.fechaCreacion,v.fechaInicio,v.fechaFinaliza,v.cantidadDias,v.contactoEmpresa, CONCAT(COALESCE(t.NOMBRE1, ''),' ', COALESCE(t.NOMBRE2, ''),' ', COALESCE(t.APELLIDO1, ''),' ', COALESCE(t.APELLIDO2, '')) as completo, v.idVentaAsociada, v.estado FROM visita v JOIN terceros t ON v.idResponsable = t.IDENTIFICACION WHERE v.idPlantaCliente = @id ORDER BY v.fechaCreacion DESC limit @limite_inferior,@elementos_pagina;";

            List<VisitaDTO> listAgendas = new List<VisitaDTO>();

            try
            {
                conexion_.Open();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@limite_inferior", limiteInferior);
                cmd.Parameters.AddWithValue("@elementos_pagina", elementosPorPagina);

                using (MySqlDataReader rdr = (MySqlDataReader) cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        VisitaDTO visita = new VisitaDTO();
                        visita.id =rdr.GetInt32(0);
                        visita.fechaCreacion= rdr.GetDateTime(1);
                        visita.fechaInicio = rdr.GetDateTime(2);
                        visita.fechaFinaliza = rdr.GetDateTime(3);
                        visita.cantidadDias= rdr.GetInt32(4);
                        visita.contactoEmpresa= rdr.GetString(5);
                        visita.nombreResponsable =  rdr.GetString(6);
                        visita.idVentaAsociada = rdr.IsDBNull(7) ? 0 : rdr.GetInt64(7);
                        visita.estado = rdr.GetString(8);

                        listAgendas.Add(visita);
                    }
                }

                return listAgendas;
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

        public int ObtenerTotalVisitasAgendadas(BigInteger idPlanta)
        {
            string sql = "SELECT COUNT(v.id) as cantidad FROM visita v WHERE v.idPlantaCliente = @idPlanta;";
            int CantidadDatos = 0;
            try
            {
                conexion_.Open();
                using MySqlCommand command = new MySqlCommand(sql, conexion_);
                command.Parameters.AddWithValue("@idPlanta", idPlanta);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadDatos = reader.GetInt32("cantidad");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
            return CantidadDatos;

        }

        public async Task BorrarVisitaAgendada(BigInteger id)
        {
            string query = "DELETE FROM visita WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al eliminar la visita: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task EditarVisitaAgendada(AgendaVisitaEmpresaCliente agenda)
        {
            string query = "UPDATE visita SET fechaInicio = @fechaInicio, fechaFinaliza = @fechaFinaliza, cantidadDias = @cantidadDias, contactoEmpresa = @contactoEmpresa, idResponsable = @responsable WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);
            cmd.Parameters.AddWithValue("@id", agenda.ObtenerId());
            cmd.Parameters.AddWithValue("@fechaInicio", agenda.ObtenerFechaInicio());
            cmd.Parameters.AddWithValue("@fechaFinaliza", agenda.ObtenerFechaFinaliza());
            cmd.Parameters.AddWithValue("@cantidadDias", agenda.ObtenerCantidadDias());
            cmd.Parameters.AddWithValue("@contactoEmpresa", agenda.ObtenerContactoEmpresa());
            cmd.Parameters.AddWithValue("@responsable", agenda.ObtenerIdResponsable());

            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al actualizar la visita: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }

        public async Task AgendarVisita(AgendaVisitaEmpresaCliente agenda, long idEmpresaServicio)
        {
            string query = "INSERT INTO visita (fechaCreacion, fechaInicio, fechaFinaliza, cantidadDias, contactoEmpresa,idEmpresaPrestaServicio, idPlantaCliente, idResponsable) VALUES (@fechaCreacion, @fechaInicio, @fechaFinaliza, @cantidadDias, @contactoEmpresa, @idEmpresaServicio,@idPlanta, @idResponsable)";
            MySqlCommand cmd = new MySqlCommand(query, conexion_);

            cmd.Parameters.AddWithValue("@fechaCreacion", DateTime.Now);
            cmd.Parameters.AddWithValue("@fechaInicio", agenda.ObtenerFechaInicio());
            cmd.Parameters.AddWithValue("@fechaFinaliza", agenda.ObtenerFechaFinaliza());
            cmd.Parameters.AddWithValue("@cantidadDias", agenda.ObtenerCantidadDias());
            cmd.Parameters.AddWithValue("@contactoEmpresa", agenda.ObtenerContactoEmpresa());
            cmd.Parameters.AddWithValue("@idEmpresaServicio", idEmpresaServicio); 
            cmd.Parameters.AddWithValue("@idPlanta", agenda.ObtenerIdPlanta());
            cmd.Parameters.AddWithValue("@idResponsable", agenda.ObtenerIdResponsable());

            try
            {
                await conexion_.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al agendar la visita: " + ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
        }


        public async Task<List<VisitaDTO>> InformacionVisitaPorFecha(DateOnly fecha,long idResponsable)
        {
            string query = "SELECT v.id, v.fechaCreacion, v.fechaInicio, v.fechaFinaliza, v.cantidadDias, v.contactoEmpresa,t.RAZON_SOCIAL, p.nombre, s.id, t.IDENTIFICACION,v.idPlantaCliente, v.idVentaAsociada, v.estado FROM visita v INNER JOIN plantaempresacliente p ON v.idPlantaCliente = p.id INNER JOIN sedes s ON p.id_sede = s.id INNER JOIN terceros t ON s.id_tercero = t.IDENTIFICACION WHERE DATE(v.fechaInicio) = @fecha AND v.idResponsable=@idResponsable order by v.fechaCreacion DESC";

            List<VisitaDTO> listVisitas = new List<VisitaDTO>();

            try
            {
                await conexion_.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@fecha", fecha.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@idResponsable", idResponsable);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        VisitaDTO visita = new VisitaDTO();
                        visita.id = rdr.GetInt32(0);
                        visita.fechaCreacion = rdr.GetDateTime(1);
                        visita.fechaInicio = rdr.GetDateTime(2);
                        visita.fechaFinaliza = rdr.GetDateTime(3);
                        visita.cantidadDias = rdr.GetInt32(4);
                        visita.contactoEmpresa = rdr.GetString(5);
                        visita.empresaCliente = rdr.GetString(6);
                        visita.plantaCliente = rdr.GetString(7);
                        visita.sedeCliente = rdr.GetInt64(8);
                        visita.idEmpresaCliente = rdr.GetDouble(9);
                        visita.idPlantaCliente = rdr.GetInt32(10);
                        visita.idVentaAsociada = rdr.IsDBNull(11) ? 0 : rdr.GetInt64(11);
                        visita.estado = rdr.GetString(12);

                        listVisitas.Add(visita);
                    }
                }

                return listVisitas;
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

        public List<VisitaDTO> InformacionVisitaPorEmpresa(String empresa, long idResponsable,int paginaActual, int elementosPorPagina)
        {
            var limiteInferior = (paginaActual - 1) * elementosPorPagina;
            string query = "SELECT v.id, v.fechaCreacion, v.fechaInicio, v.fechaFinaliza, v.cantidadDias, v.contactoEmpresa,t.RAZON_SOCIAL, p.nombre, s.id, t.IDENTIFICACION, v.idVentaAsociada,v.idPlantaCliente, v.estado FROM visita v INNER JOIN plantaempresacliente p ON v.idPlantaCliente = p.id INNER JOIN sedes s ON p.id_sede = s.id INNER JOIN terceros t ON s.id_tercero = t.IDENTIFICACION where t.RAZON_SOCIAL like @empresa AND v.idResponsable=@idResponsable order by v.fechaInicio DESC limit @limite_inferior,@elementos_pagina;";

            List<VisitaDTO> listVisitas = new List<VisitaDTO>();

            try
            {
                conexion_.Open();

                MySqlCommand cmd = new MySqlCommand(query, conexion_);
                cmd.Parameters.AddWithValue("@empresa", "%"+ empresa +"%");
                cmd.Parameters.AddWithValue("@idResponsable", idResponsable);
                cmd.Parameters.AddWithValue("@limite_inferior", limiteInferior);
                cmd.Parameters.AddWithValue("@elementos_pagina", elementosPorPagina);

                using (MySqlDataReader rdr = (MySqlDataReader) cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        VisitaDTO visita = new VisitaDTO();
                        visita.id = rdr.GetInt32(0);
                        visita.fechaCreacion = rdr.GetDateTime(1);
                        visita.fechaInicio = rdr.GetDateTime(2);
                        visita.fechaFinaliza = rdr.GetDateTime(3);
                        visita.cantidadDias = rdr.GetInt32(4);
                        visita.contactoEmpresa = rdr.GetString(5);
                        visita.empresaCliente = rdr.GetString(6);
                        visita.plantaCliente = rdr.GetString(7);
                        visita.sedeCliente =  rdr.GetInt64(8);
                        visita.idEmpresaCliente = rdr.GetDouble(9);
                        visita.idVentaAsociada = rdr.IsDBNull(10) ? 0 : rdr.GetInt64(10);
                        visita.idPlantaCliente = rdr.GetInt32(11);
                        visita.estado = rdr.GetString(12);

                        listVisitas.Add(visita);
                    }
                }

                return listVisitas;
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

        public int ObtenerTotalDatosVisitasPorEmpresa(String empresa, long idResponsable)
        {
            string sql = "SELECT COUNT(v.id) as cantidad FROM visita v INNER JOIN plantaempresacliente p ON v.idPlantaCliente = p.id INNER JOIN sedes s ON p.id_sede = s.id INNER JOIN terceros t ON s.id_tercero = t.IDENTIFICACION where t.RAZON_SOCIAL like @empresa AND v.idResponsable=@idResponsable order by v.fechaInicio DESC;";
            int CantidadDatos = 0;
            try
            {
                conexion_.Open();
                using MySqlCommand command = new MySqlCommand(sql, conexion_);
                command.Parameters.AddWithValue("@empresa", "%"+ empresa +"%");
                command.Parameters.AddWithValue("@idResponsable", idResponsable);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadDatos = reader.GetInt32("cantidad");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion_.Close();
            }
            return CantidadDatos;

        }
    }
}
