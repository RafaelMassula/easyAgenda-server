using EasyAgenda.Data.Contracts;
using EasyAgenda.Exceptions;
using EasyAgenda.Model.DTO;
using EasyAgenda.Model.ViewModel;
using EasyAgendaBase.Enums;
using EasyAgendaBase.Model;
using EasyAgendaService;
using EasyAgendaService.Contracts;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EasyAgenda.ExtensionMethods;
using EasyAgenda.Model;

namespace EasyAgenda.Data.DAL
{
    public class ScheduleDAL : IScheduleDAL
    {
        private readonly string _connectionString;
        private readonly IEmailService _emailService;
        public ScheduleDAL(
            IConfiguration configuration,
            IEmailService emailService)
        {
            _connectionString = configuration.GetConnectionString("SqlServer");
            _emailService = emailService;
        }
        public async Task<IEnumerable<ScheduleViewModel>> GetSchedulesOpen(int idProfessional)
        {
            string query = @"SELECT
                                 CONCAT(PEOPLE.[NAME], ' ', [PEOPLE].[LASTNAME]) PROFESSIONAL,
                                 [AGENDAS].[SPECIALIZATION], [AGENDAS].[DESCRIPTION], [AGENDAS].[DATE], 
                                 [AGENDAS].[START], AGENDAS.[END], [AGENDAS].[ID] AS AGENDAID,
                                 [PROFESSIONALS].[ID] AS PROFESSIONALID
                             FROM
                                 AGENDAS
                                 INNER JOIN [PROFESSIONALS]
                                 ON [AGENDAS].[PROFESSIONALID] = [PROFESSIONALS].[ID]
                                 INNER JOIN [PEOPLE]
                                 ON [PROFESSIONALS].[PEOPLEID] = [PEOPLE].[ID]
                             WHERE 
                                 NOT EXISTS(
                                 SELECT
                                     1
                                 FROM
                                     [SCHEDULES]
                                 WHERE 
                                     [SCHEDULES].[PROFESSIONALID] = [PROFESSIONALS].[ID] AND
                                     [SCHEDULES].[AGENDAID] = [AGENDAS].[ID])
                                 AND NOT EXISTS(
                                 SELECT
                                     1
                                 FROM
                                     [SCHEDULERESERVED]
                                 WHERE 
                                     [SCHEDULERESERVED].[PROFESSIONALID] = [PROFESSIONALS].[ID] AND
                                     [SCHEDULERESERVED].[AGENDAID] = [AGENDAS].[ID]) ";
            if (idProfessional > 0)
                query += "AND [PROFESSIONALS].[ID] = @idProfessional";

            try
            {
                using var conn = new DbSession(_connectionString);
                return await conn.Connection.QueryAsync<ScheduleViewModel>(query, new { idProfessional });
            }
            catch (SqlException error)
            {
                throw new Exception(error.Message);
            }
        }
        public async Task RegisterAppointment(ScheduleDTO schedule)
        {
            try
            {
                await CheckSchedule(schedule);

                string query = DmlService<ScheduleDTO>.GetQueryInsert(schedule, schedule.TableName());

                using var conn = new DbSession(_connectionString);
                await conn.Connection.ExecuteAsync(query, schedule);

                var schedulesData = await GetScheduleData(schedule);

                RecipientMessage recipientMessage = new(schedulesData.Client, schedulesData.EmailClient,
                           schedulesData.Date, schedulesData.Start, schedulesData.Professional, schedulesData.EmailProfessional, schedulesData.Company,
                           schedulesData.PhoneCompany, schedulesData.EmailCompany, schedulesData.Address);


                await _emailService.Send(recipientMessage, TypeMessage.NEW_APPOINTMENT_CLIENT);
                await _emailService.Send(recipientMessage, TypeMessage.NEW_APPOINTMENT_PROFESSIONAL);
            }
            catch (ScheduleException error)
            {
                throw new ScheduleException(error.Message);
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task CancelAppointment(ScheduleCancelledDTO scheduleCancelled)
        {
            var schedule = new ScheduleDTO(scheduleCancelled.CustomerId, scheduleCancelled.ProfessionalId,
                scheduleCancelled.AgendaId);

            try
            {
                var schedulesData = await GetScheduleData(schedule);

                string query = DmlService<ScheduleCancelledDTO>.GetQueryInsert(scheduleCancelled, scheduleCancelled.TableName());

                using var conn = new DbSession(_connectionString);
                await conn.Connection.ExecuteAsync(query, scheduleCancelled);


                RecipientMessage recipientMessage = new(schedulesData.Client, schedulesData.EmailClient,
                           schedulesData.Date, schedulesData.Start, schedulesData.Professional, schedulesData.EmailProfessional, schedulesData.Company,
                           schedulesData.PhoneCompany, schedulesData.EmailCompany, schedulesData.Address);


                await _emailService.Send(recipientMessage, TypeMessage.APPOINTMENT_CANCELLED_CLIENT);
                await _emailService.Send(recipientMessage, TypeMessage.APPOINTMENT_CANCELLED_PROFESSIONAL);

            }
            catch (ScheduleException error)
            {
                throw new ScheduleException(error.Message);
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        private async Task CheckSchedule(ScheduleDTO schedule)
        {
            string query = @"SELECT
                                 PROFESSIONALS.[ID] AS IDPROFESSIONAL,
                                 SCHEDULES.CUSTOMERID AS IDCUSTOMER,
                                 AGENDAS.[ID] AS IDAGENDA,
                                 CONCAT(SCHEDULES.[AGENDAID],SCHEDULES.[CUSTOMERID],SCHEDULES.[PROFESSIONALID]) AS IDSCHEDULE,
                                 SCHEDULESCANCELLED.[ID] AS IDSCHEDULECANCELLED
                             FROM
                                 AGENDAS
                                 INNER JOIN PROFESSIONALS
                                 ON AGENDAS.[PROFESSIONALID] = PROFESSIONALS.[ID]
                                 LEFT JOIN SCHEDULES
                                 ON AGENDAS.[ID] = SCHEDULES.[AGENDAID] AND
                                     PROFESSIONALS.[ID] = SCHEDULES.[PROFESSIONALID]
                                 LEFT JOIN SCHEDULESCANCELLED
                                 ON SCHEDULES.CUSTOMERID = SCHEDULESCANCELLED.CUSTOMERID
                                     AND SCHEDULES.PROFESSIONALID = SCHEDULESCANCELLED.PROFESSIONALID
                                     AND SCHEDULES.AGENDAID = SCHEDULESCANCELLED.AGENDAID
                             WHERE 
                                 PROFESSIONALS.[ID] = @ProfessionalId AND
                                 AGENDAS.[ID] = @AgendaId";
            try
            {
                using var conn = new DbSession(_connectionString);
                var result = await conn.Connection.QueryFirstOrDefaultAsync<(int idProfessional, int idCustomer, int idAgenda, string idSchedule,
                    int idScheduleCancelled)>(query, schedule);

                //verifica se o horário já está agendado para outro ou o mesmo cliente.
                if (result.idAgenda > 0 && !string.IsNullOrEmpty(result.idSchedule) &&
                    result.idScheduleCancelled == 0)
                    throw new ScheduleException("Appointment already booked.");

                //verifica se o horário solicitado existe na agenda do profissional.
                if (result.idAgenda == 0)
                    throw new ScheduleException("Time not available.");
            }
            catch (SqlException error)
            {
                throw new Exception(error.Message);
            }

        }

        private async Task<ScheduleViewModel> GetScheduleData(ScheduleDTO schedule)
        {
            string query = @"SELECT
                             	CONCAT(PEOPLECUSTOMER.[NAME], ' ', PEOPLECUSTOMER.[LASTNAME]) CLIENT,
                             	USERSCUSTOMER.[EMAIL] EMAILCLIENT, CONTACTSCUSTOMER.[PHONE] PHONECLIENT,
                             	CONCAT(PEOPLEPROFESSIONAL.[NAME], ' ', PEOPLEPROFESSIONAL.[LASTNAME]) PROFESSIONAL,
                             	USERSPROFESSIONAL.[EMAIL] EMAILPROFESSIONAL, CONTACTSPROFESSIONAL.[PHONE] PHONEPROFESSIONAL,
                             	AGENDAS.[DATE], AGENDAS.[START],
                             	COMPANIES.[DESCRIPTION] COMPANY, COMPANIES.[PHONE] PHONECOMPANY, COMPANIES.[EMAIL] EMAILCOMPANY,
                             	CONCAT(ADDRESSES.[STREET],' - ', ADDRESSES.[NUMBER], ' - ', ADDRESSES.[COMPLEMENT], CHAR(13), 
                             	ADDRESSES.[NEIGHBORHOOD], ' - ', ADDRESSES.[CITY], ' - ', STATES.[INITIALS], CHAR(13),
                             	ADDRESSES.[ZIPCODE]) ADDRESS
                             FROM
                             	SCHEDULES
                             INNER JOIN AGENDAS
                             	ON SCHEDULES.[AGENDAID] = AGENDAS.[ID]
                             INNER JOIN  PROFESSIONALS
                             	ON SCHEDULES.[PROFESSIONALID] = PROFESSIONALS.[ID]
                             INNER JOIN PEOPLE AS PEOPLEPROFESSIONAL
                             	ON PROFESSIONALS.[PEOPLEID] = PEOPLEPROFESSIONAL.[ID]
                             INNER JOIN CONTACTS AS CONTACTSPROFESSIONAL
                             	ON PEOPLEPROFESSIONAL.[ID] = CONTACTSPROFESSIONAL.[PEOPLEID]
                             INNER JOIN USERS AS USERSPROFESSIONAL
                             	ON PROFESSIONALS.[USERID] = USERSPROFESSIONAL.[ID]
                             INNER JOIN CUSTOMERS
                             	ON SCHEDULES.[CUSTOMERID] = CUSTOMERS.[ID]
                             INNER JOIN PEOPLE AS PEOPLECUSTOMER
                             	ON CUSTOMERS.[PEOPLEID] = PEOPLECUSTOMER.[ID]
                             INNER JOIN CONTACTS AS CONTACTSCUSTOMER
                             	ON PEOPLECUSTOMER.[ID] = CONTACTSCUSTOMER.[PEOPLEID]
                             INNER JOIN USERS AS USERSCUSTOMER
                             	ON CUSTOMERS.[USERID] = USERSCUSTOMER.[ID]
                             INNER JOIN COMPANIES
                             	ON PROFESSIONALS.[COMPANYID] = COMPANIES.[ID]
                             INNER JOIN ADDRESSES
                             	ON  ADDRESSES.[COMPANYID] = COMPANIES.[ID]
                             INNER JOIN STATES
                             	ON ADDRESSES.[STATEID] = STATES.[ID]
                             WHERE AGENDAS.[ID] = @AgendaId AND PROFESSIONALS.[ID] = @ProfessionalId AND 
                                CUSTOMERS.[ID] = @CustomerId";
            try
            {
                using var conn = new DbSession(_connectionString);
                var schedulesData = await conn.Connection.QueryFirstOrDefaultAsync<ScheduleViewModel>(query, schedule);

                if (schedulesData == null)
                {
                    throw new Exception("Appointment data not found");
                }
                return schedulesData;
            }
            catch (SqlException error)
            {
                throw new Exception(error.Message);
            }
        }

        public Task ReserveSchedule(ScheduleReservedDTO scheduleReserverd)
        {
            throw new NotImplementedException();
        }
    }
}
