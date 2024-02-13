
namespace EntidadesNegocio.InterfazGraficaBlazorDTO.SesionDTO
{
    public class PoliticasAccesoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool PoliticaNueva { get; set; }
        public bool PoliticaExistente { get; set; }
        public bool Seleccionada { get; set; }


    }
}
