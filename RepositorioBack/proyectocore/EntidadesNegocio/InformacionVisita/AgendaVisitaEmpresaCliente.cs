using EntidadesNegocio.Terceros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.InformacionVisita
{
    public class AgendaVisitaEmpresaCliente
    {
        private BigInteger id;
        private DateTime fechaCreacion;
        private DateTime fechaInicio;
        private DateTime fechaFinaliza;
        private int cantidadDias;
        private String contactoEmpresa;
        private Tercero _responsable;
        private PlantaEmpresaCliente _empresa;

        public AgendaVisitaEmpresaCliente(BigInteger id, DateTime fechaCreacion, DateTime fechaInicio, DateTime fechaFinaliza, int cantidadDias, String contactoEmpresa)
        {
            this.id = id;
            this.fechaCreacion = fechaCreacion;
            this.fechaInicio = fechaInicio;
            this.fechaFinaliza = fechaFinaliza;
            this.cantidadDias = cantidadDias;
            this.contactoEmpresa = contactoEmpresa;
        }

        public AgendaVisitaEmpresaCliente(BigInteger id, DateTime fechaCreacion, DateTime fechaInicio, DateTime fechaFinaliza, int cantidadDias, String contactoEmpresa, Tercero responsable)
        {
            this.id = id;
            this.fechaCreacion = fechaCreacion;
            this.fechaInicio = fechaInicio;
            this.fechaFinaliza = fechaFinaliza;
            this.cantidadDias = cantidadDias;
            this.contactoEmpresa = contactoEmpresa;
            this._responsable = responsable;
        }

        public AgendaVisitaEmpresaCliente(DateTime fechaInicio, DateTime fechaFinaliza, int cantidadDias, String contactoEmpresa, PlantaEmpresaCliente _empresa, Tercero responsable)
        {
            this.fechaInicio = fechaInicio;
            this.fechaFinaliza = fechaFinaliza;
            this.cantidadDias = cantidadDias;
            this.contactoEmpresa = contactoEmpresa;
            this._empresa = _empresa;
            this._responsable = responsable;
        }

        public BigInteger ObtenerId()
        {
            return id;
        }

        public DateTime ObtenerFechaCreacion()
        {
            return fechaCreacion;
        }

        public DateTime ObtenerFechaInicio()
        {
            return fechaInicio;
        }

        public DateTime ObtenerFechaFinaliza()
        {
            return fechaFinaliza;
        }

        public int ObtenerCantidadDias()
        {
            TimeSpan diferencia = fechaFinaliza.Date.Subtract(fechaInicio.Date);
            cantidadDias = (int)Math.Ceiling(diferencia.TotalDays);
            return cantidadDias;
        }

        public String ObtenerContactoEmpresa()
        {
            return contactoEmpresa;
        }

        public BigInteger ObtenerIdPlanta()
        {
            return _empresa.ObtenerId();
        }


        public Tercero ObtenerResponsable()
        {
            return _responsable;
        }

        public BigInteger ObtenerIdResponsable()
        {
            return _responsable.ObtenerIdentificacion();
        }

        public PlantaEmpresaCliente ObtenerEmpresa()
        {
            return _empresa;
        }

    }

}
