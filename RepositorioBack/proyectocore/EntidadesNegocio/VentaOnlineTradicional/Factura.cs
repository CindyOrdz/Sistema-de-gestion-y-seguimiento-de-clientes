using EntidadesNegocio.EntidadesDto;
using EntidadesNegocio.Terceros;

namespace EntidadesNegocio.VentaOnlineTradicional
{
    public class Factura : Venta
    {
        private String destinoFactura;
        private Double trm; //facturas de exportacion ¿sólo trm? se genera set y get, en java no hay get ni set



        public Factura()
        {
            tipoVenta = "FACTURA";
            destinoFactura = "Nacional";
            id = GenerarID();
        }

        public Factura(Tercero cliente,Tercero empresaVendedora, Empleado responsable, Empleado responsableComision, Double totalCosto, Double descuento, Double porcentajeDescuento, String empresa, String tipoPrecio, String entregarEn, String usuario, String pc)
            : base(cliente, empresaVendedora,responsable, responsableComision, totalCosto, descuento, porcentajeDescuento, empresa, tipoPrecio, entregarEn, usuario, pc)
        {

            tipoVenta = "FACTURA";
            id = GenerarID();
            destinoFactura = "Nacional";
        }

        public Factura(FacturaDto facturaDto)
            : base(facturaDto.cliente,facturaDto.empresaVendedora, facturaDto.responsable, facturaDto.responsableComision, facturaDto.totalCosto, facturaDto.descuento, facturaDto.porcentajeDescuento, facturaDto.empresa, facturaDto.tipoPrecio, facturaDto.entregarEn, facturaDto.usuario, facturaDto.pc)
        {

            tipoVenta = "FACTURA";
            id = GenerarID();
            destinoFactura = "Nacional";
        }

        public Factura CrearFacturaAPartirDeOrdenesDePedido(List<Factura> facturas)
        {


            return new Factura();
        }

        //no se puede dejar publico, se deja publico o protected? 
        protected override Venta CrearVentaAPartirDeVentaAnulada()
        {


            return new Factura();
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
            return base.ToString() + "Factura{" + "destino_factura=" + destinoFactura + '}';
        }

    }
}
