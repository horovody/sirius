using System.Threading;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sirius.DictionaryApi;
using Sirius.DictionaryApi.Models.Request;

namespace Sirius.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "Access")]
    public class DictionaryController: Controller
    {
        private readonly IDictApiClient _dictApiClient;

        public DictionaryController(IDictApiClient dictApiClient)
        {
            _dictApiClient = dictApiClient;
        }

        [Route("lookup")]
        [HttpGet]
        public async Task<IActionResult> Lookup(string text, CancellationToken cancellationToken)
        {
            var data = await _dictApiClient.LookupAsync(new DictApiRequestOptions("en-ru", text), cancellationToken);
            return Json(data);
        }
    }
}
