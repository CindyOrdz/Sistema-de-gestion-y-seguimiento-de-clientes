using EntidadesNegocio.Impuestos;
using EntidadesNegocio.Pagos;
using EntidadesNegocio.Terceros;
using EntidadesNegocio.VentaOnlineTradicional;

namespace EntidadesNegocio.EntidadesDto
{

    
    public class FacturaDto
    {
        public Tercero cliente;
        public Tercero empresaVendedora;
        public Empleado responsable;
        public Empleado responsableComision;
        public Double totalCosto;
        public Double descuento;
        public Double porcentajeDescuento;
        public String empresa;
        public String tipoPrecio;
        public String entregarEn;
        public String usuario;
        public String pc;
        public String tipoVenta;
        public DateTime fecha;
        public DateTime hora;
        public List<Pago> formaDePago;
        public List<Impuesto> impuestos;
        public Double subTotal;
        public Double iva;
        public Double total;
        public String orden;
        public String origen;
        public String impreso;
        public Double reteFuente;
        public Double reteIva;
        public Double reteIca;
        public String legalizado;
        public Double tasaCambio;
        public List<DetalleVenta> detalle;

    }
}
