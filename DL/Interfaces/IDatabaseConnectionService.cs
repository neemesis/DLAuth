using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DL.Interfaces {
    public interface IDatabaseConnectionService : IDisposable {
        Task<SqlConnection> CreateConnectionAsync();
        SqlConnection CreateConnection();
    }
}