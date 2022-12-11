using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model.DTO
{
    [Table("[SCHEDULES]")]
    public class ScheduleDTO
    {

        public int CustomerId { get; set; }
        public int ProfessionalId { get; set; }
        public int AgendaId { get; set; }

        public ScheduleDTO(int customerId, int professionalId, int agendaId)
        {
            CustomerId = customerId;
            ProfessionalId = professionalId;
            AgendaId = agendaId;
        }
    }
}
