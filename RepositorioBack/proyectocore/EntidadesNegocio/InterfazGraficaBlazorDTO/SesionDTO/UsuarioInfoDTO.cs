
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.ImpuestosYDescuentos;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.InterfazGraficaBlazorDTO.ValidacionesPersonalizadas;
using System.ComponentModel.DataAnnotations;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.SesionDTO
{
    public class UsuarioInfoDTO
    {
        public long Identificacion { get; set; }
        public string RazonSocial { get; set; } = string.Empty;
        public string Nombre1 { get; set; } = string.Empty;
        public string Nombre2 { get; set; } = string.Empty;
        public string Apellido1 { get; set; } = string.Empty;
        public string Apellido2 { get; set; } = string.Empty;
        public UbicacionInterfazGraficaVentaDTO? Ubicacion { get; set; } 
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;

        public List<string> Roles { get; set; } 
        public TipoDocumentoInterfazGraficaTercerosDTO? TipoDocumento { get; set; }

        public CargoEmpleadoDTO? Cargo { get; set; }

    }
}
