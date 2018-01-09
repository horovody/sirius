using System.Threading;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sirius.Controllers.Base;
using Sirius.Data.Entities;
using Sirius.Logic;
using Sirius.Logic.Dtos;
using Sirius.Shared;

namespace Sirius.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "Access")]
    public class SettingsController: ApiCrudControllerBase<SettingEntity, SettingDto>
    {
        public SettingsController(ICrudDataService<SettingEntity, SettingDto> settingDataService,
            IUnitOfWork unitOfWork): base(settingDataService, unitOfWork)
        {
        }
    }
}
