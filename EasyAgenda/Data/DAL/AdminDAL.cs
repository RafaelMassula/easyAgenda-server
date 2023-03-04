using Dapper;
using EasyAgenda.Data.Contracts;
using EasyAgenda.ExtensionMethods;
using EasyAgenda.Model;
using EasyAgenda.Model.DTO;
using EasyAgendaBase.Enums;
using EasyAgendaService;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace EasyAgenda.Data.DAL
{
  public class AdminDAL : IAdminDAL
  {
    private readonly EasyAgendaContext _context;
    private readonly IPeopleDAL _peopleRepository;
    private readonly IUserDAL _userRepository;
    private readonly IContactDAL _contactRepository;
    private readonly string _connectionString;

    public AdminDAL(EasyAgendaContext context, IPeopleDAL peopleRepository, IUserDAL userRepository,
      IContactDAL contactRepository, IConfiguration configuration)
    {
      _context = context;
      _peopleRepository = peopleRepository;
      _userRepository = userRepository;
      _contactRepository = contactRepository;
      _connectionString = configuration.GetConnectionString("SqlServer");
    }

    public async Task Register(RecordDTO record)
    {
      int peopleId = await _peopleRepository.InsertReturn(record.People);
      int userId = await _userRepository.InsertReturn(record.User);

      try
      {
        record.Contact.PeopleId = peopleId;
        await _contactRepository.Insert(record.Contact);

        CustomerDTO customer = new(peopleId, userId);

        string query = DmlService<CustomerDTO>
                      .GetQueryInsert(customer, customer.TableName());

        using var connection = new DbSession(_connectionString).Connection;
        await connection.ExecuteAsync(query, customer);

        //Posteriormente enviar um email de boas vindas SendEmail(record, TypeMessage.NEW_USER);
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
