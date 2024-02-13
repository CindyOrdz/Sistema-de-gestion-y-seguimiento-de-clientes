using EntidadesNegocio.ElementosInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.EntidadesDto
{
    public class ElementoInventarioDTO
    {
        public long CodigoElemento { get; set; }
        public string TipoElemento { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int IdMarca { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
        public string Sett { get; set; } = string.Empty;
        public double Valor { get; set; }
        public Double Cantidad { get; set; }
        public Double CantidadDisponible { get; set; }
        public Double CantidadAlmacen { get; set; }
        public Double CantidadVendidos { get; set; }
        public double PorcentajeIva { get; set; }
        public BigInteger IdRevision { get; set; }
    }
}
