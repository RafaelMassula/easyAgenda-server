namespace EasyAgenda.Model.ViewModel
{
  public class PeopleViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Cpf { get; set; }
    public string Sex { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public PeopleViewModel(int id, string name, string lastName, string cpf, string sex, DateTime birthDate, string email, string phone)
    {
      Id = id;
      Name = name;
      LastName = lastName;
      Cpf = cpf;
      Sex = sex;
      BirthDate = birthDate;
      Email = email;
      Phone = phone;
    }

  }
}
