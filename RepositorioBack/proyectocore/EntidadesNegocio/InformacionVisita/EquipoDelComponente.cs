using EntidadesNegocio.ElementosInventario;
using EntidadesNegocio.Terceros;
using System.Numerics;


namespace EntidadesNegocio.InformacionVisita
{
    public class EquipoDelComponente
    {
        private BigInteger id;
        private Componente _componente;
        private String nombre;
        private String descripcion;
        private List<CondicionOperativaInicial> _condicionesOperativasIniciales;
        private List<CondicionOperativaReal> _condicionesOperativasReal;
        private List<RegistroMedidasParte> _registroMedidas;

        private List<Parte> listaPartes;

        public EquipoDelComponente(Componente componente, String nombre, String descripcion)
        {  
            this.nombre = nombre;
            this.descripcion = descripcion;
            _componente = componente;
            _condicionesOperativasIniciales = new List<CondicionOperativaInicial>();
            _condicionesOperativasReal = new List<CondicionOperativaReal>();
            _registroMedidas = new List<RegistroMedidasParte>();
            listaPartes = new List<Parte>();
        }

        public EquipoDelComponente(BigInteger id,String nombre, String descripcion)
        {
            this.id = id;
            this.nombre = nombre;
            this.descripcion = descripcion;

        }
        
        public override String ToString()
        {
            return "\nEquipo{" + "id=" + id + ", componente=" + _componente + ", nombre=" + nombre + ", descripcion=" + descripcion + ", condicionesOperativasIniciales=" + _condicionesOperativasIniciales + ", condicionesOperativasReal=" + _condicionesOperativasReal +  ", registroMedidas =" + _registroMedidas + "}";
        }
        
        public BigInteger ObtenerId() { return id; }

        public Componente ObtenerComponente() { return _componente; }

        public String ObtenerNombre() { return nombre; }

        public String ObtenerDescripcion() { return descripcion; }

        public BigInteger ObtenerIdComponente()
        {
            return _componente.ObtenerId();
        }

        public List<CondicionOperativaInicial> ObtenerCondicionesOperativasIniciales() { return _condicionesOperativasIniciales; }

        public List<CondicionOperativaReal> ObtenerCondicionesOperativasReales() { return _condicionesOperativasReal; }

        public List<Parte> ObtenerPartes() { return listaPartes; }

        public List<RegistroMedidasParte> ObtenerRegistroMedidas() { return new List<RegistroMedidasParte> { }; }

        public void ModificarNombre(String nombre)
        {
            this.nombre = nombre;
        }

        public void ModificarDescripcion(String descripcion)
        {
            this.descripcion = descripcion;
        }
    }
}
