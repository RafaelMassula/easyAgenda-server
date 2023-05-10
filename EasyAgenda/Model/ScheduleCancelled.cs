using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model
{
  [Table("SCHEDULESCANCELLED")]
  public class ScheduleCancelled
  {
    [Column("ID")]
    public int Id { get; set; }
    [Column("CANCELLATION")]
    public DateTime Cancellation { get; set; }
    [Column("OBSERVATION")]
    public string Observation { get; set; }
    [Column("CUSTOMERID")]
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;
    [Column("PROFESSIONALID")]
    public int ProfessionalId { get; set; }
    public virtual Professional Professional { get; set; } = null!;
    [Column("AGENDAID")]
    public int AgendaId { get; set; }
    public virtual Agenda Agenda { get; set; } = null!;

    public ScheduleCancelled(int id, DateTime cancellation, string observation, int customerId, int professionalId,
        int agendaId)
    {
      Id = id;
      Cancellation = cancellation;
      Observation = observation;
      CustomerId = customerId;
      ProfessionalId = professionalId;
      AgendaId = agendaId;
    }
  }
}
