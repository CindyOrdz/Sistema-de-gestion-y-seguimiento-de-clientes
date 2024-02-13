using EntidadesNegocio.ClasesDao.ProcedenciaDAO;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;

namespace ServiciosComponentes.ProcedenciaServices
{

    public class ciudadService
    {
        private readonly MunicipioDAO _municipioDAO;

        public ciudadService(MunicipioDAO municipioDAO)
        {
            _municipioDAO = municipioDAO;
        }


        public List<CiudadInterfazGraficaVentaDTO> ObtenerCiudadesDeDepartamento(string codigoDepartamento)
        {
            return _municipioDAO.ObtenerCiudadesDeDepartamento(codigoDepartamento);
        }

    }
}
