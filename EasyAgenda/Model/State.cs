using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model
{
  [Table("STATES")]
  public class State
  {
    [Column("ID")]
    public int Id { get; set; }
    [Column("INITIALS")]
    public string Initials { get; set; }

    public State(int id, string initials)
    {
      Id = id;
      Initials = initials;
    }

  }
}
