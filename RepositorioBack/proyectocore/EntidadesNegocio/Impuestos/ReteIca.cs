using EntidadesNegocio.Terceros;
using EntidadesNegocio.VentaOnlineTradicional;
namespace EntidadesNegocio.Impuestos
{
    public class ReteIca : Impuesto
    {
        public ReteIca() : base("07", "ReteIca")
        {
            this.porcentaje = TarifasTerritorialesEmpresa.Instancia.TarifaReteIca;
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

        public override void CalcularImpuestoDetalleVenta(DetalleVenta DetalleVenta)
        {
            this.baseGravable = DetalleVenta.ObtenerSubTotal();

            base.valor = Calcular(this.baseGravable) * (-1);

        }
        
        private Double Calcular(Double valor)
        {
            Double valorARedondear = valor * this.porcentaje ;
            return OperacionesDian.RedondeoDIAN(valorARedondear,2);
            
        }

        public override Impuesto Clonar()
        {
            return new ReteIca();
        }
    }
}
