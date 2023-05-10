using EasyAgenda.Data.Contracts;
using EasyAgenda.Model;
using EasyAgendaService;
using System.Data.SqlClient;

namespace EasyAgenda.Data.DAL
{
  public class AdminDAL : IAdminDAL
  {
    private readonly EasyAgendaContext _context;
    private readonly string _connectionString;

    public AdminDAL(EasyAgendaContext context, IConfiguration configuration)
    {
      _context = context;
      _connectionString = configuration.GetConnectionString("SqlServer");
    }

    public async Task Register(Admin admin)
    {
      var encryptPassword = new PasswordService(admin.User.Password);
      admin.User.Password = encryptPassword.GetHashPassword();

      try
      {
        _context.Add(admin);
        await _context.SaveChangesAsync();
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
