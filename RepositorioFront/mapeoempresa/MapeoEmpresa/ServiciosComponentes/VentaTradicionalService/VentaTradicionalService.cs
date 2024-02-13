

using EntidadesNegocio.ClasesDao.Ventas;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Venta;

namespace ServiciosComponentes.VentaTradicionalService
{
    public class VentaTradicionalService
    {
        private readonly VentaTradicionalDAO _ventaTradicionalDAO;
        public VentaTradicionalService(VentaTradicionalDAO ventaTradicionalDAO)
        {
            _ventaTradicionalDAO = ventaTradicionalDAO;
        }

        public bool InsertarVentaTradicional(VentaInterfazGraficaVentaDTO ventaDTO)
        {
            ventaDTO.DetallesVenta = ventaDTO.DetallesVenta.Where(det => det.Item != ventaDTO.DetallesVenta.Count).ToList();
            return _ventaTradicionalDAO.InsertarVentaTradicional(ventaDTO);
        }

        public List<ElementoVendidoClienteDTO> ObtenerElementosVendidosAlCliente(long idEmpresa, long idCliente, int paginaActual, int elementosPorPagina)
        {

            return _ventaTradicionalDAO.ObtenerElementosVendidosAlCliente(idEmpresa, idCliente, paginaActual, elementosPorPagina);
        }


        public List<ElementoVendidoClienteDTO> ObtenerElementosVendidosAlClientePorIdElemento(long idEmpresa, long idCliente, int paginaActual, int elementosPorPagina, long idElemento)
        {
            return _ventaTradicionalDAO.ObtenerElementosVendidosAlClientePorIdElemento(idEmpresa, idCliente, paginaActual, elementosPorPagina, idElemento);
        }
        public int ObtenerTotalVentasCliente(long idEmpresa, long idCliente)
        {

            return _ventaTradicionalDAO.ObtenerTotalVentasCliente(idEmpresa, idCliente);
        }

        public int ObtenerTotalDetallesVentaCliente(long idEmpresa, long idCliente)
        {

            return _ventaTradicionalDAO.ObtenerTotalDetallesVentaCliente(idEmpresa, idCliente);
        }

        public int ObtenerTotalVentasClientePorElemento(long idEmpresa, long idCliente, long idElemento)
        {
            return _ventaTradicionalDAO.ObtenerTotalVentasClientePorElemento(idEmpresa, idCliente, idElemento);
        }

        public int ObtenerTotalHistorialVentasPorElemento(long idEmpresa, long idElemento)
        {
            return _ventaTradicionalDAO.ObtenerTotalHistorialVentasPorElemento(idEmpresa,idElemento);
        }

        public List<VentaHistorialClienteDTO> ObtenerHistorialCliente(long idEmpresa, long idCliente, int paginaActual, int elementosPorPagina)
        {
            var historialCliente = _ventaTradicionalDAO.ObtenerHistorialCliente(idEmpresa,idCliente, paginaActual,elementosPorPagina);

            historialCliente.ForEach(hist =>
            {
                hist.Detalles = _ventaTradicionalDAO.ObtenerDetallesVenta(hist.IdVenta);
                hist.Detalles.ForEach( det =>
                {
                    det.Total = det.Cantidad * det.Elemento.Valor;
                
                });
            
            });

            return historialCliente;
        }

        public List<HistorialVentasPorElementoDTO> ObtenerHistorialPorElemento(long idEmpresa, int paginaActual, int elementosPorPagina, long idElemento)
        {
            return _ventaTradicionalDAO.ObtenerHistorialPorElemento(idEmpresa, paginaActual, elementosPorPagina, idElemento);
        
        }

        public bool ConvertirTipoVenta(long idVentaTransaccion, string TipoVenta, long idEmpresa)
        {
            return _ventaTradicionalDAO.ConvertirTipoVenta(idVentaTransaccion, TipoVenta, idEmpresa);
        }

        public bool AnularVenta(long idVentaTransaccion, DateOnly fechaAnulacion)
        {
            return _ventaTradicionalDAO.AnularVenta(idVentaTransaccion, fechaAnulacion);
        }

        public long GenerarIdVenta()
        {
            return _ventaTradicionalDAO.GenerarIdVenta();
        }
    }
}
