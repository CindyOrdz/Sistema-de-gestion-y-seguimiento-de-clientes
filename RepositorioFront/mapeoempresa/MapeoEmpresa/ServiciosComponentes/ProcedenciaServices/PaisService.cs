using EntidadesNegocio.ClasesDao.ProcedenciaDAO;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;

namespace ServiciosComponentes.ProcedenciaServices
{
    public class PaisService
    {
        private readonly PaisDAO _paisDAO;
        public PaisService(PaisDAO paisDAO)
        {
            _paisDAO = paisDAO;
        }

        public List<PaisInterfazGraficaVentaDTO> ObtenerPaises()
        {

            return _paisDAO.ObtenerPaises();
        }
    }
}
