using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DLAuth.Infrastructure {
    public class SignInManager : SignInManager<BL.Models.User> {
        public SignInManager(UserManager<BL.Models.User> userManager, 
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<BL.Models.User> claimsFactory, 
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<BL.Models.User>> logger, 
            IAuthenticationSchemeProvider schemeProvider)
            : base(userManager, contextAccessor, claimsFactory, 
                  optionsAccessor, logger, schemeProvider) {
        }
    }
}
