namespace EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.ImpuestosYDescuentos
{
    public class DescuentoInterfazGraficaVentaDTO
    {
        public string Codigo { get; set; } = string.Empty;
        public double Porcentaje { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public double Valor { get; set; }
        public string Vigente { get; set; } = string.Empty;
        public bool DescuentoClienteExiste { get; set; }
        public bool CambiarVigencia { get; set; }
        public bool  NuevoDescuento { get; set; }

    }
}
