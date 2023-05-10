using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace EasyAgenda.Model.DTO
{
  [Table("[CONTACTS]")]
  public class ContactDTO
  {
    public string Phone { get; set; }
    [JsonIgnore]
    public int PeopleId { get; set; }

    public ContactDTO(string phone, int peopleId)
    {
      Phone = new Regex(@"\W").Replace(phone, "");
      PeopleId = peopleId;
    }
  }
}
