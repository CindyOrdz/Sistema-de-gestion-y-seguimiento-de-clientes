using EntidadesNegocio.ElementosInventario;
using EntidadesNegocio.InformacionVisita;
using EntidadesNegocio.EntidadesDto;
using System.Numerics;
using EntidadesNegocio.ClasesDao;
using MySqlConnector;
using EntidadesNegocio.Terceros;

namespace MapeoEmpresa.Services
{
    public class CondicionOperativaInicialService
    {
        public string ComponenteAnterior { get; set; } //referencia al componente razor anterior
        public CondicionInicialDTO CondicionInicialDTOSeleccionada { get; set; } //Condicion inicial seleccionada para asociar una condicion operativa
        
        private CondicionInicialDAO condicionInicialDAO;

        //Dependencia del servicio MySqlConnection que se registró en program
        private readonly MySqlConnection conexion_;

        public CondicionOperativaInicialService(MySqlConnection conexion)
        {
            conexion_ = conexion;
            condicionInicialDAO = new CondicionInicialDAO(conexion);
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

            await condicionInicialDAO.AgregarCondicionOperativa(nuevaCondicionOperativa,CondicionInicialDTOSeleccionada.id, condicionOperativaDTO.unidad.ObtenerCodigo());

        }

        public async Task <List<CondicionOperativaDTO>> ListarCondicionesOperativas()
        {
            List<CondicionOperativaDTO> listaDTO = await condicionInicialDAO.ListarCondicionesOperativas(CondicionInicialDTOSeleccionada.id);
            return listaDTO;
        }

        public static CondicionOperativaDTO ConvertirDelModeloAlDTO(CondicionOperativa condicion) => new CondicionOperativaDTO
        {
            id = condicion.ObtenerId(),
            nombre = condicion.ObtenerNombre(),
            medidaElemento = condicion.ObtenerMedidaElemento()
        };

        public async Task BorrarCondicionOperativa(BigInteger id)
        {
            await condicionInicialDAO.BorrarCondicionOperativa(id);
        }

        public async Task EditarCondicionOperativa(CondicionOperativaDTO condicion)
        {
            await condicionInicialDAO.EditarCondicionOperativa(condicion);
        }

        public static UnidadDTO ConvertirDelModeloAlDTO(Unidad unidad) => new UnidadDTO
        {
            codigo = unidad.ObtenerCodigo(),
            descripcion = unidad.ObtenerDescripcion()
        };

        public Unidad ConvertirUnidadDTOaModelo(UnidadDTO unidadDTO)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            Unidad unidad = new Unidad(
                unidadDTO.codigo,
                unidadDTO.descripcion
            );
            return unidad;
        }

        public async Task<List<UnidadDTO>> ListarUnidades()
        {
            List<Unidad> listaUnidades = await condicionInicialDAO.ListarUnidades();
            List<UnidadDTO> listaDTO = listaUnidades.Select(x => ConvertirDelModeloAlDTO(x)).ToList();
            return listaDTO;
        }

        public Unidad BuscarUnidad(String codigoBusqueda, List<UnidadDTO> listaDTO)
        {
            UnidadDTO unidad = listaDTO.Find(p => p.codigo == codigoBusqueda);
            return ConvertirUnidadDTOaModelo(unidad);
        }
    }
}
