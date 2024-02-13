namespace EntidadesNegocio.LugarProcedencia
{
    public class Pais
    {
        private String codigo;
        private String nombre;

        public Pais()
        {

            codigo = "CO";
            nombre = "COLOMBIA";
        }

        public Pais(String codigo, String nombre)
        {
            this.codigo = codigo;
            this.nombre = nombre;
        }

        public override String ToString()
        {
            return "Pais{" + "codigo=" + codigo + ", nombre=" + nombre + '}';
        }

    }
}
