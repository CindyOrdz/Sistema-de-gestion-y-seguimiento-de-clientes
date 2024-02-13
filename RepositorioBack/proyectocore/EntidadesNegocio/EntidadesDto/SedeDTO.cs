using EntidadesNegocio.VentaOnlineTradicional;
using System.Numerics;


namespace EntidadesNegocio.EntidadesDto
{
    public class SedeDTO
    {
        public long id { get; set; }
        public String responsable { get; set; }
        public String direccion { get; set; }
        public String ciudad { get; set; }
        public List<PlantaEmpresaClienteDTO> plantas { get; set; }
    }
}
