using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sirius.Data.Access.Auth;
using Sirius.Models.Account;
using Sirius.Shared;
using Sirius.Shared.Auth;
using Sirius.Shared.Constants;

namespace Sirius.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<AccountController> logger,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <returns>IdentityResult</returns>
        // POST: api/account
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] RegisterAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserEntity
                {
                    GivenName = model.GivenName,
                    AccessFailedCount = 0,
                    Email = model.UserName,
                    EmailConfirmed = false,
                    LockoutEnabled = true,
                    NormalizedEmail = model.UserName.ToUpper(),
                    NormalizedUserName = model.UserName.ToUpper(),
                    TwoFactorEnabled = false,
                    UserName = model.UserName
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await AddToRoleAsync(model.UserName, AuthRoles.User);
                    await AddClaimsAsync(model.UserName);
                    await _unitOfWork.SaveChangesAsync();
                }

                return new JsonResult(result);
            }
            return BadRequest("Model is not valid");
        }

        #region Helpers

        //TODO: this should not be here
        private async Task AddToRoleAsync(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await _userManager.AddToRoleAsync(user, roleName);
        }

        private async Task AddClaimsAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var claims = new List<Claim>
            {
                new Claim(type: SiriusClaimTypes.GivenName, value: user.GivenName),
            };
            await _userManager.AddClaimsAsync(user, claims);
        }

        #endregion
    }
}
