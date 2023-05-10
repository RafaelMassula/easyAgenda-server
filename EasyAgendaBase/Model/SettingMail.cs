using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgendaBase.Model
{
  [Table("SETTINGMAIL")]
  public class SettingMail
  {
    [Column("ID")]
    public int Id { get; set; }
    [Column("SERVERSMTP")]
    public string ServerSmtp { get; set; } = string.Empty;
    [Column("PORTTLS")]
    public int PortTls { get; set; }
    [Column("PORTSSL")]
    public int PortSsl { get; set; }
    [Column("EMAIL")]
    public string Email { get; set; } = string.Empty;
    [Column("PASSWORD")]
    public string Password { get; set; } = string.Empty;
    [Column("COMPANYNAME")]
    public string CompanyName { get; set; } = string.Empty;
  }
}
