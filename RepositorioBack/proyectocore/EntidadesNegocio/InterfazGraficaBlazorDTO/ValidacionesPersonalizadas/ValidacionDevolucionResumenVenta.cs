
using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Venta;
using EntidadesNegocio.InterfazGraficaBlazorDTO.ModelViews;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.ValidacionesPersonalizadas
{
    public class ValidacionDevolucionResumenVenta: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as ResumenVentaDTO;


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

        private bool Validacion(ResumenVentaDTO resumenVenta)
        {
            bool validacion = true;
            if (resumenVenta.Devolucion < 0)
            {
                validacion = false;
                ErrorMessage = "Los pagos deben completar el total de la venta";

            }

            return validacion;
        }
    }
}
