using EntidadesNegocio.InformacionVisita;
using EntidadesNegocio.EntidadesDto;
using System.Numerics;
using EntidadesNegocio.ClasesDao;
using MySqlConnector;

namespace MapeoEmpresa.Services
{
    public class ComponenteService
    {
        public ComponenteDTO ComponenteSeleccionado { get; set; }//componente seleccionado para asociar un equipo

        public string ComponenteAnterior { get; set; } //referencia al componente razor anterior

        private ComponenteDAO componenteDAO;

        //Dependencia del servicio MySqlConnection que se registró en program
        private readonly MySqlConnection conexion_;

        public ComponenteService(MySqlConnection conexion)
        {
            conexion_ = conexion;
            componenteDAO = new ComponenteDAO(conexion);
        }

        public static EquipoDTO ConvertirDelModeloAlDTO(EquipoDelComponente equipo) => new EquipoDTO
        {
            id = equipo.ObtenerId(),
            nombre = equipo.ObtenerNombre(),
            descripcion = equipo.ObtenerDescripcion()
        };

        public Componente ConvertirComponenteDTOaModelo(ComponenteDTO componenteDTO)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            Componente componente = new Componente(
                componenteDTO.id,
                componenteDTO.nombre,
                componenteDTO.descripcion);

            return componente;
        }


        //LLAMADO A MÉTODOS DAO EN ENTIDADES NEGOCIO

        public async Task<List<EquipoDTO>> ListarEquipos()
        {
            List<EquipoDelComponente> listaEquipos = await componenteDAO.ListarEquipos(ComponenteSeleccionado.id);
            List<EquipoDTO> listaDTO = listaEquipos.Select(x => ConvertirDelModeloAlDTO(x)).ToList();
            return listaDTO;
        }

        public Task BorrarEquipos(BigInteger id)
        {
            return componenteDAO.BorrarEquipos(id);
        }

        public async Task EditarEquipo(EquipoDTO equipoDTO)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            EquipoDelComponente equipo = new EquipoDelComponente(
                equipoDTO.id,
                equipoDTO.nombre,
                equipoDTO.descripcion);

            await componenteDAO.EditarEquipo(equipo);

        }

        public async Task AgregarEquipo(EquipoDTO equipoDTO)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            EquipoDelComponente nuevoEquipo = new EquipoDelComponente(
                ConvertirComponenteDTOaModelo(ComponenteSeleccionado),
                equipoDTO.nombre,
                equipoDTO.descripcion);

            await componenteDAO.AgregarEquipo(nuevoEquipo);

        }

        public async Task<EquipoDTO> BuscarEquipo(BigInteger id)
        {
            EquipoDelComponente equipo = await componenteDAO.InformacionEquipo(id, ComponenteSeleccionado.id);
            if (equipo != null)
            {
                EquipoDTO EquipoDTO = ConvertirDelModeloAlDTO(equipo);
                return EquipoDTO;
            }
            return null;
        }


    }
}
