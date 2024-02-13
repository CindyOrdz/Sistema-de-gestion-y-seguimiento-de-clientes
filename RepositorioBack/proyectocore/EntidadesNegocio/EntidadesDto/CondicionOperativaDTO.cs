using EntidadesNegocio.ElementosInventario;
using EntidadesNegocio.InformacionVisita;
using System.Numerics;

namespace EntidadesNegocio.EntidadesDto
{
    public class CondicionOperativaDTO
    {
        public BigInteger id { get; set; }
        public String nombre { get; set; }
        public MedidaElemento medidaElemento { get; set; }
        public String rangoCompleto { get; set; }
        public Double valorFijo { get; set; }
        public Double rangoInicial { get; set; }
        public Double rangoFinal { get; set; }
        public String codigoUnidad { get; set; }
        public Unidad unidad { get; set; }

    }
}
