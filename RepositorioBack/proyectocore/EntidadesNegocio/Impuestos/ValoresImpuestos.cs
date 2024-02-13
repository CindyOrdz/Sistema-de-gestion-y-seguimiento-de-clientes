
using System.ComponentModel.DataAnnotations;

namespace EntidadesNegocio.Impuestos
{
    public class ValoresImpuestos
    {
        private static ValoresImpuestos? _instancia;
        public Double MontoDeLey { get; set; }
        public Double PorcentajeReteFuente { get; set; }
        public Double PorcentajeReteIva { get; set; }
        public Double PorcentajeReteIca { get; set; }
        public Double PorcentajeConsumoNacional { get; set; }
        public string ConMontoDeLey { get; set; }
        public String ConMontoPersonalizado { get; set; }
        public Double MontoPersonalizado { get; set; }

        private ValoresImpuestos() 
        {
            this.MontoDeLey = 0.0;
            this.PorcentajeReteIva= 0.0;
            this.PorcentajeReteFuente= 0.0;
            this.PorcentajeConsumoNacional= 0.0;
            this.PorcentajeReteIca = 0.0;
            this.MontoPersonalizado = 0.0;
        }

        public static ValoresImpuestos Instancia
        {
            get
            {
                if (_instancia is null)
                { 
                    _instancia = new ValoresImpuestos();
                }
                return _instancia;
            }
        }

        


        


    }
}
