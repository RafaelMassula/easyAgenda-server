using Microsoft.Data.SqlClient;
using System.Data;

namespace EasyAgenda.Data
{
  public class DbSession : IDisposable
  {
    public IDbConnection Connection { get; }
    public DbSession(string connectionString)
    {
      Connection = new SqlConnection(connectionString);
      Connection.Open();
    }

    public void Dispose() => Connection?.Dispose();
  }
}
