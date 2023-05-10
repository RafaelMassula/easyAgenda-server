using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EasyAgenda.Model
{
  [Table("CONTACTSPEOPLE")]
  public class ContactPeople
  {
    [JsonIgnore]
    [Column("CONTACTID")]
    public int ContactId { get; set; }
    public virtual Contact Contact { get; set; } = null!;
    [Column("PEOPLEID")]
    [JsonIgnore]
    public int PeopleId { get; set; }
    [JsonIgnore]
    public virtual People? People { get; set; }
  }
}
