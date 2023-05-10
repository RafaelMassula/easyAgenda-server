using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model
{
  [Table("PROFILES")]
  public class Profile
  {
    [Column("ID")]
    public int Id { get; set; }
    [Column("DESCRIPTION")]
    public string Description { get; set; }
    [JsonIgnore]
    public virtual User? User { get; set; }
    public Profile(int id, string description)
    {
      Id = id;
      Description = description;
    }
  }
}
