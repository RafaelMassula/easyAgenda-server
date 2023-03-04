using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgendaBase.Model
{
    [Table("SETTINGMAIL")]
    public class SettingMail
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("SERVERSMTP")]
        public string ServerSmtp { get; set; }
        [Column("PORTTLS")]
        public int PortTls { get; set; }
        [Column("PORTSSL")]
        public int PortSsl { get; set; }
        [Column("EMAIL")]
        public string Email { get; set; }
        [Column("PASSWORD")]
        public string Password { get; set; }
        [Column("COMPANYNAME")]
        public string CompanyName { get; set; }

        public SettingMail(string serverSmtp, int portTls, int portSsl, string email, string password,
            string companyName)
        {
            ServerSmtp = serverSmtp;
            PortTls = portTls;
            PortSsl = portSsl;
            Email = email;
            Password = password;
            CompanyName = companyName;
        }
        public SettingMail(int id, string serverSmtp, int portTls, int portSsl, string email, string password, 
            string companyName) : this(serverSmtp, portTls, portSsl, email, password, companyName)
        {
            Id = id;
        }

    }
}
