using EntidadesNegocio.ClasesDao.TercerosDAO;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;

namespace ServiciosComponentes.TercerosServices
{
    public class SedeServices
    {
        private readonly SedeDAO _sedeDAO;
        public SedeServices(SedeDAO sedeDAO)
        {
            _sedeDAO = sedeDAO;
        }

        public int ActualizarSede(SedeInterfazGraficaTerceroDTO sedeDTO)
        {

            return _sedeDAO.ActualizarSede(sedeDTO);
        }

        public List<SedeInterfazGraficaTerceroDTO> ObtenerSedesDeTercero(long identificacion)
        {
            return _sedeDAO.ObtenerSedesDeTercero(identificacion);
        }

        public int InsertarSede(SedeInterfazGraficaTerceroDTO sedeDTO, long identificacionTercero)
        {

            return _sedeDAO.InsertarSede(sedeDTO, identificacionTercero);
        }

    }
}
