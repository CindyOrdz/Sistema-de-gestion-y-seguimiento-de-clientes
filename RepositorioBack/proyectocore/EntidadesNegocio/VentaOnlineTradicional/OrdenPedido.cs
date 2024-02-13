using EntidadesNegocio.EntidadesDto;
using EntidadesNegocio.Terceros;

namespace EntidadesNegocio.VentaOnlineTradicional
{
    public class OrdenPedido : Venta
    {
        private Double convertidaFacturaNumero;

        
        public OrdenPedido(Tercero cliente, Tercero empresaVendedora, Empleado responsable, Empleado responsableComision, Double totalCosto, Double descuento, Double porcentajeDescuento, String empresa, String tipoPrecio, String entregarEn, String usuario, String pc)
            : base(cliente, empresaVendedora,responsable, responsableComision, totalCosto, descuento, porcentajeDescuento, empresa, tipoPrecio, entregarEn, usuario, pc)
        {
            tipoVenta = "ORDEN";
            id = GenerarID();
            convertidaFacturaNumero = 0;
        }

        public OrdenPedido(OrdenPedidoDto ordenPedidoDto)
            : base(ordenPedidoDto.cliente,ordenPedidoDto.empresaVendedora, ordenPedidoDto.responsable, ordenPedidoDto.responsableComision, ordenPedidoDto.totalCosto, ordenPedidoDto.descuento, ordenPedidoDto.porcentajeDescuento, ordenPedidoDto.empresa, ordenPedidoDto.tipoPrecio, ordenPedidoDto.entregarEn, ordenPedidoDto.usuario, ordenPedidoDto.pc)
        {
            tipoVenta = "ORDEN";
            id = GenerarID();
            convertidaFacturaNumero = 0;
        }

        public OrdenPedido()
        {
            tipoVenta = "ORDEN";
            id = GenerarID();
        }

        
        protected override Venta CrearVentaAPartirDeVentaAnulada()
        {


            return new OrdenPedido();
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

        public override string ToString()
        {
            return base.ToString() + "OrdenPedido{" + "convertidaFacturaNumero=" + convertidaFacturaNumero + '}';
        }
    }
}
