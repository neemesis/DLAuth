using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DL.Models;
using DL.Interfaces;

namespace DL.Repositories {
    public class UsersRolesRepository : IUsersRolesRepository {
        private SqlConnection _sqlConnection;

        public UsersRolesRepository(IDatabaseConnectionService sqlConnection) {
            _sqlConnection = sqlConnection.CreateConnection();
        }

        public Task AddToRoleAsync(User user, string roleId) {
            const string command = "INSERT INTO dbo.UsersRoles " +
                                   "VALUES (@UserId, @RoleId);";

            return _sqlConnection.ExecuteAsync(command, new {
                UserId = user.Id,
                RoleId = roleId
            });
        }

        public Task RemoveFromRoleAsync(User user, string roleId) {
            const string command = "DELETE " +
                                   "FROM dbo.UsersRoles " +
                                   "WHERE UserId = @UserId AND RoleId = @RoleId;";

            return _sqlConnection.ExecuteAsync(command, new {
                UserId = user.Id,
                RoleId = roleId
            });
        }

        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken) {
            const string command = "SELECT r.Name " +
                                   "FROM dbo.Roles as r " +
                                   "INNER JOIN dbo.UsersRoles AS ur ON ur.RoleId = r.Id " +
                                   "WHERE ur.UserId = @UserId;";

            var userRoles = Task.Run(() => _sqlConnection.QueryAsync<string>(command, new {
                UserId = user.Id
            }), cancellationToken).Result;

            return Task.FromResult<IList<string>>(userRoles.ToList());
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