using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DLAuth.Infrastructure
{
    public class UserManager : UserManager<BL.Models.User>
    {
        public UserManager(IUserStore<BL.Models.User> store, 
            IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<BL.Models.User> passwordHasher,
            IEnumerable<IUserValidator<BL.Models.User>> userValidators, 
            IEnumerable<IPasswordValidator<BL.Models.User>> passwordValidators,
            ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, 
            IServiceProvider services, 
            ILogger<UserManager<BL.Models.User>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
}
