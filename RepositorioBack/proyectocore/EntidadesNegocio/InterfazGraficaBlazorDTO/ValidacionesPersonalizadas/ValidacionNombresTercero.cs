using EntidadesNegocio.InterfazGraficaBlazorDTO.InterfazGraficaVentaDTO.Terceros;
using EntidadesNegocio.InterfazGraficaBlazorDTO.ParametrosComponentes;
using System.ComponentModel.DataAnnotations;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.ValidacionesPersonalizadas
{
    public class ValidacionNombresTercero : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as TerceroInterfazGraficaDTO;
            string tipoTercero = model.TipoTercero;
            var member = validationContext.MemberName;
            
            var nombre = value as string;
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

        private bool Validacion(TerceroInterfazGraficaDTO tercero)
        {
            bool validacion = true;
            var nombre1 = tercero.Nombre1;
            var nombre2 = tercero.Nombre2;
            var razonSocial = tercero.RazonSocial;  

            if ((string.IsNullOrWhiteSpace(nombre1) && string.IsNullOrWhiteSpace(nombre2)) && tercero.TipoTercero.Equals(ParametrosComponenteTerceros.EMPLEADO))
            {
                validacion = false;
                this.ErrorMessage = "Debe digitar al menos un nombre";

            }

            if ((string.IsNullOrWhiteSpace(nombre1) && string.IsNullOrWhiteSpace(nombre2) && (string.IsNullOrWhiteSpace(razonSocial)) && tercero.TipoTercero.Equals(ParametrosComponenteTerceros.CLIENTE)))
            {
                validacion = false;
                this.ErrorMessage = "Debe digitar al menos un nombre";

            }



            return validacion;
        }
    }

}
