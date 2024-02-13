using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.EntidadesDto
{
    public class PlantaEmpresaClienteDTO
    {
        public BigInteger id { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string contacto { get; set; }
        public string telefono1 { get; set; }
        public string telefono2 { get; set; }
        public string emailContacto { get; set; }
        public string emailInforme { get; set; }
    }
}
