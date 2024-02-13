using EntidadesNegocio.InformacionVisita;
using EntidadesNegocio.Terceros;
using System.Numerics;

namespace EntidadesNegocio.EntidadesDto
{
    public class CondicionInicialDTO
    {
        public BigInteger id { get; set; }
        public String descripcion { get; set; }
        public String nombreResponsable { get; set; }
        public Tercero responsable { get; set; }
        public List<CondicionOperativaDTO> condicionesOperativas { get; set; }
        public long idResponsable { get; set; }
    }
}
