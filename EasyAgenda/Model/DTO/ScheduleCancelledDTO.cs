using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model.DTO
{
  [Table("[SCHEDULESCANCELLED]")]
  public class ScheduleCancelledDTO
  {
    public DateTime Cancellation { get; }
    public string Observation { get; set; }
    public int CustomerId { get; set; }
    public int ProfessionalId { get; set; }
    public int AgendaId { get; set; }

    public ScheduleCancelledDTO(string observation, int customerId, int professionalId,
        int agendaId)
    {
      Cancellation = DateTime.Now.Date;
      Observation = observation;
      CustomerId = customerId;
      ProfessionalId = professionalId;
      AgendaId = agendaId;
    }
  }
}
