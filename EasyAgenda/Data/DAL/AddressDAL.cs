using EasyAgenda.Data.Contracts;
using EasyAgenda.Model.DTO;
using EasyAgenda.Model.JsonProfile;
using EasyAgenda.Model;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EasyAgenda.Data.DAL
{
  public class AddressDAL: IAddressDAL
  {
      private readonly HttpClient _client = new();
      private readonly EasyAgendaContext _context;

      public AddressDAL(EasyAgendaContext context)
      {
        _context = context;
      }
      public async Task<AddressDTO> GetAddress(string cep)
      {
        cep = Regex.Replace(cep, @"\W", "").ToString();
        try
        {
          using HttpResponseMessage response = await _client.GetAsync($"http://viacep.com.br/ws/{cep}/json/");
          response.EnsureSuccessStatusCode();

          string responseBody = await response.Content.ReadAsStringAsync();
          var json = JsonConvert.DeserializeObject<AddressJson>(responseBody);
          if (json != null && !json.Erro)
          {
            var state = await _context.States.SingleAsync(state => state.Initials == json.Uf.ToUpper());

            return new AddressDTO(json.Logradouro, json.Bairro, json.Localidade,
                json.Cep, "", "", state.Id);
          }
          throw new HttpRequestException("Invalid cep.");
        }
        catch (SqlException error)
        {
          throw new Exception(error.Message);
        }
        catch (HttpRequestException error)
        {
          throw new HttpRequestException(error.Message);
        }
      }
    }
  }
