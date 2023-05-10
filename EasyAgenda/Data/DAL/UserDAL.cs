using Dapper;
using EasyAgenda.Data.Contracts;
using EasyAgenda.ExtensionMethods;
using EasyAgenda.Model;
using EasyAgenda.Model.DTO;
using EasyAgendaService;
using Microsoft.Data.SqlClient;

namespace EasyAgenda.Data.DAL
{
  public class UserDAL : IUserDAL
  {
    private readonly string _connectionString;
    public UserDAL(IConfiguration configuration)
    {
      _connectionString = configuration.GetConnectionString("SqlServer");
    }
    public async Task<User> GetByEmail(string email)
    {
      var query = @$"SELECT
                          [USERS].[ID], [USERS].[EMAIL], [USERS].[PASSWORD],
                          [PROFILES].[ID], [PROFILES].[DESCRIPTION]
                       FROM
                          [USERS]
                       INNER JOIN [PROFILES] ON [USERS].[PROFILEID] = [PROFILES].[ID]
                       WHERE
                          [EMAIL] = '{email}'";

      using var conn = new DbSession(_connectionString);
      var users = await conn.Connection.QueryAsync<User, Profile, User>(query, (user, profile) =>
      {
        user.Profile = profile;
        return user;
      });

      var user = users.FirstOrDefault();

      if (user == null)
        throw new KeyNotFoundException("User not found");

      return user;
    }

    public async Task<int> InsertReturn(UserDTO user)
    {
      var encryptPassword = new PasswordService(user.Password);
      try
      {
        var query = DmlService<UserDTO>.GetQueryInsertReturn(user, user.TableName());
        using var conn = new DbSession(_connectionString);

        user.Password = encryptPassword.GetHashPassword();

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
