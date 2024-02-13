using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.InformacionVisita
{
    public class AreasProceso
    {
        private BigInteger id;
        private String nombre;
        private String responsable;
        private String telefonoContacto;
        private String emailContacto;
        private String descripcion;
        private PlantaEmpresaCliente _planta;

        public AreasProceso(String nombre, String responsable,String telefonoContacto, String emailContacto, String descripcion, PlantaEmpresaCliente planta)
        {
            this.nombre = nombre;
            this.responsable = responsable;
            this.telefonoContacto = telefonoContacto;
            this.emailContacto = emailContacto;
            this.descripcion = descripcion;
            this._planta = planta;
        }

        public AreasProceso(BigInteger id,String nombre, String responsable, String telefonoContacto, String emailContacto, String descripcion)
        {
            this.id = id;
            this.nombre = nombre;
            this.responsable = responsable;
            this.telefonoContacto = telefonoContacto;
            this.emailContacto = emailContacto;
            this.descripcion = descripcion;
        }

        
        public override String ToString()
        {
            return "\nAreasProceso{" + "id=" + id + ", nombre=" + nombre + ", responsable=" + responsable +
                ", telefonoContacto=" + telefonoContacto + ", emailContacto=" + emailContacto + ", descripcion=" + descripcion + "}";
        }

        public BigInteger ObtenerId()
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

        public String ObtenerTelefonoContacto()
        {
            return telefonoContacto;
        }

        public String ObtenerEmailContacto()
        {
            return emailContacto;
        }

        public String ObtenerDescripcion()
        {
            return descripcion;
        }

        public BigInteger ObtenerIdPlanta()
        {
            return _planta.ObtenerId();
        }

        public void ModificarNombre(String nombre)
        {
            this.nombre = nombre;
        }

        public void ModificarResponsable(String responsable)
        {
            this.responsable = responsable;
        }

        public void ModificarTelefono(String telefono)
        {
            this.telefonoContacto = telefono;
        }

        public void ModificarEmail(String email)
        {
            this.emailContacto = email;
        }

        public void ModificarDescripcion(String descripcion)
        {
            this.descripcion = descripcion;
        }


    }

}
