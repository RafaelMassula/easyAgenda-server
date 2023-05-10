using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EasyAgenda.Model
{
  [Table("USERS")]
  public class User
  {
    [Column("ID")]
    public int Id { get; set; }
    [Column("Email")]
    public string Email { get; set; }
    [Column("Password")]
    public string Password { get; set; }
    public int ProfileId { get; set; }
    [JsonIgnore]
    public virtual Profile? Profile { get; set; } = null!;

    public User(int id, string email, string password)
    {
      Id = id;
      Email = email;
      Password = password;
    }
  }
}
