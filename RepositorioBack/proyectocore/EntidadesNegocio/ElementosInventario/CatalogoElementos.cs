

using System.Numerics;

namespace EntidadesNegocio.ElementosInventario
{
    public class CatalogoElementos
    {
        private BigInteger id;
        private String clasificacion;
        private String descripcion;
        private List<Elemento> _elementos;

        public CatalogoElementos(BigInteger id, string clasificacion, string descripcion, List<Elemento> elementos)
        {
            this.id = id;
            this.clasificacion = clasificacion;
            this.descripcion = descripcion;
            _elementos = elementos;
        }

        public List<Elemento> ObtenerElementosDelCatalogo() {
            return _elementos;
        }

    }
}
