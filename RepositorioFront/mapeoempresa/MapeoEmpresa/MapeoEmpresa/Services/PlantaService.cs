using EntidadesNegocio.InformacionVisita;
using EntidadesNegocio.ClasesDao;
using EntidadesNegocio.EntidadesDto;
using System.Numerics;
using MySqlConnector;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.Terceros;
using EntidadesNegocio.LugarProcedencia;

namespace MapeoEmpresa.Services
{
    public class PlantaService
    {
        public PlantaEmpresaClienteDTO PlantaSeleccionada { get; set; } //planta seleccionada para asociar un area
        public string ComponenteAnterior { get; set; } //referencia al componente razor anterior

        private PlantaEmpresaClienteDAO plantaDAO;

        //Dependencia del servicio MySqlConnection que se registró en program
        private readonly MySqlConnection _conexion;

        public PlantaService(MySqlConnection conexion)
        {
            _conexion = conexion;
            plantaDAO = new PlantaEmpresaClienteDAO(conexion);
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

        public PlantaEmpresaCliente ConvertirPlantaDTOaModelo(PlantaEmpresaClienteDTO plantaDTO)
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
                plantaDTO.emailInforme
            );
            return planta;
        }

        //LLAMADO A MÉTODOS DAO EN ENTIDADES NEGOCIO
        public async Task<List<AreaDTO>> ListarAreas()
        {
            List<AreasProceso> listaAreas = await plantaDAO.ListarAreas(PlantaSeleccionada.id);
            List<AreaDTO> listaDTO = listaAreas.Select(x => ConvertirDelModeloAlDTO(x)).ToList();
            return listaDTO;
        }

        public async Task<AreaDTO> BuscarArea(BigInteger id)
        {
            AreasProceso area = await plantaDAO.InformacionArea(id, PlantaSeleccionada.id);
            if (area != null)
            {
                AreaDTO areaDTO = ConvertirDelModeloAlDTO(area);
                return areaDTO;
            }

            return null;

        }

        public async Task BorrarAreasProceso(BigInteger id)
        {
            await plantaDAO.BorrarAreas(id);
        }

        public async Task EditarAreasProceso(AreaDTO areaDTO)
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

            await plantaDAO.EditarAreas(Area);

        }

        public async Task AgregarArea(AreaDTO areaDTO)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            AreasProceso nuevaArea = new AreasProceso(
            areaDTO.nombre,
            areaDTO.responsable,
            areaDTO.telefonoContacto,
            areaDTO.emailContacto,
            areaDTO.descripcion,
            ConvertirPlantaDTOaModelo(PlantaSeleccionada));

            await plantaDAO.AgregarArea(nuevaArea);

        }

        //agendamiento de visita para una planta

        public static VisitaDTO ConvertirAgendaDelModeloAlDTO(AgendaVisitaEmpresaCliente agenda) => new VisitaDTO
        {
            id = agenda.ObtenerId(),
            fechaCreacion = agenda.ObtenerFechaCreacion(),
            fechaInicio = agenda.ObtenerFechaInicio(),
            fechaFinaliza = agenda.ObtenerFechaFinaliza(),
            cantidadDias = agenda.ObtenerCantidadDias(),
            contactoEmpresa = agenda.ObtenerContactoEmpresa()
        };

        //LLAMADO A MÉTODOS DAO EN ENTIDADES NEGOCIO
        public List<VisitaDTO> ListarAgendas(int paginaActual, int elementosPorPagina)
        {
            List<VisitaDTO> listaAgendaDTO = plantaDAO.ListarVisitasAgendadas(PlantaSeleccionada.id, paginaActual,elementosPorPagina);
            return listaAgendaDTO;
        }

        public int ObtenerTotalVisitas()
        {
            return plantaDAO.ObtenerTotalVisitasAgendadas(PlantaSeleccionada.id);
        }

        public async Task<List<VisitaDTO>> BuscarPorFecha(DateOnly fecha, long idResponsable)
        {
            List<VisitaDTO> visita = await plantaDAO.InformacionVisitaPorFecha(fecha, idResponsable);
            return visita;

        }

        public List<VisitaDTO> BuscarPorEmpresa(String empresa, long idResponsable, int paginaActual, int elementosPorPagina)
        {
            List<VisitaDTO> visita = plantaDAO.InformacionVisitaPorEmpresa(empresa, idResponsable, paginaActual, elementosPorPagina);
            return visita;

        }

        public int ObtenerTotalVisitasPorEmpresa(String empresa, long idResponsable)
        {
            return plantaDAO.ObtenerTotalDatosVisitasPorEmpresa(empresa,idResponsable);
        }

        public async Task BorrarAgendas(BigInteger id)
        {
            await plantaDAO.BorrarVisitaAgendada(id);
        }

        public async Task EditarAgendas(VisitaDTO agendaDTO, TerceroInterfazGraficaDTO empleado)
        {
            if (empleado != null)
            {
                //Mapea las propiedades que vienen del DTO al modelo
                AgendaVisitaEmpresaCliente Agenda = new AgendaVisitaEmpresaCliente(
                agendaDTO.id,
                agendaDTO.fechaCreacion,
                agendaDTO.fechaInicio,
                agendaDTO.fechaFinaliza,
                agendaDTO.cantidadDias,
                agendaDTO.contactoEmpresa,
                ConvertirTerceroDTOaModelo(empleado));

                await plantaDAO.EditarVisitaAgendada(Agenda);
            }
            else
            {
                //Mapea las propiedades que vienen del DTO al modelo
                AgendaVisitaEmpresaCliente Agenda = new AgendaVisitaEmpresaCliente(
                agendaDTO.id,
                agendaDTO.fechaCreacion,
                agendaDTO.fechaInicio,
                agendaDTO.fechaFinaliza,
                agendaDTO.cantidadDias,
                agendaDTO.contactoEmpresa);

                await plantaDAO.EditarVisitaAgendada(Agenda);
            }
                

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


        public async Task AgregarAgenda(VisitaDTO agendaDTO, TerceroInterfazGraficaDTO empleado, long idEmpresa)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            AgendaVisitaEmpresaCliente nuevaAgenda = new AgendaVisitaEmpresaCliente(
            agendaDTO.fechaInicio,
            agendaDTO.fechaFinaliza,
            agendaDTO.cantidadDias,
            agendaDTO.contactoEmpresa,
            ConvertirPlantaDTOaModelo(PlantaSeleccionada),
            ConvertirTerceroDTOaModelo(empleado));

            await plantaDAO.AgendarVisita(nuevaAgenda,idEmpresa);

        }
    }
}
