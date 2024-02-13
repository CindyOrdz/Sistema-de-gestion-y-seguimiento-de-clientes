namespace EntidadesNegocio.LugarProcedencia
{
    public class Lugares
    {
        private List<Pais> paises;
        private List<DepartamentoProvincia> departamentos;
        private List<Ciudad> ciudades;

        public Lugares()
        {
            //llenar arrays
        }




        public Pais BuscarPais(String nombre)
        {
            return new Pais();
        }

        public DepartamentoProvincia BuscarDepartamentoProvincia(String pais, String nombreDepartamentoProvincia)
        {
            return new DepartamentoProvincia();
        }

        public Ciudad BuscarPais(String pais, String nombreDepartamentoProvincia, String nombreCiudad)
        {
            return new Ciudad();
        }
    }
}
