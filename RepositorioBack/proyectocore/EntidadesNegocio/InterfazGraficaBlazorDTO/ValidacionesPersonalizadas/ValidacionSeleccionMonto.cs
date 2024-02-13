using EntidadesNegocio.InterfazGraficaBlazorDTO.ModelViews;
using System.ComponentModel.DataAnnotations;


namespace EntidadesNegocio.InterfazGraficaBlazorDTO.ValidacionesPersonalizadas
{
    public class ValidacionSeleccionMonto: ValidationAttribute
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

            if (!opcionesImpuestos.ConMontoPersonalizado && !opcionesImpuestos.ConMontoDeLEY)
            {
                validacion = false;
                ErrorMessage = "Debe seleccionar un tipo de Monto";

            }

            return validacion;
        }
    }
}
