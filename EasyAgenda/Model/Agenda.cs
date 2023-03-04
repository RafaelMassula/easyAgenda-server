using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model
{
    [Table("AGENDAS")]
    public class Agenda
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("SPECIALIZATION")]
        public string Specialization { get; set; }
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        [Column("DATE")]
        public DateTime Date { get; set; }
        [Column("START")]
        public TimeSpan Start { get; set; }
        [Column("END")]
        public TimeSpan End { get; set; }
        [Column("PROFESSIONALID")]
        public int ProfessionalId { get; set; }
        public virtual Professional Professional { get; set; } = null!;

        public Agenda(int id, string specialization, string description, DateTime date, TimeSpan start, TimeSpan end)
        {
            Id = id;
            Specialization = specialization;
            Description = description;
            Date = date;
            Start = start;
            End = end;
        }
    }
}
