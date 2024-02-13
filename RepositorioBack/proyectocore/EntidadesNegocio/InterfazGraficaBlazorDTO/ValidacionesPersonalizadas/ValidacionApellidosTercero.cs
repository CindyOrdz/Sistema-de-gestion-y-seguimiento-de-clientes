using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using EntidadesNegocio.InterfazGraficaBlazorDTO.ParametrosComponentes;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.ValidacionesPersonalizadas
{
    public class ValidacionApellidosTercero : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as TerceroInterfazGraficaDTO;
            string tipoTercero = model.TipoTercero;
            var member = validationContext.MemberName;

            var apellido = value as string;
            bool validacion = Validacion(apellido, model);

            if (validacion)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }
        }

        private bool Validacion(string apellido, TerceroInterfazGraficaDTO tercero)
        {
            bool validacion = true;
            var tipoTercero = tercero.TipoTercero;
            var razonSocial = tercero.RazonSocial;

            if (string.IsNullOrWhiteSpace(apellido) && tipoTercero.Equals(ParametrosComponenteTerceros.EMPLEADO))
            {
                validacion = false;

            }

            if (string.IsNullOrWhiteSpace(apellido) && string.IsNullOrWhiteSpace(razonSocial) && tipoTercero.Equals(ParametrosComponenteTerceros.CLIENTE))
            {
                validacion = false;

            }
            return validacion;
        }
    }
}
