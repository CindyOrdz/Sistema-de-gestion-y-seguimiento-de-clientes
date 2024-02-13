using EntidadesNegocio.InformacionVisita;
using EntidadesNegocio.EntidadesDto;
using System.Numerics;
using EntidadesNegocio.ClasesDao;
using MySqlConnector;

namespace MapeoEmpresa.Services
{
    public class AreaService
    {
        public AreaDTO AreaSeleccionada { get; set; }//area seleccionada para asociar un componente
        public string ComponenteAnterior { get; set; } //referencia al componente razor anterior

        private AreaDAO areaDAO;

        //Dependencia del servicio MySqlConnection que se registró en program
        private readonly MySqlConnection conexion_;

        public AreaService(MySqlConnection conexion)
        {
            conexion_ = conexion;
            areaDAO = new AreaDAO(conexion);
        }

        public static ComponenteDTO ConvertirDelModeloAlDTO(Componente componente) => new ComponenteDTO
        {
            id = componente.ObtenerId(),
            nombre = componente.ObtenerNombre(),
            descripcion = componente.ObtenerDescripcion()
        };

        public AreasProceso ConvertirAreaDTOaModelo(AreaDTO areaDTO)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            AreasProceso Area = new AreasProceso(
                areaDTO.id,
                areaDTO.nombre,
                areaDTO.responsable,
                areaDTO.telefonoContacto,
                areaDTO.emailContacto,
                areaDTO.descripcion
            );
            return Area;
        }


        //LLAMADO A MÉTODOS DAO EN ENTIDADES NEGOCIO

        public async Task<List<ComponenteDTO>> ListarComponentes()
        {
            List<Componente> listaComponentes = await areaDAO.ListarComponentes(AreaSeleccionada.id);
            List<ComponenteDTO> listaDTO = listaComponentes.Select(x => ConvertirDelModeloAlDTO(x)).ToList();
            return listaDTO;
        }

        public Task BorrarComponentes(BigInteger id)
        {
            return areaDAO.BorrarComponentes(id);
        }

        public async Task EditarComponentes(ComponenteDTO componenteDTO)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            Componente componente = new Componente(
                componenteDTO.id,
                componenteDTO.nombre,
                componenteDTO.descripcion);
            
            await areaDAO.EditarComponentes(componente);

        }

        public async Task AgregarComponente(ComponenteDTO componenteDTO)
        {

            //Mapea las propiedades que vienen del DTO al modelo
            Componente nuevoComponente = new Componente(
               componenteDTO.nombre,
               ConvertirAreaDTOaModelo(AreaSeleccionada),
               componenteDTO.descripcion);

            await areaDAO.AgregarComponente(nuevoComponente);

        }

        public async Task<ComponenteDTO> BuscarComponente(BigInteger id)
        {
            Componente componente = await areaDAO.InformacionComponente(id, AreaSeleccionada.id);
            if(componente != null)
            {
                ComponenteDTO componenteDTO = ConvertirDelModeloAlDTO(componente);
                return componenteDTO;
            }
            return null;
        }

    }
}
