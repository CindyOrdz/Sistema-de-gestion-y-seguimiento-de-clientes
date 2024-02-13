using EntidadesNegocio.ClasesDao;
using EntidadesNegocio.ClasesDao.Ventas;
using EntidadesNegocio.EntidadesDto;
using EntidadesNegocio.InformacionVisita;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Venta;
using MySqlConnector;
using System.Numerics;

namespace MapeoEmpresa.Services
{
    public class VisitaService
    {
        private VisitaDAO visitaDAO;
        public VisitaDTO VisitaSeleccionadaAsociarVenta { get; set; } //visita seleccionada para asociar venta
        public SedeDTO SedeSeleccionada { get; set; } //sede seleccionada para crear visita

        //Dependencia del servicio MySqlConnection que se registró en program
        private readonly MySqlConnection conexion_;

        public VisitaService(MySqlConnection conexion)
        {
            conexion_ = conexion;
            visitaDAO = new VisitaDAO(conexion);
        }

        public List<PlantaEmpresaClienteDTO> ListarSedesPlantas(BigInteger idSede)
        {
            List<PlantaEmpresaClienteDTO> listaDTO = visitaDAO.ListarPlantasPorIdSede(idSede);
            return listaDTO;
        }

        public List<CabeceraHistorialDTO> ObtenerHistorial(long idEmpresa, double idCliente, int paginaActual, int elementosPorPagina)
        {
            var historialCliente = visitaDAO.ObtenerHistorial(idEmpresa, idCliente, paginaActual, elementosPorPagina);

            historialCliente.ForEach(hist =>
            {
                hist.Detalles = visitaDAO.ObtenerDetallesVenta(hist.IdVenta);
                hist.Detalles.ForEach(det =>
                {
                    det.Total = det.Cantidad * det.Elemento.Valor;

                });

            });

            return historialCliente;
        }

        public CabeceraHistorialDTO ObtenerVentaAsociada(long idVenta, long idEmpresa)
        {
            var cabeceraVenta = visitaDAO.ObtenerCabecerVentaAsociada(idVenta, idEmpresa);
            cabeceraVenta.Detalles = visitaDAO.ObtenerDetallesVenta(cabeceraVenta.IdVenta);
            cabeceraVenta.Detalles.ForEach(det =>
            {
                det.Total = det.Cantidad * det.Elemento.Valor;

            });

            return cabeceraVenta;
        }

        public int ObtenerTotalVentasCliente(long idEmpresa, double idCliente)
        {

            return visitaDAO.ObtenerTotalVentasCliente(idEmpresa, idCliente);
        }

        public int VerificarVentaAsociada(long idVenta)
        {

            return visitaDAO.VerificarVenta(idVenta);
        }

        public void AsociarVenta(long idVenta, BigInteger idVisita)
        {

            visitaDAO.AsociarVentaAVisita(idVenta, idVisita);
        }

        public async Task FinalizarVisita(BigInteger id)
        {
            await visitaDAO.FinalizarVisita(id);
        }

        //INFORMES
        public (long, long) ObtenerDatosGraficaVisitas(long idEmpresa)
        {
            return visitaDAO.ObtenerDatosParaGrafica(idEmpresa);
        }

        public async Task<List<VisitaDTO>> VisitasPorResponsable(long idEmpresa)
        {
            List<VisitaDTO> listaDTO = await visitaDAO.CantidadVisitasPorResponsable(idEmpresa);
            return listaDTO;
        }

        public VisitaDTO ObtenerVisitaPorID(BigInteger idVisita)
        {
            VisitaDTO visitaDTO = visitaDAO.InformacionVisitaPorID(idVisita);
            return visitaDTO;
        }

        public async Task<List<ElementoInventarioDTO>> ObtenerElementosInspeccionados(long idEmpresa)
        {
            List<ElementoInventarioDTO> listaDTO = await visitaDAO.ElementosRevisadosMesActual(idEmpresa);
            return listaDTO;
        }

    }
}
