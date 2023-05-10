namespace EasyAgendaService.Utilities
{
  public class PaginationFilter
  {
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public PaginationFilter()
    {
      this.PageNumber = 1;
      this.PageSize = 5;
    }

    public PaginationFilter(int pageNumber, int pageSize)
    {
      PageNumber = pageNumber < 1 ? 1 : pageNumber;
      PageSize = pageSize < 5 ? 5 : pageSize;
    }
  }
}
