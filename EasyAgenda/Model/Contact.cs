using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EasyAgenda.Model
{
  [Table("CONTACTS")]
  public class Contact
  {
    [Column("ID")]
    public int Id { get; set; }
    [Column("PHONE")]
    public string Phone { get; set; }

    [JsonIgnore]
    public virtual ICollection<ContactCompany> ContactsCompany { get; set; } = new List<ContactCompany>();
    [JsonIgnore]
    public virtual ICollection<ContactPeople> ContactsPeople { get; set; } = new List<ContactPeople>();

    public Contact(int id, string phone)
    {
      Id = id;
      Phone = phone;
    }
  }
}
