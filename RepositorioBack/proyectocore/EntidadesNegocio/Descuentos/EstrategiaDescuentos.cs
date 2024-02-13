using EntidadesNegocio.VentaOnlineTradicional;

namespace EntidadesNegocio.Descuentos
{
    public abstract class EstrategiaDescuentos
    {
       
        protected String nombre;    
        protected Double valor;
        protected Double porcentaje;

        public EstrategiaDescuentos(String nombre)
        {
            this.nombre = nombre;
        }

        public abstract Double ObtenerTotalDescuento(Venta venta);
        public abstract Double ObtenerPorcentajeDescuento(Double valor, Venta venta);

        public Double ObtenerPorcentajeDescuento() => porcentaje;
        public String ObtenerNombre() => nombre;
        public Double ObtenerValor() => valor;  

    }
}
