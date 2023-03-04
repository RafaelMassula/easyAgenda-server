using EasyAgendaService;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model
{
    [Table("PEOPLE")]
    public class People
    {
        [Key]
        [Column("ID")]
        public int Id { get; }
        [Column("NAME")]
        public string Name { get; set; }
        [Column("LASTNAME")]
        public string LastName { get; set; }
        [Column("SEX")]
        public char Sex { get; set; }
        [Column("BIRTHDATE")]
        public DateTime BirthDate { get; set; }
        [Column("CPF")]
        public string Cpf { get; set; }
        [Column("REGISTERED")]
        public DateTime Registered { get; set; }
        public People(int Id, string Name, string LastName, char Sex, DateTime BirthDate, string Cpf, DateTime registered)
        {
            this.Id = Id;
            this.Name = Name;
            this.LastName = LastName;
            this.Sex = Sex;
            this.BirthDate = BirthDate;
            this.Cpf = Cpf;
            Registered = registered;
        }
    }
}
