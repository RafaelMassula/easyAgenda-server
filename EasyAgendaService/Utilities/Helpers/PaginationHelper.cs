using EasyAgendaService.Contracts;

namespace EasyAgendaService.Utilities.Helpers
{
  public class PaginationHelper
  {
    public static PagedResponse<List<T>> CreatePagedResponse<T>(List<T> pageData, PaginationFilter validFilter,
      int totalRecord, IUriService uriService, string route)
    {
      //tratar succeso aqui!!!
      PagedResponse<List<T>> response = new(pageData, validFilter.PageNumber, validFilter.PageSize);
      double totalPages = totalRecord / validFilter.PageSize;
      int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

      response.NextPage =
          validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
          ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize), route)
          : null;

      response.PreviousPage =
        validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
        ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber, validFilter.PageSize), route)
        : null;

      response.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize), route);
      response.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize), route);
      response.TotalPages = roundedTotalPages;
      response.TotalRecord = totalRecord;

      return response;
    }
  }
}
