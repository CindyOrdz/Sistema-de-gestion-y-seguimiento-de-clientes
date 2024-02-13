

using System.Numerics;
using EntidadesNegocio.VentaOnlineTradicional;

namespace EntidadesNegocio.ElementosInventario
{
    public class ElementosDisponiblesParaVenta
    {
        private BigInteger id;
        private List<DetalleElemento> _elementos;
        private DateTime fechaInventario;

        public ElementosDisponiblesParaVenta(BigInteger id, List<DetalleElemento> elementos, DateTime fechaInventario)
        {
            this.id = id;
            _elementos = elementos;
            this.fechaInventario = fechaInventario;
        }

        public List<DetalleElemento> obtenerElementos() {
            return _elementos;
        }

        //HAY QUE HACER COMPARACIONES, SI LOS ELEMENTOS SON IGUALES, TRAE EL MÁS ACTUAL
        public List<DetalleElemento> inventarioActual()
        {
            DateTime fechaActual = DateTime.Now;
            var elementosConIgualNombreYFechaMasActual = _elementos
            .GroupBy(e => e.ObtenerElemento().ObtenerNombre()) // Agrupa los elementos por NOMBRE
            .Select(g => g.OrderBy(e => Math.Abs((e.ObtenerFecha() - fechaActual).TotalSeconds)).First()) // Ordena cada grupo por la diferencia de fechas y toma el primero
            .ToList();

            /*foreach (var elemento in elementosConIgualIdYFechaMasActual)
            {
                Console.WriteLine($"ID: {elemento.ObtenerElemento().ObtenerId()}, Nombre: {elemento.ObtenerElemento().ObtenerNombre()}, Fecha: {elemento.ObtenerFecha()}");
            }*/
            Console.WriteLine(" ");
            return elementosConIgualNombreYFechaMasActual;
        }


        public Boolean ActualizarInventarioDesdeVenta(Venta venta){
            venta.ObtenerDetallesVenta();

            foreach(DetalleVenta v in venta.ObtenerDetallesVenta()){
                var nombreElemento = v.ObtenerDetalleElemento().ObtenerElemento().ObtenerNombre();
                foreach(DetalleElemento detalleElemento in inventarioActual()){
                    if(v.ObtenerDetalleElemento() == detalleElemento){
                        Double cantAlmacen = detalleElemento.ObtenerElemento().ObtenerCantidadDisponibleAlmacen();
                        Double cantSolicitada = v.ObtenerCantidad();
                        if (cantAlmacen == 0){
                            System.Console.WriteLine($"El almacén no tiene {nombreElemento}");
                            detalleElemento.ObtenerElemento().ActualizarCantidadDisponibleAlmacen(cantSolicitada);
                        }
                        else if(cantAlmacen < cantSolicitada){
                            System.Console.WriteLine($"El almacén tiene {cantAlmacen} y se están solicitando {cantSolicitada}.");
                            detalleElemento.ObtenerElemento().ActualizarCantidadDisponibleAlmacen(cantSolicitada);
                        }
                        else{
                            System.Console.WriteLine($"El almacén tiene {cantAlmacen} y se están solicitando {cantSolicitada}.");
                            detalleElemento.ObtenerElemento().ActualizarCantidadDisponibleAlmacen(cantSolicitada);
                        }
                    }

                }
                
            }
            
            return false;

        }

        
    }
}
