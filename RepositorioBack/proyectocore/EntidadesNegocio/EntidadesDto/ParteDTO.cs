using System.Numerics;

namespace EntidadesNegocio.EntidadesDto
{
    public class ParteDTO
    {
        public BigInteger Id { get; set; }
        public long IdElemento { get; set; }
        public BigInteger IdEquipo { get; set; }
        public String NombreElemento { get; set; } = string.Empty;
        public String NombreEquipo { get; set; } = string.Empty;
        public String NombreComponente { get; set; } = string.Empty;
        public String NombreArea { get; set; } = string.Empty;
    }
}
