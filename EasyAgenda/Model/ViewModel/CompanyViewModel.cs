using EasyAgenda.Model.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model.ViewModel
{
  public class CompanyViewModel
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public string Cnpj { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public AddressViewModel Address { get; set; }
    public Status Status { get; set; }

    public CompanyViewModel(int id, string description, string cnpj, string phone, string email,
    AddressViewModel address, Status status)
    {
      Id = id;
      Description = description;
      Cnpj = cnpj;
      Phone = phone;
      Email = email;
      Address = address;
      Status = status;
    }
  }
}
