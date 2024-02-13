
using EntidadesNegocio.Impuestos;

namespace EntidadesNegocio.Descuentos
{
    public  class ValoresDescuentos
    {
        private  static ValoresDescuentos? _instancia;
        public  Double PorcentajeDescuentoPorCliente { get; set; }
        public  Double PorcentajeDescuentoValorCompra { get; set; }
        public  Double PorcentajeDescuentoPorPorcentaje { get; set; }

        private ValoresDescuentos()
        {
            this.PorcentajeDescuentoPorCliente = 0.0;
            this.PorcentajeDescuentoValorCompra = 0.0;
            this.PorcentajeDescuentoPorPorcentaje= 0.0; 
        
        }


        public static ValoresDescuentos Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new ValoresDescuentos();
                }
                return _instancia;
            }

        }
    }
}
