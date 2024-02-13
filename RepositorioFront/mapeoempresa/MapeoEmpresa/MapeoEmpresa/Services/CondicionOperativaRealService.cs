using EntidadesNegocio.ClasesDao;
using EntidadesNegocio.EntidadesDto;
using EntidadesNegocio.InformacionVisita;
using MySqlConnector;

namespace MapeoEmpresa.Services
{
    public class CondicionOperativaRealService
    {
        public CondicionRealDTO CondicionRealDTOSeleccionada { get; set; } //Condicion inicial seleccionada para asociar una condicion operativa

        private CondicionRealDAO condicionRealDAO;

        //Dependencia del servicio MySqlConnection que se registró en program
        private readonly MySqlConnection conexion_;

        public CondicionOperativaRealService(MySqlConnection conexion)
        {
            conexion_ = conexion;
            condicionRealDAO = new CondicionRealDAO(conexion);
        }
        public async Task<List<CondicionOperativaDTO>> ListarCondicionesOperativas()
        {
            List<CondicionOperativaDTO> listaDTO = await condicionRealDAO.ListarCondicionesOperativas(CondicionRealDTOSeleccionada.id);
            return listaDTO;
        }

        public async Task RegistrarCondicionOperativa(ValorDTO valorDTO, CondicionOperativaDTO condicionOperativaDTO)
        {
            //Creo un objeto de valor con la informacion recibida del DTO
            Valor valor = new Valor(
                valorDTO.valorFijo,
                valorDTO.rangoInicial,
                valorDTO.rangoFinal
            );

            //Creo un objeto de Medida elemento con los datos capturados de valor
            MedidaElemento medida = new MedidaElemento(valor, condicionOperativaDTO.unidad);

            //Creo un objeto de condicion operativa con los datos capturados y el objeto de unidad medida creado
            CondicionOperativa nuevaCondicionOperativa = new CondicionOperativa(
                condicionOperativaDTO.nombre,
                medida
            );

            await condicionRealDAO.AgregarCondicionOperativa(nuevaCondicionOperativa, CondicionRealDTOSeleccionada.id, condicionOperativaDTO.unidad.ObtenerCodigo());

        }
    }
}
