using EntidadesNegocio.InterfazGraficaBlazorDTO.ModelViews;
using EntidadesNegocio.InterfazGraficaBlazorDTO.ParametrosComponentes;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.ValidacionesPersonalizadas
{
    public class ValidacionOpcionesFiltrado: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as OpcionesFiltrado;

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

        private bool Validacion(OpcionesFiltrado opcionesFiltrado)
        {
            bool validacion = true;
            var opcion = opcionesFiltrado.FiltroSeleccionado;

            if (opcion.Equals(ParametrosComponenteTerceros.IDENTIFICACION))
            {
                string identificacion = opcionesFiltrado.CadenaParaFiltrar;
                string patronRegex = @"^\d{8,15}$";
                bool resultadoRegex = Regex.IsMatch(identificacion, patronRegex);
                if (resultadoRegex is false)
                {
                    validacion = false;
                    ErrorMessage = "la identificacion debe contener entre 8 y 15 dígitos ";
                }
            }

            if (opcion.Equals(ParametrosComponenteTerceros.INICIALES))
            {
                string cadena = opcionesFiltrado.CadenaParaFiltrar;
                string patronRegex = @"^[\p{L}\p{M}]{3,20}$";
                bool resultadoRegex = Regex.IsMatch(cadena, patronRegex);
                if (resultadoRegex is false)
                {
                    validacion = false;
                    ErrorMessage = "solo se aceptan caracteres del alfabeto rango(3,20)";
                }

            }

            return validacion;
        }
    }
}

