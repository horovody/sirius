using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sirius.Data.Entities;
using Sirius.Logic;
using Sirius.Logic.Dtos;

namespace Sirius.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class SettingsController: Controller
    {
        private readonly ICrudDataService<SettingEntity, SettingDto> _settingDataService;

        public SettingsController(ICrudDataService<SettingEntity, SettingDto> settingDataService)
        {
            _settingDataService = settingDataService;
        }

        // GET api/settings
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _settingDataService.GetQuery().ToListAsync();
            return Ok(list);
        }
    }
}
