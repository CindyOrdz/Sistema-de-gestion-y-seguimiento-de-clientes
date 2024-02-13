using EntidadesNegocio.VentaOnlineTradicional;

namespace EntidadesNegocio.LugarProcedencia
{
    public class Ciudad
    {
        private String pais;
        private String departamento;
        private String codigo;
        private String nombre;
        private Double tarifaIca;
        private Double tarifaReteIca;

        public Ciudad()
        {
            this.tarifaIca = TarifasTerritorialesEmpresa.Instancia.TarifaIca;
            this.tarifaReteIca = TarifasTerritorialesEmpresa.Instancia.TarifaReteIca;
            this.pais = "CO";
            this.departamento = "50";
            this.codigo = "50001";
            this.nombre = "VILLAVICENCIO";
        }

        public Ciudad(String pais, String departamento, String codigo, String nombre)
        {
            this.pais = pais;
            this.departamento = departamento;
            this.codigo = codigo;
            this.nombre = nombre;
            this.tarifaIca = TarifasTerritorialesEmpresa.Instancia.TarifaIca;
            this.tarifaReteIca = TarifasTerritorialesEmpresa.Instancia.TarifaReteIca;
        }

        public Double ObtenerTarifaIca()
        { 
            return this.tarifaIca;
        }

        public Double ObtenerTarifaReteIca()
        {
            return this.tarifaReteIca;
        }
         
        public override String ToString()
        {
            return "Ciudad{" + "pais=" + pais + ", departamento=" + departamento + ", codigo=" + codigo + ", nombre=" + nombre + '}';
        }
    }
}
