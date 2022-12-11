
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model.DTO
{
    [Table("[AGENDAS]")]
    public class AgendaDTO
    {
        public string Specialization { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public int ProfessionalId { get; set; }

        public AgendaDTO(string specialization, string description, DateTime date, string start, string end)
        {
            Specialization = specialization;
            Description = description;
            Date = date;
            Start = start;
            End = end;
        }
    }
}
