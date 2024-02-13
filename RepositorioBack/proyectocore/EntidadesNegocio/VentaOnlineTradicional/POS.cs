using EntidadesNegocio.Terceros;

namespace EntidadesNegocio.VentaOnlineTradicional
{
    public class POS : Venta
    {
        //qué significa POS

        public POS(Tercero cliente, Tercero empresaVendedora, Empleado responsable, Empleado responsableComision, Double totalCosto, Double descuento, Double porcentajeDescuento, String empresa, String tipoPrecio, String entregarEn, String usuario, String pc)
            : base(cliente,empresaVendedora, responsable, responsableComision, totalCosto, descuento, porcentajeDescuento, empresa, tipoPrecio, entregarEn, usuario, pc)
        {
            tipoVenta = "POS";
            id = GenerarID();
        }

        public POS()
        {
        }


        protected override Venta CrearVentaAPartirDeVentaAnulada()
        {


            return new POS();
        }

        protected override Double GenerarID()
        {
            return 0;
        }

        public override String ToString()
        {
            return base.ToString() + "POS{" + '}';
        }

        protected override Boolean AnularVenta()
        {
            Boolean valor = false;



            return valor;
        }
    }
}
