using EntidadesNegocio.Terceros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.EntidadesDto
{
    public class CondicionRealDTO
    {
        public BigInteger id { get; set; }
        public String descripcion { get; set; }
        public String nombreResponsable { get; set; }
        public Tercero responsable { get; set; }
        public List<CondicionOperativaDTO> condicionesOperativas { get; set; }
        public long idResponsable { get; set; }
        public BigInteger idVisita { get; set; }
    }
}
