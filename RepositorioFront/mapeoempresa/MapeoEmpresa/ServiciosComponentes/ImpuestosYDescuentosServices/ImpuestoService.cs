using EntidadesNegocio.ClasesDao.ImpuestosYDescuentosDAO;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.ImpuestosYDescuentos;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;

namespace ServiciosComponentes.ImpuestosYDescuentosServices
{
    public class ImpuestoService
    {
        private readonly ImpuestoDAO _impuestoDAO;
        public ImpuestoService(ImpuestoDAO impuestoDAO)
        {
            _impuestoDAO = impuestoDAO;
        }

        public List<ImpuestoInterfazGraficaVentaDTO> ObtenerImpuestos()
        {
            return _impuestoDAO.ObtenerImpuestos();


        }

        public List<ImpuestoInterfazGraficaVentaDTO> ObtenerImpuestosParaClientes()
        {
            return _impuestoDAO.ObtenerImpuestosParaClientes();


        }

        public List<ImpuestoInterfazGraficaVentaDTO> ObtenerImpuestosDelCliente(long identificacionCliente, long identificacionEmpresa)
        {

            return _impuestoDAO.ObtenerImpuestosDelCliente(identificacionCliente, identificacionEmpresa);
        }

        public bool ConfigurarImpuestosDelCliente(long identificacionEmpresa, TerceroInterfazGraficaDTO cliente)
        {

            return _impuestoDAO.ConfigurarImpuestosDelCliente(identificacionEmpresa, cliente);
        }

        public ParametrosImpuestosInterfazGraficaVentaDTO ObtenerParametrosImpuestos()
        {
            return _impuestoDAO.ObtenerParametrosImpuestos();
        }
    }
}
