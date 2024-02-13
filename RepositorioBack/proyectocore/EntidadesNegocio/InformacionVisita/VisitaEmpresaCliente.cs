using EntidadesNegocio.Terceros;
using EntidadesNegocio.VentaOnlineTradicional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.InformacionVisita
{
    public class VisitaEmpresaCliente
    {
        private AgendaVisitaEmpresaCliente _visitaAgendada;
        private PrepararInformacionVisitaAgenda _informacionInicialVisita;
        private InformacionVisitaRealizada _informacionVisitaRealizada; 
        private List<Venta> _ventas;

        public VisitaEmpresaCliente(AgendaVisitaEmpresaCliente visitaAgendada,PrepararInformacionVisitaAgenda informacionInicialVisita,InformacionVisitaRealizada informacionVisitaRealizada,List<Venta> ventas)
        {
            _visitaAgendada = visitaAgendada;
            _informacionInicialVisita = informacionInicialVisita;
            _informacionVisitaRealizada = informacionVisitaRealizada;
            _ventas = ventas;

        }

        public BigInteger ObtenerIdVisitaAgendada()
        {
            return _visitaAgendada.ObtenerId();
        }

        public Tercero ObtenerResponsableVisitaAgendada()
        {
            return _visitaAgendada.ObtenerResponsable();
        }

        public PlantaEmpresaCliente ObtenerEmpresaVisitaAgendada()
        {
            return _visitaAgendada.ObtenerEmpresa();
        }
    }
}
