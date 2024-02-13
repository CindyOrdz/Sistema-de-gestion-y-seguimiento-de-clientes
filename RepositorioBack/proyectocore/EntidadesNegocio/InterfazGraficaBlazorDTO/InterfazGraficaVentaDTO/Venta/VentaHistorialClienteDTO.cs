
namespace EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Venta
{
    public class VentaHistorialClienteDTO
    {
        public long IdVenta { get; set; }
        public string NombreResponsable { get; set; } = string.Empty;
        public string TipoVenta { get; set; } = string.Empty;
        public DateOnly? FechaAnulacion { get; set; }
        public DateOnly FechaVenta { get; set; }
        public TimeOnly Hora { get; set; }
        public string TipoPago { get; set; } = string.Empty;
        public double SubTotal { get; set; }
        public double Total { get; set; }
        public List<DetalleVentaHistorialClienteDTO> Detalles { get; set; }
        public string ClaseCss { get; set; } = string.Empty;

    }
}
