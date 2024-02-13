using EntidadesNegocio.LugarProcedencia;
using System.Numerics;

namespace EntidadesNegocio.Terceros
{
    public class Tercero
    {

        private BigInteger identificacion;
        private BigInteger digito;
        private BigInteger tipo;
        private String razonSocial;
        private String nombre1;
        private String nombre2;
        private String apellido1;
        private String apellido2;
        private Ubicacion _ubicacion;
        private String telefono;
        private String email;
        private String celular;
        private String tipoTercero;
        private String direccion;
        private EnumTipoDocumento enumTipoDocumento;

        private String regimen;
        private BigInteger tipoProveedor;
        private String reteFuente;
        private String reteIva;
        private String reteIca;
        private String naturaleza;
        private String actividadEconomica;
        private String empresa;
        private String calcularConMonto;
        private String conMontoDeLey;
        private String conMontoPersonalizado;
        private Double montoPersonalizado;
        private Double tarifaReteFuente;
        private Double tarifaReteIva;
        private String tipoRegimen;
        private BigInteger tipoPersona;
        private BigInteger tiempoEspera;
        private EnumTipoRegimenTercero enumTipoRegimenTercero;
        private EnumTipoPersona enumTipoPersona;

        public Tercero()
        {
            this.Inicializar();
        }

        public Tercero(BigInteger identificacion, BigInteger digito, BigInteger tipo, String razonSocial, String nombre1, String nombre2, String apellido1, String apellido2, Ubicacion ubicacion, String telefono, String email, String celular, String tipoTercero, String direccion)
                      
        {
            this.identificacion = identificacion;
            this.digito = digito;
            this.tipo = tipo;
            this.razonSocial = razonSocial;
            this.nombre1 = nombre1;
            this.nombre2 = nombre2;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            _ubicacion = ubicacion;
            this.telefono = telefono;
            this.email = email;
            this.celular = celular;
            this.tipoTercero = tipoTercero;
            this.direccion = direccion;
            this.enumTipoDocumento = EnumTipoDocumento.CedulaCiudadania;
            
        }

        public Tercero(BigInteger identificacion, BigInteger tipo, String razonSocial, Ubicacion ubicacion, String telefono, String email, String celular)
        {
            this.identificacion = identificacion;
            this.tipo = tipo;
            this.razonSocial = razonSocial;
            _ubicacion = ubicacion;
            this.telefono = telefono;
            this.email = email;
            this.celular = celular;
        }


        public Tercero(BigInteger identificacion, BigInteger tipo, String nombre1, String nombre2, String apellido1, String apellido2, String telefono, String email, String celular, String direccion, String conMontoDeLey, String conMontoPersonalizado, Double montoPersonalizado) 
        {
            this.identificacion = identificacion;
            this.tipo = tipo;
            this.nombre1 = nombre1;
            this.nombre2 = nombre2;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            this.telefono = telefono;
            this.email = email;
            this.celular = celular;
            this.direccion = direccion;
            this.conMontoDeLey = conMontoDeLey;
            this.conMontoPersonalizado= conMontoPersonalizado; 
            this.montoPersonalizado= montoPersonalizado;

        }

        public Tercero(BigInteger identificacion, BigInteger tipo, String nombre1, String nombre2, String apellido1, String apellido2, String telefono, String email, String celular, String direccion)
        {
            this.identificacion = identificacion;
            this.tipo = tipo;
            this.nombre1 = nombre1;
            this.nombre2 = nombre2;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            this.telefono = telefono;
            this.email = email;
            this.celular = celular;
            this.direccion = direccion;

        }

        public Tercero(String empresa)
        { 
        
        
        }


        
        

        private void Inicializar()
        {
            enumTipoRegimenTercero = EnumTipoRegimenTercero.Simple;
            regimen = enumTipoRegimenTercero.ToString();
            tipoProveedor = 0;
            reteFuente = "N";
            reteIva = "N";
            reteIca = "N";
            naturaleza = "---";
            actividadEconomica = "---";
            empresa = "---";
            calcularConMonto = "S";
            tarifaReteFuente = 0.0;
            tarifaReteIva = 0.0;
            tiempoEspera = 0;
            tipoRegimen = enumTipoRegimenTercero.ToString();
            enumTipoPersona = EnumTipoPersona.Natural;
            tipoPersona = (int) enumTipoPersona;
        }



        public Boolean EnviarFacturaElectronica()
        {
            Boolean enviar = false;

            return enviar;
        
        }



        public override String ToString()
        {
            
            return base.ToString() + "\nTercero{" + "regimen=" + regimen + ", tipo_proveedor=" + tipoProveedor + ","
                    + "\n retefuente=" + reteFuente + ", reteiva=" + reteIva + ", reteica=" + reteIca + ", "
                    + "\n naturaleza=" + naturaleza + ", actividad_economica=" + actividadEconomica + ", empresa=" + empresa + ", "
                    + "\n calcularconmonto=" + calcularConMonto + ", tarifa_retefuente=" + tarifaReteFuente + ", tarifa_reteiva=" + tarifaReteIva + ", "
                    + "\n tipo_regimen=" + tipoRegimen + ", tipo_persona=" + tipoPersona + ", tiempo_espera=" + tiempoEspera + "'}'";
        }


        public void ModificarRegimen(String regimen)
        { 
            this.regimen = regimen;
        }

        public void ModificarTipoProveedor(BigInteger tipoProveedor)
        {
            this.tipoProveedor = tipoProveedor;
        }

        public void ModificarReteFuente(String reteFuente)
        { 
            this.reteFuente = reteFuente;   
        }

        public void ModificarReteIva(String reteIva)
        {
            this.reteIva = reteIva;
        }

        public void ModificarReteIca(String reteIca)
        {
            this.reteIca = reteIca;
        }

        public void ModificarNaturaleza(String naturaleza)
        {
            this.naturaleza = naturaleza;
        }

        public void ModificarActividadEconomica(String actividadEconomica)
        {
            this.actividadEconomica = actividadEconomica;
        }

        public void ModificarEmpresa(String actividadEconomica)
        {
            this.actividadEconomica = actividadEconomica;
        }

        public void ModificarCalcularConMonto(String calcularConMonto)
        {
            this.calcularConMonto = calcularConMonto;
        }


        public void ModificarTarifaReteFuente(Double tarifaReteFuente)
        {
            this.tarifaReteFuente = tarifaReteFuente;
        }

        public void ModificarTarifaReteIva(Double tarifaReteIva)
        {
            this.tarifaReteIva = tarifaReteIva;
        }

        public void ModificarTiempoEspera(BigInteger tiempoEspera)
        {
            this.tiempoEspera = tiempoEspera;
        }

        //enum string?
        public void ModificarTipoRegimen(String tipoRegimen)
        { 
            this.tipoRegimen = tipoRegimen;
        }

        public void ModificarTipoPersona(BigInteger tipoPersona)
        { 
            this.tipoPersona = tipoPersona;
        }

        public Ubicacion ObtenerUbicacion()
        {
            return _ubicacion;
        }

        public String ObtenerNombreCompleto()
        {
            return $"{nombre1} {nombre2} {apellido1} {apellido2}";
        }

        public BigInteger ObtenerIdentificacion()
        {
            return identificacion;
        }

        public String ConMontoDeLey() => conMontoDeLey;

        public String ConMontoPersonalizado() => conMontoPersonalizado;

        public Double ObtenerMontoPersonalizado() => montoPersonalizado;

    }
}
