using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.InterfazGraficaBlazorDTO.ParametrosComponentes;
using System.ComponentModel.DataAnnotations;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.ValidacionesPersonalizadas
{
    public class ValidacionNaturalezaTercero: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as TerceroInterfazGraficaDTO;
            string tipoTercero = model.TipoTercero;


            var naturalezaTercero = value as string;
            bool validacion = Validacion(naturalezaTercero, tipoTercero);

            if (validacion)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }
        }

        private bool Validacion(string naturalezaTercero, string tipoTercero)
        {
            bool validacion = true;

            if (string.IsNullOrWhiteSpace(naturalezaTercero) && tipoTercero.Equals(ParametrosComponenteTerceros.CLIENTE))
            {
                validacion = false;
                ErrorMessage = "La naturaleza es requerida";

            }

            return validacion;
        }
    }
}
