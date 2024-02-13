
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.InterfazGraficaBlazorDTO.ModelViews;
using EntidadesNegocio.InterfazGraficaBlazorDTO.ParametrosComponentes;
using System.ComponentModel.DataAnnotations;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.ValidacionesPersonalizadas
{
    public class ValidacionMontoImpuesto: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as OpcionesImpuestosCliente;


            bool validacion = Validacion(model);

            if (validacion)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }
        }

        private bool Validacion(OpcionesImpuestosCliente opcionesImpuestos)
        {
            bool validacion = true;

            if (opcionesImpuestos.ConMontoPersonalizado && opcionesImpuestos.MontoPersonalizado <= 0)
            {
                validacion = false;
                ErrorMessage = "El monto personalizado debe ser mayor a 0";

            }

            return validacion;
        }
    }
}
