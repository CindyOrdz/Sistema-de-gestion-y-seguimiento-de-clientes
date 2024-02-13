using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace EntidadesNegocio.Terceros
{
    public class Cliente: Tercero
    {
        public Cliente(BigInteger identificacion, BigInteger tipo, String nombre1, String nombre2, String apellido1, String apellido2, String telefono, String email, String celular, String direccion, String conMontoDeLey, String ConMontoPersonalizado, Double montoPersonalizado)
        :base(identificacion, tipo, nombre1, nombre2, apellido1, apellido2, telefono, email, celular, direccion, conMontoDeLey, ConMontoPersonalizado, montoPersonalizado)
        {

       
        }

        
    }


}
