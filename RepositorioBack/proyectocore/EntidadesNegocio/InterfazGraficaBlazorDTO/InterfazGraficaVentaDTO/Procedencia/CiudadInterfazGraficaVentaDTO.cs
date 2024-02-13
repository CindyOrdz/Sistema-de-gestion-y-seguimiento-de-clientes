using System.ComponentModel.DataAnnotations;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia
{
    public class CiudadInterfazGraficaVentaDTO
    {
        
        public string Pais { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Codigo es requerido")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Sólo se permiten números de 5 dígitos")]
        public string Codigo { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Nombre es requerido")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "No se permiten caracteres especiales")]
        public string Nombre { get; set; } = string.Empty;
        public double TarifaReteIca { get; set; }
        public double TarifaIca { get; set; }

    }
}
