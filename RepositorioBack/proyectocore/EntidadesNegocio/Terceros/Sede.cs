using EntidadesNegocio.InformacionVisita;
using EntidadesNegocio.LugarProcedencia;
using System.Numerics;

namespace EntidadesNegocio.Terceros
{
    public class Sede
    {
        private long id;
        private String nombre;
        private String responsable;
        private String telefono;
        private String email1;
        private String email2;
        private Ubicacion _direccion;
        public List<PlantaEmpresaCliente> Plantas { get; set; } = new List<PlantaEmpresaCliente>();

        public Sede(String nombre, String responsable, String telefono, String email1, String email2, Ubicacion direccion)
        {
            this.nombre = nombre;
            this.responsable = responsable;
            this.telefono = telefono;
            this.email1 = email1;
            this.email2 = email2;
            _direccion = direccion;

        }

        public Sede(long id, String responsable, String telefono, String email1, String email2)
        {
            this.id = id;
            this.responsable = responsable;
            this.telefono = telefono;
            this.email1 = email1;
            this.email2 = email2;
        }

        
        public long ObtenerId()
        {
            return id;
        }

        public String ObtenerNombre()
        {
            return nombre;
        }

        public String ObtenerResponsable()
        {
            return responsable;
        }

        public String ObtenerTelefono()
        {
            return telefono;
        }

        public String ObtenerEmail1()
        {
            return email1;
        }

        public String ObtenerEmail2()
        {
            return email2;
        }

        
    }
}
