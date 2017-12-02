using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DL.Interfaces;
using DL.Models;
using Microsoft.AspNetCore.Identity;

namespace DL.Repositories {
    public class UsersLoginsRepository : IUsersLoginsRepository {
        private SqlConnection _sqlConnection;

        public UsersLoginsRepository(IDatabaseConnectionService sqlConnection) {
            _sqlConnection = sqlConnection.CreateConnection();
        }

        public Task AddLoginAsync(User user, UserLoginInfo login) {
            const string command = "INSERT INTO dbo.UsersLogins " +
                                   "VALUES (@LoginProvider, @ProviderKey, @UserId, @ProviderDisplayName);";

            return _sqlConnection.ExecuteAsync(command, new {
                login.LoginProvider,
                login.ProviderKey,
                UserId = user.Id,
                login.ProviderDisplayName
            });
        }

        public Task RemoveLoginAsync(User user, string loginProvider, string providerKey) {
            const string command = "DELETE " +
                                   "FROM dbo.UsersLogins " +
                                   "WHERE UserId = @UserId AND LoginProvider = @LoginProvider AND ProviderKey = @ProviderKey;";

            return _sqlConnection.ExecuteAsync(command, new {
                UserId = user.Id,
                LoginProvider = loginProvider,
                ProviderKey = providerKey
            });
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken) {
            const string command = "SELECT * " +
                                   "FROM dbo.UsersLogins " +
                                   "WHERE UserId = @UserId;";

            var userLogins = Task.Run(() => _sqlConnection.QueryAsync<UserLogin>(command, new {
                UserId = user.Id
            }), cancellationToken).Result;

            return Task.FromResult<IList<UserLoginInfo>>(userLogins.Select(e => new UserLoginInfo(e.LoginProvider, e.ProviderKey, e.ProviderDisplayName)).ToList());
        }

        public Task<User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken) {
            string[] command =
            {
                "SELECT UserId " +
                "FROM dbo.UsersLogins " +
                "WHERE LoginProvider = @LoginProvider AND ProviderKey = @ProviderKey;"
            };

            var userstring = Task.Run(() => _sqlConnection.QuerySingleOrDefaultAsync<string>(command[0], new {
                LoginProvider = loginProvider,
                ProviderKey = providerKey
            }), cancellationToken).Result;

            if (userstring == null) {
                return Task.FromResult<User>(null);
            }

            command[0] = "SELECT * " +
                         "FROM dbo.Users " +
                         "WHERE Id = @Id;";

            return _sqlConnection.QuerySingleAsync<User>(command[0], new { Id = userstring });
        }

        public void Dispose() {
            if (_sqlConnection == null) {
                return;
            }

            _sqlConnection.Dispose();
            _sqlConnection = null;
        }
    }
}