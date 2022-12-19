using EasyAgenda.Data.Contracts;
using EasyAgenda.Exceptions;
using EasyAgenda.ExtensionMethods;
using EasyAgenda.Model;
using EasyAgenda.Model.DTO;
using EasyAgenda.Model.ViewModel;
using EasyAgendaService;
using EasyAgendaService.Exceptions;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace EasyAgenda.Data.DAL
{
    public class ProfessionalDAL : AbstractDAL, IProfessionalDAL
    {
        private readonly EasyAgendaContext _context;
        private readonly string _connectionString;
        private readonly IPeopleDAL _peopleRepository;
        private readonly IUserDAL _userRepository;
        private readonly IContactDAL _contactRepository;
        public ProfessionalDAL(EasyAgendaContext context,
            IConfiguration configuration,
            IPeopleDAL peopleRepository,
            IUserDAL userRepository,
            IContactDAL contactRepository)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("SqlServer");
            _peopleRepository = peopleRepository;
            _userRepository = userRepository;
            _contactRepository = contactRepository;
        }
        public async Task<Professional> Get(int id)
        {
            try
            {
                var professional = await _context.Professionals
                  .Include(professional => professional.People)
                  .SingleOrDefaultAsync(professional => professional.Id == id);

                if (professional == null)
                {
                    throw new KeyNotFoundException("Not found");
                }
                return professional;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<IEnumerable<Professional>> GetAll()
        {
            try
            {
                var professional = await _context.Professionals
                    .Include(professional => professional.People)
                    .ToListAsync();

                return professional;
            }
            catch (Exception error)
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
                             WHERE SCHEDULES.[PROFESSIONALID] = @id AND 
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
                             WHERE SCHEDULES.[PROFESSIONALID] = @id AND 
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
                             WHERE SCHEDULES.[PROFESSIONALID] = @id";

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

        public async Task Register(RecordProfessionalDTO record)
        {
            try
            {
                int peopleId = await _peopleRepository.InsertReturn(record.People);
                int userId = await _userRepository.InsertReturn(record.User);

                record.Contact.PeopleId = peopleId;
                await _contactRepository.Insert(record.Contact);

                ProfessionalDTO professional = new(peopleId, userId, record.CompanyId);

                string query = DmlService<ProfessionalDTO>
                              .GetQueryInsert(professional, professional.TableName());

                using var connection = new DbSession(_connectionString).Connection;
                await connection.ExecuteAsync(query, professional);
            }
            catch (CpfException error)
            {
                throw new CpfException(error.Message);
            }
            catch (BirthDateException error)
            {
                throw new BirthDateException(error.Message);
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

        public async Task OpenAgenda(IList<AgendaDTO> agendas)
        {
            try
            {
                agendas.SortAgenda();
                agendas.CheckDatesLessCurrentDay();
                agendas.CheckConflictingSchedules();

                string query = DmlService<AgendaDTO>
                    .GetQueryInsert(agendas.First(), agendas.First().TableName());

                using var connection = new DbSession(_connectionString).Connection;

                await connection.ExecuteAsync(query, agendas);
            }
            catch (TimeoutException error)
            {
                throw new TimeoutException(error.Message);

            }
            catch (ScheduleException error)
            {
                throw new ScheduleException(error.Message);

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
