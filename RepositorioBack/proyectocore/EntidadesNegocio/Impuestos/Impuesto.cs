using EntidadesNegocio.VentaOnlineTradicional;
using System.Runtime.Serialization.Formatters.Binary;

namespace EntidadesNegocio.Impuestos
{
    public abstract class Impuesto
    {
        protected String id;
        protected String nombre;
        protected Double valor;
        protected Double porcentaje;
        protected Double baseGravable;


        public Impuesto(String id, String nombre)
        {
            this.id = id;
            this.nombre = nombre;
            this.valor = 0;
            this.baseGravable = 0;
        }



        public abstract void CalcularImpuesto(Venta venta);
        public abstract void CalcularImpuestoDetalleVenta(DetalleVenta venta);
        public abstract Impuesto Clonar();


        public Double ObtenerValor()
        {
            return valor;
        }

        public String ObtenerId() => id;
        public String ObtenerNombre() => nombre;
        public Double ObtenerPorcentaje() => porcentaje;
        




        public override String ToString()
        {
            return "Impuesto{" + "id=" + id + ", nombre=" + nombre + ", valor=" + valor.ToString("C")  + ", porcentaje=" + porcentaje + '}';
        }
    }
}
 