using EntidadesNegocio.ElementosInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.InformacionVisita
{
    public class MedidaElemento
    {
        private Valor _valorMedido;
        private Unidad _unidad;

        public MedidaElemento(Valor valorMedido, Unidad unidad)
        {
            _valorMedido = valorMedido;
            _unidad = unidad;
        }

        public Double ObtenerValorFijoMedido()
        {
            return _valorMedido.ObtenerValorFijo();
        }

        public String ObtenerRangoMedido()
        {
            return _valorMedido.ObtenerRangoCompleto();
        }

        public Unidad ObtenerUnidad()
        {
            return _unidad;
        }

        public Double ObtenerValorInicial()
        {
            return _valorMedido.ObtenerRangoInicial();
        }

        public Double ObtenerValorFinal()
        {
            return _valorMedido.ObtenerRangoFinal();
        }

        public void ModificarValorFijoMedida(Double fijo)
        {
            _valorMedido.ModificarValorFijo(fijo);
        }

        public void ModificarRangoInicialMedida(Double inicial)
        {
            _valorMedido.ModificarRangoIncial(inicial);
        }

        public void ModificarRangoFinalMedida(Double final)
        {
            _valorMedido.ModificarRangoFinal(final);
        }


    }
}
