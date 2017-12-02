using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BL.Interfaces;
using DL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BL.Stores {
    public class UserStore : IQueryableUserStore<BL.Models.User>, IUserEmailStore<BL.Models.User>, IUserLoginStore<BL.Models.User>, IUserPasswordStore<BL.Models.User>,
        IUserPhoneNumberStore<BL.Models.User>, IUserTwoFactorStore<BL.Models.User>, IUserSecurityStampStore<BL.Models.User>,
        IUserClaimStore<BL.Models.User>, IUserLockoutStore<BL.Models.User>, IUserRoleStore<BL.Models.User>, IUserStore, IUserStore<BL.Models.User> {
        private readonly IUsersRepository _usersRepository;
        private readonly IUsersRolesRepository _usersRolesRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly IUsersClaimsRepository _usersClaimsRepository;
        private readonly IUsersLoginsRepository _usersLoginsRepository;
        private readonly IMapper _mapper;

        public UserStore(IUsersRepository usersRepository,
                IUsersRolesRepository usersRolesRepository,
                IRolesRepository rolesRepository,
                IUsersClaimsRepository usersClaimsRepository,
                IUsersLoginsRepository usersLoginsRepository,
                IMapper mapper) {
            _usersRepository = usersRepository;
            _usersRolesRepository = usersRolesRepository;
            _rolesRepository = rolesRepository;
            _usersClaimsRepository = usersClaimsRepository;
            _usersLoginsRepository = usersLoginsRepository;
            _mapper = mapper;
        }

        public IQueryable<BL.Models.User> Users => _mapper.Map<IQueryable<BL.Models.User>>(Task.Run(() => _usersRepository.GetAllUsers().Result.AsQueryable()));

        #region IUserStore<ApplicationUser> implementation.

        IQueryable<BL.Models.User> IUserStore.Users() {
            return Users;
        }

        public Task<IdentityResult> CreateAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            return _usersRepository.CreateAsync(_mapper.Map<DL.Models.User>(user), cancellationToken);
        }

        public Task<IdentityResult> DeleteAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            return _usersRepository.DeleteAsync(_mapper.Map<DL.Models.User>(user), cancellationToken);
        }

        public Task<BL.Models.User> FindByIdAsync(string userId, CancellationToken cancellationToken) {
            cancellationToken.ThrowIfCancellationRequested();
            return _mapper.Map<Task<BL.Models.User>>(_usersRepository.FindByIdAsync(userId));
        }

        public Task<BL.Models.User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken) {
            if (string.IsNullOrEmpty(normalizedUserName)) {
                throw new ArgumentNullException(nameof(normalizedUserName), "Parameter normalizedUserName cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return _mapper.Map<Task<BL.Models.User>>(_usersRepository.FindByNameAsync(normalizedUserName));
        }

        public Task<string> GetNormalizedUserNameAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(BL.Models.User user, string normalizedName, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            if (string.IsNullOrEmpty(normalizedName)) {
                throw new ArgumentNullException(nameof(normalizedName), "Parameter normalizedName cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.NormalizedUserName = normalizedName;
            return Task.FromResult<object>(null);
        }

        public Task SetUserNameAsync(BL.Models.User user, string userName, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            if (string.IsNullOrEmpty(userName)) {
                throw new ArgumentNullException(nameof(userName), "Parameter userName cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.UserName = userName;
            return Task.FromResult<object>(null);
        }

        public Task<IdentityResult> UpdateAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            return _usersRepository.UpdateAsync(_mapper.Map<DL.Models.User>(user), cancellationToken);
        }

        public void Dispose() {
            _usersRepository.Dispose();
        }
        #endregion IUserStore<ApplicationUser> implementation.

        #region IUserEmailStore<ApplicationUser> implementation.
        public Task SetEmailAsync(BL.Models.User user, string email, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            if (string.IsNullOrEmpty(email)) {
                throw new ArgumentNullException(nameof(email), "Parameter email cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.Email = email;
            return Task.FromResult<object>(null);
        }

        public Task<string> GetEmailAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(BL.Models.User user, bool confirmed, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.EmailConfirmed = confirmed;
            return Task.FromResult<object>(null);
        }

        public Task<BL.Models.User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken) {
            if (string.IsNullOrEmpty(normalizedEmail)) {
                throw new ArgumentNullException(nameof(normalizedEmail), "Parameter normalizedEmail cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return _mapper.Map<Task<BL.Models.User>>(_usersRepository.FindByEmailAsync(normalizedEmail));
        }

        public Task<string> GetNormalizedEmailAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(BL.Models.User user, string normalizedEmail, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            if (string.IsNullOrEmpty(normalizedEmail)) {
                throw new ArgumentNullException(nameof(normalizedEmail), "Parameter normalizedEmail cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult<object>(null);
        }
        #endregion IUserEmailStore<ApplicationUser> implementation.

        #region IUserLoginStore<ApplicationUser> implementation.
        public Task AddLoginAsync(BL.Models.User user, UserLoginInfo login, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            if (login == null) {
                throw new ArgumentNullException(nameof(login), "Parameter login is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return _usersLoginsRepository.AddLoginAsync(_mapper.Map<DL.Models.User>(user), login);
        }

        public Task RemoveLoginAsync(BL.Models.User user, string loginProvider, string providerKey, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            if (string.IsNullOrEmpty(loginProvider)) {
                throw new ArgumentNullException(nameof(loginProvider), "Parameter loginProvider and providerKey cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(providerKey)) {
                throw new ArgumentNullException(nameof(providerKey), "Parameter providerKey and providerKey cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return _usersLoginsRepository.RemoveLoginAsync(_mapper.Map<DL.Models.User>(user), loginProvider, providerKey);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return _usersLoginsRepository.GetLoginsAsync(_mapper.Map<DL.Models.User>(user), cancellationToken);
        }

        public Task<BL.Models.User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken) {
            if (string.IsNullOrEmpty(loginProvider)) {
                throw new ArgumentNullException(nameof(loginProvider), "Parameter loginProvider cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(providerKey)) {
                throw new ArgumentNullException(nameof(providerKey), "Parameter providerKey cannot be null or empty.");
            }

            return _mapper.Map<Task<BL.Models.User>>(_usersLoginsRepository.FindByLoginAsync(loginProvider, providerKey, cancellationToken));
        }
        #endregion IUserLoginStore<ApplicationUser> implementation.

        #region IUserPasswordStore<ApplicationUser> implementation.
        public Task SetPasswordHashAsync(BL.Models.User user, string passwordHash, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            if (string.IsNullOrEmpty(passwordHash)) {
                throw new ArgumentNullException(nameof(passwordHash), "Parameter passwordHash cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.PasswordHash = passwordHash;
            return Task.FromResult<object>(null);
        }

        public Task<string> GetPasswordHashAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }
        #endregion IUserPasswordStore<ApplicationUser> implementation.

        #region IUserPhoneNumberStore<ApplicationUser> implementation.
        public Task SetPhoneNumberAsync(BL.Models.User user, string phoneNumber, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.PhoneNumber = phoneNumber;
            return Task.FromResult<object>(null);
        }

        public Task<string> GetPhoneNumberAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(BL.Models.User user, bool confirmed, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult<object>(null);
        }
        #endregion IUserPhoneNumberStore<ApplicationUser> implementation.

        #region IUserTwoFactorStore<ApplicationUser> implementation.
        public Task SetTwoFactorEnabledAsync(BL.Models.User user, bool enabled, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.TwoFactorEnabled = enabled;
            return Task.FromResult<object>(null);
        }

        public Task<bool> GetTwoFactorEnabledAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.TwoFactorEnabled);
        }
        #endregion IUserTwoFactorStore<ApplicationUser> implementation.

        #region IUserSecurityStampStore<ApplicationUser> implementation.
        public Task SetSecurityStampAsync(BL.Models.User user, string stamp, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.SecurityStamp = stamp;
            return Task.FromResult<object>(null);
        }

        public Task<string> GetSecurityStampAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.SecurityStamp);
        }
        #endregion IUserSecurityStampStore<ApplicationUser> implementation.

        #region IUserClaimStore<ApplicationUser> implementation.
        public Task<IList<Claim>> GetClaimsAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            return _usersClaimsRepository.GetClaimsAsync(_mapper.Map<DL.Models.User>(user), cancellationToken);
        }

        public Task AddClaimsAsync(BL.Models.User user, IEnumerable<Claim> claims, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            if (claims == null) {
                throw new ArgumentNullException(nameof(claims), "Parameter claims is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return _usersClaimsRepository.AddClaimsAsync(_mapper.Map<DL.Models.User>(user), claims);
        }

        public Task ReplaceClaimAsync(BL.Models.User user, Claim claim, Claim newClaim, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            if (claim == null) {
                throw new ArgumentNullException(nameof(claim), "Parameter claim is not set to an instance of an object.");
            }

            if (newClaim == null) {
                throw new ArgumentNullException(nameof(newClaim), "Parameter newClaim is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return _usersClaimsRepository.ReplaceClaimAsync(_mapper.Map<DL.Models.User>(user), claim, newClaim);
        }

        public Task RemoveClaimsAsync(BL.Models.User user, IEnumerable<Claim> claims, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        public Task<IList<BL.Models.User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
        #endregion IUserClaimStore<ApplicationUser> 

        #region IUserLockoutStore<ApplicationUser> implementation.
        public Task<DateTimeOffset?> GetLockoutEndDateAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.LockoutEndDateTimeUtc.HasValue
                ? new DateTimeOffset?(DateTime.SpecifyKind(user.LockoutEndDateTimeUtc.Value, DateTimeKind.Utc))
                : null);
        }

        public Task SetLockoutEndDateAsync(BL.Models.User user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.LockoutEndDateTimeUtc = lockoutEnd?.UtcDateTime;
            return Task.FromResult<object>(null);
        }

        public Task<int> IncrementAccessFailedCountAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.AccessFailedCount = 0;
            return Task.FromResult<object>(null);
        }

        public Task<int> GetAccessFailedCountAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.LockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(BL.Models.User user, bool enabled, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.LockoutEnabled = enabled;
            return Task.FromResult<object>(null);
        }
        #endregion IUserLockoutStore<ApplicationUser> implementation.

        #region IUserRoleStore<ApplicationUser> implementation.
        public Task AddToRoleAsync(BL.Models.User user, string roleName, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            if (string.IsNullOrEmpty(roleName)) {
                throw new ArgumentNullException(nameof(roleName), "Parameter roleName is not set to an instance of an object.");
            }

            var role = Task.Run(() => _rolesRepository.GetAllRoles(), cancellationToken).Result.SingleOrDefault(e => e.NormalizedName == roleName);

            return role != null
                ? _usersRolesRepository.AddToRoleAsync(_mapper.Map<DL.Models.User>(user), role.Id)
                : Task.FromResult<object>(null);
        }

        public Task RemoveFromRoleAsync(BL.Models.User user, string roleName, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            var role = Task.Run(() => _rolesRepository.GetAllRoles(), cancellationToken).Result.SingleOrDefault(e => e.NormalizedName == roleName);

            return role != null
                ? _usersRolesRepository.RemoveFromRoleAsync(_mapper.Map<DL.Models.User>(user), role.Id)
                : Task.FromResult<object>(null);
        }

        public Task<IList<string>> GetRolesAsync(BL.Models.User user, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            return _usersRolesRepository.GetRolesAsync(_mapper.Map<DL.Models.User>(user), cancellationToken);
        }

        public async Task<bool> IsInRoleAsync(BL.Models.User user, string roleName, CancellationToken cancellationToken) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            if (string.IsNullOrEmpty(roleName)) {
                return false;
            }

            var userRoles = await GetRolesAsync(user, cancellationToken);
            return userRoles.Contains(roleName);
        }

        public Task<IList<BL.Models.User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
        #endregion IUserRoleStore<ApplicationUser> implementation.
    }
}