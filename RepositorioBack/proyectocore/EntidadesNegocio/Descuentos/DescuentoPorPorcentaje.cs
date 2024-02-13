
using EntidadesNegocio.VentaOnlineTradicional;

namespace EntidadesNegocio.Descuentos
{
    public class DescuentoPorPorcentaje : EstrategiaDescuentos
    {
        public DescuentoPorPorcentaje(): base("DescuentoPorPorcentaje")
        {
            this.valor = 0;
        }

        public override Double ObtenerPorcentajeDescuento(Double valor, Venta venta)
        {
            this.valor = valor;
            var porcentajeSinredondear = (this.valor * 100) / venta.ObtenerSubtotal();
            this.porcentaje = OperacionesDian.RedondeoDIAN(porcentajeSinredondear,2);

            return this.porcentaje;
        }

        public override Double ObtenerTotalDescuento(Venta venta)
        {
            Double valorARedondear = venta.ObtenerSubtotal() * (this.porcentaje / 100);
            this.valor = OperacionesDian.RedondeoDIAN(valorARedondear, 2);
            return valor;
        }

        
    }
}
