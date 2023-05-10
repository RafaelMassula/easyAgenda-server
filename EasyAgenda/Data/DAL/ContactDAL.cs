using Dapper;
using EasyAgenda.Data.Contracts;
using EasyAgenda.ExtensionMethods;
using EasyAgenda.Model;
using EasyAgenda.Model.DTO;
using EasyAgendaService;
using Microsoft.Data.SqlClient;

namespace EasyAgenda.Data.DAL
{
  public class ContactDAL : IContactDAL
  {
    private readonly string _connectionString;
    private EasyAgendaContext _context;
    public ContactDAL(IConfiguration configuration, EasyAgendaContext context)
    {
      _connectionString = configuration.GetConnectionString("SqlServer");
      _context = context;
    }

    public async Task Update(IList<Contact> contacts)
    {
      try
      {
        _context.Update(contacts);
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
