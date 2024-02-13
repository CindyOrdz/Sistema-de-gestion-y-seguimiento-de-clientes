namespace EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia
{
    public class DepartamentoProvinciaInterfazGraficaRepoTercerosDTO
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string CodigoIso { get; set; } = string.Empty;
        public List<CiudadInterfazGraficaRepoTercerosDTO>? Ciudades { get; set; }
    }
}
