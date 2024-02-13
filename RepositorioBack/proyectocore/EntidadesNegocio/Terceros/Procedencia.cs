using EntidadesNegocio.LugarProcedencia;
using System.Numerics;

namespace EntidadesNegocio.Terceros
{
    public class Procedencia
    {
        private DepartamentoProvincia departamento;
        private Ciudad ciudad;
        private Pais pais;
        private BigInteger idPersona;
        

        public Procedencia(BigInteger id)
        {
            this.idPersona = id;
            this.ObtenerProcedenciaPersona(id);
        }


        public void ObtenerProcedenciaPersona(BigInteger id)
        {
            this.pais = new Pais();
            this.departamento = new DepartamentoProvincia();
            this.ciudad = new Ciudad();
        }


        public override String ToString()
        {
            //return "Procedencia{" + "\ndepartamento=" + departamento + ", \nciudad=" + ciudad + ", \npais=" + pais + ", \nidPersona=" + idPersona + '}';

            return "Procedencia{" + "\npais=" + pais + ", \ndepartamento=" + departamento + ", \nciudad=" + ciudad + ", \nidPersona=" + idPersona + '}';

        }
    }
}
