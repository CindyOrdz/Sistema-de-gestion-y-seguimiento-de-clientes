using EntidadesNegocio.ElementosInventario;
using EntidadesNegocio.InterfazGraficaBlazorDTO;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Inventario;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.Pagos;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Venta;
using EntidadesNegocio.Terceros;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Xml.Linq;

namespace EntidadesNegocio.ClasesDao.ElementosDAO
{
    public class ElementoDAO
    {
        private readonly MySqlConnection _conexion;

        public ElementoDAO(MySqlConnection conexion)
        {
            _conexion = conexion;
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorTodasLasOpcionesPaginado(string ref1, string ref2, string ref3, string ref4, string ref5, int codigo, string nombre, string sett, string marca, long identificacionEmpresa, int pag, int paginarPor)
        {
            var limiteInicial = (pag - 1) * paginarPor;
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();
            if (ref1 == string.Empty && ref2 == string.Empty && ref3 == string.Empty && ref4 == string.Empty && ref5 == string.Empty && codigo == 0 && nombre == string.Empty && sett == string.Empty && marca == string.Empty)
            {
                return Elementos;
            }
            bool bandera = false;
            string sql = "SELECT el.codigo, el.idempresa, el.empresa, el.tipo_articulo, " +
                "el.nombre, el.ref1, el.ref2, el.ref3, el.ref4, el.ref5, el.sett, el.unidad, " +
                "el.valor, el.tipo, el.cantidad, el.cantidad_disponible, el.cantidad_en_almacen ,el.cantidad_vendidos, " +
                "el.ref11, el.ref12, el.ref13, el.ref14, el.ref15, el.ref16, el.iva, el.url_imagen, " +
                "m.id as idmarca, m.marca FROM elementos el INNER JOIN marcas m ON el.marca = m.id WHERE";
            if (ref1 != string.Empty)
            {

                sql += $" ref1 LIKE '%{ref1}%'";
                bandera = true;
            }
            if (ref2 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref2 LIKE '%{ref2}%'";
                }
                else
                {
                    sql += $" ref2 LIKE '%{ref2}%'";
                    bandera = true;
                }
            }
            if (ref3 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref3 LIKE '%{ref3}%'";
                }
                else
                {
                    sql += $" ref3 LIKE '%{ref3}%'";
                    bandera = true;
                }
            }
            if (ref4 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref4 LIKE '%{ref4}%'";
                }
                else
                {
                    sql += $" ref4 LIKE '%{ref4}%'";
                    bandera = true;
                }
            }
            if (ref5 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref5 LIKE '%{ref5}%'";
                }
                else
                {
                    sql += $" ref5 LIKE '%{ref5}%'";
                    bandera = true;
                }
            }
            if (codigo != 0)
            {
                if (bandera)
                {
                    sql += $" AND codigo LIKE '%{codigo}%'";
                }
                else
                {
                    sql += $" codigo LIKE '%{codigo}%'";
                    bandera = true;
                }
            }
            if (nombre != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND nombre LIKE '%{nombre}%'";
                }
                else
                {
                    sql += $" nombre LIKE '%{nombre}%'";
                    bandera = true;
                }
            }
            if (sett != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND sett LIKE '%{sett}%'";
                }
                else
                {
                    sql += $" sett LIKE '%{sett}%'";
                    bandera = true;
                }
            }
            if (marca != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND m.marca LIKE '%{marca}%'";
                }
                else
                {
                    sql += $" m.marca LIKE '%{marca}%'";
                }
            }
            sql += " AND el.idempresa = @identificacionEmpresa  limit @limiteInicial, @paginarPor";
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                                command.Parameters.AddWithValue("@identificacionEmpresa", identificacionEmpresa);
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

        public int ObtenerCantidadElementosPorTodasLasOpcionesPaginado(string ref1, string ref2, string ref3, string ref4, string ref5, int codigo, string nombre, string sett, string marca, long identificacionEmpresa)
        {
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();
            if (ref1 == string.Empty && ref2 == string.Empty && ref3 == string.Empty && ref4 == string.Empty && ref5 == string.Empty && codigo == 0 && nombre == string.Empty && sett == string.Empty && marca == string.Empty)
            {
                return 0;
            }
            bool bandera = false;
            string sql = "SELECT COUNT(*) as cantidad FROM elementos ele INNER JOIN marcas m ON m.id = ele.marca WHERE";
            int CantidadElementosEmpresa = 0;
            if (ref1 != string.Empty)
            {

                sql += $" ele.ref1 LIKE '%{ref1}%'";
                bandera = true;
            }
            if (ref2 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ele.ref2 LIKE '%{ref2}%'";
                }
                else
                {
                    sql += $" ele.ref2 LIKE '%{ref2}%'";
                    bandera = true;
                }
            }
            if (ref3 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ele.ref3 LIKE '%{ref3}%'";
                }
                else
                {
                    sql += $" ele.ref3 LIKE '%{ref3}%'";
                    bandera = true;
                }
            }
            if (ref4 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ele.ref4 LIKE '%{ref4}%'";
                }
                else
                {
                    sql += $" ele.ref4 LIKE '%{ref4}%'";
                    bandera = true;
                }
            }
            if (ref5 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ele.ref5 LIKE '%{ref5}%'";
                }
                else
                {
                    sql += $" ele.ref5 LIKE '%{ref5}%'";
                    bandera = true;
                }
            }
            if (codigo != 0)
            {
                if (bandera)
                {
                    sql += $" AND ele.codigo LIKE '%{codigo}%'";
                }
                else
                {
                    sql += $" ele.codigo LIKE '%{codigo}%'";
                    bandera = true;
                }
            }
            if (nombre != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ele.nombre LIKE '%{nombre}%'";
                }
                else
                {
                    sql += $" ele.nombre LIKE '%{nombre}%'";
                    bandera = true;
                }
            }
            if (sett != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ele.sett LIKE '%{sett}%'";
                }
                else
                {
                    sql += $" ele.sett LIKE '%{sett}%'";
                    bandera = true;
                }
            }
            if (marca != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND m.marca LIKE '%{marca}%'";
                }
                else
                {
                    sql += $" m.marca LIKE '%{marca}%'";
                }
            }
            sql += $" AND ele.idempresa = {identificacionEmpresa} ";
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadElementosEmpresa = reader.GetInt32("cantidad");
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

            return CantidadElementosEmpresa;
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorNombreYReferenciasPaginado(string ref1, string ref2, string ref3, string ref4, string ref5, string nombre, long identificacionEmpresa, int pag, int paginarPor)
        {
            var limiteInicial = (pag - 1) * paginarPor;
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();
            if (ref1 == string.Empty && ref2 == string.Empty && ref3 == string.Empty && ref4 == string.Empty && ref5 == string.Empty && nombre == string.Empty)
            {
                return Elementos;
            }
            bool bandera = false;
            string sql = "SELECT el.codigo, el.idempresa, el.empresa, el.tipo_articulo, " +
                "el.nombre, el.ref1, el.ref2, el.ref3, el.ref4, el.ref5, el.sett, el.unidad, " +
                "el.valor, el.tipo, el.cantidad, el.cantidad_disponible, el.cantidad_en_almacen ,el.cantidad_vendidos, " +
                "el.ref11, el.ref12, el.ref13, el.ref14, el.ref15, el.ref16, el.iva, el.url_imagen, " +
                "m.id as idmarca, m.marca FROM elementos el INNER JOIN marcas m ON el.marca = m.id WHERE";
            if (ref1 != string.Empty)
            {

                sql += $" ref1 LIKE '%{ref1}%'";
                bandera = true;
            }
            if (ref2 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref2 LIKE '%{ref2}%'";
                }
                else
                {
                    sql += $" ref2 LIKE '%{ref2}%'";
                    bandera = true;
                }
            }
            if (ref3 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref3 LIKE '%{ref3}%'";
                }
                else
                {
                    sql += $" ref3 LIKE '%{ref3}%'";
                    bandera = true;
                }
            }
            if (ref4 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref4 LIKE '%{ref4}%'";
                }
                else
                {
                    sql += $" ref4 LIKE '%{ref4}%'";
                    bandera = true;
                }
            }
            if (ref5 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref5 LIKE '%{ref5}%'";
                }
                else
                {
                    sql += $" ref5 LIKE '%{ref5}%'";
                    bandera = true;
                }
            }

            if (nombre != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND nombre LIKE '%{nombre}%'";
                }
                else
                {
                    sql += $" nombre LIKE '%{nombre}%'";
                    bandera = true;
                }
            }
            sql += " AND el.idempresa = @identificacionEmpresa  limit @limiteInicial, @paginarPor";
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@identificacionEmpresa", identificacionEmpresa);
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

        public int ObtenerCantidadElementosPorNombreYReferenciasPaginado(string ref1, string ref2, string ref3, string ref4, string ref5, string nombre, long identificacionEmpresa)
        {
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();
            if (ref1 == string.Empty && ref2 == string.Empty && ref3 == string.Empty && ref4 == string.Empty && ref5 == string.Empty && nombre == string.Empty)
            {
                return 0;
            }
            bool bandera = false;
            string sql = "SELECT COUNT(*) as cantidad FROM elementos WHERE";
            int CantidadElementosEmpresa = 0;
            if (ref1 != string.Empty)
            {

                sql += $" ref1 LIKE '%{ref1}%'";
                bandera = true;
            }
            if (ref2 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref2 LIKE '%{ref2}%'";
                }
                else
                {
                    sql += $" ref2 LIKE '%{ref2}%'";
                    bandera = true;
                }
            }
            if (ref3 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref3 LIKE '%{ref3}%'";
                }
                else
                {
                    sql += $" ref3 LIKE '%{ref3}%'";
                    bandera = true;
                }
            }
            if (ref4 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref4 LIKE '%{ref4}%'";
                }
                else
                {
                    sql += $" ref4 LIKE '%{ref4}%'";
                    bandera = true;
                }
            }
            if (ref5 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref5 LIKE '%{ref5}%'";
                }
                else
                {
                    sql += $" ref5 LIKE '%{ref5}%'";
                    bandera = true;
                }
            }

            if (nombre != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND nombre LIKE '%{nombre}%'";
                }
                else
                {
                    sql += $" nombre LIKE '%{nombre}%'";
                    bandera = true;
                }
            }
            sql += $" AND idempresa = {identificacionEmpresa} ";
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadElementosEmpresa = reader.GetInt32("cantidad");
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

            return CantidadElementosEmpresa;
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorReferenciaPaginado(String ref1, String ref2, String ref3, String ref4, String ref5, long identificacionEmpresa, int pag, int paginarPor)
        {
            var limiteInicial = (pag - 1) * paginarPor;
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();
            if (ref1 == string.Empty && ref2 == string.Empty && ref3 == string.Empty && ref4 == string.Empty && ref5 == string.Empty)
            {
                return Elementos;
            }
            bool bandera = false;
            string sql = "SELECT el.codigo, el.idempresa, el.empresa, el.tipo_articulo, " +
                "el.nombre, el.ref1, el.ref2, el.ref3, el.ref4, el.ref5, el.sett, el.unidad, " +
                "el.valor, el.tipo, el.cantidad, el.cantidad_disponible, el.cantidad_en_almacen ,el.cantidad_vendidos, " +
                "el.ref11, el.ref12, el.ref13, el.ref14, el.ref15, el.ref16, el.iva, el.url_imagen, " +
                "m.id as idmarca, m.marca FROM elementos el INNER JOIN marcas m ON el.marca = m.id WHERE";
            if (ref1 != string.Empty)
            {

                sql += $" ref1 LIKE '%{ref1}%'";
                bandera = true;
            }
            if (ref2 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref2 LIKE '%{ref2}%'";
                }
                else
                {
                    sql += $" ref2 LIKE '%{ref2}%'";
                    bandera = true;
                }
            }
            if (ref3 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref3 LIKE '%{ref3}%'";
                }
                else
                {
                    sql += $" ref3 LIKE '%{ref3}%'";
                    bandera = true;
                }
            }
            if (ref4 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref4 LIKE '%{ref4}%'";
                }
                else
                {
                    sql += $" ref4 LIKE '%{ref4}%'";
                    bandera = true;
                }
            }
            if (ref5 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref5 LIKE '%{ref5}%'";
                }
                else
                {
                    sql += $" ref5 LIKE '%{ref5}%'";
                }
            }

            sql += " AND el.idempresa = @identificacionEmpresa  limit @limiteInicial, @paginarPor";
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@identificacionEmpresa", identificacionEmpresa);
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

        public int ObtenerCantidadElementosPorReferenciaPaginado(String ref1, String ref2, String ref3, String ref4, String ref5, long identificacionEmpresa)
        {
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();
            if (ref1 == string.Empty && ref2 == string.Empty && ref3 == string.Empty && ref4 == string.Empty && ref5 == string.Empty)
            {
                return 0;
            }
            bool bandera = false;
            string sql = "SELECT COUNT(*) as cantidad FROM elementos WHERE"; 
            int CantidadElementosEmpresa = 0;
            if (ref1 != string.Empty)
            {

                sql += $" ref1 LIKE '%{ref1}%'";
                bandera = true;
            }
            if (ref2 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref2 LIKE '%{ref2}%'";
                }
                else
                {
                    sql += $" ref2 LIKE '%{ref2}%'";
                    bandera = true;
                }
            }
            if (ref3 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref3 LIKE '%{ref3}%'";
                }
                else
                {
                    sql += $" ref3 LIKE '%{ref3}%'";
                    bandera = true;
                }
            }
            if (ref4 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref4 LIKE '%{ref4}%'";
                }
                else
                {
                    sql += $" ref4 LIKE '%{ref4}%'";
                    bandera = true;
                }
            }
            if (ref5 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref5 LIKE '%{ref5}%'";
                }
                else
                {
                    sql += $" ref5 LIKE '%{ref5}%'";
                }
            }

            sql += $" AND idempresa = {identificacionEmpresa} ";
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                        CantidadElementosEmpresa = reader.GetInt32("cantidad");
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

            return CantidadElementosEmpresa;
        }


        public int ObtenerCantidadElementosTotalesPorMarcaPaginado(long idempresa, string marca)
        {
            string sql = "SELECT COUNT(*) as cantidad FROM elementos el INNER JOIN marcas m " +
                "ON el.marca = m.id INNER JOIN catalogos_elementos as ce ON ce.id_elemento = el.codigo " +
                $"WHERE el.idempresa = @idempresa AND m.marca LIKE '%{marca}%'";
            int CantidadElementosEmpresa = 0;
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@idempresa", idempresa);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadElementosEmpresa = reader.GetInt32("cantidad");
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
            return CantidadElementosEmpresa;
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorMarcaPaginado(string marca, long identificacionEmpresa, int pag, int paginarPor)
        {
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();
            var limiteInicial = (pag - 1) * paginarPor;
            string sql = "SELECT el.codigo, el.idempresa, el.empresa, el.tipo_articulo, el.nombre, " +
                "el.ref1, el.ref2, el.ref3, el.ref4, el.ref5, el.sett, el.unidad,  el.valor, el.tipo, " +
                "el.cantidad, el.cantidad_disponible, el.cantidad_en_almacen ,el.cantidad_vendidos,  " +
                "el.ref11, el.ref12, el.ref13, el.ref14, el.ref15, el.ref16, el.iva, el.url_imagen,  " +
                "m.id as idmarca, m.marca, ce.id_catalogo FROM elementos el INNER JOIN marcas m " +
                "ON el.marca = m.id INNER JOIN catalogos_elementos as ce ON ce.id_elemento = el.codigo " +
                $"WHERE el.idempresa = @identificacionEmpresa AND m.marca LIKE '%{marca}%' limit @limiteInicial, @paginarPor ";

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@identificacionEmpresa", identificacionEmpresa);
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
                        url_imagen = reader.GetString("url_imagen"),
                        IdCatalogo = reader.GetInt64("id_catalogo")
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

        public int ObtenerCantidadElementosTotalesPorSettPaginado(long idempresa, string sett)
        {
            string sql = "SELECT COUNT(*) as cantidad FROM elementos el INNER JOIN marcas m " +
                "ON el.marca = m.id INNER JOIN catalogos_elementos as ce ON ce.id_elemento = el.codigo " +
                $"WHERE el.idempresa = @idempresa AND el.sett LIKE '%{sett}%'";
            int CantidadElementosEmpresa = 0;
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@idempresa", idempresa);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadElementosEmpresa = reader.GetInt32("cantidad");
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
            return CantidadElementosEmpresa;
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorSettPaginado(string sett, long identificacionEmpresa, int pag, int paginarPor)
        {
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();
            var limiteInicial = (pag - 1) * paginarPor;
            string sql = "SELECT el.codigo, el.idempresa, el.empresa, el.tipo_articulo, el.nombre, " +
                "el.ref1, el.ref2, el.ref3, el.ref4, el.ref5, el.sett, el.unidad,  el.valor, el.tipo, " +
                "el.cantidad, el.cantidad_disponible, el.cantidad_en_almacen ,el.cantidad_vendidos,  " +
                "el.ref11, el.ref12, el.ref13, el.ref14, el.ref15, el.ref16, el.iva, el.url_imagen,  " +
                "m.id as idmarca, m.marca, ce.id_catalogo FROM elementos el INNER JOIN marcas m " +
                "ON el.marca = m.id INNER JOIN catalogos_elementos as ce ON ce.id_elemento = el.codigo " +
                $"WHERE el.idempresa = @identificacionEmpresa AND el.sett LIKE '%{sett}%' limit @limiteInicial, @paginarPor ";

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@identificacionEmpresa", identificacionEmpresa);
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
                        url_imagen = reader.GetString("url_imagen"),
                        IdCatalogo = reader.GetInt64("id_catalogo")
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

        public int ObtenerCantidadElementosTotalesPorCodigoPaginado(long idempresa, int codigo)
        {
            string sql = "SELECT COUNT(*) as cantidad FROM elementos el INNER JOIN marcas m " +
                "ON el.marca = m.id INNER JOIN catalogos_elementos as ce ON ce.id_elemento = el.codigo " +
                $"WHERE el.idempresa = @idempresa AND el.codigo LIKE '%{codigo}%'";
            int CantidadElementosEmpresa = 0;
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@idempresa", idempresa);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadElementosEmpresa = reader.GetInt32("cantidad");
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
            return CantidadElementosEmpresa;
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorCodigoPaginado(int codigo, long identificacionEmpresa, int pag, int paginarPor)
        {
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();
            var limiteInicial = (pag - 1) * paginarPor;
            string sql = "SELECT el.codigo, el.idempresa, el.empresa, el.tipo_articulo, el.nombre, " +
                "el.ref1, el.ref2, el.ref3, el.ref4, el.ref5, el.sett, el.unidad,  el.valor, el.tipo, " +
                "el.cantidad, el.cantidad_disponible, el.cantidad_en_almacen ,el.cantidad_vendidos,  " +
                "el.ref11, el.ref12, el.ref13, el.ref14, el.ref15, el.ref16, el.iva, el.url_imagen,  " +
                "m.id as idmarca, m.marca, ce.id_catalogo FROM elementos el INNER JOIN marcas m " +
                "ON el.marca = m.id INNER JOIN catalogos_elementos as ce ON ce.id_elemento = el.codigo " +
                $"WHERE el.idempresa = @identificacionEmpresa AND el.codigo LIKE '%{codigo}%' limit @limiteInicial, @paginarPor ";

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@identificacionEmpresa", identificacionEmpresa);
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
                        url_imagen = reader.GetString("url_imagen"),
                        IdCatalogo = reader.GetInt64("id_catalogo")
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

        public int ObtenerCantidadElementosTotalesPorNombrePaginado(long idempresa, string nombre)
        {
            string sql = "SELECT COUNT(*) as cantidad FROM elementos el INNER JOIN marcas m " +
                "ON el.marca = m.id INNER JOIN catalogos_elementos as ce ON ce.id_elemento = el.codigo " +
                $"WHERE el.idempresa = @idempresa AND el.nombre LIKE '%{nombre}%'";
            int CantidadElementosEmpresa = 0;
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@idempresa", idempresa);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadElementosEmpresa = reader.GetInt32("cantidad");
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
            return CantidadElementosEmpresa;
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorNombrePaginado(string nombre, long identificacionEmpresa, int pag, int paginarPor)
        {
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();
            var limiteInicial = (pag - 1) * paginarPor;
            string sql = "SELECT el.codigo, el.idempresa, el.empresa, el.tipo_articulo, el.nombre, " +
                "el.ref1, el.ref2, el.ref3, el.ref4, el.ref5, el.sett, el.unidad,  el.valor, el.tipo, " +
                "el.cantidad, el.cantidad_disponible, el.cantidad_en_almacen ,el.cantidad_vendidos,  " +
                "el.ref11, el.ref12, el.ref13, el.ref14, el.ref15, el.ref16, el.iva, el.url_imagen,  " +
                "m.id as idmarca, m.marca, ce.id_catalogo FROM elementos el INNER JOIN marcas m " +
                "ON el.marca = m.id INNER JOIN catalogos_elementos as ce ON ce.id_elemento = el.codigo " +
                $"WHERE el.idempresa = @identificacionEmpresa AND el.nombre LIKE '%{nombre}%' limit @limiteInicial, @paginarPor ";
           
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@identificacionEmpresa", identificacionEmpresa);
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
                        url_imagen = reader.GetString("url_imagen"),
                        IdCatalogo = reader.GetInt64("id_catalogo")
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

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPaginado(long identificacionEmpresa, int pag, int paginarPor)
        {
            var limiteInicial = (pag - 1) * paginarPor;
            string sql = "SELECT el.codigo, el.idempresa, el.empresa, el.tipo_articulo, el.nombre, " +
                "el.ref1, el.ref2, el.ref3, el.ref4, el.ref5, el.sett, el.unidad,  el.valor, el.tipo, " +
                "el.cantidad, el.cantidad_disponible, el.cantidad_en_almacen ,el.cantidad_vendidos,  " +
                "el.ref11, el.ref12, el.ref13, el.ref14, el.ref15, el.ref16, el.iva, el.url_imagen,  " +
                "m.id as idmarca, m.marca, ce.id_catalogo FROM elementos el INNER JOIN marcas m " +
                "ON el.marca = m.id INNER JOIN catalogos_elementos as ce ON ce.id_elemento = el.codigo " +
                "WHERE el.idempresa = @identificacionEmpresa limit @limiteInicial, @paginarPor ";
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@identificacionEmpresa", identificacionEmpresa);
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
                        url_imagen = reader.GetString("url_imagen"),
                        IdCatalogo = reader.GetInt64("id_catalogo")
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

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementos(long identificacionEmpresa)
        {
            string sql = "SELECT el.codigo, el.idempresa, el.empresa, el.tipo_articulo, el.nombre, " +
                "el.ref1, el.ref2, el.ref3, el.ref4, el.ref5, el.sett, el.unidad,  el.valor, el.tipo, " +
                "el.cantidad, el.cantidad_disponible, el.cantidad_en_almacen ,el.cantidad_vendidos,  " +
                "el.ref11, el.ref12, el.ref13, el.ref14, el.ref15, el.ref16, el.iva, el.url_imagen,  " +
                "m.id as idmarca, m.marca, ce.id_catalogo FROM elementos el INNER JOIN marcas m " +
                "ON el.marca = m.id INNER JOIN catalogos_elementos as ce ON ce.id_elemento = el.codigo " +
                "WHERE el.idempresa = @identificacionEmpresa";
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@identificacionEmpresa", identificacionEmpresa);
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
                        url_imagen = reader.GetString("url_imagen"),
                        IdCatalogo = reader.GetInt64("id_catalogo")
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

        public ElementoInterfazGraficaVentaDTO ObtenerElementoEmpresaPorCodigo(long codigoElemento,long identificacionEmpresa)
        {
            string sql = "SELECT el.codigo, el.idempresa, el.empresa, el.tipo_articulo, el.nombre, " +
                "el.ref1, el.ref2, el.ref3, el.ref4, el.ref5, el.sett, el.unidad,  el.valor, el.tipo, " +
                "el.cantidad, el.cantidad_disponible, el.cantidad_en_almacen ,el.cantidad_vendidos,  " +
                "el.ref11, el.ref12, el.ref13, el.ref14, el.ref15, el.ref16, el.iva, el.url_imagen,  " +
                "m.id as idmarca, m.marca, ce.id_catalogo FROM elementos el INNER JOIN marcas m " +
                "ON el.marca = m.id INNER JOIN catalogos_elementos as ce ON ce.id_elemento = el.codigo " +
                "WHERE el.idempresa = @identificacionEmpresa and el.codigo=@codigo";
            var ElementoDTO = new ElementoInterfazGraficaVentaDTO();

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@identificacionEmpresa", identificacionEmpresa);
                command.Parameters.AddWithValue("@codigo", codigoElemento);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ElementoDTO.CodigoArticulo = reader.GetInt64("codigo");
                    ElementoDTO.IdEmpresa = (long)reader.GetDouble("idempresa");
                    ElementoDTO.Empresa = reader.GetString("empresa");
                    ElementoDTO.TipoElemento = reader.GetString("tipo_articulo");
                    ElementoDTO.Nombre = reader.GetString("nombre");
                    ElementoDTO.IdMarca = reader.GetInt32("idmarca");
                    ElementoDTO.Marca = reader.GetString("marca");
                    ElementoDTO.Ref1 = reader.GetString("ref1");
                    ElementoDTO.Ref2 = reader.GetString("ref2");
                    ElementoDTO.Ref3 = reader.GetString("ref3");
                    ElementoDTO.Ref4 = reader.GetString("ref4");
                    ElementoDTO.Ref5 = reader.GetString("ref5");
                    ElementoDTO.Sett = reader.GetString("sett");
                    ElementoDTO.Unidad = new UnidadInterfazGraficaVentaDTO { Id = reader.GetString("unidad") };
                    ElementoDTO.Valor = reader.GetDouble("valor");
                    ElementoDTO.Tipo = reader.GetString("tipo");
                    ElementoDTO.CantidadDisponibleBodega = reader.GetDouble("cantidad");
                    ElementoDTO.CantidadDisponibleAlmacen = reader.GetDouble("cantidad_en_almacen");
                    ElementoDTO.CantidadVendidos = reader.GetDouble("cantidad_vendidos");
                    ElementoDTO.Ref11 = reader.GetDouble("ref11");
                    ElementoDTO.Ref12 = reader.GetDouble("ref12");
                    ElementoDTO.Ref13 = reader.GetDouble("ref13");
                    ElementoDTO.Ref14 = reader.GetDouble("ref14");
                    ElementoDTO.Ref15 = reader.GetDouble("ref15");
                    ElementoDTO.Ref16 = reader.GetDouble("ref16");
                    ElementoDTO.PorcentajeIva = reader.GetDouble("iva");
                    ElementoDTO.url_imagen = reader.GetString("url_imagen");
                    ElementoDTO.IdCatalogo = reader.GetInt64("id_catalogo");


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

            if (ElementoDTO.CodigoArticulo == 0) return null;
            
            return ElementoDTO;
        }


        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosCarrusel()
        {
            string sql = "SELECT el.codigo, el.idempresa, el.empresa, el.tipo_articulo, el.nombre, " +
                "el.ref1, el.ref2, el.ref3, el.ref4, el.ref5, el.sett, el.unidad,  el.valor, el.tipo, " +
                "el.cantidad, el.cantidad_disponible, el.cantidad_en_almacen ,el.cantidad_vendidos,  " +
                "el.ref11, el.ref12, el.ref13, el.ref14, el.ref15, el.ref16, el.iva, el.url_imagen,  " +
                "m.id as idmarca, m.marca, ce.id_catalogo FROM elementos el INNER JOIN marcas m " +
                "ON el.marca = m.id INNER JOIN catalogos_elementos as ce ON ce.id_elemento = el.codigo ";
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
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
                        url_imagen = reader.GetString("url_imagen"),
                        IdCatalogo = reader.GetInt64("id_catalogo")
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

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorReferencia(String ref1, String ref2, String ref3, String ref4, String ref5, long identificacionEmpresa)
        {
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();
            if (ref1 == string.Empty && ref2 == string.Empty && ref3 == string.Empty && ref4 == string.Empty && ref5 == string.Empty)
            {
                return Elementos;
            }
            bool bandera = false;
            string sql = "SELECT el.codigo, el.idempresa, el.empresa, el.tipo_articulo, " +
                "el.nombre, el.ref1, el.ref2, el.ref3, el.ref4, el.ref5, el.sett, el.unidad, " +
                "el.valor, el.tipo, el.cantidad, el.cantidad_disponible, el.cantidad_en_almacen ,el.cantidad_vendidos, " +
                "el.ref11, el.ref12, el.ref13, el.ref14, el.ref15, el.ref16, el.iva, el.url_imagen, " +
                "m.id as idmarca, m.marca FROM elementos el INNER JOIN marcas m ON el.marca = m.id WHERE";
            if (ref1 != string.Empty)
            {

                sql += $" ref1 LIKE '%{ref1}%'";
                bandera = true;
            }
            if (ref2 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref2 LIKE '%{ref2}%'";
                }
                else
                {
                    sql += $" ref2 LIKE '%{ref2}%'";
                    bandera = true;
                }
            }
            if (ref3 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref3 LIKE '%{ref3}%'";
                }
                else
                {
                    sql += $" ref3 LIKE '%{ref3}%'";
                    bandera = true;
                }
            }
            if (ref4 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref4 LIKE '%{ref4}%'";
                }
                else
                {
                    sql += $" ref4 LIKE '%{ref4}%'";
                    bandera = true;
                }
            }
            if (ref5 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref5 LIKE '%{ref5}%'";
                }
                else
                {
                    sql += $" ref5 LIKE '%{ref5}%'";
                }
            }
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
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

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorTodasLasOpciones(string ref1, string ref2, string ref3, string ref4, string ref5, int codigo, string nombre, string sett, string marca, long identificacionEmpresa)
        {
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();
            if (ref1 == string.Empty && ref2 == string.Empty && ref3 == string.Empty && ref4 == string.Empty && ref5 == string.Empty && codigo == 0 && nombre == string.Empty && sett == string.Empty && marca == string.Empty)
            {
                return Elementos;
            }
            bool bandera = false;
            string sql = "SELECT el.codigo, el.idempresa, el.empresa, el.tipo_articulo, " +
                "el.nombre, el.ref1, el.ref2, el.ref3, el.ref4, el.ref5, el.sett, el.unidad, " +
                "el.valor, el.tipo, el.cantidad, el.cantidad_disponible, el.cantidad_en_almacen ,el.cantidad_vendidos, " +
                "el.ref11, el.ref12, el.ref13, el.ref14, el.ref15, el.ref16, el.iva, el.url_imagen, " +
                "m.id as idmarca, m.marca FROM elementos el INNER JOIN marcas m ON el.marca = m.id WHERE";
            if (ref1 != string.Empty)
            {

                sql += $" ref1 LIKE '%{ref1}%'";
                bandera = true;
            }
            if (ref2 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref2 LIKE '%{ref2}%'";
                }
                else
                {
                    sql += $" ref2 LIKE '%{ref2}%'";
                    bandera = true;
                }
            }
            if (ref3 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref3 LIKE '%{ref3}%'";
                }
                else
                {
                    sql += $" ref3 LIKE '%{ref3}%'";
                    bandera = true;
                }
            }
            if (ref4 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref4 LIKE '%{ref4}%'";
                }
                else
                {
                    sql += $" ref4 LIKE '%{ref4}%'";
                    bandera = true;
                }
            }
            if (ref5 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref5 LIKE '%{ref5}%'";
                }
                else
                {
                    sql += $" ref5 LIKE '%{ref5}%'";
                    bandera = true;
                }
            }
            if (codigo != 0)
            {
                if (bandera)
                {
                    sql += $" AND codigo LIKE '%{codigo}%'";
                }
                else
                {
                    sql += $" codigo LIKE '%{codigo}%'";
                    bandera = true;
                }
            }
            if (nombre != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND nombre LIKE '%{nombre}%'";
                }
                else
                {
                    sql += $" nombre LIKE '%{nombre}%'";
                    bandera = true;
                }
            }
            if (sett != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND sett LIKE '%{sett}%'";
                }
                else
                {
                    sql += $" sett LIKE '%{sett}%'";
                    bandera = true;
                }
            }
            if (marca != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND m.marca LIKE '%{marca}%'";
                }
                else
                {
                    sql += $" m.marca LIKE '%{marca}%'";
                }
            }
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
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
                        Unidad = new UnidadInterfazGraficaVentaDTO{ Id = reader.GetString("unidad") } ,
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

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorNombreYReferencias(string ref1, string ref2, string ref3, string ref4, string ref5, string nombre, long identificacionEmpresa)
        {
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();
            if (ref1 == string.Empty && ref2 == string.Empty && ref3 == string.Empty && ref4 == string.Empty && ref5 == string.Empty && nombre == string.Empty)
            {
                return Elementos;
            }
            bool bandera = false;
            string sql = "SELECT el.codigo, el.idempresa, el.empresa, el.tipo_articulo, " +
                "el.nombre, el.ref1, el.ref2, el.ref3, el.ref4, el.ref5, el.sett, el.unidad, " +
                "el.valor, el.tipo, el.cantidad, el.cantidad_disponible, el.cantidad_en_almacen ,el.cantidad_vendidos, " +
                "el.ref11, el.ref12, el.ref13, el.ref14, el.ref15, el.ref16, el.iva, el.url_imagen, " +
                "m.id as idmarca, m.marca FROM elementos el INNER JOIN marcas m ON el.marca = m.id WHERE";
            if (ref1 != string.Empty)
            {

                sql += $" ref1 LIKE '%{ref1}%'";
                bandera = true;
            }
            if (ref2 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref2 LIKE '%{ref2}%'";
                }
                else
                {
                    sql += $" ref2 LIKE '%{ref2}%'";
                    bandera = true;
                }
            }
            if (ref3 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref3 LIKE '%{ref3}%'";
                }
                else
                {
                    sql += $" ref3 LIKE '%{ref3}%'";
                    bandera = true;
                }
            }
            if (ref4 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref4 LIKE '%{ref4}%'";
                }
                else
                {
                    sql += $" ref4 LIKE '%{ref4}%'";
                    bandera = true;
                }
            }
            if (ref5 != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND ref5 LIKE '%{ref5}%'";
                }
                else
                {
                    sql += $" ref5 LIKE '%{ref5}%'";
                    bandera = true;
                }
            }

            if (nombre != string.Empty)
            {
                if (bandera)
                {
                    sql += $" AND nombre LIKE '%{nombre}%'";
                }
                else
                {
                    sql += $" nombre LIKE '%{nombre}%'";
                    bandera = true;
                }
            }
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
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

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorCodigo(int codigo, long identificacionEmpresa)
        {
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();
            foreach (var ele in ObtenerElementos(identificacionEmpresa))
            {
                if (ele.CodigoArticulo == codigo && codigo != 0)
                {
                    Elementos.Add(ele);
                }
            }
            return Elementos;
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorSett(String sett, long identificacionEmpresa)
        {
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();
            foreach (var ele in ObtenerElementos(identificacionEmpresa))
            {
                if (ele.Sett.ToUpper().Contains(sett.ToUpper()) && sett != string.Empty)
                {
                    Elementos.Add(ele);
                }
            }
            return Elementos;
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorMarca(String marca, long identificacionEmpresa)
        {
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();
            foreach (var ele in ObtenerElementos(identificacionEmpresa))
            {
                if (ele.Marca.ToUpper().Contains(marca.ToUpper()) && marca != string.Empty)
                {
                    Elementos.Add(ele);
                }
            }
            return Elementos;
        }

        public bool InsertarElemento(ElementoInterfazGraficaVentaDTO elemento)
        {


            string queryElementos = "INSERT INTO elementos (codigo, idempresa," +
                " nombre, marca, ref1, ref2, ref3, ref4, sett, unidad, valor, tipo, cantidad, " +
                "cantidad_disponible, cantidad_en_almacen, cantidad_vendidos, ref11, ref12, ref13, " +
                "ref14, ref15, ref16, ref5, iva, url_imagen) " +
                "VALUES (@codigo, @idempresa, @nombre, " +
                "@marca, @ref1, @ref2, @ref3, @ref4, @sett, " +
                "@unidad, @valor, @tipo,@cantidad, @cantidad_disponible, @cantidad_en_almacen," +
                " @cantidad_vendidos, @ref11, @ref12, @ref13, @ref14, @ref15, @ref16, " +
                "@ref5, @iva, @url_imagen);";
            string queryCatalogos_elementos = "INSERT INTO catalogos_elementos (id_catalogo, id_elemento) " +
                "VALUES (@id_catalogo, @id_elemento);";
            string codMasUno = "SELECT MAX(codigo)+1 as cod FROM elementos;";
            _conexion.Open();
            var MysqlConnection = _conexion;
            int filasElementos = 0;
            int filasCatalogoElemento = 0;
            bool validacionTransaccion = false;
            using MySqlTransaction transaction = MysqlConnection.BeginTransaction();



            try
            {
                var TraerMayorCodigo = MysqlConnection.CreateCommand();
                TraerMayorCodigo.Transaction = transaction;
                TraerMayorCodigo.CommandText = codMasUno;
                using MySqlDataReader reader = TraerMayorCodigo.ExecuteReader();
                while (reader.Read())
                {
                    bool hayElementos = reader.IsDBNull(0);
                    if (hayElementos)
                    {
                        elemento.CodigoArticulo = 1;
                        
                    }
                    else 
                    {
                        elemento.CodigoArticulo = reader.GetInt64("cod");
                    }
                    
                }
                reader.Close();


                var InsercionElemento = MysqlConnection.CreateCommand();
                InsercionElemento.Transaction = transaction;
                InsercionElemento.CommandText = queryElementos;
                InsercionElemento.Parameters.AddWithValue("@codigo", elemento.CodigoArticulo);
                InsercionElemento.Parameters.AddWithValue("@idempresa", elemento.IdEmpresa);
                InsercionElemento.Parameters.AddWithValue("@nombre", elemento.Nombre);
                InsercionElemento.Parameters.AddWithValue("@marca", elemento.IdMarca);
                InsercionElemento.Parameters.AddWithValue("@ref1", elemento.Ref1);
                InsercionElemento.Parameters.AddWithValue("@ref2", elemento.Ref2);
                InsercionElemento.Parameters.AddWithValue("@ref3", elemento.Ref3);
                InsercionElemento.Parameters.AddWithValue("@ref4", elemento.Ref4);
                InsercionElemento.Parameters.AddWithValue("@sett", elemento.Sett);
                InsercionElemento.Parameters.AddWithValue("@unidad", elemento.Unidad.Id);
                InsercionElemento.Parameters.AddWithValue("@valor", elemento.Valor);
                InsercionElemento.Parameters.AddWithValue("@tipo", elemento.Tipo);
                InsercionElemento.Parameters.AddWithValue("@cantidad", 0);
                InsercionElemento.Parameters.AddWithValue("@cantidad_disponible", elemento.CantidadDisponibleBodega);
                InsercionElemento.Parameters.AddWithValue("@cantidad_en_almacen", elemento.CantidadDisponibleAlmacen);
                InsercionElemento.Parameters.AddWithValue("@cantidad_vendidos", elemento.CantidadVendidos);
                InsercionElemento.Parameters.AddWithValue("@ref11", elemento.Ref11);
                InsercionElemento.Parameters.AddWithValue("@ref12", elemento.Ref12);
                InsercionElemento.Parameters.AddWithValue("@ref13", elemento.Ref13);
                InsercionElemento.Parameters.AddWithValue("@ref14", elemento.Ref14);
                InsercionElemento.Parameters.AddWithValue("@ref15", elemento.Ref15);
                InsercionElemento.Parameters.AddWithValue("@ref16", elemento.Ref16);
                InsercionElemento.Parameters.AddWithValue("@ref5", elemento.Ref5);
                InsercionElemento.Parameters.AddWithValue("@iva", elemento.PorcentajeIva);
                InsercionElemento.Parameters.AddWithValue("@url_imagen", "img");
                filasElementos = InsercionElemento.ExecuteNonQuery();

                var InsercionCatalogosElementos = MysqlConnection.CreateCommand();
                InsercionCatalogosElementos.Transaction = transaction;
                InsercionCatalogosElementos.CommandText = queryCatalogos_elementos;
                InsercionCatalogosElementos.Parameters.AddWithValue("@id_catalogo", elemento.IdCatalogo);
                InsercionCatalogosElementos.Parameters.AddWithValue("@id_elemento", elemento.CodigoArticulo);
                filasCatalogoElemento = InsercionCatalogosElementos.ExecuteNonQuery();

                if (filasCatalogoElemento == 1 && filasCatalogoElemento == 1)
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

        public long GenerarCodigoElemento()
        {
            string queryGeneracionCodigo = "SELECT MAX(codigo)+1 as cod FROM elementos";

            long codigoGenerado = 0;

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(queryGeneracionCodigo, _conexion);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    bool NohayElementos = reader.IsDBNull(0);
                    if (NohayElementos)
                    {
                        codigoGenerado = 1;
                    }
                    else
                    {
                        codigoGenerado = reader.GetInt64("cod");
                    }
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


            return codigoGenerado;

        }

        public bool ActualizarElemento(ElementoInterfazGraficaVentaDTO elemento)
        {
            string queryUpdateElement = "UPDATE elementos SET " +
                " nombre = @nombre, marca = @marca, ref1 = @ref1, " +
                "ref2 = @ref2, ref3 = @ref3, ref4 = @ref4, sett = @sett, unidad = @unidad, valor = @valor, " +
                "tipo = @tipo, cantidad = @cantidad, cantidad_disponible = @cantidad_disponible, cantidad_en_almacen = @cantidad_en_almacen, " +
                "cantidad_vendidos = @cantidad_vendidos, ref11 = @ref11, ref12 = @ref12, ref13 = @ref13, ref14 = @ref14," +
                " ref15 = @ref15, ref16 = @ref16, ref5 = @ref5, iva = @iva, url_imagen = @url_imagen " +
                "WHERE codigo = @codigo";
            string queryUpdateCatalogoElemento = "UPDATE catalogos_elementos SET id_catalogo = @idcatalogo WHERE id_elemento = @codigo;";
            _conexion.Open();
            var MysqlConnection = _conexion;
            int filasElementos = 0;
            int filasCatalogoElementos = 0;
            bool validacionTransaccion = false;
            using MySqlTransaction transaction = MysqlConnection.BeginTransaction();

            try
            {
                var InsercionElemento = MysqlConnection.CreateCommand();
                InsercionElemento.Transaction = transaction;
                InsercionElemento.CommandText = queryUpdateElement;
                InsercionElemento.Parameters.AddWithValue("@codigo", elemento.CodigoArticulo);
                InsercionElemento.Parameters.AddWithValue("@nombre", elemento.Nombre);
                InsercionElemento.Parameters.AddWithValue("@marca", elemento.IdMarca);
                InsercionElemento.Parameters.AddWithValue("@ref1", elemento.Ref1);
                InsercionElemento.Parameters.AddWithValue("@ref2", elemento.Ref2);
                InsercionElemento.Parameters.AddWithValue("@ref3", elemento.Ref3);
                InsercionElemento.Parameters.AddWithValue("@ref4", elemento.Ref4);
                InsercionElemento.Parameters.AddWithValue("@sett", elemento.Sett);
                InsercionElemento.Parameters.AddWithValue("@unidad", elemento.Unidad.Id);
                InsercionElemento.Parameters.AddWithValue("@valor", elemento.Valor);
                InsercionElemento.Parameters.AddWithValue("@tipo", elemento.Tipo);
                InsercionElemento.Parameters.AddWithValue("@cantidad", 0);
                InsercionElemento.Parameters.AddWithValue("@cantidad_disponible", elemento.CantidadDisponibleBodega);
                InsercionElemento.Parameters.AddWithValue("@cantidad_en_almacen", elemento.CantidadDisponibleAlmacen);
                InsercionElemento.Parameters.AddWithValue("@cantidad_vendidos", elemento.CantidadVendidos);
                InsercionElemento.Parameters.AddWithValue("@ref11", elemento.Ref11);
                InsercionElemento.Parameters.AddWithValue("@ref12", elemento.Ref12);
                InsercionElemento.Parameters.AddWithValue("@ref13", elemento.Ref13);
                InsercionElemento.Parameters.AddWithValue("@ref14", elemento.Ref14);
                InsercionElemento.Parameters.AddWithValue("@ref15", elemento.Ref15);
                InsercionElemento.Parameters.AddWithValue("@ref16", elemento.Ref16);
                InsercionElemento.Parameters.AddWithValue("@ref5", elemento.Ref5);
                InsercionElemento.Parameters.AddWithValue("@iva", elemento.PorcentajeIva);
                InsercionElemento.Parameters.AddWithValue("@url_imagen", "img");
                filasElementos = InsercionElemento.ExecuteNonQuery();

                var InsercionCatalogosElementos = MysqlConnection.CreateCommand();
                InsercionCatalogosElementos.Transaction = transaction;
                InsercionCatalogosElementos.CommandText = queryUpdateCatalogoElemento;
                InsercionCatalogosElementos.Parameters.AddWithValue("@codigo", elemento.CodigoArticulo);
                InsercionCatalogosElementos.Parameters.AddWithValue("@idcatalogo", elemento.IdCatalogo);
                filasCatalogoElementos = InsercionCatalogosElementos.ExecuteNonQuery(); 
                if (filasElementos == 1)
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

        public int ObtenerCantidadElementosTotalesPorEmpresa(long idempresa)
        {
            string sql = "SELECT COUNT(*) as cantidad FROM elementos where idempresa = @idempresa";
            int CantidadElementosEmpresa = 0;
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@idempresa", idempresa);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadElementosEmpresa = reader.GetInt32("cantidad");
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
            return CantidadElementosEmpresa;
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

        public string BuscarCatalogoPorId(long id)
        {
            string sql = "SELECT clasificacion FROM catalogos WHERE id = @id;";
            string clasificacion = null;

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@id", id);
                using MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    clasificacion = reader.GetString("clasificacion");
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

            return clasificacion;
        }

        public string BuscarUnidadPorId(string id)
        {
            string sql = "SELECT unidad FROM unidad WHERE id = @id;";
            string unidad = null;

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@id", id);
                using MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    unidad = reader.GetString("unidad");
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

            return unidad;
        }

        public string BuscarIdPorUnidad(string Unidad)
        {
            string sql = "SELECT id FROM unidad WHERE unidad = @unidad;";
            string Id = null;

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@unidad", Unidad);
                using MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Id = reader.GetString("id");
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

            return Id;
        }

        public List<UnidadInterfazGraficaVentaDTO> ObtenerUnidades()
        {
            string sql = "SELECT id, unidad FROM unidad;";
            var Unidades = new List<UnidadInterfazGraficaVentaDTO>();

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var unidadDTO = new UnidadInterfazGraficaVentaDTO
                    {
                        Id = reader.GetString("id"),
                        Unidad = reader.GetString("unidad"),
                    };

                    Unidades.Add(unidadDTO);

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

            return Unidades;
        }

        public List<CatalogoInterfazGraficaVentaDTO> ObtenerCatalogos()
        {
            string sql = "SELECT id, idempresa, clasificacion, descripcion, url_imagen FROM catalogos;";
            var Catalogos = new List<CatalogoInterfazGraficaVentaDTO>();

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var catalogoDTO = new CatalogoInterfazGraficaVentaDTO
                    {
                        id = reader.GetInt64("id"),
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

        public List<MarcasInterfazGraficaVentaDTO> ObtenerMarcas()
        {
            string sql = "SELECT id, marca FROM marcas;";
            var Marcas = new List<MarcasInterfazGraficaVentaDTO>();

            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var marcaDTO = new MarcasInterfazGraficaVentaDTO
                    {
                        Id = reader.GetInt32("id"),
                        Marca = reader.GetString("marca")
                    };

                    Marcas.Add(marcaDTO);

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

            return Marcas;
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosPorNombre(string nombre, long identificacionEmpresa)
        {
            var Elementos = new List<ElementoInterfazGraficaVentaDTO>();
            foreach (var ele in ObtenerElementos(identificacionEmpresa))
            {
                if (ele.Nombre.ToUpper().Contains(nombre.ToUpper()) && nombre != string.Empty)
                {
                    Elementos.Add(ele);
                }
            }
            return Elementos;
        }

    }
}
