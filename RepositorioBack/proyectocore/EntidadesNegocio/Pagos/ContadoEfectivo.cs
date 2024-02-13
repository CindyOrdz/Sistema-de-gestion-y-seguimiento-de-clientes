using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.Pagos
{
    public class ContadoEfectivo : Contado
    {
        public ContadoEfectivo(Double valor, String nombreDelPago = "contadoEfectivo") : base(valor, nombreDelPago)
        {
        }

        public override string ToString()
        {
            return base.ToString() + "ContadoEfectivo{ }";
        }
    }
}
