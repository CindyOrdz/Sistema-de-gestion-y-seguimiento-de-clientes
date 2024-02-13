

using EntidadesNegocio.InterfazGraficaBlazorDTO.ValidacionesPersonalizadas;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Venta
{
    public class ResumenVentaDTO
    {
        public double Descuento { get; set; }
        public double PorcentajeDescuento { get; set; }
        public double ValorCheque{ get; set; }
        public double ValorEfectivo { get; set; } 
        public double ValorTarjeta { get; set; }
        public double ValorCredito { get; set; }

        [ValidacionDevolucionResumenVenta]
        public double Devolucion { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }
    }
}
