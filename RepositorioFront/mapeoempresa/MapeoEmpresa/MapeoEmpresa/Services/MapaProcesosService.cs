using EntidadesNegocio.ClasesDao;
using EntidadesNegocio.ClasesDao.TercerosDAO;
using EntidadesNegocio.EntidadesDto;
using EntidadesNegocio.InformacionVisita;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.Terceros;
using MySqlConnector;
using System.Numerics;

namespace MapeoEmpresa.Services
{
    public class MapaProcesosService
    {
        private MapaDeProcesosDAO mapaDAO;

        //Dependencia del servicio MySqlConnection que se registró en program
        private readonly MySqlConnection conexion_;

        public MapaProcesosService(MySqlConnection conexion)
        {
            conexion_ = conexion;
            mapaDAO = new MapaDeProcesosDAO(conexion);
        }
        public static PlantaEmpresaClienteDTO ConvertirDelModeloAlDTO(PlantaEmpresaCliente planta) => new PlantaEmpresaClienteDTO
        {
            id = planta.ObtenerId(),
            nombre = planta.ObtenerNombre(),
            contacto = planta.ObtenerContacto(),
            telefono1 = planta.ObtenerTelefono1(),
            telefono2 = planta.ObtenerTelefono2(),
            emailContacto = planta.ObtenerEmailContacto(),
            emailInforme = planta.ObtenerEmailParaInforme()
        };

        
        //LLAMADO A MÉTODOS DAO EN ENTIDADES NEGOCIO

        public async Task<List<PlantaEmpresaClienteDTO>> ListarPlantas(BigInteger id)
        {
            List<PlantaEmpresaCliente> listaPlantas = await mapaDAO.ListarPlantas(id);
            List<PlantaEmpresaClienteDTO> listaDTO = listaPlantas.Select(x => ConvertirDelModeloAlDTO(x)).ToList();
            return listaDTO;
        }

        public static AreaDTO ConvertirDelModeloAlDTO(AreasProceso area) => new AreaDTO
        {
            id = area.ObtenerId(),
            nombre = area.ObtenerNombre(),
            responsable = area.ObtenerResponsable(),
            telefonoContacto = area.ObtenerTelefonoContacto(),
            emailContacto = area.ObtenerEmailContacto(),
            descripcion = area.ObtenerDescripcion()
        };

        public async Task<List<AreaDTO>> ListarAreas(BigInteger id)
        {
            List<AreasProceso> listaAreas = await mapaDAO.ListarAreas(id);
            List<AreaDTO> listaDTO = listaAreas.Select(x => ConvertirDelModeloAlDTO(x)).ToList();
            return listaDTO;
        }

        public static ComponenteDTO ConvertirDelModeloAlDTO(Componente componente) => new ComponenteDTO
        {
            id = componente.ObtenerId(),
            nombre = componente.ObtenerNombre(),
            descripcion = componente.ObtenerDescripcion()
        };

        public async Task<List<ComponenteDTO>> ListarComponentes(BigInteger id)
        {
            List<Componente> listaComponentes = await mapaDAO.ListarComponentes(id);
            List<ComponenteDTO> listaDTO = listaComponentes.Select(x => ConvertirDelModeloAlDTO(x)).ToList();
            return listaDTO;
        }

        public static EquipoDTO ConvertirDelModeloAlDTO(EquipoDelComponente equipo) => new EquipoDTO
        {
            id = equipo.ObtenerId(),
            nombre = equipo.ObtenerNombre(),
            descripcion = equipo.ObtenerDescripcion()
        };


        public async Task<List<EquipoDTO>> ListarEquipos(BigInteger id)
        {
            List<EquipoDelComponente> listaEquipos = await mapaDAO.ListarEquipos(id);
            List<EquipoDTO> listaDTO = listaEquipos.Select(x => ConvertirDelModeloAlDTO(x)).ToList();
            return listaDTO;
        }

        public static SedeInterfazGraficaTerceroDTO ConvertirDelModeloAlDTO(Sede sede) => new SedeInterfazGraficaTerceroDTO
        {
            Id = sede.ObtenerId(),
            Responsable = sede.ObtenerResponsable(),
            Telefono = sede.ObtenerTelefono(),
            Email1 = sede.ObtenerEmail1(),
            Email2 = sede.ObtenerEmail2()
        };

        public async Task<List<SedeInterfazGraficaTerceroDTO>> ListarSedes(long id)
        {
            List<Sede> listaSedes = await mapaDAO.ListarSedes(id);
            List<SedeInterfazGraficaTerceroDTO> listaDTO = listaSedes.Select(x => ConvertirDelModeloAlDTO(x)).ToList();
            return listaDTO;
        }

        public async Task<TerceroInterfazGraficaDTO> BuscarEmpresa(BigInteger idCliente, long idEmpresaLogueada)
        {
            TerceroInterfazGraficaDTO empresa = await mapaDAO.InformacionEmpresa(idCliente, idEmpresaLogueada);
            if (empresa != null)
            {
                return empresa;
            }
            return null;
        }

        public async Task<List<ParteDTO>> ListarPartes(BigInteger id)
        {
            List<ParteDTO> listaDTO = await mapaDAO.ListarPartes(id);
            return listaDTO;
        }
    }
}
