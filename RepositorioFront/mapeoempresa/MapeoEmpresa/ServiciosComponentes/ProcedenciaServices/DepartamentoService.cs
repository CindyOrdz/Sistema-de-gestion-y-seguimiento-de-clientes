using EntidadesNegocio.ClasesDao.ProcedenciaDAO;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Procedencia;

namespace ServiciosComponentes.ProcedenciaServices
{
    public class DepartamentoService
    {
        private readonly DepartamentoDAO _departamentoDAO;
        public DepartamentoService(DepartamentoDAO departamentoDAO)
        {
            _departamentoDAO = departamentoDAO;
        }

        public List<DepartamentoProvinciaInterfazGraficaVentaDTO> ObtenerDepartamentosDePais(string codigoPais)
        {
            return _departamentoDAO.ObtenerDepartamentosDePais(codigoPais);
        }
    }
}
