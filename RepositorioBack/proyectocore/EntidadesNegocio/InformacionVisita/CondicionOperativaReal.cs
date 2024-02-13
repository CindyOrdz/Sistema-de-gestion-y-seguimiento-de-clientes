using EntidadesNegocio.Terceros;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.InformacionVisita
{
    public class CondicionOperativaReal
    {
        private BigInteger id;
        private Parte _parte;
        private List<CondicionOperativa> _condiciones;
        private Tercero _responsable;
        private String descripcion;

        public CondicionOperativaReal(BigInteger id, String descripcion)
        {
            this.id = id;
            this.descripcion = descripcion;

        }

        public CondicionOperativaReal(Parte parte, List<CondicionOperativa> condiciones, Tercero responsable, String descripcion)
        {
            _parte = parte;
            _condiciones = condiciones;
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

        public override String ToString()
        {
            return "\nCondicionOperativaReal{" + "parte =" + _parte + ", condiciones =" + _condiciones + ", responsable=" + _responsable +
                ", descripcion=" + descripcion + "}";
        }
    }
}
