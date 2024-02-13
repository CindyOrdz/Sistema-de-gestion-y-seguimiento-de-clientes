namespace EntidadesNegocio.LugarProcedencia
{
    public class DepartamentoProvincia
    {
        private String pais;
        private String codigo;
        private String nombre;
        private String codigoIso;

        public DepartamentoProvincia()
        {
            this.pais = "CO";
            this.codigo = "50";
            this.codigoIso = "MET";
            this.nombre = "META";

        }

        public DepartamentoProvincia(String pais, String codigo, String nombre, String codigoIso)
        {
            this.pais = pais;
            this.codigo = codigo;
            this.nombre = nombre;
            this.codigoIso = codigoIso;
        }


        public override String ToString()
        {
            return "DepartamentoProvincia{" + "pais=" + pais + ", codigo=" + codigo + ", nombre=" + nombre + ", codigoISO=" + codigoIso + '}';
        }


    }
}
