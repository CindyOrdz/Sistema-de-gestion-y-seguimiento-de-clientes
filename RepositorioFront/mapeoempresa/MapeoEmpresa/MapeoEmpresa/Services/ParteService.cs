using EntidadesNegocio.ClasesDao;
using EntidadesNegocio.EntidadesDto;
using EntidadesNegocio.InformacionVisita;
using MySqlConnector;
using System.Numerics;

namespace MapeoEmpresa.Services
{
    public class ParteService
    {
        public string ComponenteAnterior { get; set; } //referencia al componente razor anterior
        public ParteDTO ParteDTOSeleccionada { get; set; }//parte seleccionada para asociar condiciones

        private ParteDAO parteDAO;

        //Dependencia del servicio MySqlConnection que se registró en program
        private readonly MySqlConnection conexion_;

        public ParteService(MySqlConnection conexion)
        {
            conexion_ = conexion;
            parteDAO = new ParteDAO(conexion);
        }

        public async Task AdicionarCondicionInicial(CondicionInicialDTO condicionInicialDTO)
        {
            await parteDAO.AgregarCondicionInicial(condicionInicialDTO, ParteDTOSeleccionada.Id);
        }

        public List<CondicionInicialDTO> ListarCondicionesIniciales()
        {
            List<CondicionInicialDTO> listaDTO = parteDAO.ListarCondicionesIniciales(ParteDTOSeleccionada.Id);
            return listaDTO;
        }

        public Task BorrarCondicionInicial(BigInteger id)
        {
            return parteDAO.BorrarCondicionInicial(id);
        }

        public async Task EditarCondicionInicial(CondicionInicialDTO condicion)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            CondicionOperativaInicial condicionInicial = new CondicionOperativaInicial(
                condicion.id,
                condicion.descripcion);

            await parteDAO.EditarCondicionInicial(condicionInicial);

        }

        public static CondicionInicialDTO ConvertirDelModeloAlDTO(CondicionOperativaInicial condicionInicial) => new CondicionInicialDTO
        {
            id = condicionInicial.ObtenerId(),
            responsable = condicionInicial.ObtenerResponsable(),
            descripcion = condicionInicial.ObtenerDescripcion()
        };

        public async Task<CondicionInicialDTO> BuscarCondicionInicial(BigInteger id)
        {
            CondicionOperativaInicial inicial = await parteDAO.InformacionCondicionInicial(id, ParteDTOSeleccionada.Id);
            if (inicial != null)
            {
                CondicionInicialDTO inicialDTO = ConvertirDelModeloAlDTO(inicial);
                return inicialDTO;
            }

            return null;
        }

        public async Task<List<ParteDTO>> ListarPartesRegistroCondicionesVisita(String nombre, BigInteger idPlanta)
        {
            List<ParteDTO> listaDTO = await parteDAO.BuscarPartesParaRegistrarCondicionesEnVisita(nombre, idPlanta);
            return listaDTO;
        }

        public List<CondicionInicialDTO> ObtenerCondicionesInicialesConOperativas(BigInteger idParte)
        {
            var condiciones = parteDAO.ListarCondicionesIniciales(idParte);

            condiciones.ForEach(cond =>
            {
                cond.condicionesOperativas = parteDAO.ObtenerCondicionesOperativas(cond.id);
            });

            return condiciones;
        }

        //Condiciones reales

        public List<CondicionRealDTO> ObtenerCondicionesRealesConOperativas(BigInteger idParte)
        {
            var condiciones = parteDAO.ListarCondicionesReales(idParte);

            condiciones.ForEach(cond =>
            {
                cond.condicionesOperativas = parteDAO.ObtenerCondicionesOperativasReales(cond.id);
            });

            return condiciones;
        }

        public async Task AdicionarCondicionReal(CondicionRealDTO condicionRealDTO)
        {
            await parteDAO.AgregarCondicionReal(condicionRealDTO, ParteDTOSeleccionada.Id);
        }

        public List<CondicionRealDTO> ListarCondicionesReales()
        {
            List<CondicionRealDTO> listaDTO = parteDAO.ListarCondicionesReales(ParteDTOSeleccionada.Id);
            return listaDTO;
        }

        public Task BorrarCondicionReal(BigInteger id)
        {
            return parteDAO.BorrarCondicionReal(id);
        }

        public async Task EditarCondicionReal(CondicionRealDTO condicion)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            CondicionOperativaReal condicionReal = new CondicionOperativaReal(
                condicion.id,
                condicion.descripcion);

            await parteDAO.EditarCondicionReal(condicionReal);

        }

        public static CondicionRealDTO ConvertirDelModeloAlDTO(CondicionOperativaReal condicionReal) => new CondicionRealDTO
        {
            id = condicionReal.ObtenerId(),
            responsable = condicionReal.ObtenerResponsable(),
            descripcion = condicionReal.ObtenerDescripcion()
        };

        public async Task<CondicionRealDTO> BuscarCondicionReal(BigInteger id)
        {
            CondicionOperativaReal real = await parteDAO.InformacionCondicionReal(id, ParteDTOSeleccionada.Id);
            if (real != null)
            {
                CondicionRealDTO realDTO = ConvertirDelModeloAlDTO(real);
                return realDTO;
            }

            return null;
        }
    }
}
