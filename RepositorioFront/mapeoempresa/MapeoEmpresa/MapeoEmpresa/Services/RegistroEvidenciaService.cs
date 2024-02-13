using EntidadesNegocio.ClasesDao;
using EntidadesNegocio.EntidadesDto;
using EntidadesNegocio.InformacionVisita;
using MySqlConnector;
using RegistroEvidencia;
using System.Numerics;

namespace MapeoEmpresa.Services
{
    public class RegistroEvidenciaService
    {
        public VisitaDTO VisitaSeleccionada { get; set; }

        public RegistroArchivo registro { get; set; } = new RegistroArchivo();
        private RegistroEvidenciaDAO registroDAO;

        //Dependencia del servicio MySqlConnection que se registró en program
        private readonly MySqlConnection _conexion;

        public RegistroEvidenciaService(MySqlConnection conexion)
        {
            _conexion = conexion;
            registroDAO = new RegistroEvidenciaDAO(conexion);
        }

        //MÉTODOS LLAMADOS DEL PROYECTO REGISTROEVIDENCIA
        public async Task GuardarArchivo()
        {
            await registro.GuardarArchivoVisita(VisitaSeleccionada.id);
            await registroDAO.RegistrarEvidencia(registro,VisitaSeleccionada.id,"visita");
        }

        public static RegistroEvidenciaDTO ConvertirDelModeloAlDTO(RegistroArchivo archivo) => new RegistroEvidenciaDTO
        {
            id = archivo.ObtenerIdArchivo(),
            extension = archivo.ObtenerExtensionArchivo(),
            tipo = archivo.ObtenerTipoArchivo(),
            nombre = archivo.ObtenerNombreArchivo(),
            rutaRelativa = archivo.ObtenerRutaRelativaArchivo(),
            fechaCaptura = archivo.ObtenerFechaCaptura(),
            fechaRegistro = archivo.ObtenerFechaRegistro()

        };

        //LLAMADO A MÉTODOS DAO EN ENTIDADES NEGOCIO

        public async Task<List<RegistroEvidenciaDTO>> ListarEvidencia()
        {
            List<RegistroArchivo> listaEvidencia= await registroDAO.ListarEvidencia(VisitaSeleccionada.id, "visita");
            List<RegistroEvidenciaDTO> listaDTO = listaEvidencia.Select(x => ConvertirDelModeloAlDTO(x)).ToList();
            return listaDTO;
        }

        public async Task<List<RegistroEvidenciaDTO>> ListarEvidenciaRevision(BigInteger idRevision)
        {
            List<RegistroArchivo> listaEvidencia = await registroDAO.ListarEvidencia(idRevision, "revision");
            List<RegistroEvidenciaDTO> listaDTO = listaEvidencia.Select(x => ConvertirDelModeloAlDTO(x)).ToList();
            return listaDTO;
        }

        public Task BorrarEvidencia(BigInteger id, string ruta)
        {
            registro.BorrarArchivo(ruta);
            return registroDAO.BorrarEvidencia(id);
        }

        public async Task GuardarArchivoRevision(BigInteger idRevision)
        {
            await registro.GuardarArchivoRevision(idRevision);
            await registroDAO.RegistrarEvidencia(registro, idRevision, "revision");
        }

        public async Task GuardarArchivoAnomalia(BigInteger idRevision, int idAnomalia)
        {
            await registro.GuardarArchivoRevision(idRevision,idAnomalia);
            await registroDAO.RegistrarEvidencia(registro, idAnomalia, "anomalia");
        }
    }
}
