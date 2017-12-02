using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DLAuth.Infrastructure
{
    public class RoleManager : RoleManager<BL.Models.Role> {
        public RoleManager(IRoleStore<BL.Models.Role> store, 
            IEnumerable<IRoleValidator<BL.Models.Role>> roleValidators, 
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, 
            ILogger<RoleManager<BL.Models.Role>> logger)
            : base(store, roleValidators, keyNormalizer, errors, logger) {
        }
    }
}
