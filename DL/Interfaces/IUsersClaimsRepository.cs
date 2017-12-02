using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DL.Models;

namespace DL.Interfaces {
    public interface IUsersClaimsRepository {
        Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken);

        Task AddClaimsAsync(User user, IEnumerable<Claim> claims);

        Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim);

        void Dispose();
    }
}