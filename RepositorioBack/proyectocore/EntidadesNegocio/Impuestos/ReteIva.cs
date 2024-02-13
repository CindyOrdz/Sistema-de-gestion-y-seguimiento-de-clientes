using EntidadesNegocio.VentaOnlineTradicional;

namespace EntidadesNegocio.Impuestos
{
    public class ReteIva : Impuesto
    {
        public ReteIva() : base("05", "ReteIVA")
        {
            this.porcentaje = ValoresImpuestos.Instancia.PorcentajeReteIva;
        }


        public override void CalcularImpuesto(Venta venta)
        {

            var cliente = venta.ObtenerCliente();
            if ((cliente.ConMontoDeLey().Equals("S") && (venta.ObtenerSubtotal() + venta.ObtenerIva()) > ValoresImpuestos.Instancia.MontoDeLey) || (cliente.ConMontoPersonalizado().Equals("S") && (venta.ObtenerSubtotal() + venta.ObtenerIva()) > cliente.ObtenerMontoPersonalizado()))
            {
                this.baseGravable = venta.ObtenerIva();
            }

            base.valor = Calcular(this.baseGravable) * (-1);


        }

        public override void CalcularImpuestoDetalleVenta(DetalleVenta DetalleVenta)
        {
            Iva ivaDetalle = DetalleVenta.ObtenerImpuestos().FirstOrDefault(impuesto => impuesto is Iva ) as Iva;

            this.baseGravable = ivaDetalle.ObtenerValor();

            base.valor = Calcular(this.baseGravable) * (-1);


        }


        private Double Calcular(Double valor)
        {
            Double valorARedondear = valor * (this.porcentaje / 100);
            return OperacionesDian.RedondeoDIAN(valorARedondear,2);

        }

        public override Impuesto Clonar()
        {
            return new ReteIva();
        }
    }
}
