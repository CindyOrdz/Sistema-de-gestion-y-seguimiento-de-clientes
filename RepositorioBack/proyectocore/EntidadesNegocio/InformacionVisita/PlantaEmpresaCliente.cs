using EntidadesNegocio.Terceros;
using EntidadesNegocio.LugarProcedencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace EntidadesNegocio.InformacionVisita
{
    public class PlantaEmpresaCliente
    {
        private BigInteger id;
        private String nombre;
        private String direccion;
        private Tercero _empresa;
        private String contacto;
        private String telefono1Contacto;
        private String telefono2Contacto;
        private String emailContacto;
        private String emailParaInforme;
        private Sede _sede;

        public PlantaEmpresaCliente(String nombre, string direccion, Tercero empresa, string contacto, string telefono1Contacto, string telefono2Contacto, string emailContacto, string emailParaInforme,Sede sede)
        {
            this.nombre = nombre;
            this.direccion = direccion;
            _empresa = empresa;
            this.contacto = contacto;
            this.telefono1Contacto = telefono1Contacto;
            this.telefono2Contacto = telefono2Contacto;
            this.emailContacto = emailContacto;
            this.emailParaInforme = emailParaInforme;
            _sede = sede;
           
        }

        public PlantaEmpresaCliente(BigInteger id,string nombre, string direccion, string contacto, string telefono1Contacto, string telefono2Contacto, string emailContacto, string emailParaInforme)
        {
            this.id = id;
            this.nombre = nombre;
            this.direccion = direccion;
            this.contacto = contacto;
            this.telefono1Contacto = telefono1Contacto;
            this.telefono2Contacto = telefono2Contacto;
            this.emailContacto = emailContacto;
            this.emailParaInforme = emailParaInforme;
        }

        
        public override String ToString()
        {
            return "\nPlantaEmpresaCliente{" + "_empresa=" + _empresa +  ", contacto=" + contacto +
                ", telefono1Contacto=" + telefono1Contacto +", telefono2Contacto=" + telefono2Contacto +", emailContacto=" + emailContacto +
                ", emailParaInforme=" + emailParaInforme +", _direccion=" + direccion + "}";
        }

        public BigInteger ObtenerId()
        {
            return id;
        }

        public String ObtenerNombre()
        {
            return nombre;
        }

        public Tercero ObtenerEmpresa()
        {
            return _empresa;
        }

        public String ObtenerContacto()
        {
            return contacto;
        }

        public String ObtenerTelefono1()
        {
            return telefono1Contacto;
        }

        public String ObtenerTelefono2()
        {
            return telefono2Contacto;
        }

        public String ObtenerEmailContacto()
        {
            return emailContacto;
        }

        public String ObtenerEmailParaInforme()
        {
            return emailParaInforme;
        }

        public String ObtenerDireccion()
        {
            return direccion;
        }

        public long ObtenerIdSede()
        {
            return _sede.ObtenerId();
        }

        public BigInteger ObtenerIdEmpresa()
        {
            return _empresa.ObtenerIdentificacion();
        }


        public void ModificarNombre(String nombre)
        {
           this.nombre = nombre;
        }

        public void ModificarContacto(String contacto)
        {
            this.contacto = contacto;
        }

        public void ModificarTelefono1(String telefono1)
        {
            this.telefono1Contacto = telefono1;
        }

        public void ModificarTelefono2(String telefono2)
        {
            this.telefono2Contacto = telefono2;
        }

        public void ModificarEmailContacto(String emailContacto)
        {
            this.emailContacto = emailContacto;
        }

        public void ModificarEmailParaInforme(String emailInforme)
        {
            this.emailParaInforme = emailInforme;
        }

        public void ModificarDireccion(String direccion)
        {
            this.direccion = direccion;
        }

    }
}
