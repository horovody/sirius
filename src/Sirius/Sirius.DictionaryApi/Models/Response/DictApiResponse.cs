using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Sirius.DictionaryApi.Models.Response
{
    /// <summary>
    /// Dictionary API response
    /// </summary>
    public class DictApiResponse
    {
        /// <summary>
        /// Definition entries
        /// </summary>
        [DataMember(Name = "def")]
        public ICollection<DictApiResponseEntry> Entries { get; set; } = new List<DictApiResponseEntry>();
    }
}
