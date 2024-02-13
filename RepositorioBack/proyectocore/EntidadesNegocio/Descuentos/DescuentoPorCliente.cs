using EntidadesNegocio.VentaOnlineTradicional;

namespace EntidadesNegocio.Descuentos
{
    public class DescuentoPorCliente : EstrategiaDescuentos
    {
        public DescuentoPorCliente():base("DescuentoPorCliente")
        {
            this.porcentaje = ValoresDescuentos.Instancia.PorcentajeDescuentoPorCliente;
            this.valor = 0;
        }

        public override Double ObtenerPorcentajeDescuento(Double valor, Venta venta)
        {
            return valor;
        }

        public override Double ObtenerTotalDescuento(Venta venta)
        {
            Double valorARedondear = venta.ObtenerSubtotal() * (this.porcentaje / 100);
            this.valor = OperacionesDian.RedondeoDIAN(valorARedondear,2);
            return valor;
        }

        
    }
}
