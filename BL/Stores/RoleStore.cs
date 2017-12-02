using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BL.Interfaces;
using DL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BL.Stores {
    public class RoleStore : IQueryableRoleStore<BL.Models.Role>, IRoleStore {
        private readonly IRolesRepository _rolesRepository;
        private readonly IMapper _mapper;

        public RoleStore(IRolesRepository rolesRepository, IMapper mapper) {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }

        public IQueryable<BL.Models.Role> Roles => _mapper.Map<IQueryable<BL.Models.Role>>(Task.Run(() => _rolesRepository.GetAllRoles()).Result.AsQueryable());

        IQueryable<BL.Models.Role> IRoleStore.Roles() {
            return Roles;
        }

        public Task<IdentityResult> CreateAsync(BL.Models.Role role, CancellationToken cancellationToken) {
            if (role == null) {
                throw new ArgumentNullException(nameof(role), "Parameter role is not set to an instance of an object.");
            }

            return _rolesRepository.CreateAsync(_mapper.Map<DL.Models.Role>(role), cancellationToken);
        }

        public Task<IdentityResult> UpdateAsync(BL.Models.Role role, CancellationToken cancellationToken) {
            if (role == null) {
                throw new ArgumentNullException(nameof(role), "Parameter role is not set to an instance of an object.");
            }

            return _rolesRepository.UpdateAsync(_mapper.Map<DL.Models.Role>(role), cancellationToken);
        }

        public Task<IdentityResult> DeleteAsync(BL.Models.Role role, CancellationToken cancellationToken) {
            if (role == null) {
                throw new ArgumentNullException(nameof(role), "Parameter role is not set to an instance of an object.");
            }

            return _rolesRepository.DeleteAsync(_mapper.Map<DL.Models.Role>(role), cancellationToken);
        }

        public Task<string> GetRoleIdAsync(BL.Models.Role role, CancellationToken cancellationToken) {
            if (role == null) {
                throw new ArgumentNullException(nameof(role), "Parameter role is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(BL.Models.Role role, CancellationToken cancellationToken) {
            if (role == null) {
                throw new ArgumentNullException(nameof(role), "Parameter role is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(BL.Models.Role role, string roleName, CancellationToken cancellationToken) {
            if (role == null) {
                throw new ArgumentNullException(nameof(role), "Parameter role is not set to an instance of an object.");
            }

            if (string.IsNullOrEmpty(roleName)) {
                throw new ArgumentNullException(nameof(roleName), "Parameter roleName cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            role.Name = roleName;
            return Task.FromResult<object>(null);
        }

        public Task<string> GetNormalizedRoleNameAsync(BL.Models.Role role, CancellationToken cancellationToken) {
            if (role == null) {
                throw new ArgumentNullException(nameof(role), "Parameter role is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(BL.Models.Role role, string normalizedName, CancellationToken cancellationToken) {
            if (role == null) {
                throw new ArgumentNullException(nameof(role), "Parameter role is not set to an instance of an object.");
            }

            if (string.IsNullOrEmpty(normalizedName)) {
                throw new ArgumentNullException(nameof(normalizedName), "Parameter normalizedName cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            role.NormalizedName = normalizedName;
            return Task.FromResult<object>(null);
        }

        public Task<BL.Models.Role> FindByIdAsync(string roleId, CancellationToken cancellationToken) {
            if (string.IsNullOrEmpty(roleId)) {
                throw new ArgumentNullException(nameof(roleId), "Parameter roleId cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return _mapper.Map<Task<BL.Models.Role>>(_rolesRepository.FindByIdAsync(roleId));
        }

        public Task<BL.Models.Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken) {
            if (string.IsNullOrEmpty(normalizedRoleName)) {
                throw new ArgumentNullException(nameof(normalizedRoleName), "Parameter normalizedRoleName cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return _mapper.Map<Task<BL.Models.Role>>(_rolesRepository.FindByNameAsync(normalizedRoleName));
        }

        public void Dispose() {
            _rolesRepository.Dispose();
        }
    }
}