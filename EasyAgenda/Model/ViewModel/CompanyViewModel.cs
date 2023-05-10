namespace EasyAgenda.Model.ViewModel
{
  public class CompanyViewModel
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public string Cnpj { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }
    public Status Status { get; set; }

    public CompanyViewModel(int id, string description, string cnpj, string email,
    Address address, Status status)
    {
      Id = id;
      Description = description;
      Cnpj = cnpj;
      Email = email;
      Address = address;
      Status = status;
    }
  }
}
