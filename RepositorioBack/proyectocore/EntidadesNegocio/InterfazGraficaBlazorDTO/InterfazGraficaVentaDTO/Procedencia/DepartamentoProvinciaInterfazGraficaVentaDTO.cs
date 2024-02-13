using System.ComponentModel.DataAnnotations;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia
{
    public class DepartamentoProvinciaInterfazGraficaVentaDTO
    {
        public string Pais { get; set; } = string.Empty;
        

        [Required(ErrorMessage = "El Codigo es requerido")]
        [RegularExpression(@"^\d{2}$", ErrorMessage = "Sólo se permiten números de 2 dígitos")]
        public string Codigo { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Nombre es requerido")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "No se permiten caracteres especiales")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Codigo ISO es requerido")]
        [RegularExpression(@"^(?:[\p{L}\s]+|[-]{3})$", ErrorMessage = "No se permiten caracteres especiales")]
        public string CodigoIso { get; set; } = string.Empty;
    }
}
