using System;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;
using Sirius.DictionaryApi.Enums;
using Sirius.DictionaryApi.Exceptions;
using Sirius.DictionaryApi.Models.Request;
using Sirius.DictionaryApi.Models.Response;
using Sirius.Shared;

namespace Sirius.DictionaryApi
{
    /// <inheritdoc />
    internal class DictApiClient: IDictApiClient
    {
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
                $"{apiUrl}?key={apiKey}&lang={options.Language}&text={options.Text}&ui={options.Ui}&flags={options.Flags}_11";

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
                throw new SiriusDictApiException($"The url is: {url}", e, DictApiErrorCode.KeyBlocked);
            }
            
        }

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
                    DictApiErrorCode code = DictApiErrorCode.Unknown;
                    if (((int) data.StatusCode & (int) DictApiErrorCode.Known) != 0)
                    {
                        code = (DictApiErrorCode)(int)data.StatusCode;
                    }

                    throw new SiriusDictApiException($"The url is: {url}", code); //TODO:+payload
                }

            }
        }
    }
}
