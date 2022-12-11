using EasyAgenda.Data.Contracts;
using EasyAgenda.Model.DTO;
using EasyAgendaService;
using Dapper;
using Microsoft.Data.SqlClient;
using EasyAgenda.ExtensionMethods;

namespace EasyAgenda.Data.DAL
{
    public class UserDAL : IUserDAL
    {
        private readonly string _connectionString;
        public UserDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServer");
        }
        public async Task<int> InsertReturn(UserDTO user)
        {
            var passwordEncrypt = new PasswordEncryptionService(user.Password);
            try
            {
                var query = DmlService<UserDTO>.GetQueryInsertReturn(user, user.TableName());
                using var conn = new DbSession(_connectionString);

                user.Password = passwordEncrypt.GetHashPassword();

                return (int)await conn.Connection.ExecuteScalarAsync(query, user);

            }
            catch (SqlException error)
            {
                await DeletePeople();
                throw new Exception(error.Message);
            }
        }
        private async Task DeletePeople()
        {
            string query = @"DELETE FROM 
                             	PEOPLE
                             WHERE 
                             	ID NOT IN
                             		(SELECT [PEOPLEID] FROM CUSTOMERS) 
                             AND 
                             	ID NOT IN
                             		(SELECT [PEOPLEID] FROM PROFESSIONALS)";

            using var conn = new DbSession(_connectionString).Connection;
            await conn.ExecuteAsync(query);
        }
    }
}
