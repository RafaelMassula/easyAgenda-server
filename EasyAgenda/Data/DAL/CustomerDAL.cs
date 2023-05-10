using Dapper;
using EasyAgenda.Data.Contracts;
using EasyAgenda.ExtensionMethods;
using EasyAgenda.Model;
using EasyAgenda.Model.DTO;
using EasyAgenda.Model.ViewModel;
using EasyAgendaBase.Enums;
using EasyAgendaBase.Model;
using EasyAgendaService;
using EasyAgendaService.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EasyAgenda.Data.DAL
{
  public class CustomerDAL : AbstractDAL, ICustomerDAL
  {
    private readonly EasyAgendaContext _context;
    private readonly IPeopleDAL _peopleRepository;
    private readonly IUserDAL _userRepository;
    private readonly IContactDAL _contactRepository;
    private readonly IEmailService _emailService;
    private readonly string _connectionString;
    public CustomerDAL(EasyAgendaContext context,
        IPeopleDAL peopleRepository,
        IUserDAL userRepository,
        IContactDAL contactRepository,
        IEmailService emailService,
        IConfiguration configuration)
    {
      _context = context;
      _peopleRepository = peopleRepository;
      _userRepository = userRepository;
      _contactRepository = contactRepository;
      _emailService = emailService;
      _connectionString = configuration.GetConnectionString("SqlServer");
    }


    public async Task<Customer> Get(int id)
    {
      try
      {
        var customer = await _context.Customers
          .Include(customer => customer.People)
          .SingleOrDefaultAsync(customer => customer.Id == id);

        if (customer == null)
        {
          throw new KeyNotFoundException("Not found");
        }
        return customer;
      }
      catch (Exception error)
      {
        throw new Exception(error.Message);
      }
    }

    public async override Task<PeopleViewModel> GetByUser(int id)
    {
      string query = @"SELECT
                           [CUSTOMERS].[ID],
                           [PEOPLE].[NAME], [PEOPLE].[LASTNAME], [PEOPLE].[CPF], [PEOPLE].[SEX], [PEOPLE].[BIRTHDATE],
                           [USERS].[EMAIL],
                           [CONTACTS].[PHONE]
                       FROM [CUSTOMERS]
                           INNER JOIN [PEOPLE]
                           ON [CUSTOMERS].[PEOPLEID] = [PEOPLE].[ID]
                           INNER JOIN [USERS]
                           ON [CUSTOMERS].[USERID] = [USERS].[ID]
                           LEFT JOIN [CONTACTS]
                           ON [PEOPLE].[ID] = [CONTACTS].[PEOPLEID]
                       WHERE [USERS].[ID] = @id";

      using var conn = new DbContext(_connectionString);

      var customer = await conn.Connection
        .QuerySingleOrDefaultAsync<PeopleViewModel>(query, new { id });

      if (customer == null)
      {
        throw new KeyNotFoundException("Not found");
      }
      return customer;
    }

    public async Task<IEnumerable<Customer>> GetAll()
    {
      try
      {
        var customers = await _context.Customers
            .Include(customer => customer.People)
            .OrderBy(customers => customers.Id)
            .ToListAsync();
        return customers;
      }
      catch (SqlException error)
      {
        throw new Exception(error.Message);
      }
    }

    public async override Task<IEnumerable<ScheduleViewModel>> GetAppointments(int id)
    {
      string query = @"SELECT 
                                CONCAT(PEOPLECLIENT.[NAME], ' ', PEOPLECLIENT.[LASTNAME]) CLIENT,
                                CONCAT(PEOPLEPROFESSIONAL.[NAME], ' ', PEOPLEPROFESSIONAL.[LASTNAME]) PROFESSIONAL,
                                AGENDAS.[SPECIALIZATION], AGENDAS.[DESCRIPTION], AGENDAS.[DATE], AGENDAS.[START], AGENDAS.[END],
                                SCHEDULES.[CUSTOMERID],
                                SCHEDULES.[AGENDAID],
                                SCHEDULES.[PROFESSIONALID] 
                             FROM
                             	SCHEDULES
                             INNER JOIN CUSTOMERS
                             	ON SCHEDULES.[CUSTOMERID] = CUSTOMERS.[ID]
                             INNER JOIN PEOPLE PEOPLECLIENT
                             	ON CUSTOMERS.[PEOPLEID] = PEOPLECLIENT.[ID]
                             INNER JOIN AGENDAS
                             	ON SCHEDULES.[AGENDAID] = AGENDAS.[ID]
                             INNER JOIN PROFESSIONALS
                             	ON SCHEDULES.[PROFESSIONALID] = PROFESSIONALS.[ID]
                             INNER JOIN PEOPLE PEOPLEPROFESSIONAL
                             	ON PROFESSIONALS.[PEOPLEID] = PEOPLEPROFESSIONAL.[ID]
                             WHERE SCHEDULES.[CUSTOMERID] = @id AND 
                            	 NOT EXISTS (SELECT 
                                                1
                            	  		      FROM 
                                                SCHEDULESCANCELLED 
                            	  		      WHERE 
                            	  			     SCHEDULESCANCELLED.[AGENDAID] = AGENDAS.[ID] AND
                            	  			     SCHEDULESCANCELLED.[CUSTOMERID] = CUSTOMERS.[ID] AND
                            	  			     SCHEDULESCANCELLED.[PROFESSIONALID] = PROFESSIONALS.[ID])";
      try
      {
        using var conn = new DbSession(_connectionString);
        return await conn.Connection.QueryAsync<ScheduleViewModel>(query, new { id });

      }
      catch (SqlException error)
      {
        throw new Exception(error.Message);
      }
    }

    public async override Task<IEnumerable<ScheduleViewModel>> GetAppointmentsByDate(int id, DateTime date)
    {
      string query = @"SELECT 
                                CONCAT(PEOPLECLIENT.[NAME], ' ', PEOPLECLIENT.[LASTNAME]) CLIENT,
                                CONCAT(PEOPLEPROFESSIONAL.[NAME], ' ', PEOPLEPROFESSIONAL.[LASTNAME]) PROFESSIONAL,
                                AGENDAS.[SPECIALIZATION], AGENDAS.[DESCRIPTION], AGENDAS.[DATE], AGENDAS.[START], AGENDAS.[END],
                                SCHEDULES.[CUSTOMERID],
                                SCHEDULES.[AGENDAID],
                                SCHEDULES.[PROFESSIONALID] 
                             FROM
                             	SCHEDULES
                             INNER JOIN CUSTOMERS
                             	ON SCHEDULES.[CUSTOMERID] = CUSTOMERS.[ID]
                             INNER JOIN PEOPLE PEOPLECLIENT
                             	ON CUSTOMERS.[PEOPLEID] = PEOPLECLIENT.[ID]
                             INNER JOIN AGENDAS
                             	ON SCHEDULES.[AGENDAID] = AGENDAS.[ID]
                             INNER JOIN PROFESSIONALS
                             	ON SCHEDULES.[PROFESSIONALID] = PROFESSIONALS.[ID]
                             INNER JOIN PEOPLE PEOPLEPROFESSIONAL
                             	ON PROFESSIONALS.[PEOPLEID] = PEOPLEPROFESSIONAL.[ID]
                             WHERE SCHEDULES.[CUSTOMERID] = @id AND 
                            	 AGENDAS.[DATE] = @date AND 
                            	 NOT EXISTS (SELECT 
                                                1
                            	  		      FROM 
                                                SCHEDULESCANCELLED 
                            	  		      WHERE 
                            	  			     SCHEDULESCANCELLED.[AGENDAID] = AGENDAS.[ID] AND
                            	  			     SCHEDULESCANCELLED.[CUSTOMERID] = CUSTOMERS.[ID] AND
                            	  			     SCHEDULESCANCELLED.[PROFESSIONALID] = PROFESSIONALS.[ID])";
      try
      {
        using var conn = new DbSession(_connectionString);
        return await conn.Connection.QueryAsync<ScheduleViewModel>(query, new { id, date });

      }
      catch (SqlException error)
      {
        throw new Exception(error.Message);
      }
    }

    public async override Task<IEnumerable<ScheduleCancelledViewModel>> GetCanceledAppointments(int id)
    {
      string query = @"SELECT 
                             	CONCAT(PEOPLECUSTOMER.[NAME], ' ', PEOPLECUSTOMER.[LASTNAME]) CLIENT,
                             	CONCAT(PEOPLEPROFESSIONAL.[NAME], ' ', PEOPLEPROFESSIONAL.[LASTNAME]) PROFESSIONAL,
                             	AGENDAS.[SPECIALIZATION], AGENDAS.[DESCRIPTION], AGENDAS.[DATE], AGENDAS.[START], AGENDAS.[END],
                             	SCHEDULESCANCELLED.[CANCELLATION], SCHEDULESCANCELLED.[OBSERVATION]
                             FROM
                             	SCHEDULES
                             INNER JOIN PROFESSIONALS
                             	ON SCHEDULES.[PROFESSIONALID] = PROFESSIONALS.[ID]
                             INNER JOIN PEOPLE AS PEOPLEPROFESSIONAL
                             	ON PROFESSIONALS.[PEOPLEID] = PEOPLEPROFESSIONAL.[ID]
                             INNER JOIN AGENDAS
                             	ON SCHEDULES.[AGENDAID] = AGENDAS.[ID]
                             INNER JOIN SCHEDULESCANCELLED
                             	ON SCHEDULES.[CUSTOMERID] = SCHEDULESCANCELLED.[CUSTOMERID] AND
                             	SCHEDULES.[PROFESSIONALID] = SCHEDULESCANCELLED.[PROFESSIONALID] AND
                             	SCHEDULES.[AGENDAID] = SCHEDULESCANCELLED.[AGENDAID]
                             INNER JOIN CUSTOMERS
                             	ON SCHEDULESCANCELLED.[CUSTOMERID] = CUSTOMERS.[ID]
                             INNER JOIN PEOPLE AS PEOPLECUSTOMER
                             	ON CUSTOMERS.[PEOPLEID] = PEOPLECUSTOMER.[ID]
                             WHERE SCHEDULES.[CUSTOMERID] = @id";

      try
      {
        using var conn = new DbSession(_connectionString);
        return await conn.Connection.QueryAsync<ScheduleCancelledViewModel>(query, new { id });
      }
      catch (SqlException error)
      {
        throw new Exception(error.Message);
      }
    }

    public async Task Register(RecordDTO record)
    {
      int peopleId = await _peopleRepository.InsertReturn(record.People);
      int userId = await _userRepository.InsertReturn(record.User);

      try
      {
        record.Contact.PeopleId = peopleId;

        CustomerDTO customer = new(peopleId, userId);

        string query = DmlService<CustomerDTO>
                      .GetQueryInsert(customer, customer.TableName());

        using var connection = new DbSession(_connectionString).Connection;
        await connection.ExecuteAsync(query, customer);

        SendEmail(record, TypeMessage.NEW_USER);
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
    private void SendEmail(RecordDTO record, TypeMessage typeMessage)
    {
      //RECUPERAR DADOS DA EMPRESA PARA ENVIAR MSG DE BOAS VINDAS
      RecipientMessage recipientMessage = new(string.Concat(record.People.Name, " ", record.People.LastName), record.User.Email);

      _emailService.Send(recipientMessage, typeMessage);
    }
  }
}
