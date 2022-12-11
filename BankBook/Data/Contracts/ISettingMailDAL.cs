using EasyAgendaBase.Model;

namespace EasyAgenda.Data.Contracts
{
    public interface ISettingMailDAL
    {
        Task<SettingMail> Get();
        Task Configure(SettingMail settingMail);
    }
}
