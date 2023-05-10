using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model.DTO
{
  [Table("[CUSTOMERS]")]
  public class CustomerDTO
  {
    public int PeopleId { get; set; }
    public int UserId { get; set; }
    public CustomerDTO(int peopleId, int userId)
    {
      PeopleId = peopleId;
      UserId = userId;
    }
  }
}
