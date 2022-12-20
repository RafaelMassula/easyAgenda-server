using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model
{
    [Table("ROLES")]
    public class Role
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        public Role(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
