using System.ComponentModel.DataAnnotations.Schema;

namespace EasyAgendaBase.Model
{
  [Table("[CUSTOMIZEDMESSAGES]")]
  public class CustomizedMessage
  {
    [Column("DESCRIPTION")]
    public string Description { get; set; }
    [Column("SUBJECT")]
    public string Subject { get; set; }
    [Column("MESSAGETYPEID")]
    public int MessageTypeId { get; set; }

    public CustomizedMessage(string description, string subject)
    {
      Description = description;
      Subject = subject;
    }
    public CustomizedMessage(string description, string subject, int messageTypeId) :
        this(description, subject)
    {
      MessageTypeId = messageTypeId;
    }
  }
}
