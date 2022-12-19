using EasyAgendaService.Contracts;
using MimeKit.Encodings;
using System.Net;
using System.Text;

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
    }
}
