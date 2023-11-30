using Microsoft.CodeAnalysis.Elfie.Model;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Models
{
    public class Agendar
    {
        [Key]
        public int IdAgendamento { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public DateTime Data_Agendamento { get; set; }
        
    }
}
