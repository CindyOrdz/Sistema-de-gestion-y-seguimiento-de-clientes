using EntidadesNegocio.ClasesDao.ImpuestosYDescuentosDAO;
using EntidadesNegocio.ClasesDao.TercerosDAO;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;

namespace ServiciosComponentes.TercerosServices
{

    public class ClienteService
    {
        private readonly ClienteDAO _clienteDAO;
        private readonly SedeDAO _sedeDAO;
        private readonly ImpuestoDAO _impuestoDAO;

        public ClienteService(ClienteDAO clienteDAO, SedeDAO sedeDAO, ImpuestoDAO impuestoDAO)
        {
            _clienteDAO = clienteDAO;
            _sedeDAO = sedeDAO;
            _impuestoDAO = impuestoDAO;
        }

        public bool InsertarCliente(TerceroInterfazGraficaDTO Cliente, long identificacionEmpresa)
        {

            return _clienteDAO.InsertarCliente(Cliente, identificacionEmpresa);

        }

        public bool ActualizarCliente(TerceroInterfazGraficaDTO cliente, long identificacionEmpresa)
        {

            return _clienteDAO.ActualizarCliente(cliente, identificacionEmpresa);
        }

        public List<TerceroInterfazGraficaDTO> FiltrarClientesPorIniciales(string iniciales, long identificacionEmpresa)
        {
            var listaClientes = _clienteDAO.FiltrarClientesPorNombre(iniciales, identificacionEmpresa);

            listaClientes.ForEach(cliente =>
            {
                cliente.Sedes = _sedeDAO.ObtenerSedesDeTercero(cliente.Identificacion);

            });


            return listaClientes;
        }

        public TerceroInterfazGraficaDTO FiltrarClientePorIdentificacion(long identificacionCliente, long identificacionEmpresa)
        {

            var clienteDTO = _clienteDAO.FiltrarClientesPorIdentificacion(identificacionCliente, identificacionEmpresa);
            clienteDTO.Sedes = _sedeDAO.ObtenerSedesDeTercero(identificacionCliente);

            return clienteDTO;
        }
    }
}
