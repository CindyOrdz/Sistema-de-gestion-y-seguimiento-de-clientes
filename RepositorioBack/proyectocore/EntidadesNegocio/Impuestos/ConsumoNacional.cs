using EntidadesNegocio.VentaOnlineTradicional;

namespace EntidadesNegocio.Impuestos
{
    public class ConsumoNacional : Impuesto
    {
        public ConsumoNacional() : base("04", "INC")
        {
            this.porcentaje = ValoresImpuestos.Instancia.PorcentajeConsumoNacional;
        }

        public override void CalcularImpuesto(Venta venta)
        {
            var cliente = venta.ObtenerCliente();
            if ((cliente.ConMontoDeLey().Equals("S") && (venta.ObtenerSubtotal() + venta.ObtenerIva()) > ValoresImpuestos.Instancia.MontoDeLey) || (cliente.ConMontoPersonalizado().Equals("S") && (venta.ObtenerSubtotal() + venta.ObtenerIva()) > cliente.ObtenerMontoPersonalizado()))
            {
                this.baseGravable = venta.ObtenerSubtotal();
            }

            base.valor = Calcular(this.baseGravable) * (-1);

        }

        public override void CalcularImpuestoDetalleVenta(DetalleVenta detalleventa)
        {

            this.baseGravable = detalleventa.ObtenerSubTotal();

            base.valor = Calcular(this.baseGravable) * (-1);

        }

        private Double Calcular(Double valor)
        {
            Double valorARedondear = valor * (this.porcentaje / 100);
            return OperacionesDian.RedondeoDIAN(valorARedondear, 2);

        }

        public override Impuesto Clonar()
        {
            return new ConsumoNacional();
        }

        public override String ToString()
        {
            return base.ToString() + "ConsumoNacional{}";
        }
    }
}
