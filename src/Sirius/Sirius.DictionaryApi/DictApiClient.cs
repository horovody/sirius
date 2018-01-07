using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;
using Sirius.DictionaryApi.Models.Request;
using Sirius.DictionaryApi.Models.Response;

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

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var serializer = new DataContractJsonSerializer(typeof(DictApiResponse));
                    var url =
                        $"{apiUrl}?key={apiKey}&lang={options.Language}&text={options.Text}&ui={options.Ui}&flags={options.Flags}";
                    var data = await httpClient.GetStringAsync(url);
                    
                    var streamData = await httpClient.GetStreamAsync(url);
                    var response = serializer.ReadObject(streamData) as DictApiResponse;
                    return response;
                }
            }
            catch (Exception e)
            {
                throw;
            }
            
        }
    }
}
