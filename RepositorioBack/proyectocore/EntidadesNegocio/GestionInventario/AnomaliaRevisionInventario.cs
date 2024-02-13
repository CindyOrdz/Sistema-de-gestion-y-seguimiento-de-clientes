using EntidadesNegocio.ElementosInventario;
using EntidadesNegocio.EntidadesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.GestionInventario
{
    public class AnomaliaRevisionInventario
    {
        private BigInteger id;
        private String descripcion;
        private RevisionInventarioDTO _revision;
        private ElementoInventarioDTO _elemento;

        public AnomaliaRevisionInventario(String descripcion, RevisionInventarioDTO revision, ElementoInventarioDTO elemento) { 
            this.descripcion = descripcion;
            _elemento = elemento;
            _revision = revision;
        }

        public BigInteger ObtenerIdAnomalia()
        {
            return id;
        }

        public BigInteger ObtenerIdRevision()
        {
            return _revision.id;
        }

        public long ObtenerIdElemento()
        {
            return _elemento.CodigoElemento;
        }


        public String ObtenerDescripcion()
        {
            return descripcion;
        }
    }
}
