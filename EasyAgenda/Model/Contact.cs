using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model
{
    [Table("CONTACTS")]
    public class Contact
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("PHONE")]
        public string Phone { get; set; }
        [Column("PEOPLEID")]
        public int PeopleId { get; set; }
        public virtual People People { get; set; } = null!;
        public Contact(int id, string phone, int peopleId)
        {
            Id = id;
            Phone = phone;
            PeopleId = peopleId;
        }
    }
}
