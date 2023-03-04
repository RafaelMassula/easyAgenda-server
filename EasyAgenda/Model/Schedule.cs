using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model
{
    [Table("SCHEDULES")]
    public class Schedule
    {
        [Column("CUSTOMERID")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!;
        [Column("PROFESSIONALID")]
        public int ProfessionalId { get; set; }
        public virtual Professional Professional { get; set; } = null!;
        [Column("AGENDAID")]
        public int AgendaId { get; set; }
        public virtual Agenda Agenda { get; set; } = null!;

        public Schedule(int customerId, int professionalId, int agendaId)
        {
            CustomerId = customerId;
            ProfessionalId = professionalId;
            AgendaId = agendaId;
        }
    }
}
