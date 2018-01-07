using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Sirius.DictionaryApi.Exceptions;
using Sirius.DictionaryApi.Models.Request;
using Sirius.DictionaryApi.Models.Response;

namespace Sirius.DictionaryApi
{
    /// <summary>
    /// Client to access Dictionary API
    /// </summary>
    public interface IDictApiClient
    {
        /// <summary>
        /// Searches for a word or phrase in the dictionary and returns an automatically generated dictionary entry.
        /// </summary>
        /// <param name="apiUrl">Url to the API (example: https://dictionary.yandex.net/api/v1/dicservice.json/lookup)</param>
        /// <param name="apiKey">API key</param>
        /// <param name="options">Lookup options</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Dictionary entry</returns>
        /// <exception cref="SiriusDictApiException">Dictionary api exception</exception>
        Task<DictApiResponse> LookupAsync([NotNull] string apiUrl, [NotNull] string apiKey,
            [NotNull] DictApiRequestOptions options,
            CancellationToken cancellationToken);
    }
}
