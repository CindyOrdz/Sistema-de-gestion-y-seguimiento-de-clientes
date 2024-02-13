
using EntidadesNegocio.Descuentos;

namespace EntidadesNegocio.VentaOnlineTradicional
{
    public class TarifasTerritorialesEmpresa
    {
        private static TarifasTerritorialesEmpresa? _instancia;
        public Double TarifaReteIca { get; set; }
        public Double TarifaIca { get; set; }

        private TarifasTerritorialesEmpresa()
        {
            this.TarifaReteIca = 0;
            this.TarifaIca = 0;

        }


        public static TarifasTerritorialesEmpresa Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new TarifasTerritorialesEmpresa();
                }
                return _instancia;
            }

        }
    }
}
