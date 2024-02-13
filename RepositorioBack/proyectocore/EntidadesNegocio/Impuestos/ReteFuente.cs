using EntidadesNegocio.VentaOnlineTradicional;
using System;

namespace EntidadesNegocio.Impuestos
{
    public class ReteFuente : Impuesto
    {
        public ReteFuente() : base("06", "ReteFuente")
        {
            this.porcentaje = ValoresImpuestos.Instancia.PorcentajeReteFuente;
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
            return OperacionesDian.RedondeoDIAN(valorARedondear,2);
            
        }

        public override Impuesto Clonar()
        {
            return new ReteFuente();
        }

    }
}
 