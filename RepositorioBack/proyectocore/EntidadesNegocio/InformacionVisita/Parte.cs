using EntidadesNegocio.ElementosInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EntidadesNegocio.InformacionVisita
{
    public class Parte
    { 
        private BigInteger id;
        private Elemento _elemento;
        private EquipoDelComponente _equipo;

        public Parte(Elemento elemento, EquipoDelComponente equipo)
        {
            _elemento = elemento;
            _equipo = equipo;
        }

        public override String ToString()
        {
            return "\nParte{" + "id=" + id + ", elemento=" + _elemento + ", equipo =" + _equipo + "}";
        }

        public BigInteger ObtenerId() { return id; }

        public BigInteger ObtenerIdElemento() { return _elemento.ObtenerId(); }

        public BigInteger ObtenerIdEquipo() { return _equipo.ObtenerId(); }

    }
}
