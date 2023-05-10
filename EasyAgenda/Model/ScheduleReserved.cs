using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model
{
  [Table("SCHEDULESRESERVED")]
  public class ScheduleReserved
  {
    [Column("ID")]
    public int Id { get; set; }
    [Column("CUSTOMERID")]
    public int CustomerId { get; set; }
    [Column("PROFESSIONALID")]
    public int ProfessionalId { get; set; }
    [Column("AGENDAID")]
    public int AgendaId { get; set; }
    [Column("REGISTERED")]
    public DateTime Registered { get; set; }

    public ScheduleReserved(int id, int customerId, int professionalId, int agendaId,
        DateTime registered)
    {
      Id = id;
      CustomerId = customerId;
      ProfessionalId = professionalId;
      AgendaId = agendaId;
      Registered = registered;
    }

  }
}
