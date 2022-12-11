using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model
{
    [Table("USERS")]
    public class User
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        [Column("RoleId")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;

        public User(int id, string email, string password, int roleId)
        {
            Id = id;
            Email = email;
            Password = password;
            RoleId = roleId;
        }
    }
}
