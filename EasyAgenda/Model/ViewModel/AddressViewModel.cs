namespace EasyAgenda.Model.ViewModel
{
  public class AddressViewModel
  {
    public string Street { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }
    public State State { get; set; }

    public AddressViewModel(string street, string neighborhood, string city,
    string zipCode, string number, string complement, State state)
    {
      Street = street;
      Neighborhood = neighborhood;
      City = city;
      ZipCode = zipCode;
      Number = number;
      Complement = complement;
      State = state;
    }
  }
}
