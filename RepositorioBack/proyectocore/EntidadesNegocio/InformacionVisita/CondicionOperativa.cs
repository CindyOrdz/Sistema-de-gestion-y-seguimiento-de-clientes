using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.InformacionVisita
{
    public class CondicionOperativa
    {
        private static BigInteger contador =0;
        private BigInteger id;
        private String nombre;
        private MedidaElemento _medidaElemento;

        public CondicionOperativa(string nombre, MedidaElemento medidaElemento)
        {
            this.nombre = nombre;
            _medidaElemento = medidaElemento;
        }

        public CondicionOperativa(BigInteger id, String nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }

        public CondicionOperativa()
        {
            id = 0;
            nombre = "cond op";
        }
        public void ModificarNombre(String nombre)
        {
            this.nombre = nombre;
        }

        public void ModificarValorFijo(Double fijo)
        {
            _medidaElemento.ModificarValorFijoMedida(fijo);
        }

        public void ModificarRangoInicial(Double inicial)
        {
            _medidaElemento.ModificarRangoInicialMedida(inicial);
        }

        public void ModificarRangoFinal(Double final)
        {
            _medidaElemento.ModificarRangoFinalMedida(final);
        }

        public BigInteger ObtenerId()
        {
            return id;
        }

        public String ObtenerNombre()
        {
            return nombre;
        }

        public Double ObtenerValorFijoMedido()
        {
            return _medidaElemento.ObtenerValorFijoMedido();
        }

        public Double ObtenerRangoInicial()
        {
            return _medidaElemento.ObtenerValorInicial();
        }

        public Double ObtenerRangoFinal()
        {
            return _medidaElemento.ObtenerValorFinal();

        }
        public String ObtenerRangoMedido()
        {
            return _medidaElemento.ObtenerRangoMedido();
        }

        public MedidaElemento ObtenerMedidaElemento()
        {
            return _medidaElemento;
        }

        public override String ToString()
        {
            return "\nCondicionOperativa{" + "id =" + id + ", nombre =" + nombre + "}";
        }
    }
}
