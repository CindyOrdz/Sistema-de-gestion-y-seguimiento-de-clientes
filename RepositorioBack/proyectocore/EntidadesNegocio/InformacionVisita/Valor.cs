using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace EntidadesNegocio.InformacionVisita
{
    public class Valor
    {
        private Double valorFijo;
        private Double rangoInicial;
        private Double rangoFinal;

        public Valor(double valorFijo, double rangoInicial, double rangoFinal)
        {
            this.valorFijo = valorFijo;
            this.rangoInicial = rangoInicial;
            this.rangoFinal = rangoFinal;
        }

        public Double ObtenerValorFijo()
        {
            return valorFijo;
        }

        public Double ObtenerRangoInicial()
        {
            return rangoInicial;
        }

        public Double ObtenerRangoFinal()
        {
            return rangoFinal;
        }

        public String ObtenerRangoCompleto()
        {
            return rangoInicial.ToString() + " - " + rangoFinal.ToString();
        }

        public void ModificarValorFijo(Double fijo)
        {
            this.valorFijo = fijo;
        }

        public void ModificarRangoIncial(Double incial)
        {
            this.rangoInicial = incial;
        }

        public void ModificarRangoFinal(Double final)
        {
            this.rangoFinal = final;
        }

    }
}
