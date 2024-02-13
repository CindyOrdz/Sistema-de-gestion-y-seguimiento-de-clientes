
using EntidadesNegocio.ClasesDao.UsuarioDAO;
using EntidadesNegocio.InterfazGraficaBlazorDTO;
using EntidadesNegocio.InterfazGraficaBlazorDTO.SesionDTO;

namespace ServiciosComponentes.UsuarioServices
{
    public class UsuarioService
    {
        private readonly UsuarioDAO _usuarioDao;

        public UsuarioService(UsuarioDAO usuarioDao)
        {
            _usuarioDao = usuarioDao;
        }

        public int UsuarioExiste(string email)
        {
            int resultado = _usuarioDao.VerificarUsuario(email);

            return resultado;

        }

        public string ObtenerHashPasswd(string email)
        {
            string hashPasswd = _usuarioDao.ObtenerHashPasswd(email);

            return hashPasswd;
        }

        public List<string> ObtenerRoles(string email)
        {
            List<string> roles = _usuarioDao.ObtenerRolesUsuario(email);

            return roles;
        }

        public List<string> ObtenerPoliticasAccesoDeUsuario(string emailUsuario)
        {
            return _usuarioDao.ObtenerPoliticasAccesoDeUsuario(emailUsuario);
        }

        public DetalleUsuarioDTO ObtenerUsuarioPorIdentificacionDelTercero(long identificacionTercero)
        {
            return _usuarioDao.ObtenerUsuarioPorIdentificacionDelTercero(identificacionTercero);
        }

        public List<PoliticasAccesoDTO> ObtenerPoliticasDeAccesoParaUsuarioEmpresa()
        {

            return _usuarioDao.ObtenerPoliticasDeAccesoParaUsuarioEmpresa();
        }

        public long ObtenerIdentificacionEmpresaPorUsuario(string emailUsuario)
        {
            return _usuarioDao.ObtenerIdentificacionEmpresaPorUsuario(emailUsuario);
            
        }
        public long ObtenerIdentificacionEmpresaPorUsuarioEmpleado(string emailUsuario)
        {
            return _usuarioDao.ObtenerIdentificacionEmpresaPorUsuarioEmpleado(emailUsuario);

        }
        

        public UsuarioInfoDTO ObtenerInformacionPorUsuario(string emailUsuario)
        {
            var usuarioDTO = _usuarioDao.ObtenerInformacionPorUsuario(emailUsuario);
            usuarioDTO.Roles = ObtenerRoles(emailUsuario);
            return usuarioDTO;
        }

        public bool ConfigurarPoliticasDeUsuario(DetalleUsuarioDTO detalleUsuario, List<PoliticasAccesoDTO> politicasUsuario)
        {
            return _usuarioDao.ConfigurarPoliticasDeUsuario(detalleUsuario, politicasUsuario);
        }

        public List<PoliticasAccesoDTO> ObtenerPoliticasAccesoDeUsuarioParaConfiguracion(string emailUsuario)
        {
            return _usuarioDao.ObtenerPoliticasAccesoDeUsuarioParaConfiguracion(emailUsuario);
        }

        public List<PoliticasAccesoDTO> ObtenerPoliticasDeAccesoParaUsuarioEmpleado()
        { 
            return _usuarioDao.ObtenerPoliticasDeAccesoParaUsuarioEmpleado();
        }

        public bool InsertarUsuario(UserDTO usuarioDTO, long identificacionTercero, int idRol)
        {
            var passHash = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Password);
            var usarioParaInsertarDTO = new UsuarioParaInsertarDTO
            {
                Email = usuarioDTO.Email,
                HashPassword = passHash
            };

            return _usuarioDao.InsertarUsuario(usarioParaInsertarDTO,identificacionTercero,idRol);
            
        }
    }
}
