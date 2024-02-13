using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros
{
    public class EmpresaInterfazGraficaVentaDTO
    {
        public long Identificacion { get; set; }
        public TipoDocumentoInterfazGraficaTercerosDTO? TipoDocumento { get; set; }
        public string RazonSocial { get; set; } = string.Empty;
        public string Nombre1 { get; set; } = string.Empty;
        public string Nombre2 { get; set; } = string.Empty;
        public string Apellido1 { get; set; } = string.Empty;
        public string Apellido2 { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UbicacionInterfazGraficaVentaDTO? Ubicacion { get; set; }
        public string Direccion { get; set; } = string.Empty;


    }
}
