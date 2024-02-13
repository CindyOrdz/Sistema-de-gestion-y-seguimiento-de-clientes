using EntidadesNegocio.ElementosInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.EntidadesDto
{
    public class ElementosCompradosDTO
    {
        //Duda atributos correctos??
        private BigInteger id;
        private String tipoElemento;
        private String nombre;
        private Marca _marca;
        private Double cantidadComprados;
        private Double impuestoIva;
        private Unidad _unidad;
    }
}
