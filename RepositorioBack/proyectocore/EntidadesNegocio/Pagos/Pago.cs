using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.Pagos
{
    public class Pago
    {
        private Double valor;
        private String valorEnLetras;
        private String nombreDelPago;

        public Pago(Double valor, String nombreDelPago)
        {
            this.valor = valor;
            this.nombreDelPago = nombreDelPago;
            this.valorEnLetras = CalcularValorEnLetras();
        }

        private String CalcularValorEnLetras()
        {
            return "calcularValorEnLetras()";
        }

        public Double ObtenerValor() 
        { 
            return valor;
        }
        public String ObtenerValorEnLetras()
        {

            return valorEnLetras;
        }

        public String ObtenerNombreDelPago()
        { 
            return nombreDelPago;
        }

        public void ModificarNombreDelPago(String nombreDelPago)
        {
            this.nombreDelPago = nombreDelPago;
        }


        public override String ToString()
        {
            return $"Pago{{ Valor={valor}, ValorEnletras={valorEnLetras} }}";
        }
    }
}
