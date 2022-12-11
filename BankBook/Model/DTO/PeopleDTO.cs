using EasyAgendaService;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model.DTO
{
    [Table("[PEOPLE]")]
    public class PeopleDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public char Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public string Cpf { get; set; }
        public DateTime Registered { get; set; }

        public PeopleDTO(string name, string lastName, char sex, DateTime birthDate, string cpf, DateTime registered)
        {
            Name = name;
            LastName = lastName;
            Sex = sex;
            BirthDate = PeopleService.CheckedBirthDate(birthDate);
            Cpf = PeopleService.CheckedCpf(cpf);
            Registered = registered;
        }
    }
}
