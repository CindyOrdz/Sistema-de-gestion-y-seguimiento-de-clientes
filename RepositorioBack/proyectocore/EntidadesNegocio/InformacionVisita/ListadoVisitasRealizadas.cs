using EntidadesNegocio.Terceros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesNegocio.InformacionVisita
{
    public class ListadoVisitasRealizadas
    {
        private List<VisitaEmpresaCliente> ListadoVisitas;

        public ListadoVisitasRealizadas()
        {
            ListadoVisitas = new List<VisitaEmpresaCliente>();
        }

        public void AgregarVisitaRealizada(VisitaEmpresaCliente visitaEmpresa)
        {
            ListadoVisitas.Add(visitaEmpresa);
        }

        //public void VerResponsableDeVisita(BigInteger idAgendaVisita)
        //{
        //    // Buscar en VisitaEmpresaCliente la AgendaVisita por su ID
        //    VisitaEmpresaCliente? visitaEncontrada = ListadoVisitas.Find(visita => visita.ObtenerIdVisitaAgendada() == idAgendaVisita);

        //    if (visitaEncontrada != null)
        //    {
        //        // Obtener la lista de responsables de AgendaVisita en VisitaEmpresaCliente
        //        List<Tercero> responsables = visitaEncontrada.ObtenerResponsableVisitaAgendada();

        //        if (responsables.Count > 0)
        //        {
        //            Console.WriteLine("Responsable(s) de la visita con ID " + idAgendaVisita + ":");
        //            foreach (Tercero responsable in responsables)
        //            {
        //                Console.WriteLine(" - " + responsable.ObtenerNombreCompleto());
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("No se encontraron responsables para la visita con ID " + idAgendaVisita + ".");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("No se encontró ninguna visita con ID " + idAgendaVisita + ".");
        //    }
        //}

        public void BuscarVisitaPorEmpresa(BigInteger IdEmpresa)
        {
             List<VisitaEmpresaCliente> visitasEncontradas = ListadoVisitas
             .Where(visita => visita.ObtenerEmpresaVisitaAgendada().ObtenerEmpresa().ObtenerIdentificacion().Equals(IdEmpresa)).ToList();

             if (visitasEncontradas.Count > 0)
             {
                 Console.WriteLine($"Visitas encontradas para " + IdEmpresa + ":");
                 foreach (VisitaEmpresaCliente visita in visitasEncontradas)
                 {
                     Console.WriteLine($"ID Visita:" + visita.ObtenerIdVisitaAgendada());
                 }
             }
             else
             {
                 Console.WriteLine($"No se encontraron visitas para " + IdEmpresa + ".");
             }

        }
    }
}
