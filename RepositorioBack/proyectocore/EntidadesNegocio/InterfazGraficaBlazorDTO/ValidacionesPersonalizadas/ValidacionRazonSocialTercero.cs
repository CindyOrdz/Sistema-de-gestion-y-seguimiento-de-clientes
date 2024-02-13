using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.InterfazGraficaBlazorDTO.ParametrosComponentes;
using System.ComponentModel.DataAnnotations;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.ValidacionesPersonalizadas
{
    public class ValidacionRazonSocialTercero: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as TerceroInterfazGraficaDTO;
            string tipoTercero = model.TipoTercero;


            var razonSocial = value as string;
            bool validacion = Validacion(razonSocial, model);

            if (validacion)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }
        }

        private bool Validacion(string razonSocial, TerceroInterfazGraficaDTO tercero)
        {
            bool validacion = true;
            var tipoTercero = tercero.TipoTercero;
            var nombre1 = tercero.Nombre1;
            var nombre2 = tercero.Nombre2;
            var apellido1 = tercero.Apellido1;
            var apellido2 = tercero.Apellido2;

            if (string.IsNullOrWhiteSpace(razonSocial) && tipoTercero.Equals(ParametrosComponenteTerceros.EMPRESA))
            {
                validacion = false;
                ErrorMessage = "La razon social es requerida";

            }

            if (string.IsNullOrWhiteSpace(razonSocial) && string.IsNullOrWhiteSpace(nombre1) && string.IsNullOrWhiteSpace(nombre2) && string.IsNullOrWhiteSpace(apellido1) && string.IsNullOrWhiteSpace(apellido2) && tipoTercero.Equals(ParametrosComponenteTerceros.CLIENTE))
            {
                validacion = false;
                ErrorMessage = "Debe digitar la razon social o la información personal ";
            }

            if (!string.IsNullOrWhiteSpace(razonSocial) && (!string.IsNullOrWhiteSpace(nombre1) || !string.IsNullOrWhiteSpace(nombre2) || !string.IsNullOrWhiteSpace(apellido1) || !string.IsNullOrWhiteSpace(apellido2)) && tipoTercero.Equals(ParametrosComponenteTerceros.CLIENTE))
            {
                validacion = false;
                ErrorMessage = "Debe digitar la razon social o la información personal ";
            }

            return validacion;
        }
    }
}
