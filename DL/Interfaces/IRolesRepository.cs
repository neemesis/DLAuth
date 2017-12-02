using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DL.Models;
using Microsoft.AspNetCore.Identity;

namespace DL.Interfaces {
    public interface IRolesRepository {
        Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken);

        Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken);

        Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken);

        Task<Role> FindByIdAsync(string roleId);

        Task<Role> FindByNameAsync(string normalizedRoleName);

        Task<IEnumerable<Role>> GetAllRoles();

        void Dispose();
    }
}