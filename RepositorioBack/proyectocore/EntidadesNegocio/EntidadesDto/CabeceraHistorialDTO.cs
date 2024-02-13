using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Venta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.EntidadesDto
{
    public class CabeceraHistorialDTO
    {
        public long IdVenta { get; set; }
        public string NombreResponsable { get; set; } = string.Empty;
        public string TipoVenta { get; set; } = string.Empty;
        public DateOnly FechaVenta { get; set; }
        public TimeOnly Hora { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }
        public double IVA { get; set; }
        public List<DetalleVentaHistorialClienteDTO> Detalles { get; set; }
        public double IdCliente { get; set; }
        public string NombreCliente { get; set; } = string.Empty;

    }
}
