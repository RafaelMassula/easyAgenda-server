using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model
{
  [Table("PROFESSIONALS")]
  public class Professional
  {
    [Key]
    [Column("ID")]
    public int Id { get; set; }
    [Column("PEOPLEID")]
    public int PeopleId { get; set; }
    public virtual People People { get; set; } = null!;
    [Column("USERID")]
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; } = null!;

    public Professional(int id, int peopleId, int userId, int companyId)
    { 
      Id = id;
      PeopleId = peopleId;
      UserId = userId;
      CompanyId = companyId;
    }

  }
}
