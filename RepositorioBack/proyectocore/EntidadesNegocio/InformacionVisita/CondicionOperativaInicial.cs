using EntidadesNegocio.ElementosInventario;
using EntidadesNegocio.Terceros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.InformacionVisita
{
    public class CondicionOperativaInicial
    {
        private BigInteger id;
        private Parte _parte;
        private List<CondicionOperativa> _condiciones;
        private Tercero _responsable;
        private String descripcion; 
        public CondicionOperativaInicial(Parte parte, Tercero responsable, String descripcion)
        {
            _parte = parte;
            _condiciones = new List<CondicionOperativa>();
            _responsable = responsable;
            this.descripcion = descripcion;

        }

        public CondicionOperativaInicial(BigInteger id, String descripcion)
        {
            this.id = id;
            this.descripcion = descripcion;

        }

        public CondicionOperativaInicial(Tercero responsable, String descripcion)
        {
            _condiciones = new List<CondicionOperativa>();
            _responsable = responsable;
            this.descripcion = descripcion;

        }

        public Parte ObtenerParte()
        {
            return _parte;
        }

        public Tercero ObtenerResponsable()
        {
            return _responsable;
        }

        public String ObtenerDescripcion()
        {
            return descripcion;
        }

        public BigInteger ObtenerId()
        {
            return id;
        }

        public BigInteger ObtenerIdParte()
        {
            return _parte.ObtenerId();
        }

        public BigInteger ObtenerIdResponsable()
        {
            return _responsable.ObtenerIdentificacion();
        }

        public void ModificarDescripcion(String descripcion)
        {
            this.descripcion = descripcion;
        }

        public void ModificarResponsable(Tercero responsable)
        {
            this._responsable = responsable;
        }

        public override String ToString()
        {
            return "\nCondicionOperativaInicial{" + "equipo =" + _parte + ", condiciones =" + _condiciones + ", responsable=" + _responsable +
                ", descripcion=" + descripcion + "}";
        }
    }
}
