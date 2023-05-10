using EasyAgenda.Data.Contracts;
using EasyAgenda.Model;
using EasyAgenda.Model.DTO;
using EasyAgenda.Model.JsonProfile;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EasyAgenda.Data.DAL
{
  public class AddressDAL : IAddressDAL
  {
    private readonly HttpClient _client = new();
    private readonly EasyAgendaContext _context;

    public AddressDAL(EasyAgendaContext context)
    {
      _context = context;
    }
    public async Task<IEnumerable<State>> GetStates()
    {
      try
      {
        return await _context.States
           .OrderBy(state => state.Initials)
           .ToListAsync();
      }
      catch (Exception error)
      {
        throw new Exception(error.Message);
      }
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
    }
    public async Task Update(Address address)
    {
      try
      {
        var addressChange = new Address(address.Id, address.Street, address.Neighborhood,
          address.City, address.ZipCode, address.Number, address.Complement, address.StateId, address.CompanyId);

        _context.Update(addressChange);
        await _context.SaveChangesAsync();
      }
      catch (SqlException error)
      {
        throw new Exception(error.Message);
      }
      catch (Exception error)
      {
        throw new Exception(error.Message);
      }
    }
  }
}
