using EntidadesNegocio.VentaOnlineTradicional;

namespace EntidadesNegocio.Impuestos
{
    public class Iva : Impuesto
    {
        public Iva() : base("01", "IVA")
        {

        }


        public override void CalcularImpuesto(Venta venta)
        {

            List<DetalleVenta> detallesVenta = venta.ObtenerDetallesVenta();
            Double valorTotal = 0;

            foreach (var detalleVenta in detallesVenta)
            {
                Double valorARedondear = detalleVenta.ObtenerSubTotal() * detalleVenta.ObtenerImpuesto();
                valorTotal += OperacionesDian.RedondeoDIAN(valorARedondear, 2);

            }

            base.valor = valorTotal;
        }


        public override void CalcularImpuestoDetalleVenta(DetalleVenta DetalleVenta)
        {
            this.porcentaje = DetalleVenta.ObtenerDetalleElemento().ObtenerElemento().ObtenerImpuestoIva()/100;
            Double valorImpuestoSinRedondear = DetalleVenta.ObtenerSubTotal() * this.porcentaje;
            this.valor = OperacionesDian.RedondeoDIAN(valorImpuestoSinRedondear,2);

        }

        public override Impuesto Clonar()
        {
            return new Iva();
        }

        public override String ToString()
        {
            return base.ToString() + "Iva{}";
        }
    }
}
 