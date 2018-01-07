using System.Threading;
using System.Threading.Tasks;
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
            var data = await _dictApiClient.LookupAsync("https://dictionary.yandex.net/api/v1/dicservice.json/lookup",
                "dict.1.1.20180101T132215Z.c832ddf3204a676c.d4097f6130aa5842efeba7f2972b02197218a5c8",
                new DictApiRequestOptions("en-ru", text), cancellationToken);
            return Json(data);
        }
    }
}
