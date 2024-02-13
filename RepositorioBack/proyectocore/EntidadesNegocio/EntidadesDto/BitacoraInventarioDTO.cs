using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.EntidadesDto
{
    public class BitacoraInventarioDTO
    {
        public BigInteger IdBitacora { get; set; }
        public BigInteger IdRevision { get; set; }
        public BigInteger IdElemento { get; set; }
        public Double CantidadAnterior { get; set; }
        public Double NuevaCantidad { get; set; }
        public String Observaciones { get; set; } = string.Empty;
        public DateTime fecha { get; set; }
        public Double IdResponsable { get; set; }
        public String NombreResponsable { get; set; } = string.Empty;
        public String NombreElemento { get; set; } = string.Empty;

    }
}
