using System;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Sirius.DictionaryApi.Enums;
using Sirius.DictionaryApi.Exceptions;
using Sirius.DictionaryApi.Models.Request;
using Sirius.DictionaryApi.Models.Response;

namespace Sirius.DictionaryApi
{
    /// <inheritdoc />
    internal class DictApiClient: IDictApiClient
    {
        private readonly DictionaryApiOptions _apiOptions;

        public DictApiClient(IOptions<DictionaryApiOptions> apiOptions)
        {
            _apiOptions = apiOptions.Value;
        }

        /// <inheritdoc />
        public async Task<DictApiResponse> LookupAsync(string apiUrl, string apiKey, DictApiRequestOptions options, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(apiUrl))
            {
                throw new ArgumentNullException(nameof(apiKey));
            }
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var url =
                $"{apiUrl}?key={apiKey}&lang={options.Language}&text={options.Text}&ui={options.Ui}&flags={options.Flags}";

            try
            {
                return await this.LookupInternalAsync(url, cancellationToken);
            }
            catch (SiriusDictApiException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new SiriusDictApiException($"Lookup failed. Requested url is: {url}", e, DictApiErrorCode.Unknown);
            }
            
        }

        /// <inheritdoc />
        public async Task<DictApiResponse> LookupAsync(DictApiRequestOptions options, CancellationToken cancellationToken)
        {
            return await this.LookupAsync(_apiOptions.BaseUrl, _apiOptions.ApiKey, options, cancellationToken);
        }

        /// <summary>
        /// internal method for Dictionary lookup
        /// </summary>
        /// <param name="url">Lookuop url</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<DictApiResponse> LookupInternalAsync(string url, CancellationToken cancellationToken)
        {
            using (var httpClient = new HttpClient())
            {
                var serializer = new DataContractJsonSerializer(typeof(DictApiResponse));
                
                var data = await httpClient.GetAsync(url, cancellationToken);
                if (data.IsSuccessStatusCode)
                {
                    var streamData = await data.Content.ReadAsStreamAsync();
                    var response = serializer.ReadObject(streamData) as DictApiResponse;
                    return response;
                }
                else
                {
                    DictApiErrorCode code = (DictApiErrorCode)(int)data.StatusCode;
                    if (!typeof(DictApiErrorCode).IsEnumDefined(code))
                    {
                        code = DictApiErrorCode.Unknown;
                    }
                    var responseBody = await data.Content.ReadAsStringAsync();
                    throw new SiriusDictApiException($"Lookup failed. Requested url is: {url}", code, responseBody);
                }

            }
        }
    }
}
