using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Dapper;
using DL.Interfaces;
using DL.Models;

namespace DL.Repositories {
    public class RolesRepository : IRolesRepository {
        private SqlConnection _sqlConnection;

        public RolesRepository(IDatabaseConnectionService sqlConnection) {
            _sqlConnection = sqlConnection.CreateConnection();
        }

        public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken) {
            const string command = "INSERT INTO dbo.Roles " +
                                   "VALUES (@Id, @ConcurrencyStamp, @Name, @NormalizedName);";

            var rowsInserted = Task.Run(() => _sqlConnection.ExecuteAsync(command, new {
                role.Id,
                role.ConcurrencyStamp,
                role.Name,
                role.NormalizedName
            }), cancellationToken).Result;

            return Task.FromResult(rowsInserted.Equals(1) ? IdentityResult.Success : IdentityResult.Failed(new IdentityError {
                Code = string.Empty,
                Description = $"The role with name {role.Name} could not be inserted in the dbo.Roles table."
            }));
        }

        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken) {
            const string command = "UPDATE dbo.Roles " +
                                   "SET ConcurrencyStamp = @ConcurrencyStamp, Name = @Name, NormalizedName = @NormalizedName " +
                                   "WHERE Id = @Id;";

            var rowsUpdated = Task.Run(() => _sqlConnection.ExecuteAsync(command, new {
                role.ConcurrencyStamp,
                role.Name,
                role.NormalizedName,
                role.Id
            }), cancellationToken).Result;

            return Task.FromResult(rowsUpdated.Equals(1) ? IdentityResult.Success : IdentityResult.Failed(new IdentityError {
                Code = string.Empty,
                Description = $"The role with name {role.Name} could not be updated in the dbo.Roles table."
            }));
        }

        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken) {
            const string command = "DELETE " +
                                   "FROM dbo.Roles " +
                                   "WHERE Id = @Id;";

            var rowsDeleted = Task.Run(() => _sqlConnection.ExecuteAsync(command, new { role.Id }), cancellationToken).Result;

            return Task.FromResult(rowsDeleted.Equals(1) ? IdentityResult.Success : IdentityResult.Failed(new IdentityError {
                Code = string.Empty,
                Description = $"The role with name {role.Name} could not be deleted from the dbo.Roles table."
            }));
        }

        public Task<Role> FindByIdAsync(string roleId) {
            const string command = "SELECT * " +
                                   "FROM dbo.Roles " +
                                   "WHERE Id = @Id;";

            return _sqlConnection.QuerySingleOrDefaultAsync<Role>(command, new {
                Id = roleId
            });
        }

        public Task<Role> FindByNameAsync(string normalizedRoleName) {
            const string command = "SELECT * " +
                                   "FROM dbo.Roles " +
                                   "WHERE NormalizedName = @NormalizedName;";

            return _sqlConnection.QuerySingleOrDefaultAsync<Role>(command, new {
                NormalizedName = normalizedRoleName
            });
        }

        public Task<IEnumerable<Role>> GetAllRoles() {
            const string command = "SELECT * " +
                                   "FROM dbo.Roles;";

            return _sqlConnection.QueryAsync<Role>(command);
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