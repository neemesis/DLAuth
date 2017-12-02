using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BL.Interfaces {
    public interface IRoleStore {
        IQueryable<BL.Models.Role> Roles();

        Task<IdentityResult> CreateAsync(BL.Models.Role role, CancellationToken cancellationToken);

        Task<IdentityResult> UpdateAsync(BL.Models.Role role, CancellationToken cancellationToken);

        Task<IdentityResult> DeleteAsync(BL.Models.Role role, CancellationToken cancellationToken);

        Task<string> GetRoleIdAsync(BL.Models.Role role, CancellationToken cancellationToken);

        Task<string> GetRoleNameAsync(BL.Models.Role role, CancellationToken cancellationToken);

        Task SetRoleNameAsync(BL.Models.Role role, string roleName, CancellationToken cancellationToken);

        Task<string> GetNormalizedRoleNameAsync(BL.Models.Role role, CancellationToken cancellationToken);

        Task SetNormalizedRoleNameAsync(BL.Models.Role role, string normalizedName,
           CancellationToken cancellationToken);

        Task<BL.Models.Role> FindByIdAsync(string roleId, CancellationToken cancellationToken);

        Task<BL.Models.Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken);

        void Dispose();
    }
}