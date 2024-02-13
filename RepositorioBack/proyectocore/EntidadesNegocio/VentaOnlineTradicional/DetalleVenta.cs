
using System.Numerics;
using System.Text;
using EntidadesNegocio.ElementosInventario;
using EntidadesNegocio.Impuestos;

namespace EntidadesNegocio.VentaOnlineTradicional
{
    public class DetalleVenta
    {
       
        private BigInteger item;
        private DetalleElemento _detalleElemento;
        private Double cantidad;
        private Double valor;
        private Double subTotal;
        private Double impuesto;
        private Double totalDetalleVenta;
        private List<Impuesto> _impuestos;

        public DetalleVenta(DetalleElemento detalleElemento, Double cantidad, Double valor)
        {
            _detalleElemento = detalleElemento;
            this.cantidad = cantidad;
            this.valor = valor; 
            _impuestos = new List<Impuesto>();
            impuesto = _detalleElemento.ObtenerElemento().ObtenerImpuestoIva() / 100; 
            Double subTotalSinRedondear = this.cantidad * this.valor;
            subTotal = OperacionesDian.RedondeoDIAN(subTotalSinRedondear,2);
            totalDetalleVenta = subTotal + OperacionesDian.RedondeoDIAN(subTotal * impuesto, 2); 
        }

        public DetalleVenta(BigInteger item, DetalleElemento detalleElemento, Double cantidad, Double valor)
        {
            this.item = item;
            _detalleElemento = detalleElemento;
            this.cantidad = cantidad;
            this.valor = valor;
            _impuestos = new List<Impuesto>();
            impuesto = _detalleElemento.ObtenerElemento().ObtenerImpuestoIva() / 100;
            Double subTotalSinRedondear = this.cantidad * this.valor;
            subTotal = OperacionesDian.RedondeoDIAN(subTotalSinRedondear, 2);
            totalDetalleVenta = subTotal + OperacionesDian.RedondeoDIAN(subTotal * impuesto, 2);
        }

        public BigInteger ObtenerItem()
        {
            return item;
        }

        public Double ObtenerSubTotal()
        {
            return subTotal;
        }

        public Double ObtenerImpuesto()
        {
            return impuesto;
        }

        public DetalleElemento ObtenerDetalleElemento(){
            return _detalleElemento;
        }

        public Double ObtenerCantidad(){
            return cantidad;
        }

        public void AdicionarImpuestosDetalleVenta(List<Impuesto> impuestos)
        {
            impuestos.ForEach(impuesto => 
            {
                _impuestos.Add(impuesto);
            });
        }

        public void AdicionarImpuesto(Impuesto impuesto)
        {
            _impuestos.Add(impuesto);
        }


        public List<Impuesto> ObtenerImpuestos()
        {
            return _impuestos;
        }

        public void ModificarTotalDetalleVenta(Double totalDetalleVenta)
        {
            this.totalDetalleVenta = totalDetalleVenta;
        
        }

        public override string ToString()
        {
            StringBuilder impuestosStr = new StringBuilder();
            foreach (Impuesto impuesto in _impuestos)
            {
                impuestosStr.Append(impuesto.ToString());
                impuestosStr.Append(", ");
            }
            return "DetalleVenta{" + "item=" + item + ", articulo=" + _detalleElemento + ", cantidad=" + cantidad + ", valor=" + valor.ToString("C") + ", subtotal=" + subTotal + ", impuestos=[" + impuestosStr.ToString() + "]}";
        }
    }
} 