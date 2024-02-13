using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Inventario;
using EntidadesNegocio.Terceros;
using RegistroEvidencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.EntidadesDto
{
    public class RevisionInventarioDTO
    {
        public BigInteger id { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFinalizacion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public Double idResponsable { get; set; }
        public String nombreResponsable { get; set; } = string.Empty;
        public String descripcion { get; set; } = string.Empty;        
        public String observaciones { get; set; } = string.Empty;
        public String estado { get; set; } = string.Empty;
        public List<ElementoInterfazGraficaVentaDTO> elementosRevisados { get; set; }
        public int cantidad { get; set; }
    }
}
