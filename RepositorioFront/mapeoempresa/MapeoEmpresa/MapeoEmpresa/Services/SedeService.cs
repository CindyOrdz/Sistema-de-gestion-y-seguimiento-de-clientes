using EntidadesNegocio.InformacionVisita;
using EntidadesNegocio.LugarProcedencia;
using EntidadesNegocio.Terceros;
using EntidadesNegocio.EntidadesDto;
using System.Numerics;
using EntidadesNegocio.ClasesDao;
using MySqlConnector;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;

namespace MapeoEmpresa.Services
{
    public class SedeService
    {
        public SedeInterfazGraficaTerceroDTO SedeSeleccionada { get; set; } //sede seleccionada para asociar una planta
        public TerceroInterfazGraficaDTO EmpresaSeleccionada { get; set; }

        private SedeDAO sedeDAO;

        //Dependencia del servicio MySqlConnection que se registró en program
        private readonly MySqlConnection conexion_;

        public SedeService(MySqlConnection conexion)
        {
            conexion_ = conexion;
            sedeDAO = new SedeDAO(conexion);
        }

        public static PlantaEmpresaClienteDTO ConvertirDelModeloAlDTO(PlantaEmpresaCliente planta) => new PlantaEmpresaClienteDTO
        {
            id = planta.ObtenerId(),
            nombre = planta.ObtenerNombre(),
            direccion = planta.ObtenerDireccion(),
            contacto = planta.ObtenerContacto(),
            telefono1 = planta.ObtenerTelefono1(),
            telefono2 = planta.ObtenerTelefono2(),
            emailContacto = planta.ObtenerEmailContacto(),
            emailInforme = planta.ObtenerEmailParaInforme()
        };

        public Sede ConvertirSedeDTOaModelo(SedeInterfazGraficaTerceroDTO sedeDTO)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            Sede sede = new Sede(
                sedeDTO.Id, 
                sedeDTO.Responsable,
                sedeDTO.Telefono,
                sedeDTO.Email1,
                sedeDTO.Email2
            );
            return sede;
        }

        public Tercero ConvertirTerceroDTOaModelo(TerceroInterfazGraficaDTO terceroDTO)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            Tercero tercero = new Tercero(
                terceroDTO.Identificacion,
                terceroDTO.Digito,
                terceroDTO.Digito,
                terceroDTO.RazonSocial,
                terceroDTO.Nombre1,
                terceroDTO.Nombre2,
                terceroDTO.Apellido1,
                terceroDTO.Apellido2,
                new Ubicacion(),
                terceroDTO.Telefono,
                terceroDTO.Email,
                terceroDTO.Celular,
                terceroDTO.TipoTercero,
                terceroDTO.Direccion
            );
            return tercero;
        }

        //LLAMADO A MÉTODOS DAO EN ENTIDADES NEGOCIO

        public async Task<List<PlantaEmpresaClienteDTO>> ListarPlantas()
        {
            List<PlantaEmpresaCliente> listaPlantas = await sedeDAO.ListarPlantas(SedeSeleccionada.Id); 
            List<PlantaEmpresaClienteDTO> listaDTO = listaPlantas.Select(x => ConvertirDelModeloAlDTO(x)).ToList();
            return listaDTO;
        }

        public async Task AgregarPlanta(PlantaEmpresaClienteDTO plantaDTO)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            PlantaEmpresaCliente nuevaPlanta = new PlantaEmpresaCliente(
            plantaDTO.nombre,
            plantaDTO.direccion,
            ConvertirTerceroDTOaModelo(EmpresaSeleccionada),
            plantaDTO.contacto,
            plantaDTO.telefono1,
            plantaDTO.telefono2,
            plantaDTO.emailContacto,
            plantaDTO.emailInforme,
            ConvertirSedeDTOaModelo(SedeSeleccionada));

            await sedeDAO.AgregarPlanta(nuevaPlanta);
        }

        public async Task<PlantaEmpresaClienteDTO> BuscarPlantas(BigInteger id)
        {
            PlantaEmpresaCliente planta = await sedeDAO.InformacionPlanta(id, SedeSeleccionada.Id);
            if (planta != null)
            {
                PlantaEmpresaClienteDTO plantaDTO = ConvertirDelModeloAlDTO(planta);
                return plantaDTO;
            }
            return null;
        }

        public async Task BorrarPlanta(BigInteger id)
        {
            await sedeDAO.BorrarPlantas(id);
        }

        public async Task EditarPlanta(PlantaEmpresaClienteDTO plantaDTO)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            PlantaEmpresaCliente planta = new PlantaEmpresaCliente(
                plantaDTO.id,
                plantaDTO.nombre,
                plantaDTO.direccion,
                plantaDTO.contacto,
                plantaDTO.telefono1,
                plantaDTO.telefono2,
                plantaDTO.emailContacto,
                plantaDTO.emailInforme);

            await sedeDAO.EditarPlantas(planta);

        }


    }
}
