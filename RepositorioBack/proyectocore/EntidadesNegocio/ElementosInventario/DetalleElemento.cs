using System.Numerics;

namespace EntidadesNegocio.ElementosInventario
{
    public class DetalleElemento
    {
        private BigInteger id;
        private Elemento _elemento;
        private Double valor;
        private DateTime fechaDetalle;
        private Marca _marca;

        public DetalleElemento(BigInteger id, Elemento elemento, Double valor, DateTime fechaDetalle, Marca marca)
        {
            this.id = id;
            _elemento = elemento;
            this.valor = valor;
            this.fechaDetalle = fechaDetalle;
            _marca = marca;
        }

        public DetalleElemento(Elemento elemento, Double valor, DateTime fechaDetalle, Marca marca)
        {
            _elemento = elemento;
            this.valor = valor;
            this.fechaDetalle = fechaDetalle;
            _marca = marca;
        }

        public Elemento ObtenerElemento()
        {
            return _elemento;
        }

        public DateTime ObtenerFecha() {
            return fechaDetalle;
        }

        public Double ObtenerValor(){
            return valor;
        }
    }
}
