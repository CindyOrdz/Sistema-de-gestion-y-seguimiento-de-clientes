

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Venta
{
    public class ElementoVendidoClienteDTO
    {
        public long  IdVenta { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public double Total { get; set; }
        public string NombreElemento { get; set; } = string.Empty;
        public string ReferenciaElemento { get; set; } = string.Empty;
        public double  CantidadElementos { get; set; }
        public double ValorElemento { get; set; }

    }
}
