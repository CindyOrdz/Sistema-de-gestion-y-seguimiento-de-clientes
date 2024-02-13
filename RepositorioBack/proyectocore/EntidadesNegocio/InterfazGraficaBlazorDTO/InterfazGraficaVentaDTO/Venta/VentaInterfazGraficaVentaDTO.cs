using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.ImpuestosYDescuentos;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Inventario;
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using System.ComponentModel.DataAnnotations;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Venta
{
    public class VentaInterfazGraficaVentaDTO
    {
        public long IdVentaTransaccion { get; set; }
        public double Id { get; set; }
        public double Numero { get; set; }
        public List<DetalleVentaInterfazGraficaVentaDTO> DetallesVenta { get; set; } = new List<DetalleVentaInterfazGraficaVentaDTO> { new DetalleVentaInterfazGraficaVentaDTO { Elemento = new ElementoInterfazGraficaVentaDTO() } };
        public List<ImpuestoInterfazGraficaVentaDTO> Impuestos { get; set; } = new List<ImpuestoInterfazGraficaVentaDTO>();
        public List<DescuentoInterfazGraficaVentaDTO> Descuentos { get; set; } = new List<DescuentoInterfazGraficaVentaDTO>();
        public EmpresaInterfazGraficaVentaDTO? Empresa { get; set; }
        public EmpleadoInterfazGraficaVentaDTO? Empleado { get; set; }
        public TerceroInterfazGraficaDTO? Cliente { get; set; }
        public DateOnly FechaVenta { get; set; }
        public TimeOnly Hora { get; set; }
        public string TipoVenta { get; set; } = string.Empty;
        public string TipoPago { get; set; } = string.Empty;
        public string TipoPrecio { get; set; } = string.Empty;

        [RegularExpression(@"^[\p{L}0-9\s,.]+$", ErrorMessage = "solo se permiten caracteres alfanuméricos con espacios, comas y puntos")]
        public string DescripcionOrden { get; set; } = string.Empty;
        public string DestinoProducto { get; set; } = string.Empty;
        public double Descuento { get; set; }
        public double PorcentajeDescuento { get; set; }
        public double ValorEfectivo { get; set; }
        public double  ValorCredito { get; set; }
        public double ValorCheque { get; set; }
        public double ValorTarjeta { get; set; }
        public double SubTotal { get; set; }
        public double IVA { get; set; }
        public double Total { get; set; }
        public string PC { get; set; } = string.Empty;
        public string Origen { get; set; } = string.Empty;
        public bool Anulada { get; set; } = false;
        public string ClaseCss { get; set; } = string.Empty;



    }
}
