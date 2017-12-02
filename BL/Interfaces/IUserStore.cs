using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BL.Interfaces {
    public interface IUserStore {
        IQueryable<BL.Models.User> Users();

        #region IUserStore<ApplicationUser> implementation.

        Task<IdentityResult> CreateAsync(BL.Models.User user, CancellationToken cancellationToken);

        Task<IdentityResult> DeleteAsync(BL.Models.User user, CancellationToken cancellationToken);

        Task<BL.Models.User> FindByIdAsync(string userId, CancellationToken cancellationToken);

        Task<BL.Models.User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken);

        Task<string> GetNormalizedUserNameAsync(BL.Models.User user, CancellationToken cancellationToken);

        Task<string> GetUserIdAsync(BL.Models.User user, CancellationToken cancellationToken);

        Task<string> GetUserNameAsync(BL.Models.User user, CancellationToken cancellationToken);

        Task SetNormalizedUserNameAsync(BL.Models.User user, string normalizedName,
           CancellationToken cancellationToken);

        Task SetUserNameAsync(BL.Models.User user, string userName, CancellationToken cancellationToken);

        Task<IdentityResult> UpdateAsync(BL.Models.User user, CancellationToken cancellationToken);

        void Dispose();
        #endregion IUserStore<ApplicationUser> implementation.

        #region IUserEmailStore<ApplicationUser> implementation.

        Task SetEmailAsync(BL.Models.User user, string email, CancellationToken cancellationToken);

        Task<string> GetEmailAsync(BL.Models.User user, CancellationToken cancellationToken);

        Task<bool> GetEmailConfirmedAsync(BL.Models.User user, CancellationToken cancellationToken);

        Task SetEmailConfirmedAsync(BL.Models.User user, bool confirmed, CancellationToken cancellationToken);

        Task<BL.Models.User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken);

        Task<string> GetNormalizedEmailAsync(BL.Models.User user, CancellationToken cancellationToken);

        Task SetNormalizedEmailAsync(BL.Models.User user, string normalizedEmail,
           CancellationToken cancellationToken);
        #endregion IUserEmailStore<ApplicationUser> implementation.

        #region IUserLoginStore<ApplicationUser> implementation.

        Task AddLoginAsync(BL.Models.User user, UserLoginInfo login, CancellationToken cancellationToken);

        Task RemoveLoginAsync(BL.Models.User user, string loginProvider, string providerKey,
           CancellationToken cancellationToken);

        Task<IList<UserLoginInfo>> GetLoginsAsync(BL.Models.User user, CancellationToken cancellationToken);

        Task<BL.Models.User> FindByLoginAsync(string loginProvider, string providerKey,
           CancellationToken cancellationToken);
        #endregion IUserLoginStore<ApplicationUser> implementation.

        #region IUserPasswordStore<ApplicationUser> implementation.

        Task SetPasswordHashAsync(BL.Models.User user, string passwordHash,
           CancellationToken cancellationToken);

        Task<string> GetPasswordHashAsync(BL.Models.User user, CancellationToken cancellationToken);

        Task<bool> HasPasswordAsync(BL.Models.User user, CancellationToken cancellationToken);
        #endregion IUserPasswordStore<ApplicationUser> implementation.

        #region IUserPhoneNumberStore<ApplicationUser> implementation.

        Task SetPhoneNumberAsync(BL.Models.User user, string phoneNumber, CancellationToken cancellationToken);

        Task<string> GetPhoneNumberAsync(BL.Models.User user, CancellationToken cancellationToken);

        Task<bool> GetPhoneNumberConfirmedAsync(BL.Models.User user, CancellationToken cancellationToken);

        Task SetPhoneNumberConfirmedAsync(BL.Models.User user, bool confirmed,
           CancellationToken cancellationToken);

        #endregion IUserPhoneNumberStore<ApplicationUser> implementation.

        #region IUserTwoFactorStore<ApplicationUser> implementation.

        Task SetTwoFactorEnabledAsync(BL.Models.User user, bool enabled, CancellationToken cancellationToken);

        Task<bool> GetTwoFactorEnabledAsync(BL.Models.User user, CancellationToken cancellationToken);
        #endregion IUserTwoFactorStore<ApplicationUser> implementation.

        #region IUserSecurityStampStore<ApplicationUser> implementation.

        Task SetSecurityStampAsync(BL.Models.User user, string stamp, CancellationToken cancellationToken);

        Task<string> GetSecurityStampAsync(BL.Models.User user, CancellationToken cancellationToken);
        #endregion IUserSecurityStampStore<ApplicationUser> implementation.

        #region IUserClaimStore<ApplicationUser> implementation.

        Task<IList<Claim>> GetClaimsAsync(BL.Models.User user, CancellationToken cancellationToken);

        Task AddClaimsAsync(BL.Models.User user, IEnumerable<Claim> claims,
           CancellationToken cancellationToken);

        Task ReplaceClaimAsync(BL.Models.User user, Claim claim, Claim newClaim,
           CancellationToken cancellationToken);

        Task RemoveClaimsAsync(BL.Models.User user, IEnumerable<Claim> claims,
           CancellationToken cancellationToken);

        Task<IList<BL.Models.User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken);
        #endregion IUserClaimStore<ApplicationUser> 

        #region IUserLockoutStore<ApplicationUser> implementation.

        Task<DateTimeOffset?> GetLockoutEndDateAsync(BL.Models.User user, CancellationToken cancellationToken);

        Task SetLockoutEndDateAsync(BL.Models.User user, DateTimeOffset? lockoutEnd,
           CancellationToken cancellationToken);

        Task<int> IncrementAccessFailedCountAsync(BL.Models.User user, CancellationToken cancellationToken);

        Task ResetAccessFailedCountAsync(BL.Models.User user, CancellationToken cancellationToken);

        Task<int> GetAccessFailedCountAsync(BL.Models.User user, CancellationToken cancellationToken);

        Task<bool> GetLockoutEnabledAsync(BL.Models.User user, CancellationToken cancellationToken);

        Task SetLockoutEnabledAsync(BL.Models.User user, bool enabled, CancellationToken cancellationToken);
        #endregion IUserLockoutStore<ApplicationUser> implementation.

        #region IUserRoleStore<ApplicationUser> implementation.

        Task AddToRoleAsync(BL.Models.User user, string roleName, CancellationToken cancellationToken);

        Task RemoveFromRoleAsync(BL.Models.User user, string roleName, CancellationToken cancellationToken);

        Task<IList<string>> GetRolesAsync(BL.Models.User user, CancellationToken cancellationToken);

        Task<bool> IsInRoleAsync(BL.Models.User user, string roleName,
           CancellationToken cancellationToken);

        Task<IList<BL.Models.User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken);

        #endregion IUserRoleStore<ApplicationUser> implementation.
    }
}