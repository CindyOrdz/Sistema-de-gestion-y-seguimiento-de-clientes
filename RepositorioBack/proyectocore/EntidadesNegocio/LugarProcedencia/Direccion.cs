
namespace EntidadesNegocio.LugarProcedencia
{
    public class Direccion
    {
        private String calle;
        private String numero;
        private String codigoPostal;
        private String ciudad;
        private String departamentoProvincia;
        private String pais;
        private String informacionAdicional;

        public Direccion(String codigoPostal,String calle, String numero, String ciudad, String provinciaEstado, String pais, String informacionAdicional)
        {
            this.calle = calle;
            this.numero = numero;
            this.codigoPostal = codigoPostal;
            this.ciudad = ciudad;
            this.departamentoProvincia = provinciaEstado;
            this.pais = pais;
            this.informacionAdicional = informacionAdicional;
        }

        public Direccion()
        {
            this.calle = "25";
            this.numero = "1";
            this.codigoPostal = "2338474";
            this.ciudad = "Villavicencio";
            this.departamentoProvincia = "Meta";
            this.pais = "Colombia";
            this.informacionAdicional = "tercera cuadra";
        
        }

    }
}
