using EntidadesNegocio.ClasesDao;
using EntidadesNegocio.EntidadesDto;
using EntidadesNegocio.GestionInventario;
using EntidadesNegocio.InformacionVisita;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Inventario;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.LugarProcedencia;
using EntidadesNegocio.Terceros;
using MySqlConnector;
using System.Numerics;

namespace MapeoEmpresa.Services
{
    public class RevisionInventarioService
    {
        private RevisionInventarioDAO inventarioDAO;
        public RevisionInventarioDTO RevisionSeleccionada { get; set; }

        //Dependencia del servicio MySqlConnection que se registró en program
        private readonly MySqlConnection conexion_;

        public RevisionInventarioService(MySqlConnection conexion)
        {
            conexion_ = conexion;
            inventarioDAO = new RevisionInventarioDAO(conexion);
        }

        public List<ElementoInventarioDTO> ObtenerInventario(long idEmpresaLogueada,int paginaActual, int elementosPorPagina)
        {
            List<ElementoInventarioDTO> listaDTO = inventarioDAO.ListarInventario(idEmpresaLogueada,paginaActual,elementosPorPagina);
            return listaDTO;
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


        public async Task AgendarRevision(RevisionInventarioDTO revisionDTO, TerceroInterfazGraficaDTO empleado)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            RevisionInventario revision = new RevisionInventario(
            revisionDTO.fechaCreacion, 
            revisionDTO.fechaInicio,
            ConvertirTerceroDTOaModelo(empleado), 
            revisionDTO.descripcion,
            revisionDTO.elementosRevisados);

            await inventarioDAO.ProgramarRevision(revision);

        }

        public List<RevisionInventarioDTO> ListarRevisiones(long idEmpresaLogueada,int paginaActual, int elementosPorPagina)
        {
            List<RevisionInventarioDTO> listaDTO = inventarioDAO.ListarRevisionesInventario(idEmpresaLogueada,paginaActual,elementosPorPagina);
            return listaDTO;
        }

        public int ObtenerTotaRevisiones(long idEmpresaLogueada)
        {
            return inventarioDAO.ObtenerTotalRevisionesAgendadas(idEmpresaLogueada);
        }

        public Task BorrarRevision(BigInteger id)
        {
            return inventarioDAO.BorrarRevisionProgramada(id);
        }

        public async Task EditarRevision(RevisionInventarioDTO revisionDTO, TerceroInterfazGraficaDTO empleado)
        {
            if (empleado != null)
            {
                //Mapea las propiedades que vienen del DTO al modelo
                RevisionInventario revisionInventario = new RevisionInventario(
                    revisionDTO.id,
                    revisionDTO.fechaInicio,
                    ConvertirTerceroDTOaModelo(empleado),
                    revisionDTO.descripcion);

                await inventarioDAO.EditarRevisionConResponsable(revisionInventario);
            }
            else
            {
                //Mapea las propiedades que vienen del DTO al modelo
                RevisionInventario revisionInventario = new RevisionInventario(
                    revisionDTO.id,
                    revisionDTO.fechaInicio,
                    revisionDTO.descripcion);

                await inventarioDAO.EditarRevisionSinResponsable(revisionInventario);

            }

        }

        public async Task<List<RevisionInventarioDTO>> BuscarPorFecha(DateOnly fecha, long idResponsable)
        {
            List<RevisionInventarioDTO> revision = await inventarioDAO.InformacionRevisionPorFecha(fecha, idResponsable);
            return revision;

        }

        public async Task<List<RevisionInventarioDTO>> BuscarPorResponsable(String responsable)
        {
            List<RevisionInventarioDTO> revision = await inventarioDAO.InformacionRevisionPorResponsable(responsable);
            return revision;

        }


        public async Task<List<ElementoInventarioDTO>> ObtenerElementosRevision()
        {
            List<ElementoInventarioDTO> listaDTO = await inventarioDAO.ListarElementosRevision(RevisionSeleccionada.id);
            return listaDTO;
        }

        public async Task ActualizarBitacora(BitacoraInventarioDTO bitacora)
        {
            await inventarioDAO.RegistrarBitacora(bitacora);

        }

        public async Task EditarCantidad(BigInteger codigo, Double nuevaCantidad)
        {
            await inventarioDAO.EditarCantidadDisponible(codigo, nuevaCantidad);
        }

        public async Task<int> GuardarAnomalia(AnomaliaInventarioDTO anomaliaDTO)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            AnomaliaRevisionInventario anomalia = new AnomaliaRevisionInventario(
                anomaliaDTO.Descripcion,
                anomaliaDTO.Revision,
                anomaliaDTO.Elemento);


            return await inventarioDAO.RegistrarAnomalia(anomalia);

        }

        public async Task<List<AnomaliaInventarioDTO>> ObtenerEvidenciaAnomalias()
        {
            List<AnomaliaInventarioDTO> listaDTO = await inventarioDAO.ListarEvidenciaAnomaliasPorRevision(RevisionSeleccionada.id);
            return listaDTO;
        }

        public async Task EliminarAnomalia(BigInteger idAnomalia)
        {
            await inventarioDAO.BorrarAnomalia(idAnomalia);
        }

        public List<BitacoraInventarioDTO> ObtenerBitacoraInventario(long idEmpresaLogueada,int paginaActual, int elementosPorPagina)
        {
            List<BitacoraInventarioDTO> listaDTO = inventarioDAO.ObtenerBitacoraRevisionesPorEmpresa(idEmpresaLogueada, paginaActual,elementosPorPagina);
            return listaDTO;
        }

        public List<CatalogoInterfazGraficaVentaDTO> ObtenerCatalogos(long idEmpresaLogueada, int paginaActual, int elementosPorPagina)
        {
            List<CatalogoInterfazGraficaVentaDTO> listaDTO = inventarioDAO.ObtenerCatalogoPorEmpresa(idEmpresaLogueada, paginaActual, elementosPorPagina);
            return listaDTO;
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerElementosCatalogo(long idCatalogo, int paginaActual, int elementosPorPagina)
        {
            List<ElementoInterfazGraficaVentaDTO> listaDTO = inventarioDAO.ObtenerElementosCatalogo(idCatalogo, paginaActual, elementosPorPagina);
            return listaDTO;
        }

        public int ObtenerTotalCatalogos(long idEmpresa)
        {
            return inventarioDAO.ObtenerTotalCatalogoPorEmpresa(idEmpresa);
        }

        public int ObtenerTotalElementosCatalogo(long idCatalogo)
        {
            return inventarioDAO.ObtenerTotalElementosPorCatalogo(idCatalogo);
        }


        public async Task FinalizarRevision()
        {
            await inventarioDAO.FinalizarRevision(RevisionSeleccionada.id);
        }

        public int ObtenerTotalElementosInventario(long idEmpresa)
        {
            return inventarioDAO.ObtenerTotalElementosInventarioPorEmpresa(idEmpresa);
        }

        public int ObtenerTotalBitacora(long idEmpresa)
        {
            return inventarioDAO.ObtenerTotalDatosBitacora(idEmpresa);
        }

        public RevisionInventarioDTO ObtenerRevisionPorID(BigInteger idRevision)
        {
            RevisionInventarioDTO revisionDTO = inventarioDAO.InformacionRevisionPorID(idRevision);
            return revisionDTO;
        }


        //INFORMES
        public (long, long) ObtenerDatosGraficaRevisiones(long idEmpresa)
        {
            return inventarioDAO.ObtenerDatosParaGrafica(idEmpresa);
        }

        public async Task<List<ElementoInventarioDTO>> ObtenerElementosAnomalias(long idEmpresa)
        {
            List<ElementoInventarioDTO> listaDTO = await inventarioDAO.ElementosConAnomaliasMesActual(idEmpresa);
            return listaDTO;
        }

        public async Task<List<RevisionInventarioDTO>> RevisionesPorResponsable(long idEmpresa)
        {
            List<RevisionInventarioDTO> listaDTO = await inventarioDAO.CantidadRevisionesPorResponsable(idEmpresa);
            return listaDTO;
        }

    }
}
