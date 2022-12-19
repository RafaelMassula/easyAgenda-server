namespace EasyAgendaService.Contracts
{
    public interface IUriService
    {
        Uri GetUriConfirmSchedule(string route, int idReserved);
    }
}
