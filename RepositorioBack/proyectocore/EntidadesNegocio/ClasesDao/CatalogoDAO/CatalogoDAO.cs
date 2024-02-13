using System.Configuration;
using System.Text.RegularExpressions;
using EntidadesNegocio.ElementosInventario;
using EntidadesNegocio.InterfazGraficaBlazorDTO;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Inventario;
using MySqlConnector;
using System.Numerics;

namespace EntidadesNegocio.ClasesDao.CatalogoDAO
{
    public class CatalogoDAO
    {
        private readonly MySqlConnection _conexion;
        public CatalogoDAO(MySqlConnection conexion)
        {
            _conexion = conexion;
        }

        public int BuscarIdPorCatalogo(string clasificacion)
        {
            string sql = "SELECT id FROM catalogos WHERE clasificacion = @clasificacion;";
            int id = 0;

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@clasificacion", clasificacion);
                using MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    id = reader.GetInt32("id");
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

        public long ObtenerIdCatalogoSegunIdElemento(long id)
        {
            string query = $"SELECT id_catalogo FROM catalogos_elementos WHERE id_elemento = {id};";
            int id_catalogo = 0;

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(query, _conexion);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id_catalogo = reader.GetInt32("id_catalogo");
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
            return id_catalogo;
        }

        public List<CatalogoInterfazGraficaVentaDTO> ObtenerCatalogosPorEmpresa(long identificacionEmpresa)
        {
            string query = "SELECT id, idempresa, clasificacion, descripcion, url_imagen FROM catalogos " +
                "where idempresa= @idempresa";
            var Catalogos = new List<CatalogoInterfazGraficaVentaDTO>();

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(query, _conexion);
                command.Parameters.AddWithValue("@idempresa", identificacionEmpresa);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var catalogoDTO = new CatalogoInterfazGraficaVentaDTO
                    {
                        id = reader.GetInt32("id"),
                        idempresa = reader.GetDouble("idempresa"),
                        clasificacion = reader.GetString("clasificacion"),
                        descripcion = reader.GetString("descripcion"),
                        url_imagen = reader.GetString("url_imagen")

                    };

                    Catalogos.Add(catalogoDTO);

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
            return Catalogos;
        }

        public List<CatalogoInterfazGraficaVentaDTO> ObtenerCatalogos()
        {
            string query = "SELECT id, idempresa, clasificacion, descripcion, url_imagen FROM catalogos ";
            var Catalogos = new List<CatalogoInterfazGraficaVentaDTO>();

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(query, _conexion);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var catalogoDTO = new CatalogoInterfazGraficaVentaDTO
                    {
                        id = reader.GetInt32("id"),
                        idempresa = reader.GetDouble("idempresa"),
                        clasificacion = reader.GetString("clasificacion"),
                        descripcion = reader.GetString("descripcion"),
                        url_imagen = reader.GetString("url_imagen")

                    };

                    Catalogos.Add(catalogoDTO);

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
            return Catalogos;
        }

        public int ObtenerCantidadElementosSegunCatalogos(BigInteger idCatalogo) {
            string sql = "SELECT COUNT(*) as cantidad FROM catalogos_elementos where id_catalogo = @idcatalogo";
            int CantidadElementosSegunCatalogo = 0;
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@idcatalogo", idCatalogo);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadElementosSegunCatalogo = reader.GetInt32("cantidad");
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
            return CantidadElementosSegunCatalogo;
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosSegunCatalogos(BigInteger idCatalogo)
        {
            string sql = "SELECT el.codigo, el.idempresa, el.empresa, el.tipo_articulo, " +
                " el.nombre, el.ref1, el.ref2, el.ref3, el.ref4, el.ref5, el.sett, el.unidad, " +
                 "el.valor, el.tipo, el.cantidad, el.cantidad_disponible, el.cantidad_en_almacen ,el.cantidad_vendidos, " +
                 "el.ref11, el.ref12, el.ref13, el.ref14, el.ref15, el.ref16, el.iva, el.url_imagen, " +
                 " m.id as idmarca, m.marca FROM elementos el INNER JOIN marcas m ON el.marca = m.id " +
                " INNER JOIN catalogos_elementos ce ON ce.id_elemento = el.codigo " +
                  $"WHERE ce.id_catalogo = @idcatalogo";
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@idcatalogo", idCatalogo);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var elementoDTO = new ElementoInterfazGraficaVentaDTO
                    {
                        CodigoArticulo = reader.GetInt32("codigo"),
                        IdEmpresa = (long)reader.GetDouble("idempresa"),
                        Empresa = reader.GetString("empresa"),
                        TipoElemento = reader.GetString("tipo_articulo"),
                        Nombre = reader.GetString("nombre"),
                        IdMarca = reader.GetInt32("idmarca"),
                        Marca = reader.GetString("marca"),
                        Ref1 = reader.GetString("ref1"),
                        Ref2 = reader.GetString("ref2"),
                        Ref3 = reader.GetString("ref3"),
                        Ref4 = reader.GetString("ref4"),
                        Ref5 = reader.GetString("ref5"),
                        Sett = reader.GetString("sett"),
                        Unidad = new UnidadInterfazGraficaVentaDTO { Id = reader.GetString("unidad") },
                        Valor = reader.GetDouble("valor"),
                        Tipo = reader.GetString("tipo"),
                        CantidadDisponibleBodega = reader.GetDouble("cantidad"),
                        CantidadDisponibleAlmacen = reader.GetDouble("cantidad_en_almacen"),
                        CantidadVendidos = reader.GetDouble("cantidad_vendidos"),
                        Ref11 = reader.GetDouble("ref11"),
                        Ref12 = reader.GetDouble("ref12"),
                        Ref13 = reader.GetDouble("ref13"),
                        Ref14 = reader.GetDouble("ref14"),
                        Ref15 = reader.GetDouble("ref15"),
                        Ref16 = reader.GetDouble("ref16"),
                        PorcentajeIva = reader.GetDouble("iva"),
                        url_imagen = reader.GetString("url_imagen")
                    };

                    Elementos.Add(elementoDTO);

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

            return Elementos;
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosSegunCatalogosPaginado(BigInteger idCatalogo, int pag, int paginarPor) {
            var limiteInicial = (pag-1)*paginarPor;
            string sql = "SELECT el.codigo, el.idempresa, el.empresa, el.tipo_articulo, "+
                " el.nombre, el.ref1, el.ref2, el.ref3, el.ref4, el.ref5, el.sett, el.unidad, "+
                 "el.valor, el.tipo, el.cantidad, el.cantidad_disponible, el.cantidad_en_almacen ,el.cantidad_vendidos, "+
                 "el.ref11, el.ref12, el.ref13, el.ref14, el.ref15, el.ref16, el.iva, el.url_imagen, "+
                 " m.id as idmarca, m.marca FROM elementos el INNER JOIN marcas m ON el.marca = m.id "+
                " INNER JOIN catalogos_elementos ce ON ce.id_elemento = el.codigo "+
                  "WHERE ce.id_catalogo = @idcatalogo limit @limiteInicial, @paginarPor ";
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@idcatalogo", idCatalogo);
                command.Parameters.AddWithValue("@limiteInicial", limiteInicial);
                command.Parameters.AddWithValue("@paginarPor", paginarPor);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var elementoDTO = new ElementoInterfazGraficaVentaDTO
                    {
                        CodigoArticulo = reader.GetInt32("codigo"),
                        IdEmpresa = (long)reader.GetDouble("idempresa"),
                        Empresa = reader.GetString("empresa"),
                        TipoElemento = reader.GetString("tipo_articulo"),
                        Nombre = reader.GetString("nombre"),
                        IdMarca = reader.GetInt32("idmarca"),
                        Marca = reader.GetString("marca"),
                        Ref1 = reader.GetString("ref1"),
                        Ref2 = reader.GetString("ref2"),
                        Ref3 = reader.GetString("ref3"),
                        Ref4 = reader.GetString("ref4"),
                        Ref5 = reader.GetString("ref5"),
                        Sett = reader.GetString("sett"),
                        Unidad = new UnidadInterfazGraficaVentaDTO { Id = reader.GetString("unidad") },
                        Valor = reader.GetDouble("valor"),
                        Tipo = reader.GetString("tipo"),
                        CantidadDisponibleBodega = reader.GetDouble("cantidad"),
                        CantidadDisponibleAlmacen = reader.GetDouble("cantidad_en_almacen"),
                        CantidadVendidos = reader.GetDouble("cantidad_vendidos"),
                        Ref11 = reader.GetDouble("ref11"),
                        Ref12 = reader.GetDouble("ref12"),
                        Ref13 = reader.GetDouble("ref13"),
                        Ref14 = reader.GetDouble("ref14"),
                        Ref15 = reader.GetDouble("ref15"),
                        Ref16 = reader.GetDouble("ref16"),
                        PorcentajeIva = reader.GetDouble("iva"),
                        url_imagen = reader.GetString("url_imagen")
                    };

                    Elementos.Add(elementoDTO);

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

            return Elementos;
        }

        public List<CatalogoInterfazGraficaVentaDTO> BuscarCatalogoOElemento(string textoBuscar)
        {
            var Catalogos = new List<CatalogoInterfazGraficaVentaDTO>();
            foreach (var cat in ObtenerCatalogos()) {
                if (cat.clasificacion.ToLower().Contains(textoBuscar)) {
                    Catalogos.Add(cat);
                }
            }
            return Catalogos;
        }



        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorCatalogo(int id)
        {
            var elementosCatalogo = new List<ElementoInterfazGraficaVentaDTO>();
            foreach (var cat in ObtenerCatalogos())
            {
                if (cat.id == id)
                {
                    elementosCatalogo = cat._elementos;
                }
            }
            return elementosCatalogo;
        }

        public async Task InsertarCatalogo(CatalogoInterfazGraficaVentaDTO catalogo) {
            string query = "INSERT INTO catalogos (id, idempresa, clasificacion, descripcion,url_imagen) VALUES (@id, @idempresa, @clasificacion, @descripcion, @url_imagen)";
            MySqlCommand cmd = new MySqlCommand(query, _conexion);

            cmd.Parameters.AddWithValue("@id", catalogo.id);
            cmd.Parameters.AddWithValue("@idempresa", catalogo.idempresa);
            cmd.Parameters.AddWithValue("@clasificacion", catalogo.clasificacion);
            cmd.Parameters.AddWithValue("@descripcion", catalogo.descripcion);
            cmd.Parameters.AddWithValue("@url_imagen", catalogo.url_imagen);
            try
            {
                await _conexion.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al agregar el catálogo: " + ex.Message);
            }
            finally
            {
                _conexion.Close();
            }
        }






    }
}
