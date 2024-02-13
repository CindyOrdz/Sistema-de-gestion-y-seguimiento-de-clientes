using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.InformacionVisita
{
    public class Componente
    {
        private BigInteger id;
        private String nombre;
        private AreasProceso _areasProceso;
        private String descripcion;

        public Componente(String nombre, AreasProceso areasProceso, String descripcion)
        {
            this.nombre = nombre;
            _areasProceso = areasProceso;
            this.descripcion = descripcion;
        }

        public Componente(BigInteger id, String nombre, String descripcion)
        {
            this.id = id;
            this.nombre = nombre;
            this.descripcion = descripcion;
        }

        
        public override String ToString()
        {
            return "\nComponente{" + "id=" + id + ", nombre=" + nombre + ", areasProceso=" + _areasProceso + ", descripcion=" + descripcion + "}";
        }

        public BigInteger ObtenerId() { return id; }

        public String ObtenerNombre() { return nombre; }

        public AreasProceso ObtenerAreaProceso() { return _areasProceso; }

        public String ObtenerDescripcion() { return descripcion; }

        public BigInteger ObtenerIdArea()
        {
            return _areasProceso.ObtenerId();
        }

        public void ModificarNombre(String nombre)
        {
            this.nombre = nombre;
        }

        public void ModificarDescripcion(String descripcion)
        {
            this.descripcion = descripcion;
        }
    } 

}
