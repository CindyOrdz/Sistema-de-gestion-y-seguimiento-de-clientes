using System.Numerics;

namespace EntidadesNegocio.EntidadesDto
{
    public class VisitaDTO
    {
        public BigInteger id { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFinaliza { get; set; }
        public int cantidadDias { get; set; }
        public String contactoEmpresa { get; set; }
        public String empresaCliente { get; set; }
        public String plantaCliente { get; set; }
        public long sedeCliente { get; set; }
        public String empresaPrestaServicio { get; set; }
        public String nombreResponsable { get; set; }
        public Double idEmpresaCliente { get; set; }
        public long idVentaAsociada { get; set; }
        public BigInteger idPlantaCliente { get; set; }
        public String estado { get; set; }
        public int cantidad { get; set; }
        public Double idResponsable { get; set; }

    }
}
