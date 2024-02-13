using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.InterfazGraficaBlazorDTO.ParametrosComponentes;
using System.ComponentModel.DataAnnotations;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.ValidacionesPersonalizadas
{
    public class ValidacionActividadEconomicaTercero: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as TerceroInterfazGraficaDTO;
            string tipoTercero = model.TipoTercero;


            var ActividadEconomica = value as string;
            bool validacion = Validacion(ActividadEconomica, tipoTercero);

            if (validacion)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }
        }

        private bool Validacion(string ActividadEconomica, string tipoTercero)
        {
            bool validacion = true;

            if (string.IsNullOrWhiteSpace(ActividadEconomica) && tipoTercero.Equals(ParametrosComponenteTerceros.CLIENTE))
            {
                validacion = false;
                ErrorMessage = "La actividad economica es requerida";

            }

            return validacion;
        }
    }
}
