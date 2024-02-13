using EntidadesNegocio.LugarProcedencia;
using System.Numerics;

namespace EntidadesNegocio.Terceros 
{

    public class Empleado : Tercero
    {
        private String cargo;

        public Empleado()
        {
            this.cargo = "Vendedor";
        }

        public Empleado(BigInteger identificacion, BigInteger digito, BigInteger tipo, String razonSocial, String nombre1, String nombre2, String apellido1, String apellido2, Ubicacion ubicacion, String telefono, String email, String celular, String tipoTercero, String direccion)
                      : base(identificacion, digito, tipo, razonSocial, nombre1, nombre2, apellido1, apellido2, ubicacion, telefono, email, celular, tipoTercero, direccion)
        {
            this.cargo = "Vendedor";

        }

        public Empleado(BigInteger identificacion, BigInteger tipo, String nombre1, String nombre2, String apellido1, String apellido2, String telefono, String email, String celular, String direccion)
                      :base(identificacion, tipo, nombre1, nombre2, apellido1, apellido2, telefono, email, celular, direccion)
        {
            this.cargo = "Vendedor";

        }




        public override String ToString()
        {
            return base.ToString() + "\nEmpleado{" + "cargo=" + cargo + '}';
        }

    }
}


