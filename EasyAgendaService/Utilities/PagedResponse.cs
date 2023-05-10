namespace EasyAgendaService.Utilities
{
  public class PagedResponse<T> : Wrappers<T>
  {
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Uri FirstPage { get; set; } = null!;
    public Uri LastPage { get; set; } = null!;
    public int TotalPages { get; set; }
    public int TotalRecord { get; set; }
    public Uri? NextPage { get; set; }
    public Uri? PreviousPage { get; set; }

    public PagedResponse(T data, int pageNumber, int pageSize)
    {
      Data = data;
      PageNumber = pageNumber;
      PageSize = pageSize;
      Message = string.Empty;
      Succeded = true;
      Erros = Array.Empty<string>();
    }
  }
}
