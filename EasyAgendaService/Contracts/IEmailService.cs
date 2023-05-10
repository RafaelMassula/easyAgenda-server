using EasyAgendaBase.Enums;
using EasyAgendaBase.Model;

namespace EasyAgendaService.Contracts
{
  public interface IEmailService
  {
    Task Send(RecipientMessage message, TypeMessage typeMessage);
    Task TestSend(SettingMail settingMail);
  }
}
