using EasyAgendaService.Utilities;

namespace EasyAgendaService.Contracts
{
  public interface IUriService
  {
    Uri GetUriConfirmSchedule(string route, int idReserved);
    Uri GetPageUri(PaginationFilter paginationFilter, string route);
  }
}
