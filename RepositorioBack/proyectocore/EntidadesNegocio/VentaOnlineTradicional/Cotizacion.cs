using EntidadesNegocio.EntidadesDto;
using EntidadesNegocio.Terceros;

namespace EntidadesNegocio.VentaOnlineTradicional
{
    public class Cotizacion : Venta
    {
        private Double convertidaFacturaNumero;
        private Double convertidaOrdenPedido;

        public Cotizacion(Tercero cliente, Tercero empresaVendedora, Empleado responsable, Empleado responsableComision, Double totalCosto, Double descuento, Double porcentajeDescuento, String empresa, String tipoPrecio, String entregarEn, String usuario, String pc)
            : base(cliente, empresaVendedora, responsable, responsableComision, totalCosto, descuento, porcentajeDescuento, empresa, tipoPrecio, entregarEn, usuario, pc)
        {

            tipoVenta = "COTIZACION";
            convertidaFacturaNumero = 0;
            convertidaOrdenPedido = 0;
            id = GenerarID();
        }

        public Cotizacion()
        {
            tipoVenta = "COTIZACION";
            id = GenerarID();
        }

        protected override Boolean AnularVenta()
        {
            Boolean valor = false;



            return valor;
        }

        protected override Double GenerarID()
        {
            return 0;
        }

        public override String ToString()
        {
            return base.ToString() + "Cotizacion{" + '}';
        }

        public Factura PasarCotizacionAfactura()
        {

            FacturaDto facturaDto = new FacturaDto();
            facturaDto.cliente = _cliente;
            facturaDto.empresaVendedora = _empresaVendedora;
            facturaDto.responsable = _responsable;
            facturaDto.responsableComision = _responsableComision;
            facturaDto.tipoVenta = "FACTURA";
            facturaDto.fecha = fecha;
            facturaDto.hora = hora;
            facturaDto.formaDePago = _formasDePago;
            facturaDto.impuestos = _impuestos;
            facturaDto.subTotal = subTotal;
            facturaDto.iva = iva;
            facturaDto.total = total;
            facturaDto.totalCosto = totalCosto;
            facturaDto.descuento = descuento;
            facturaDto.porcentajeDescuento = porcentajeDescuento;
            facturaDto.orden = orden;
            facturaDto.empresa = empresa;
            facturaDto.origen = origen;
            facturaDto.impreso = impreso;
            facturaDto.tipoPrecio = tipoPrecio;
            facturaDto.reteFuente = reteFuente;
            facturaDto.reteIva = reteIva;
            facturaDto.reteIca = reteIca;
            facturaDto.entregarEn = entregarEn;
            facturaDto.legalizado = legalizado;
            facturaDto.tasaCambio = tasaCambio;
            facturaDto.usuario = usuario;
            facturaDto.pc = pc;
            facturaDto.detalle = _detallesVenta;

            //? Generar id esta definido en Venta pero implementado en Cotización

            convertidaFacturaNumero = GenerarID();
            anulado = "S";

            Factura factura = new Factura(facturaDto);

            return factura;

        }

        public OrdenPedido PasarCotizacionAOrdenDePedido()
        {

            OrdenPedidoDto ordenDto = new OrdenPedidoDto();
            ordenDto.cliente = _cliente;
            ordenDto.empresaVendedora = _empresaVendedora;
            ordenDto.responsable = _responsable;
            ordenDto.responsableComision = _responsableComision;
            ordenDto.tipoVenta = "ORDEN";
            ordenDto.fecha = fecha;
            ordenDto.hora = hora;
            ordenDto.formaDePago = _formasDePago;
            ordenDto.impuestos = _impuestos;
            ordenDto.subTotal = subTotal;
            ordenDto.iva = iva;
            ordenDto.total = total;
            ordenDto.totalCosto = totalCosto;
            ordenDto.descuento = descuento;
            ordenDto.porcentajeDescuento = porcentajeDescuento;
            ordenDto.orden = this.orden;
            ordenDto.empresa = empresa;
            ordenDto.origen = origen;
            ordenDto.impreso = impreso;
            ordenDto.tipoPrecio = tipoPrecio;
            ordenDto.reteFuente = reteFuente;
            ordenDto.reteIva = reteIva;
            ordenDto.reteIca = reteIca;
            ordenDto.entregarEn = entregarEn;
            ordenDto.legalizado = legalizado;
            ordenDto.tasaCambio = tasaCambio;
            ordenDto.usuario = usuario;
            ordenDto.pc = pc;
            ordenDto.detalle = _detallesVenta;

            convertidaFacturaNumero = GenerarID();
            anulado = "S";

            OrdenPedido orden = new OrdenPedido(ordenDto);
            return orden;
        }


        protected override Cotizacion CrearVentaAPartirDeVentaAnulada()
        {


            return new Cotizacion();
        }


    }
}
