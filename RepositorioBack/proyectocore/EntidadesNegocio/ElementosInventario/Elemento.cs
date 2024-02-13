using System.Numerics;

namespace EntidadesNegocio.ElementosInventario
{
    public class Elemento
    {
        private BigInteger id;
        private String tipoElemento;
        private String nombre;
        private String ref1;
        private String ref2;
        private String ref3;
        private String ref4;
        private String sett;
        
        private BigInteger unidad;
        private Double valor;
        private String tipo;
        private Double cantidadComprados;
        private Double cantidadDisponibleAlmacen;
        private Double cantidadDisponibleBodega;
        private Double cantidadVendidos;
        private Double ref11;
        private Double ref12;
        private Double ref13;
        private Double ref14;
        private Double ref15;
        private Double ref16;
        private String ref5;
        private Double impuestoIva;
        private Unidad _unidad;
       

        public Elemento()
        {
        }

        

        public Elemento(BigInteger id,String tipoElemento, String nombre,Unidad Unidad, String ref1, String ref2, String ref3, String ref4, String ref5, String sett, Double valor, String tipo, Double cantidadComprados, Double cantidadDisponibleAlmacen,Double cantidadDisponibleBodega, Double cantidadVendidos, Double ref11, Double ref12, Double ref13, Double ref14, Double ref15, Double ref16, Double impuestoIva)
        {
            //Inicializar();
            this.id = id;
            this.impuestoIva = impuestoIva;
            this.tipoElemento = tipoElemento;
            this.nombre = nombre;
            _unidad = Unidad;
            this.ref1 = ref1;
            this.ref2 = ref2;
            this.ref3 = ref3;
            this.ref4 = ref4;
            this.sett = sett;
            //this.unidad = unidad;
            this.valor = valor;
            this.tipo = tipo;
            this.cantidadComprados = cantidadComprados;
            this.cantidadDisponibleAlmacen = cantidadDisponibleAlmacen;
            this.cantidadDisponibleBodega = cantidadDisponibleBodega;
            this.cantidadVendidos = cantidadVendidos;
            this.ref11 = ref11;
            this.ref12 = ref12;
            this.ref13 = ref13;
            this.ref14 = ref14;
            this.ref15 = ref15;
            this.ref16 = ref16;
            this.ref5 = ref5;
            
        }

        public Elemento(BigInteger id, String ref1, Double valor, Double cantidadDisponibleBodega, Double cantidadDisponibleAlmacen, Double impuestoIva)
        {
            this.id = id;
            this.ref1 = ref1;
            this.valor = valor;
            this.cantidadDisponibleAlmacen = cantidadDisponibleAlmacen;
            this.cantidadDisponibleBodega = cantidadDisponibleBodega;
            this.impuestoIva = impuestoIva; 
        }


        public override string ToString()
        {
            return "Elemento{" + "id=" + id + ", tipoElemento=" + tipoElemento + ", nombre=" + nombre  + ", ref1=" + ref1 + ", ref2=" + ref2 + ", ref3=" + ref3 + ", ref4=" + ref4 + ", sett=" + sett + ", unidad=" + unidad + ", valor=" + valor + ", tipo=" + tipo + ", cantidad_comprados=" + cantidadComprados + ", cantidad_disponible_almacen=" + cantidadDisponibleAlmacen + ", cantidad_disponible_bodega=" + cantidadDisponibleBodega + ", cantidad_vendidos=" + cantidadVendidos + ", ref11=" + ref11 + ", ref12=" + ref12 + ", ref13=" + ref13 + ", ref14=" + ref14 + ", ref15=" + ref15 + ", ref16=" + ref16 + ", ref5=" + ref5 + ", impuestoIva=" + impuestoIva + '}';
        }

        public Double ObtenerImpuestoIva()
        {
            return impuestoIva;
        }

        public String ObtenerTipo() {
            return tipoElemento;
        }

        public String ObtenerNombre()
        {
            return nombre;
        }

        public Double ObtenerValor() {
            return valor;
        }

       public BigInteger ObtenerId() { 
            return id; 
        }

        public Double ObtenerCantidadDisponibleAlmacen(){
            return cantidadDisponibleAlmacen;
        }

        public void ActualizarCantidadDisponibleAlmacen(Double Cantidad){
            cantidadDisponibleAlmacen -= Cantidad;
            cantidadVendidos += Cantidad;
            if (cantidadDisponibleAlmacen < 0){
                cantidadDisponibleBodega += cantidadDisponibleAlmacen;
            }
            System.Console.WriteLine($"El elemento {nombre} ahora tiene {cantidadDisponibleAlmacen} en almacén, {cantidadDisponibleBodega} en bodega y se han vendido {cantidadVendidos}");
        }

    }
}
