using EntidadesNegocio.ClasesDao.ImpuestosYDescuentosDAO;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.ImpuestosYDescuentos;

namespace ServiciosComponentes.ImpuestosYDescuentosServices
{

    public class DescuentoService
    {
        private readonly DescuentoDAO _descuentoDAO;
        public DescuentoService(DescuentoDAO descuentoDAO)
        {
            _descuentoDAO= descuentoDAO;    
        }

        public List<DescuentoInterfazGraficaVentaDTO> ObtenerDescuentosParaClientes()
        {

            return _descuentoDAO.ObtenerDescuentosParaClientes();
        
        }
    }
}
