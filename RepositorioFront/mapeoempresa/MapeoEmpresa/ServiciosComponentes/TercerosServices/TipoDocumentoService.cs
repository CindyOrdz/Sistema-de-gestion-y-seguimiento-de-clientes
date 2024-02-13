using EntidadesNegocio.ClasesDao.TercerosDAO;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;

namespace ServiciosComponentes.TercerosServices
{
    public class TipoDocumentoService
    {

        private readonly TiposDocumentoDAO _tiposDocumentoDAO;
        public TipoDocumentoService(TiposDocumentoDAO tiposDocumentoDAO)
        {
            _tiposDocumentoDAO = tiposDocumentoDAO;
        }

        public List<TipoDocumentoInterfazGraficaTercerosDTO> ObtenerTiposDocumento()
        {
            return _tiposDocumentoDAO.ObtenerTiposDocumento();
        }

    }
}
