using Microsoft.Data.SqlClient;
using System.Data;

namespace EasyAgenda.Data
{
    public class DbContext : IDisposable
    {
        public IDbConnection Connection { get; }
        public DbContext(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
