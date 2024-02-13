using EntidadesNegocio.VentaOnlineTradicional;

namespace EntidadesNegocio.Descuentos
{
    public class DescuentoPorValorCompra : EstrategiaDescuentos
    {
        public DescuentoPorValorCompra(): base("DescuentoPorValorCompra")
        {
            this.porcentaje = ValoresDescuentos.Instancia.PorcentajeDescuentoValorCompra;
            this.valor = 0;
        }


        public override Double ObtenerPorcentajeDescuento(Double valor, Venta venta)
        {
            return valor;
        }

        public override Double ObtenerTotalDescuento(Venta venta)
        {
            Double valorARedondear = venta.ObtenerSubtotal() * (this.porcentaje / 100);
            this.valor = OperacionesDian.RedondeoDIAN(valorARedondear, 2);
            return valor;
        }
    }
}
