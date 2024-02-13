using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.LugarProcedencia
{
    public class Ubicacion
    {
        private Pais _pais;
        private DepartamentoProvincia _departamento;
        private Ciudad _ciudad;
        private Direccion _direccion;

        public Ubicacion(){
            _pais = new Pais();
            _departamento = new DepartamentoProvincia();
            _ciudad = new Ciudad();
            _direccion = new Direccion();
        }

        public Ubicacion(Pais pais, DepartamentoProvincia departamento, Ciudad ciudad, Direccion direccion)
        {
            _pais = pais;
            _departamento = departamento;
            _ciudad = ciudad;
            _direccion = direccion;
        }

        public Ubicacion(Pais pais, DepartamentoProvincia departamento, Ciudad ciudad)
        {
            _pais = pais;
            _departamento = departamento;
            _ciudad = ciudad;
        }

        public Ciudad ObtenerCiudad()
        {
            return _ciudad;
        }
        public override string ToString()
        {
            return "\nUbicacion{pais = " + _pais + ", departamento = " + _departamento + ", ciudad = " + _ciudad + ", direccion = " + _direccion + "}";
        }

    }
}
