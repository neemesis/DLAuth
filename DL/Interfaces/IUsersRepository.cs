using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DL.Models;
using Microsoft.AspNetCore.Identity;

namespace DL.Interfaces {
    public interface IUsersRepository {
        Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken);

        Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken);

        Task<User> FindByIdAsync(string userId);

        Task<User> FindByNameAsync(string normalizedUserName);

        Task<User> FindByEmailAsync(string normalizedEmail);

        Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken);

        Task<IEnumerable<User>> GetAllUsers();

        void Dispose();
    }
}