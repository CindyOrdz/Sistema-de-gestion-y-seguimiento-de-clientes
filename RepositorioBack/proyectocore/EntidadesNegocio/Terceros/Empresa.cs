

using EntidadesNegocio.LugarProcedencia;
using System.Numerics;

namespace EntidadesNegocio.Terceros
{
    public class Empresa: Tercero
    {
        private BigInteger nit;
        private String razonSocial; //Tercero también tiene razón social
        private List<Sede> _sedes;

        public Empresa(BigInteger identificacion, BigInteger tipo, String razonSocial, Ubicacion ubicacion, String telefono, String email, String celular)
        :base( identificacion, tipo, razonSocial, ubicacion, telefono, email, celular)
        {   

        }

        public Empresa() : base()
        { 
        
        }

        public Empresa(BigInteger nit, String razonSocial)
        {
            this.nit = nit;
            this.razonSocial = razonSocial;
        }

        public BigInteger ObtenerNIT()
        {
            return nit;
        }

        public String ObtenerRazonSocial()
        {
            return razonSocial;
        }

    }
}
