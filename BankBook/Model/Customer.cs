using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model
{
    [Table("CUSTOMERS")]
    public class Customer
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("PEOPLEID")]
        public int PeopleId { get; set; }
        public virtual People People { get; set; } = null!;
        [Column("USERID")]
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public Customer(int id, int peopleId, int userId)
        {
            Id = id;
            PeopleId = peopleId;
            UserId = userId;
        }
    }
}
