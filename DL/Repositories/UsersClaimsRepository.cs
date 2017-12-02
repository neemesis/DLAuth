using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DL.Interfaces;
using DL.Models;

namespace DL.Repositories {
    public class UsersClaimsRepository : IUsersClaimsRepository {
        private SqlConnection _sqlConnection;

        public UsersClaimsRepository(IDatabaseConnectionService sqlConnection) {
            _sqlConnection = sqlConnection.CreateConnection();
        }

        public Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken) {
            const string command = "SELECT * " +
                                   "FROM dbo.UsersClaims " +
                                   "WHERE UserId = @UserId;";

            var userClaims = Task.Run(() => _sqlConnection.QueryAsync<UserClaim>(command, new {
                UserId = user.Id
            }), cancellationToken).Result;

            return Task.FromResult<IList<Claim>>(userClaims.Select(e => new Claim(e.ClaimType, e.ClaimValue)).ToList());
        }

        public Task AddClaimsAsync(User user, IEnumerable<Claim> claims) {
            const string command = "INSERT INTO dbo.UsersClaims " +
                                   "VALUES (@Id, @UserId, @ClaimType, @ClaimValue);";

            return _sqlConnection.ExecuteAsync(command, claims.Select(e => new {
                Id = Guid.NewGuid().ToString(),
                UserId = user.Id,
                ClaimType = e.Type,
                ClaimValue = e.Value
            }));
        }

        public Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim) {
            const string command = "UPDATE dbo.UsersClaims " +
                                   "SET ClaimType = @NewClaimType, ClaimValue = @NewClaimValue " +
                                   "WHERE UserId = @UserId AND ClaimType = @ClaimType AND ClaimValue = @ClaimType;";

            return _sqlConnection.ExecuteAsync(command, new {
                NewClaimType = newClaim.Type,
                NewClaimValue = newClaim.Value,
                UserId = user.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            });
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