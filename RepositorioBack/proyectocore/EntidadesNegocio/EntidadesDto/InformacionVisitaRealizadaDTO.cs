using EntidadesNegocio.InformacionVisita;
using RegistroEvidencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.EntidadesDto
{
    public class InformacionVisitaRealizadaDTO
    {
        private BigInteger id;
        private AgendaVisitaEmpresaCliente _visita;
        private List<RegistroEvidencia_> _registroEvidenciaVisita;
        private String descripcion;
    }
}
