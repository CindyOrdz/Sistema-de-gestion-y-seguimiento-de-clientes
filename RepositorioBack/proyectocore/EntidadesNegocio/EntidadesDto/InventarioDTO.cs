using EntidadesNegocio.ElementosInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.EntidadesDto
{
    public class InventarioDTO
    {
        //Duda atributos correctos??
        private BigInteger id;
        private List<DetalleElemento> _elementos;
        private DateTime fechaInventario;
    }
}
