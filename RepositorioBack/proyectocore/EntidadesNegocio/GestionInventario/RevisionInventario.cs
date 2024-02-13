using EntidadesNegocio.EntidadesDto;
using EntidadesNegocio.InformacionVisita;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Inventario;
using EntidadesNegocio.Terceros;
using RegistroEvidencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.GestionInventario
{
    public class RevisionInventario
    {
        private BigInteger id;
        private DateTime fechaInicio;
        private DateTime fechaFinalizacion;
        private DateTime fechaCreacion;
        private Tercero _responsable;
        private String descripcion;
        private List<RegistroEvidencia_> _registroEvidencia;
        private List<ElementoInterfazGraficaVentaDTO> _elementosRevisados;
        private String estado;

        public RevisionInventario(DateTime fechaCreacion,DateTime fechaInicio, Tercero responsable, String descripcion, List<ElementoInterfazGraficaVentaDTO> elementosRevisados)
        {
            this.fechaCreacion = fechaCreacion;
            this.fechaInicio = fechaInicio;
            _responsable = responsable;
            this.descripcion = descripcion;
            _elementosRevisados = elementosRevisados;
        }

        public RevisionInventario(BigInteger id, DateTime fechaInicio, Tercero responsable, String descripcion)
        {
            this.id = id;
            this.fechaInicio = fechaInicio;
            _responsable = responsable;
            this.descripcion = descripcion;
        }

        public RevisionInventario(BigInteger id, DateTime fechaInicio,String descripcion)
        {
            this.id = id;
            this.fechaInicio = fechaInicio;
            this.descripcion = descripcion;

        }

        public BigInteger ObtenerId()
        {
            return id;
        }

        public DateTime ObtenerFechaInicio()
        {
            return fechaInicio;
        }

        public DateTime ObtenerFechaFinalizacion()
        {
            return fechaFinalizacion;
        }

        public DateTime ObtenerFechaCreacion()
        {
            return fechaCreacion;
        }

        public String ObtenerDescripcion()
        {
            return descripcion;
        }

        public String ObtenerNombreResponsable()
        {
            return _responsable.ObtenerNombreCompleto();
        }

        public BigInteger ObtenerIdResponsable()
        {
            return _responsable.ObtenerIdentificacion();
        }

        public List<ElementoInterfazGraficaVentaDTO> ObtenerListaElementosRevisados()
        {
            return _elementosRevisados;
        }

        public List<RegistroEvidencia_> ObtenerListaEvidencia()
        {
            return _registroEvidencia;
        }
    }
}
