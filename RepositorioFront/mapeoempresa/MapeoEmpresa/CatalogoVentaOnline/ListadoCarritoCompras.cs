using EntidadesNegocio.LugarProcedencia;
using EntidadesNegocio.Terceros;
using System.Numerics;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Inventario;
using EntidadesNegocio.ElementosInventario;
namespace CatalogoVentaOnline.ListadoCarritoCompras
{
    public class ListadoCarritoCompras
    {
        private static List<ElementoInterfazGraficaVentaDTO> elementosCarrito = new List<ElementoInterfazGraficaVentaDTO> { };
        public static void InsertarElementoAlCarrito(ElementoInterfazGraficaVentaDTO elemento)
        {
            elementosCarrito.Add(elemento);
        }
        public static void EliminarElementoDelCarrito(ElementoInterfazGraficaVentaDTO elemento)
        {
            elementosCarrito.Remove(elemento);
        }
        public static List<ElementoInterfazGraficaVentaDTO> ObtenerElementosCarrito() => new List<ElementoInterfazGraficaVentaDTO>(elementosCarrito);
    }
}