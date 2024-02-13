using EntidadesNegocio.InterfazGraficaBlazorDTO.ValidacionesPersonalizadas;
namespace EntidadesNegocio.InterfazGraficaBlazorDTO.ModelViews
{
    public class OpcionesFiltrado
    {
        public string FiltroSeleccionado { get; set; } = string.Empty;

        [ValidacionOpcionesFiltrado]
        public string CadenaParaFiltrar { get; set; } = string.Empty;
    }
}
