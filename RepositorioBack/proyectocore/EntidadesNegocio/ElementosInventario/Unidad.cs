

using System.Numerics;

namespace EntidadesNegocio.ElementosInventario
{
    public class Unidad
    {
        private BigInteger id;
        private String codigo;
        private String descripcion;

        public Unidad(BigInteger id, String codigo, String descripcion)
        {
            this.id = id;
            this.codigo = codigo;
            this.descripcion = descripcion;
        }

        public Unidad( String codigo, String descripcion)
        {
            this.codigo = codigo;
            this.descripcion = descripcion;
        }

        public Unidad()
        {
            this.id = 1;
            this.codigo = "ABC";
            this.descripcion = "Unidades genericas";
        
        }

        public String ObtenerCodigo()
        {
            return codigo;
        }

        public String ObtenerDescripcion()
        {
            return descripcion;
        }
    }
}
