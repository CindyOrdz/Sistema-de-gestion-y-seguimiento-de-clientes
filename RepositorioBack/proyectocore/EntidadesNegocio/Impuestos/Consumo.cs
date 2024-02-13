using EntidadesNegocio.VentaOnlineTradicional;

namespace EntidadesNegocio.Impuestos
{
    public class Consumo : Impuesto
    {
        public Consumo() : base("02", "IC")
        {
        }

        public override void CalcularImpuesto(Venta venta)
        {
            double valor = Math.Round((venta.ObtenerSubtotal() * 0.19) * 100.0) / 100.0;
            base.valor = valor;
            
            /*
                revisar cada producto por que cada uno puede tener un iva diferente
            */
        }

        public override void CalcularImpuestoDetalleVenta(DetalleVenta detalleVenta)
        { 
        
        }
        public override Impuesto Clonar()
        {
            return new Consumo();
        }

        public override String ToString()
        {
            return base.ToString() + "Consumo{}";
        }
    }
}
