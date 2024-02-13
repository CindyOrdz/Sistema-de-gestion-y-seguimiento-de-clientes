using EntidadesNegocio.ClasesDao.TercerosDAO;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;

namespace ServiciosComponentes.TercerosServices
{
    public class EmpresaService
    {
        private readonly EmpresaDAO _empresaDAO;
        private readonly SedeDAO _sedeDAO;
        public EmpresaService(EmpresaDAO empresaDAO, SedeDAO sedeDAO)
        {
            _empresaDAO = empresaDAO;
            _sedeDAO = sedeDAO;
        }


        public bool InsertarEmpresa(TerceroInterfazGraficaDTO tercero)
        {
            bool resultado = _empresaDAO.InsertarEmpresa(tercero);
            return resultado;
        }

        public List<TerceroInterfazGraficaDTO> FiltrarEmpresaPorIniciales(string iniciales)
        {
            var listaEmpresas = _empresaDAO.FiltrarEmpresasPorNombre(iniciales);

            listaEmpresas.ForEach(empresa =>
            {
                empresa.Sedes = _sedeDAO.ObtenerSedesDeTercero(empresa.Identificacion);

            });


            return listaEmpresas;
        }

        public TerceroInterfazGraficaDTO FiltrarEmpresaPorIdentificacion(long identificacion)
        {
            var empresaFiltrada = _empresaDAO.FiltrarEmpresasPorIdentificacion(identificacion);

            if (empresaFiltrada.Identificacion == 0)
            {
                return empresaFiltrada;
            }

            empresaFiltrada.Sedes = _sedeDAO.ObtenerSedesDeTercero(empresaFiltrada.Identificacion);
            return empresaFiltrada;
        }

        public EmpresaInterfazGraficaVentaDTO ObtenerEmpresaPorIdentificacion(long identificacionEmpresa)
        {
            return _empresaDAO.ObtenerEmpresaPorIdentificacion(identificacionEmpresa);
        }

        public bool ActualizarEmpresa(TerceroInterfazGraficaDTO empresaDTO)
        {

            return _empresaDAO.ActualizarEmpresa(empresaDTO);
        }
    }
}
