using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Inventario;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Venta
{
    public class DetalleVentaInterfazGraficaVentaDTO
    {
        public long IdDetalle { get; set; }
        public int Item { get; set; }
        public ElementoInterfazGraficaVentaDTO? Elemento { get; set; }
        public double CantidadSolicitada { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }




    }
}
