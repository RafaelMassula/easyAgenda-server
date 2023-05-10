using EasyAgendaService.Utilities;

namespace EasyAgendaService
{
  public class PageDataService<T> where T : class
  {
    public PaginationFilter ValidFilter { get; }

    public PageDataService(int pageNumber, int pageSize)
    {
      ValidFilter = new PaginationFilter(pageNumber, pageSize);
    }

    public IList<T> GetDataPage(IEnumerable<T> datas)
    {
      return datas.Skip(ValidFilter.PageNumber - 1 * ValidFilter.PageSize)
        .Take(ValidFilter.PageSize)
        .ToList();
    }
  }
}
