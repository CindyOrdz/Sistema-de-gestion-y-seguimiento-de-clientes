using EntidadesNegocio.EntidadesDto;
using EntidadesNegocio.Terceros;
using EntidadesNegocio.VentaOnlineTradicional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.InformacionVisita
{
    public class CargarInformacionVisitaRealizada
    {
        public List<Venta> ObtenerVentas(Empresa empresa)
        {
            return new List<Venta> { };
        }
        public List<ElementosCompradosDTO> ObtenerElementosComprados(Empresa empresa)
        {
            return new List<ElementosCompradosDTO> { };
        }

        public List<InformacionVisitaRealizadaDTO> ObtenerInformacionVisitaRealizada(Empresa empresa)
        {
            return new List<InformacionVisitaRealizadaDTO> { };
        }

        public InventarioDTO ObtenerInformacionInventarioDisponible(Empresa empresa)
        {
            return new InventarioDTO();
        }
    }
}
