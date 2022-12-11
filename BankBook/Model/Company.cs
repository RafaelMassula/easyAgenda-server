using EasyAgendaService;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgenda.Model
{
    [Table("COMPANIES")]
    public class Company
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        [Column("CNPJ")]
        public string Cnpj { get; set; }
        [Column("PHONE")]
        public string Phone { get; set; }
        [Column("EMAIL")]
        public string Email { get; set; }
        [Column("STATUSID")]
        public int StatusId { get; set; }
        public static string  TableName { get => "COMPANIES"; }

        public Company(int id, string description, string cnpj, string phone, string email, int statusId)
        {
            Id = id;
            Description = description;
            Cnpj = CompanyService.CheckedCnpj(cnpj);
            Phone = phone;
            Email = email;
            StatusId = statusId;
        }
    }
}
