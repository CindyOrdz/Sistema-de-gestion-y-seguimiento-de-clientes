using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;
using System.ComponentModel.DataAnnotations;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros
{
    public class SedeInterfazGraficaTerceroDTO
    {

        public long Id { get; set; }
        [Required(ErrorMessage = "El Responsable es requerido")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Sólo se permiten caracteres del alfabeto")]
        public string Responsable { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Telefono es requerido")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Sólo se permiten numeros")]
        public string Telefono { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Email 1 es requerido")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El correo electrónico no es válido.")]
        public string Email1 { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Email 2 es requerido")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El correo electrónico no es válido.")]
        public string Email2 { get; set; } = string.Empty;
        [Required(ErrorMessage = "La Dirección es requerida")]
        public string Direccion { get; set; } = string.Empty;
        public UbicacionInterfazGraficaVentaDTO Ubicacion { get; set; } = new UbicacionInterfazGraficaVentaDTO();
        public string ClaseCss { get; set; } = string.Empty;
    }
}
