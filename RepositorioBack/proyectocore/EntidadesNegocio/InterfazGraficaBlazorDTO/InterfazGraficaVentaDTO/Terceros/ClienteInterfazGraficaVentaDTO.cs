using EntidadesNegocio.Terceros;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros
{
    public class ClienteInterfazGraficaVentaDTO
    {
        public long Identificacion { get; set; }
        public int Tipo { get; set; }
        public UbicacionInterfazGraficaVentaDTO? Ubicacion { get; set; }
        public string Nombre1 { get; set; } = string.Empty;
        public string Nombre2 { get; set; } = string.Empty;
        public string Apellido1 { get; set; } = string.Empty;
        public string Apellido2 { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public EnumTipoDocumento EnumTipoDocumento { get; set; }

    }
}
