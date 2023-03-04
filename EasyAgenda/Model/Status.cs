using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model
{
    [Table("STATUS")]
    public class Status
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        public Status(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
