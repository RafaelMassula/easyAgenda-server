using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model.DTO
{
    [Table("[USERS]")]
    public class UserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public UserDTO(string email, string password, int roleId)
        {
            Email = email;
            Password = password;
            RoleId = roleId;
        }
    }
}
