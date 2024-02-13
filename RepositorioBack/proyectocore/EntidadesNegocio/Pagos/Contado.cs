

namespace EntidadesNegocio.Pagos
{
    public class Contado : Pago
    {
        private DateTime fecha;
        private DateTime hora;

        public Contado(Double Valor, String NombreDelPago = "Contado") : base(Valor, NombreDelPago)
        {
            

        }

        public DateTime ObtenerFecha()
        { 
            return fecha;
        }

        public DateTime ObtenerHora()
        {
            return hora;    
        }
        public override string ToString()
        {
            return base.ToString() + "Contado{ fecha=" + fecha + ", hora=" + hora + " }";
        }

    }

        
    
}
