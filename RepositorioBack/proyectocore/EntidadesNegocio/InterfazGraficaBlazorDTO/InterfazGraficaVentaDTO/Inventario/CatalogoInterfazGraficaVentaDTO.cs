
using System.Net.NetworkInformation;
using System.Numerics;
using EntidadesNegocio.ElementosInventario;
using System.ComponentModel.DataAnnotations;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Inventario
{
    public class CatalogoInterfazGraficaVentaDTO
    {
        public long id { get; set; }
        [Required(ErrorMessage = "La clasificación es obligatorio")]
        public String clasificacion { get; set; } = string.Empty;
        [Required(ErrorMessage = "La descripción es obligatoria")]
        public String descripcion { get; set; } = string.Empty;
        public Double idempresa { get; set; }
        //[Required(ErrorMessage = "La imagen es obligatoria")]
        public String url_imagen { get; set; } = string.Empty;
        public List<ElementoInterfazGraficaVentaDTO> _elementos { get; set; }
    }
}
