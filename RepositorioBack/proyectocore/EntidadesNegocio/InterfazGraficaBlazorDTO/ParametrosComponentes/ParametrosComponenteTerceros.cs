using EntidadesNegocio.InterfazGraficaBlazorDTO.ModelViews;
using System.Dynamic;

namespace EntidadesNegocio.InterfazGraficaBlazorDTO.ParametrosComponentes
{
    public static class ParametrosComponenteTerceros
    {
        public static List<ImpuestoTercero> impuestos = new List<ImpuestoTercero>
        {
            new ImpuestoTercero
            {
                Nombre = RETEFUENTE,
            },
            new ImpuestoTercero
            {
                Nombre = RETEIVA,
            },
            new ImpuestoTercero
            {
                Nombre = RETEICA,
            }
        };


        public const string RETEFUENTE = "ReteFuente";
        public const string RETEIVA = "ReteIVA";
        public const string RETEICA = "ReteICA";
        public const string NUEVO_TERCERO = "NUEVO_TERCERO";
        public const string ACTUALIZAR_TERCERO = "ACTUALIZAR_TERCERO";
        public const string NUEVA_SEDE = "NUEVA_SEDE";
        public const string ACTUALIZAR_SEDE = "ACTUALIZAR_SEDE";

        public const string NUEVO_DEPARTAMENTO = "NUEVO_DEPARTAMENTO";
        public const string ACTUALIZAR_DEPARTAMENTO = "ACTUALIZAR_DEPARTAMENTO";

        public const string NUEVA_CIUDAD = "NUEVA_CIUDAD";
        public const string ACTUALIZAR_CIUDAD = "ACTUALIZAR_CIUDAD";

        public const string EMPRESA = "EMPRESA";
        public const string CLIENTE = "CLIENTE";
        public const string EMPLEADO = "EMPLEADO";

        public const string INICIALES = "INICIALES";
        public const string IDENTIFICACION = "IDENTIFICACION";

        public const string PAGE = "PAGE";
        public const string COMPONENTE_MODAL = "COMPONENTE_MODAL";




    }
}
