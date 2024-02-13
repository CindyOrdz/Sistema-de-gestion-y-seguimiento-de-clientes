using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.InformacionVisita
{
    public class RegistroMedidasParte
    {
        private Parte _parte;
        private List<CondicionOperativa> _registroMedida;

        public RegistroMedidasParte(Parte parte, List<CondicionOperativa> registroMedida)
        {
            _parte = parte;
            _registroMedida = registroMedida;
        }

        

        public override String ToString()
        {
            return "\nRegistroMedidasParte{" + "parte=" + _parte + ", elemento=" + _registroMedida + "}";
        }
    }
}
