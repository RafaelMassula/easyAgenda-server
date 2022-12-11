using EasyAgendaBase.Model;

namespace EasyAgendaService.Data.Contracts
{
    public interface IMailConfigurationDAL
    {
        Task<SettingMail> GetMailConfiguration();
        Task<CustomizedMessage> GetCustomizedMessage(RecipientMessage recipientMessage, int idMessageType);
    }
}
