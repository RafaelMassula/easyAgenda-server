using EasyAgendaService.Contracts;
using EasyAgendaService.Utilities;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;


namespace EasyAgendaService
{
  public class UriService : IUriService
  {
    private readonly string _baseUri;

    public UriService(string baseUri)
    {
      _baseUri = baseUri;
    }

    public Uri GetUriConfirmSchedule(string route, int idReserved)
    {
      var paramEncode = WebUtility.UrlEncode(string.Concat("/", idReserved));
      return new Uri(string.Concat(_baseUri, route, paramEncode));
    }

    public Uri GetPageUri(PaginationFilter filter, string route)
    {
      var _enpointUri = new Uri(string.Concat(_baseUri, route));
      var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
      modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
      return new Uri(modifiedUri);
    }
  }
}
