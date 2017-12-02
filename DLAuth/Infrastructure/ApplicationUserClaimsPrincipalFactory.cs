using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DLAuth.Infrastructure {
    public class ApplicationUserClaimsPrincipalFactory : 
        UserClaimsPrincipalFactory<BL.Models.User, BL.Models.Role> {

        public ApplicationUserClaimsPrincipalFactory(UserManager<BL.Models.User> userManager, 
            RoleManager<BL.Models.Role> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor) {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(BL.Models.User user) {
            var principal = await base.CreateAsync(user);

            ((ClaimsIdentity)principal.Identity).AddClaims(new[]
            {
                new Claim(ClaimTypes.Uri, user.UserName)
            });

            return principal;
        }
    }
}
