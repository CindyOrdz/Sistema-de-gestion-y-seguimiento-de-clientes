using EntidadesNegocio.InterfazGraficaBlazorDTO.ValidacionesPersonalizadas;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.ImpuestosYDescuentos;
using System.ComponentModel.DataAnnotations;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros
{
    public class TerceroInterfazGraficaDTO
    {

        [Range(10000000, long.MaxValue, ErrorMessage = "se aceptan al menos 8 dígitos.")]

        public long Identificacion { get; set; }

        [Required(ErrorMessage = "El digito es requerido")]
        [RegularExpression(@"^\d{1}$", ErrorMessage = "Sólo se permite un dígito")]
        public int Digito { get; set; }

        [ValidacionRazonSocialTercero]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "sólo se permiten caracteres del alfabeto")]
        public string RazonSocial { get; set; } = string.Empty;

        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "sólo se permiten caracteres del alfabeto")]
        [ValidacionNombresTercero]
        public string Nombre1 { get; set; } = string.Empty;

        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "sólo se permiten caracteres del alfabeto")]
        [ValidacionNombresTercero]
        public string Nombre2 { get; set; } = string.Empty;

        [ValidacionApellidosTercero(ErrorMessage = "El apellido 1 es requerido")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "sólo se permiten caracteres del alfabeto")]
        public string Apellido1 { get; set; } = string.Empty;

        [ValidacionApellidosTercero(ErrorMessage = "El apellido 2 es requerido")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "sólo se permiten caracteres del alfabeto")]
        public string Apellido2 { get; set; } = string.Empty;

        public List<SedeInterfazGraficaTerceroDTO>? Sedes { get; set; }
        public UbicacionInterfazGraficaVentaDTO Ubicacion { get; set; } = new UbicacionInterfazGraficaVentaDTO();

        [Required(ErrorMessage = "El telefono es requerido")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es requerida")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Celular es requerido")]
        public string Celular { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Email es requerido")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El correo electrónico no es válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de tercero es requerido")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "sólo se permiten caracteres del alfabeto")]
        public string TipoTercero { get; set; } = string.Empty;
        public TipoDocumentoInterfazGraficaTercerosDTO? TipoDocumento { get; set; }

        public List<ImpuestoInterfazGraficaVentaDTO> Impuestos { get; set; }

        public List<DescuentoInterfazGraficaVentaDTO> Descuentos { get; set; }

        [ValidacionNaturalezaTercero]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "sólo se permiten caracteres del alfabeto")]
        public string Naturaleza { get; set; } = string.Empty;

        [ValidacionActividadEconomicaTercero]
        [RegularExpression("^[a-zA-Z0-9]{6}$", ErrorMessage = "sólo se permiten caracteres alfanuméricos de 6 digitos")]
        public string ActividadEconomica { get; set; } = string.Empty;
        public CargoEmpleadoDTO? Cargo { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        public string ConMontoDeLey { get; set; } = string.Empty;
        public string ConMontoPersonalizado { get; set; } = string.Empty;
        public double MontoPersonalizado { get; set; }
        public string ClaseCss { get; set; } = string.Empty;



    }
}
