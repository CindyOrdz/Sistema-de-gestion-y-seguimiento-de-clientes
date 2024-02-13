namespace EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Venta
{
    public class DetalleVentaHistorialClienteDTO
    {
        public long Id { get; set; }
        public ElementoHistorialClienteDTO?  Elemento { get; set; }
        public double Cantidad { get; set; }
        public double Total { get; set; }
    }
}
