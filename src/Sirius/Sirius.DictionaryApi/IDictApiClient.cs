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
        /// <exception cref="SiriusDictApiExceededLimitException">Exceeded the daily limit on the number of requests.</exception>
        /// <exception cref="SiriusDictApiKeyBlockedException">The API key has been blocked.</exception>
        /// <exception cref="SiriusDictApiKeyInvalidException">Invalid API key.</exception>
        /// <exception cref="SiriusDictApiNotSupportedException">The specified translation direction is not supported.</exception>
        /// <exception cref="SiriusDictApiTextLongException">The text size exceeds the maximum.</exception>
        /// <exception cref="SiriusDictApiUnknownException">Unknown dictionary api exception</exception>
        Task<DictApiResponse> LookupAsync([NotNull] string apiUrl, [NotNull] string apiKey,
            [NotNull] DictApiRequestOptions options,
            CancellationToken cancellationToken);
    }
}
