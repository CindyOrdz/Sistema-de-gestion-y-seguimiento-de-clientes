using EntidadesNegocio.ElementosInventario;
using EntidadesNegocio.GestionInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.EntidadesDto
{
    public class AnomaliaInventarioDTO
    {
        public BigInteger IdAnomalia { get; set; }
        public String Descripcion { get; set; } = string.Empty;
        public RevisionInventarioDTO Revision { get; set; }
        public ElementoInventarioDTO Elemento { get; set; }
        public long IdElemento { get; set; }
        public String NombreArchivo { get; set; } = string.Empty;
        public String TipoArchivo { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaCaptura { get; set; }
        public String Ruta { get; set; } = string.Empty;
        public BigInteger IdEvidencia { get; set; }



    }
}
