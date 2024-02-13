namespace EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia
{
    public class PaisInterfazGraficaRepoTercerosDTO
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public List<DepartamentoProvinciaInterfazGraficaRepoTercerosDTO>? Departamentos { get; set; }
    }
}
