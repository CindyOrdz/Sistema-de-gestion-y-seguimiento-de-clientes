using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.EntidadesDto
{
    public class AreaDTO
    {
        public BigInteger id { get; set; }
        public string nombre { get; set; }
        public string responsable { get; set; }
        public string telefonoContacto { get; set; }
        public string emailContacto { get; set; }
        public string descripcion { get; set; }
    }
}
