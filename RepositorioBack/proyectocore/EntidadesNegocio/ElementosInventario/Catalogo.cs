using System.Numerics;

namespace EntidadesNegocio.ElementosInventario
{
    public class Catalogo
    {
        private BigInteger id;
        private String clasificacion;
        private String descripcion;

        public Catalogo(BigInteger id, string clasificacion, string descripcion) {
            this.id = id;
            this.clasificacion = clasificacion;
            this.descripcion = descripcion;
        }
    }
}
