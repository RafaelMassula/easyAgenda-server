using EasyAgenda.Data.Contracts;
using EasyAgenda.Model;
using EasyAgenda.Model.DTO;
using EasyAgendaService;
using Dapper;
using Microsoft.Data.SqlClient;
using EasyAgenda.ExtensionMethods;

namespace EasyAgenda.Data.DAL
{
    public class ContactDAL : IContactDAL
    {
        private readonly string _connectionString;
        public ContactDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServer");
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Insert(ContactDTO contact)
        {
            try
            {
                string query = DmlService<ContactDTO>.GetQueryInsert(contact, contact.TableName());
                using var conn = new DbSession(_connectionString);
                await conn.Connection.ExecuteAsync(query, contact);
            }
            catch (SqlException error)
            {
                throw new Exception(error.Message);
            }
        }

        public Task Update(ContactDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
