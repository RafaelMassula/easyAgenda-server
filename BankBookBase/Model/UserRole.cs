using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAgendaBase.Model
{
  public class UserRole
  {
    public int Id { get; set; }
    public string Email { get; set; }

    public string Password { get; set; }

    public string RoleDescription { get; set; }

    public UserRole(int id, string email, string password, string roleDescription)
    {
      Id = id;
      Email = email;
      Password = password;
      RoleDescription = roleDescription;
    }
  }
}
