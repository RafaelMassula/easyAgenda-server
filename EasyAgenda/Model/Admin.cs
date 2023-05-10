using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EasyAgenda.Model
{
  [Table("ADMINS")]
  public class Admin
  {
    [Column("ID")]
    public int Id { get; set; }
    [Column("PEOPLEID")]
    [JsonIgnore]
    public int PeopleId { get; set; }
    public virtual People People { get; set; } = null!;
    [Column("USERID")]
    [JsonIgnore]
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public int CompanyId { get; set; }
    [JsonIgnore]
    public virtual Company? Company { get; set; } = null!;

  }
}
