using EasyAgenda.ExtensionMethods;
using EasyAgendaService;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model.DTO
{
  [Table("[COMPANIES]")]
  public class CompanyDTO
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public string Cnpj { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public int StatusId { get; set; }

    public CompanyDTO(string description, string cnpj, string phone, string email, int statusId)
    {
      Description = description;
      Cnpj = CompanyService.CheckedCnpj(cnpj);
      Phone = phone.RemoveMaskPhone();
      Email = email;
      StatusId = statusId;
    }
    public CompanyDTO(int id, string description, string cnpj, string phone, string email)
    {
      Id = id;
      Description = description;
      Cnpj = CompanyService.CheckedCnpj(cnpj);
      Phone = phone.RemoveMaskPhone();
      Email = email;
    }
  }
}
