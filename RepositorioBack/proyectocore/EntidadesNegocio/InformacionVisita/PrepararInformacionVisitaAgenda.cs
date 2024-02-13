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
    public class PrepararInformacionVisitaAgenda
    {
        private List<Venta> _compras;
        private List<ElementosCompradosDTO> elementosComprados;
        private List<InformacionVisitaRealizadaDTO> historialVisitas;
        private InventarioDTO inventarioDisponible;

        private Empresa CargarInformacionVisita(Empresa empresa)
        {
            return new Empresa();
        }


    }
}
