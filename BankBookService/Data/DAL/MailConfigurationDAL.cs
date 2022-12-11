using EasyAgenda.Data;
using EasyAgendaService.Data.Contracts;
using EasyAgendaBase.Model;
using EasyAgendaService.ExtesionMethods;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace EasyAgendaService.DAL
{
    public class MailConfigurationDAL : IMailConfigurationDAL
    {
        private readonly string _connectionString;
        public MailConfigurationDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServer");
        }
        public async Task<SettingMail> GetMailConfiguration()
        {
            string query = @"SELECT 
                                [SERVERSMTP], [PORTTLS], [PORTSSL], [EMAIL], [PASSWORD], [COMPANYNAME]
                             FROM
                                [SETTINGMAIL]";
            try
            {
                using var conn = new DbContext(_connectionString);
                return await conn.Connection.QueryFirstOrDefaultAsync<SettingMail>(query);
            }
            catch (SqlException error)
            {
                throw new Exception(error.Message);
            }
        }
        public async Task<CustomizedMessage> GetCustomizedMessage(RecipientMessage recipientMessage, int idMessageType)
        {
            string query = @"SELECT 
                                [DESCRIPTION], [SUBJECT]
                             FROM
                                [CUSTOMIZEDMESSAGES]
                             WHERE
                                [MESSAGETYPEID] = @idMessageType";
            try
            {
                using var conn = new DbContext(_connectionString);
                CustomizedMessage message = await conn.Connection
                                                      .QueryFirstOrDefaultAsync<CustomizedMessage>(query, new { idMessageType });
                string description = message.Description;
                string subject = message.Subject;

                CustomizeMessage(recipientMessage, ref description);
                CustomizeMessage(recipientMessage, ref subject);
                
                message.Description = description;
                message.Subject = subject;

                return message;
            }
            catch (SqlException error)
            {
                throw new Exception(error.Message);
            }
        }
        private static void CustomizeMessage(RecipientMessage recipientMessage, ref string message)
        {
            string pattern = @"\[[A-Z]*\]";
            var regex = new Regex(pattern);

            var variables = regex.Matches(message);
            for (int i = 0; i < variables.Count; i++)
            {
                switch (variables[i].Value)
                {
                    case "[PEOPLE]":
                        message = message.Replace("[PEOPLE]", recipientMessage.NameClient);
                        break;
                    case "[COMPANY]":
                        message = message.Replace("[COMPANY]", recipientMessage.Company);
                        break;
                    case "[PHONE]":
                        message = message.Replace("[PHONE]", recipientMessage.PhoneCompany.ApplyMasckPhone());
                        break;
                    case "[EMAIL]":
                        message = message.Replace("[EMAIL]", recipientMessage.EmailCompany);
                        break;
                    case "[PROFESSIONAL]":
                        message = message.Replace("[PROFESSIONAL]", recipientMessage.NameProfessional);
                        break;
                    case "[DATA]":
                        message = message.Replace("[DATA]", recipientMessage.Date.Date.ToString("dd/MM/yyyy"));
                        break;
                    case "[START]":
                        message = message.Replace("[START]", recipientMessage.Start.ToString());
                        break;
                    case "[ADDRESS]":
                        regex = new Regex(@"[0-9]{8}");
                        string cep = regex.Match(recipientMessage.CompanyAddress).Value;
                        message = message.Replace("[ADDRESS]", recipientMessage.CompanyAddress.Replace(cep, cep.AplyMasckCep()));
                        break;

                }
            }
        }
    }
}
