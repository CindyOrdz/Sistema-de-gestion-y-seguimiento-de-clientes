using EntidadesNegocio.ClasesDao.TercerosDAO;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;

namespace ServiciosComponentes.TercerosServices
{
    public class EmpleadoService
    {
        private readonly EmpleadoDAO _empleadoDAO;

        public EmpleadoService(EmpleadoDAO empleadoDAO)
        {
            _empleadoDAO = empleadoDAO;
        }


        public List<CargoEmpleadoDTO> ObtenerCargosDeEmpleado()
        {
            return _empleadoDAO.ObtenerCargosDeEmpleado();
        }

        public bool InsertarEmpleado(TerceroInterfazGraficaDTO empleado, long identificacionEmpresa)
        {

            return _empleadoDAO.InsertarEmpleado(empleado, identificacionEmpresa);
        }

        public List<TerceroInterfazGraficaDTO> FiltrarEmpleadosPorNombre(string nombre, long identificacionEmpresa)
        {
            return _empleadoDAO.FiltrarEmpleadosPorNombre(nombre, identificacionEmpresa);

        }

        public TerceroInterfazGraficaDTO FiltrarEmpleadoPorIdentificacion(long identificacion, long identificacionEmpresa)
        {

            return _empleadoDAO.FiltrarEmpleadoPorIdentificacion(identificacion, identificacionEmpresa);
        }

        public bool ActualizarEmpleado(TerceroInterfazGraficaDTO empleado, long identificacionEmpresa)
        {
            return _empleadoDAO.ActualizarEmpleado(empleado, identificacionEmpresa);

        }

        public List<EmpleadoInterfazGraficaVentaDTO> ObtenerEmpleadosDeEmpresa(long identificacionEmpresa)
        {
            return _empleadoDAO.ObtenerEmpleadosDeEmpresa(identificacionEmpresa);
        }
    }
}
