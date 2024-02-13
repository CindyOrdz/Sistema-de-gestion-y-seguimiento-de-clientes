using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.Pagos
{
    public class ContadoCheque : Contado
    {
        private String banco;
        private String cuenta;
        private String tipoCuenta;
        private String titularCuenta;
        private String nitTitularCuenta;

        public ContadoCheque(Double Valor, String NombreDelPago = "contadoCheque") : base(Valor, NombreDelPago)
        {
        }


        public String ObtenerBanco()
        {
            return banco;
        }

        public void ModificarBanco(String banco)
        {
            this.banco = banco;
        }

        public String ObtenerCuenta()
        {
            return cuenta;
        }

        public void ModificarCuenta(String cuenta)
        {
            this.cuenta = cuenta;
        }

        public String ObtenerTipoCuenta()
        {
            return tipoCuenta;
        }

        public void ModificarTipoCuenta(String tipoCuenta)
        {
            this.tipoCuenta = tipoCuenta;
        }

        public String ObtenerTitularCuenta()
        {
            return titularCuenta;
        }

        public void ModificarTitularCuenta(string titularCuenta)
        {
            this.titularCuenta = titularCuenta;
        }

        public String ObtenerNitTitularCuenta()
        {
            return nitTitularCuenta;
        }

        public void ModificarNitTitularCuenta(String nitTitularCuenta)
        {
            this.nitTitularCuenta = nitTitularCuenta;
        }

        public override String ToString()
        {
            return base.ToString() + "ContadoCheque{ banco=" + banco + ", cuenta=" + cuenta + ", tipoCuenta=" + tipoCuenta + ", titularCuenta=" + titularCuenta + ", nitTitularCuenta=" + nitTitularCuenta + " }";
        }
    }
}
