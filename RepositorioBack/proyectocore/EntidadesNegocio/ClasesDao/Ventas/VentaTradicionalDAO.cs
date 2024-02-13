
using EntidadesNegocio.ElementosInventario;
using EntidadesNegocio.Impuestos;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Venta;
using EntidadesNegocio.Terceros;
using MySqlConnector;
using System.Transactions;

namespace EntidadesNegocio.ClasesDao.Ventas
{
    public class VentaTradicionalDAO
    {
        private readonly MySqlConnection _conexion;
        public VentaTradicionalDAO(MySqlConnection conexion)
        {
            _conexion= conexion;    
        }

        public bool InsertarVentaTradicional(VentaInterfazGraficaVentaDTO ventaDTO)
        {
            string sqlventa = "insert into ventas(id_venta_transaccion, idempresa, empresa, id, numero, tipo_venta, fecha, hora, " +
                "cliente, tipo_pago, sub_total, iva, total, valor_efectivo, valor_credito, valor_cheque, valor_tarjeta, DESCUENTO, " +
                "responsable, orden, origen, tipo_precio, retefuente, reteiva, reteica, porcentaje_descuento, destino_factura, pc) " +
                "values(@id_venta_transaccion, @idempresa,  @empresa, @id, @numero, @tipo_venta, @fecha, @hora, @cliente, " +
                "@tipo_pago, @sub_total, @iva, @total, @valor_efectivo, @valor_credito, @valor_cheque, @valor_tarjeta, @DESCUENTO," +
                "@responsable,@orden, @origen, @tipo_precio,@retefuente,@reteiva,@reteica,@porcentaje_descuento,@destino_factura," +
                "@pc)";

            string sqlImpuestosVenta = "insert into venta_impuestos(id_venta, id_impuesto, valor) " +
                "values(@id_venta, @id_impuesto, @valor)";

            string sqlDetalleVenta = "insert into venta_detalles(detalle, id_venta, idempresa, empresa, tipo_venta, id_elemento, cantidad, valor) " +
                "values (@detalle, @id_venta, @idempresa,  @empresa, @tipo_venta, @id_elemento, @cantidad, @valor)";

            string sqlIdVentaTransaccion = "SELECT MAX(id_venta_transaccion)+1 as id_venta_transac FROM ventas";

            string sqlIdVenta = "select max(id)+1 as id_venta from ventas " +
                "where idempresa=@idempresa and tipo_venta=@tipo_venta";

            string sqlIdDetalle = "select max(detalle)+1 as id_detalle from venta_detalles";

            _conexion.Open();
            var MysqlConnection = _conexion;
            int filasVenta = 0;
            int filasImpuestosVenta = 0;
            int filasDetalle = 0;   
            bool validacionTransaccion = false;
            using MySqlTransaction transaction = MysqlConnection.BeginTransaction();

            try
            {
                double valorIva = 0;
                double valorReteFuente = 0;
                double valorReteIca = 0;
                double valorReteIva = 0;
                var impuestoIva = ventaDTO.Impuestos.FirstOrDefault(imp => imp.Id.Equals("01"));
                var impuestoReteFuente = ventaDTO.Impuestos.FirstOrDefault(imp => imp.Id.Equals("06"));
                var impuestoReteIva = ventaDTO.Impuestos.FirstOrDefault(imp => imp.Id.Equals("05"));
                var impuestoReteIca = ventaDTO.Impuestos.FirstOrDefault(imp => imp.Id.Equals("07"));

                if (impuestoIva is not null)
                    valorIva = impuestoIva.Valor;
                if (impuestoReteIva is not null)
                    valorReteIva = impuestoReteIva.Valor;
                if (impuestoReteIca is not null)
                    valorReteIca = impuestoReteIca.Valor;
                if (impuestoReteFuente is not null)
                    valorReteFuente = impuestoReteFuente.Valor;

                var comandoIdVentaTr = MysqlConnection.CreateCommand();
                comandoIdVentaTr.Transaction = transaction;
                comandoIdVentaTr.CommandText = sqlIdVentaTransaccion;
                using MySqlDataReader reader = comandoIdVentaTr.ExecuteReader();
                while (reader.Read())
                {
                    bool NohayVentas = reader.IsDBNull(0);
                    if (NohayVentas)
                    {
                        ventaDTO.IdVentaTransaccion = 1;
                    }
                    else
                    {
                        ventaDTO.IdVentaTransaccion = reader.GetInt64("id_venta_transac");
                    }
                }
                reader.Close();

                var comandoIdVenta = MysqlConnection.CreateCommand();
                comandoIdVenta.Transaction = transaction;
                comandoIdVenta.CommandText = sqlIdVenta;
                comandoIdVenta.Parameters.AddWithValue("@idempresa", ventaDTO.Empresa.Identificacion);
                comandoIdVenta.Parameters.AddWithValue("@tipo_venta", ventaDTO.TipoVenta);
                using MySqlDataReader readerIdVenta = comandoIdVenta.ExecuteReader();
                while (readerIdVenta.Read())
                {
                    bool NohayTipoVentas = readerIdVenta.IsDBNull(0);
                    if (NohayTipoVentas)
                    {
                        ventaDTO.Id = 1;
                        ventaDTO.Numero = 1;
                    }
                    else
                    {
                        ventaDTO.Id = readerIdVenta.GetDouble("id_venta");
                        ventaDTO.Numero = readerIdVenta.GetDouble("id_venta");
                    }
                }
                readerIdVenta.Close();



                var commandoCreacionVenta = MysqlConnection.CreateCommand();
                commandoCreacionVenta.Transaction = transaction;
                commandoCreacionVenta.CommandText = sqlventa;
                commandoCreacionVenta.Parameters.AddWithValue("@id_venta_transaccion", ventaDTO.IdVentaTransaccion);
                commandoCreacionVenta.Parameters.AddWithValue("@idempresa", ventaDTO.Empresa.Identificacion);
                commandoCreacionVenta.Parameters.AddWithValue("@empresa", ventaDTO.Empresa.RazonSocial);
                commandoCreacionVenta.Parameters.AddWithValue("@id", ventaDTO.Id);
                commandoCreacionVenta.Parameters.AddWithValue("@numero", ventaDTO.Numero);
                commandoCreacionVenta.Parameters.AddWithValue("@tipo_venta", ventaDTO.TipoVenta);
                commandoCreacionVenta.Parameters.AddWithValue("@fecha", ventaDTO.FechaVenta.ToString("yyyy/MM/dd"));
                commandoCreacionVenta.Parameters.AddWithValue("@hora", ventaDTO.Hora.ToString("HH:mm:ss"));
                commandoCreacionVenta.Parameters.AddWithValue("@cliente", ventaDTO.Cliente.Identificacion);
                commandoCreacionVenta.Parameters.AddWithValue("@tipo_pago", ventaDTO.TipoPago);
                commandoCreacionVenta.Parameters.AddWithValue("@sub_total", ventaDTO.SubTotal);
                commandoCreacionVenta.Parameters.AddWithValue("@iva", valorIva);
                commandoCreacionVenta.Parameters.AddWithValue("@total", ventaDTO.Total);
                commandoCreacionVenta.Parameters.AddWithValue("@valor_efectivo", ventaDTO.ValorEfectivo);
                commandoCreacionVenta.Parameters.AddWithValue("@valor_credito", ventaDTO.ValorCredito);
                commandoCreacionVenta.Parameters.AddWithValue("@valor_cheque", ventaDTO.ValorCheque);
                commandoCreacionVenta.Parameters.AddWithValue("@valor_tarjeta", ventaDTO.ValorTarjeta);
                commandoCreacionVenta.Parameters.AddWithValue("@DESCUENTO", ventaDTO.Descuento);
                commandoCreacionVenta.Parameters.AddWithValue("@responsable", ventaDTO.Empleado.Identificacion);
                commandoCreacionVenta.Parameters.AddWithValue("@orden", ventaDTO.DescripcionOrden);
                commandoCreacionVenta.Parameters.AddWithValue("@origen", ventaDTO.Origen);
                commandoCreacionVenta.Parameters.AddWithValue("@tipo_precio", ventaDTO.TipoPrecio);
                commandoCreacionVenta.Parameters.AddWithValue("@retefuente", valorReteFuente);
                commandoCreacionVenta.Parameters.AddWithValue("@reteiva", valorReteIva);
                commandoCreacionVenta.Parameters.AddWithValue("@reteica", valorReteIca);
                commandoCreacionVenta.Parameters.AddWithValue("@porcentaje_descuento", ventaDTO.PorcentajeDescuento);
                commandoCreacionVenta.Parameters.AddWithValue("@destino_factura", ventaDTO.DestinoProducto);
                commandoCreacionVenta.Parameters.AddWithValue("@pc", ventaDTO.PC);
                filasVenta = commandoCreacionVenta.ExecuteNonQuery();
                var impuestosVenta = ventaDTO.Impuestos;

                impuestosVenta.ForEach(impuesto =>
                {
                    var commandoCreacionImpuestosVenta = MysqlConnection.CreateCommand();
                    commandoCreacionImpuestosVenta.Transaction = transaction;
                    commandoCreacionImpuestosVenta.CommandText = sqlImpuestosVenta;
                    commandoCreacionImpuestosVenta.Parameters.AddWithValue("@id_venta", ventaDTO.IdVentaTransaccion);
                    commandoCreacionImpuestosVenta.Parameters.AddWithValue("@id_impuesto", impuesto.Id);
                    commandoCreacionImpuestosVenta.Parameters.AddWithValue("@valor", impuesto.Valor);
                    filasImpuestosVenta += commandoCreacionImpuestosVenta.ExecuteNonQuery();
                });

                var detallesVenta = ventaDTO.DetallesVenta;

                detallesVenta.ForEach(detalle =>
                {
                var comandoIdDetalle = MysqlConnection.CreateCommand();
                comandoIdDetalle.Transaction = transaction;
                comandoIdDetalle.CommandText = sqlIdDetalle;
                using MySqlDataReader readerIdDetalle = comandoIdDetalle.ExecuteReader();
                while (readerIdDetalle.Read())
                {
                    bool NohayDetallesVenta = readerIdDetalle.IsDBNull(0);
                    if (NohayDetallesVenta)
                    {
                        detalle.IdDetalle = 1;
                    }
                    else
                    {
                        detalle.IdDetalle = readerIdDetalle.GetInt64("id_detalle");
                    }
                }
                    readerIdDetalle.Close();

                    var commandoCreacionImpuestosVenta = MysqlConnection.CreateCommand();
                    commandoCreacionImpuestosVenta.Transaction = transaction;
                    commandoCreacionImpuestosVenta.CommandText = sqlDetalleVenta;
                    commandoCreacionImpuestosVenta.Parameters.AddWithValue("@detalle",detalle.IdDetalle );
                    commandoCreacionImpuestosVenta.Parameters.AddWithValue("@id_venta", ventaDTO.IdVentaTransaccion);
                    commandoCreacionImpuestosVenta.Parameters.AddWithValue("@idempresa", ventaDTO.Empresa.Identificacion);
                    commandoCreacionImpuestosVenta.Parameters.AddWithValue("@empresa", ventaDTO.Empresa.RazonSocial);
                    commandoCreacionImpuestosVenta.Parameters.AddWithValue("@tipo_venta", ventaDTO.TipoVenta);
                    commandoCreacionImpuestosVenta.Parameters.AddWithValue("@id_elemento", detalle.Elemento.CodigoArticulo);
                    commandoCreacionImpuestosVenta.Parameters.AddWithValue("@cantidad", detalle.CantidadSolicitada);
                    commandoCreacionImpuestosVenta.Parameters.AddWithValue("@valor", detalle.Total);
                    filasDetalle += commandoCreacionImpuestosVenta.ExecuteNonQuery();

                });

                

                if (filasVenta == 1 && filasImpuestosVenta == impuestosVenta.Count && filasDetalle == detallesVenta.Count)
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

        public List<ElementoVendidoClienteDTO> ObtenerElementosVendidosAlCliente(long idEmpresa, long idCliente, int paginaActual, int elementosPorPagina)
        {
            var limiteInferior = (paginaActual - 1) * elementosPorPagina;

            string sql = "select vent.id_venta_transaccion, vent.fecha, vent.hora, vent.total, vent_det.cantidad, " +
                "elmt.nombre, concat(elmt.ref1, ' ',elmt.ref2,' ', elmt.ref3, ' ', elmt.ref4, ' ', elmt.ref5)" +
                "as referencia, elmt.valor from ventas vent " +
                "inner join venta_detalles vent_det on vent.id_venta_transaccion = vent_det.id_venta inner join  " +
                "elementos elmt on elmt.codigo = vent_det.id_elemento  " +
                "where vent.idempresa = @idEmpresa and vent.cliente= @idCliente " +
                "order by vent.fecha desc, vent.hora desc limit @limite_inferior,@elementos_pagina";

            var listaVentas = new List<ElementoVendidoClienteDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                command.Parameters.AddWithValue("@idCliente", idCliente);
                command.Parameters.AddWithValue("@limite_inferior", limiteInferior);
                command.Parameters.AddWithValue("@elementos_pagina", elementosPorPagina);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var ventaDTO = new ElementoVendidoClienteDTO();

                    ventaDTO.IdVenta = reader.GetInt64("id_venta_transaccion");
                    ventaDTO.Fecha = reader.GetDateOnly("fecha");
                    ventaDTO.Hora = reader.GetTimeOnly("hora");
                    ventaDTO.NombreElemento = reader.GetString("nombre");
                    ventaDTO.ReferenciaElemento = reader.GetString("referencia");
                    ventaDTO.CantidadElementos = reader.GetDouble("cantidad");
                    ventaDTO.ValorElemento = reader.GetDouble("valor");
                    ventaDTO.Total = reader.GetDouble("total");
                    

                    listaVentas.Add(ventaDTO);

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


            return listaVentas;

        }

        public List<ElementoVendidoClienteDTO> ObtenerElementosVendidosAlClientePorIdElemento(long idEmpresa, long idCliente, int paginaActual, int elementosPorPagina, long idElemento)
        {
            var limiteInferior = (paginaActual - 1) * elementosPorPagina;

            string sql = "select vent.id_venta_transaccion, vent.fecha, vent.hora, vent.total, vent_det.cantidad, " +
                "elmt.nombre, concat(elmt.ref1, ' ',elmt.ref2,' ', elmt.ref3, ' ', elmt.ref4, ' ', elmt.ref5)" +
                "as referencia, elmt.valor from ventas vent " +
                "inner join venta_detalles vent_det on vent.id_venta_transaccion = vent_det.id_venta inner join  " +
                "elementos elmt on elmt.codigo = vent_det.id_elemento  " +
                "where vent.idempresa = @idEmpresa and vent.cliente= @idCliente and vent_det.id_elemento = @id_elemento " +
                "order by vent.fecha desc, vent.hora desc " +
                "limit @limite_inferior,@elementos_pagina";

            var listaVentas = new List<ElementoVendidoClienteDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                command.Parameters.AddWithValue("@idCliente", idCliente);
                command.Parameters.AddWithValue("@id_elemento", idElemento);
                command.Parameters.AddWithValue("@limite_inferior", limiteInferior);
                command.Parameters.AddWithValue("@elementos_pagina", elementosPorPagina);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var ventaDTO = new ElementoVendidoClienteDTO();

                    ventaDTO.IdVenta = reader.GetInt64("id_venta_transaccion");
                    ventaDTO.Fecha = reader.GetDateOnly("fecha");
                    ventaDTO.Hora = reader.GetTimeOnly("hora");
                    ventaDTO.NombreElemento = reader.GetString("nombre");
                    ventaDTO.ReferenciaElemento = reader.GetString("referencia");
                    ventaDTO.CantidadElementos = reader.GetDouble("cantidad");
                    ventaDTO.ValorElemento = reader.GetDouble("valor");
                    ventaDTO.Total = reader.GetDouble("total");


                    listaVentas.Add(ventaDTO);

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


            return listaVentas;

        }

        public List<HistorialVentasPorElementoDTO> ObtenerHistorialPorElemento(long idEmpresa, int paginaActual, int elementosPorPagina, long idElemento)
        {
            var limiteInferior = (paginaActual - 1) * elementosPorPagina;

            string sql = "select vent.id_venta_transaccion,vent.fecha, vent.hora, " +
                "concat(resp.NOMBRE1, ' ',COALESCE(resp.NOMBRE2, ''), ' ', resp.APELLIDO1, ' ', resp.APELLIDO2) as nombre_responsable, " +
                "concat(COALESCE(cli.NOMBRE1, ''), ' ',COALESCE(cli.NOMBRE2, ''), ' ', COALESCE(cli.APELLIDO1, ''), ' ', COALESCE(cli.APELLIDO2, ''),' ',COALESCE(cli.RAZON_SOCIAL, '')) as nombre_cliente, " +
                "elmt.nombre, concat(elmt.ref1, ' ',elmt.ref2,' ', elmt.ref3, ' ', elmt.ref4, ' ', elmt.ref5) " +
                "as referencia, vent_det.cantidad, elmt.valor, vent.tipo_venta from ventas vent " +
                "inner join venta_detalles vent_det on vent.id_venta_transaccion = vent_det.id_venta inner join  " +
                "elementos elmt on elmt.codigo = vent_det.id_elemento inner join " +
                "terceros cli on cli.IDENTIFICACION = vent.cliente inner join " +
                "terceros resp on resp.IDENTIFICACION = vent.responsable " +
                "where vent.idempresa = @idEmpresa and vent_det.id_elemento = @id_elemento " +
                "order by vent.fecha desc, vent.hora desc " +
                "limit @limite_inferior,@elementos_pagina";

            var listaVentas = new List<HistorialVentasPorElementoDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                command.Parameters.AddWithValue("@id_elemento", idElemento);
                command.Parameters.AddWithValue("@limite_inferior", limiteInferior);
                command.Parameters.AddWithValue("@elementos_pagina", elementosPorPagina);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var ventaDTO = new HistorialVentasPorElementoDTO();

                    ventaDTO.IdVenta = reader.GetInt64("id_venta_transaccion");
                    ventaDTO.FechaVenta = reader.GetDateOnly("fecha");
                    ventaDTO.Hora = reader.GetTimeOnly("hora");
                    ventaDTO.NombreResponsable = reader.GetString("nombre_responsable");
                    ventaDTO.NombreCliente = reader.GetString("nombre_cliente");
                    ventaDTO.NombreElemento = reader.GetString("nombre");
                    ventaDTO.ReferenciaElemento = reader.GetString("referencia");
                    ventaDTO.CantidadElementos = reader.GetDouble("cantidad");
                    ventaDTO.ValorElemento = reader.GetDouble("valor");
                    ventaDTO.TipoVenta = reader.GetString("tipo_venta");


                    listaVentas.Add(ventaDTO);

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


            return listaVentas;

        }


        public List<VentaHistorialClienteDTO> ObtenerHistorialCliente(long idEmpresa, long idCliente, int paginaActual, int elementosPorPagina)
        {
            var limiteInferior = (paginaActual - 1) * elementosPorPagina;

            string sql = "select vent.id_venta_transaccion, vent.tipo_venta, vent.fechaanulacion, " +
                "vent.fecha, vent.hora,  vent.tipo_pago, vent.total, vent.sub_total, " +
                "concat(ter.NOMBRE1, ' ',COALESCE(ter.NOMBRE2, ''), ' ', ter.APELLIDO1, ' ', ter.APELLIDO2) as nombre_responsable " +
                "from ventas vent inner join terceros ter on vent.responsable= ter.IDENTIFICACION " +
                "where vent.idempresa = @idEmpresa and vent.cliente= @idCliente order by vent.fecha desc, vent.hora desc " +
                "limit @limite_inferior,@elementos_pagina";

            var listaVentas = new List<VentaHistorialClienteDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                command.Parameters.AddWithValue("@idCliente", idCliente);
                command.Parameters.AddWithValue("@limite_inferior", limiteInferior);
                command.Parameters.AddWithValue("@elementos_pagina", elementosPorPagina);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var ventaDTO = new VentaHistorialClienteDTO();


                    ventaDTO.IdVenta = reader.GetInt64("id_venta_transaccion");
                    ventaDTO.FechaVenta = reader.GetDateOnly("fecha");
                    ventaDTO.Hora = reader.GetTimeOnly("hora");
                    ventaDTO.TipoVenta = reader.GetString("tipo_venta");
                    ventaDTO.TipoPago = reader.GetString("tipo_pago");
                    ventaDTO.Total = reader.GetDouble("total");
                    ventaDTO.SubTotal = reader.GetDouble("sub_total");
                    ventaDTO.NombreResponsable = reader.GetString("nombre_responsable");

                    bool NotieneFechaAnulacion = reader.IsDBNull(2);

                    if (NotieneFechaAnulacion)
                    {
                        ventaDTO.FechaAnulacion = null;
                    }
                    else
                    {
                        ventaDTO.FechaAnulacion = reader.GetDateOnly("fechaanulacion");
                    }

                    listaVentas.Add(ventaDTO);

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


            return listaVentas;

        }

        public List<DetalleVentaHistorialClienteDTO> ObtenerDetallesVenta(long idVenta)
        {
            string sql = "select det.detalle, det.cantidad, elmt.codigo, elmt.nombre, " +
                "concat(elmt.ref1,' ', elmt.ref2, ' ', elmt.ref3, ' ', elmt.ref4,' ', elmt.ref5) as referencia, " +
                "elmt.valor, mrc.marca " +
                "from venta_detalles det inner join elementos elmt on det.id_elemento = elmt.codigo " +
                "inner join marcas mrc on mrc.id = elmt.marca " +
                "where det.id_venta =@id_venta";

            var listaDetalles = new List<DetalleVentaHistorialClienteDTO>();
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@id_venta", idVenta);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var DetalleVentaDTO = new DetalleVentaHistorialClienteDTO { Elemento = new ElementoHistorialClienteDTO()};

                    DetalleVentaDTO.Id = reader.GetInt64("detalle");
                    DetalleVentaDTO.Cantidad = reader.GetDouble("cantidad");
                    DetalleVentaDTO.Elemento.Codigo = reader.GetInt64("codigo");
                    DetalleVentaDTO.Elemento.Nombre = reader.GetString("nombre");
                    DetalleVentaDTO.Elemento.Referencia = reader.GetString("referencia");
                    DetalleVentaDTO.Elemento.Valor = reader.GetDouble("valor");
                    DetalleVentaDTO.Elemento.Marca = reader.GetString("marca");
                    listaDetalles.Add(DetalleVentaDTO);

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


            return listaDetalles;


        }

        public int ObtenerTotalDetallesVentaCliente(long idEmpresa, long idCliente)
        {
            string sql = "SELECT COUNT(vent_det.detalle) as cantidad FROM ventas vent inner join " +
                "venta_detalles vent_det on vent.id_venta_transaccion=vent_det.id_venta " +
                "where vent.idempresa = @id_empresa and vent.cliente=@id_cliente";
            int CantidadventasDelCliente = 0;
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@id_empresa", idEmpresa);
                command.Parameters.AddWithValue("@id_cliente", idCliente);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadventasDelCliente = reader.GetInt32("cantidad");
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
            return CantidadventasDelCliente;

        }

        public int ObtenerTotalVentasCliente(long idEmpresa, long idCliente)
        {
            string sql = "SELECT COUNT(vent.id_venta_transaccion) as cantidad FROM ventas vent " +
                "where vent.idempresa = @id_empresa and vent.cliente=@id_cliente";
            int CantidadventasDelCliente = 0;
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@id_empresa", idEmpresa);
                command.Parameters.AddWithValue("@id_cliente", idCliente);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadventasDelCliente = reader.GetInt32("cantidad");
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
            return CantidadventasDelCliente;

        }

        public int ObtenerTotalVentasClientePorElemento(long idEmpresa, long idCliente, long idElemento)
        {
            string sql = "SELECT COUNT(vent.id_venta_transaccion) as cantidad FROM ventas vent " +
                "inner join venta_detalles vent_det on vent.id_venta_transaccion = vent_det.id_venta " +
                "where vent.idempresa = @idempresa and vent.cliente= @idcliente and vent_det.id_elemento = @idelemento";
            int CantidadventasDelCliente = 0;
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@idempresa", idEmpresa);
                command.Parameters.AddWithValue("@idcliente", idCliente);
                command.Parameters.AddWithValue("@idelemento", idElemento);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadventasDelCliente = reader.GetInt32("cantidad");
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
            return CantidadventasDelCliente;

        }

        public int ObtenerTotalHistorialVentasPorElemento(long idEmpresa, long idElemento)
        {
            string sql = "SELECT COUNT(vent.id_venta_transaccion) as cantidad FROM ventas vent " +
                "inner join venta_detalles vent_det on vent.id_venta_transaccion = vent_det.id_venta " +
                "where vent.idempresa = @idempresa and vent_det.id_elemento = @idelemento";
            int CantidadventasDelCliente = 0;
            try
            {
                _conexion.Open();
                using MySqlCommand command = new MySqlCommand(sql, _conexion);
                command.Parameters.AddWithValue("@idempresa", idEmpresa);
                command.Parameters.AddWithValue("@idelemento", idElemento);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CantidadventasDelCliente = reader.GetInt32("cantidad");
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
            return CantidadventasDelCliente;

        }

        public bool ConvertirTipoVenta(long idVentaTransaccion, string TipoVenta,long idEmpresa)
        {
            string sqlventa = "update ventas set tipo_venta=@tipo_venta, id=@id_venta, numero=@numero " +
                "where id_venta_transaccion=@id_venta_transaccion";

            string sqlIdVenta = "select max(id)+1 as id_venta from ventas " +
                "where idempresa=@idempresa and tipo_venta=@tipo_venta";

            _conexion.Open();
            var MysqlConnection = _conexion;
            int filasVenta = 0;
            double idVenta = 0;
            
            bool validacionTransaccion = false;
            using MySqlTransaction transaction = MysqlConnection.BeginTransaction();

            try
            {
                

                var comandoIdVenta = MysqlConnection.CreateCommand();
                comandoIdVenta.Transaction = transaction;
                comandoIdVenta.CommandText = sqlIdVenta;
                comandoIdVenta.Parameters.AddWithValue("@idempresa", idEmpresa);
                comandoIdVenta.Parameters.AddWithValue("@tipo_venta", TipoVenta);
                using MySqlDataReader readerIdVenta = comandoIdVenta.ExecuteReader();
                while (readerIdVenta.Read())
                {
                    bool NohayTipoVentas = readerIdVenta.IsDBNull(0);
                    if (NohayTipoVentas)
                    {
                        idVenta = 1;
                    }
                    else
                    {
                        idVenta = readerIdVenta.GetDouble("id_venta");
                    }
                    
                }
                readerIdVenta.Close();



                var commandoActualizacionVenta = MysqlConnection.CreateCommand();
                commandoActualizacionVenta.Transaction = transaction;
                commandoActualizacionVenta.CommandText = sqlventa;
                commandoActualizacionVenta.Parameters.AddWithValue("@id_venta_transaccion", idVentaTransaccion);
                commandoActualizacionVenta.Parameters.AddWithValue("@id_venta", idVenta);
                commandoActualizacionVenta.Parameters.AddWithValue("@numero", idVenta);
                commandoActualizacionVenta.Parameters.AddWithValue("@tipo_venta", TipoVenta);
                filasVenta = commandoActualizacionVenta.ExecuteNonQuery();

                if (filasVenta == 1)
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


        public bool AnularVenta(long idVentaTransaccion, DateOnly fechaAnulacion)
        {
            string sqlventa = "update ventas set fechaanulacion=@fecha_anulacion " +
                "where id_venta_transaccion=@id_venta_transaccion";

            _conexion.Open();
            var MysqlConnection = _conexion;
            int filasVenta = 0;

            bool validacionTransaccion = false;
            using MySqlTransaction transaction = MysqlConnection.BeginTransaction();

            try
            {

                var commandoActualizacionVenta = MysqlConnection.CreateCommand();
                commandoActualizacionVenta.Transaction = transaction;
                commandoActualizacionVenta.CommandText = sqlventa;
                commandoActualizacionVenta.Parameters.AddWithValue("@id_venta_transaccion", idVentaTransaccion);
                commandoActualizacionVenta.Parameters.AddWithValue("@fecha_anulacion", fechaAnulacion);
                filasVenta = commandoActualizacionVenta.ExecuteNonQuery();

                if (filasVenta == 1)
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

        public long GenerarIdVenta()
        {
            string queryGeneracionCodigo = "SELECT MAX(id_venta_transaccion)+1 as id_venta FROM ventas";

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
                        codigoGenerado = reader.GetInt64("id_venta");
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

    }
}
