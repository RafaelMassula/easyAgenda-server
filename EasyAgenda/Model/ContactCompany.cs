using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EasyAgenda.Model
{
  [Table("CONTACTSCOMPANY")]
  public class ContactCompany
  {
    [JsonIgnore]
    [Column("CONTACTID")]
    public int ContactId { get; set; }
    public virtual Contact Contact { get; set; } = null!;
    [Column("COMPANYID")]
    [JsonIgnore]
    public int CompanyId { get; set; }
    [JsonIgnore]
    public virtual Company? Company { get; set; } = null!;

  }

}
