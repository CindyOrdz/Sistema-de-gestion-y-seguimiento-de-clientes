
using System.Numerics;

namespace EntidadesNegocio.ElementosInventario
{
    public class Marca
    {
        private BigInteger id;
        private String nombre;
        private String pais;
        private String web;
        private String categoria;

        public Marca(BigInteger id, String nombre, String pais, String web, String categoria)
        {
            this.id = id;
            this.nombre = nombre;
            this.pais = pais;
            this.web = web;
            this.categoria = categoria;
        }

        public Marca()
        {
            this.id = 0;
            this.nombre = "marca1";
            this.pais = "Colombia";
            this.web = "miweb.com.co";
            this.categoria = "micategoria";
        }
    }
}
