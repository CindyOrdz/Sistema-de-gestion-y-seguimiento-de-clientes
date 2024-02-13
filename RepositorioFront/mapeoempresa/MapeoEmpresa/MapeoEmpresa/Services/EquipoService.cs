using EntidadesNegocio.InformacionVisita;
using EntidadesNegocio.Terceros;
using EntidadesNegocio.EntidadesDto;
using System.Numerics;
using EntidadesNegocio.ClasesDao;
using MySqlConnector;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.LugarProcedencia;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Inventario;

namespace MapeoEmpresa.Services
{
    public class EquipoService
    {
        public EquipoDTO EquipoDTOSeleccionado { get; set; }//equipo seleccionado para asociar una parte

        public string ComponenteAnterior { get; set; } //referencia al componente razor anterior

        private EquipoDAO equipoDAO;

        //Dependencia del servicio MySqlConnection que se registró en program
        private readonly MySqlConnection conexion_;

        public EquipoService(MySqlConnection conexion)
        {
            conexion_ = conexion;
            equipoDAO = new EquipoDAO(conexion);
        }

        public async Task AgregarParte(long idElemento)
        {
            await equipoDAO.AgregarParte(EquipoDTOSeleccionado.id,idElemento);
        }

        public async Task BorrarParte(BigInteger id)
        {
            await equipoDAO.BorrarParte(id);
        }

        public async Task<List<ParteDTO>> ListarPartes()
        {
            List<ParteDTO> listaDTO = await equipoDAO.ListarPartes(EquipoDTOSeleccionado.id);
            return listaDTO;
        }

        public async Task<ParteDTO> BuscarParte(BigInteger id)
        {
            ParteDTO parte = await equipoDAO.InformacionParte(id);
            return parte;
        }

        public EquipoDelComponente ConvertirComponenteDTOaModelo(EquipoDTO equipoDTO)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            EquipoDelComponente equipo = new EquipoDelComponente(
                equipoDTO.id,
                equipoDTO.nombre,
                equipoDTO.descripcion);

            return equipo;
        }
    }
}
