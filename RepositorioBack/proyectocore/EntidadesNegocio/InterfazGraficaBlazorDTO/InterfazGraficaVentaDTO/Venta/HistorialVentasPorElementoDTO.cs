

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Venta
{
    public class HistorialVentasPorElementoDTO
    {
        public long IdVenta { get; set; }
        public DateOnly FechaVenta { get; set; }
        public TimeOnly Hora { get; set; }
        public string NombreResponsable { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = string.Empty;
        public string NombreElemento { get; set; } = string.Empty;
        public string ReferenciaElemento { get; set; } = string.Empty;
        public double CantidadElementos { get; set; }
        public double ValorElemento { get; set; }
        public string TipoVenta { get; set; } = string.Empty;
        
        
       
    }
}
