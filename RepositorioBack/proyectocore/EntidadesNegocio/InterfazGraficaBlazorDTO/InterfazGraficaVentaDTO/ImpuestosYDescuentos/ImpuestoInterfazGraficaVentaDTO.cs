namespace EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.ImpuestosYDescuentos
{
    public class ImpuestoInterfazGraficaVentaDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public double Valor { get; set; }
        public double MontoDefinido { get; set; }
        public double Porcentaje { get; set; }
        public bool NuevoImpuesto { get; set; } = false;
        public bool CambiarVigencia { get; set; } = false;
        public bool ImpuestoClienteExiste { get; set; } = false;
        public string Vigente { get; set; } = string.Empty;


    }

}
