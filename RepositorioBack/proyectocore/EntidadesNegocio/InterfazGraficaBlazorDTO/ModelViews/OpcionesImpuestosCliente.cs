
using EntidadesNegocio.InterfazGraficaBlazorDTO.ValidacionesPersonalizadas;
using System.ComponentModel.DataAnnotations;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.ModelViews
{
    public class OpcionesImpuestosCliente
    {
        [ValidacionSeleccionMonto]
        public bool ConMontoDeLEY { get; set; } = true;
        public bool ConMontoPersonalizado { get; set; } = false;

        [ValidacionMontoImpuesto]
        public double MontoPersonalizado { get; set; }
    }
}
