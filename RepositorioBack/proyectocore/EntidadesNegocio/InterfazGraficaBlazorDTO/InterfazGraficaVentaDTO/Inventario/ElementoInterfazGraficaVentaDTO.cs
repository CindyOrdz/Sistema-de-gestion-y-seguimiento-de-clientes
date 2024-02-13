
using System.ComponentModel.DataAnnotations;
namespace EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Inventario
{
    public class ElementoInterfazGraficaVentaDTO
    {
        [Required(ErrorMessage = "El artículo es obligatorio")]
        public long CodigoArticulo { get; set; }
        [Required(ErrorMessage = "El tipo es obligatorio")]
        public string Tipo { get; set; } = string.Empty;
        public string TipoElemento { get; set; } = string.Empty;
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "La REF1 es obligatoria")]
        public string Ref1 { get; set; } = string.Empty;
        [Required(ErrorMessage = "La REF2 es obligatoria")]
        public string Ref2 { get; set; } = string.Empty;
        [Required(ErrorMessage = "La REF3 es obligatoria")]
        public string Ref3 { get; set; } = string.Empty;
        [Required(ErrorMessage = "La REF4 es obligatoria")]
        public string Ref4 { get; set; } = string.Empty;
        [Required(ErrorMessage = "La REF5 es obligatoria")]
        public string Ref5 { get; set; } = string.Empty;
        public Double Ref11 { get; set; }
        public Double Ref12 { get; set; }
        public Double Ref13 { get; set; }
        public Double Ref14 { get; set; }
        public Double Ref15 { get; set; }
        public Double Ref16 { get; set; }
        [Required(ErrorMessage = "El SET es obligatorio")]
        public string Sett { get; set; } = string.Empty;
        [Required(ErrorMessage = "La unidad es obligatoria")]
        public UnidadInterfazGraficaVentaDTO? Unidad { get; set; }
        public Double CantidadVendidos { get; set; }
        public string Referencia { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public int IdMarca { get; set; }
        public double UltimaVenta { get; set; } = 0;
        public double ValorUnitario { get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "El valor debe ser mayor que cero.")]
        public double Valor { get; set; }
        public double CantidadDisponibleBodega { get; set; }
        public double CantidadDisponibleAlmacen { get; set; }
        public int CantidadSolicitadaEnCarrito { get; set; }
        public double PorcentajeIva { get; set; }
        public string ClaseCss { get; set; } = string.Empty;
        public long IdEmpresa { get; set; }
        public string Empresa { get; set; } = string.Empty;
        public string url_imagen { get; set; } = string.Empty;
        public string Catalogo { get; set; } = string.Empty;
        public long IdCatalogo { get; set; }
    }
}
