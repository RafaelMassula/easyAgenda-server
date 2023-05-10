namespace EasyAgendaBase.Model
{
  public class UserRole
  {
    public int Id { get; set; }
    public string Email { get; set; }
    public int ProfileId { get; set; }
    public string ProfileDescription { get; set; }

    public UserRole(int id, string email, int profileId, string profileDescription)
    {
      Id = id;
      Email = email;
      ProfileId = profileId;
      ProfileDescription = profileDescription;
    }
    public UserRole(string email, string profileDescription)
    {
      Email = email;
      ProfileDescription = profileDescription;
    }
  }
}
