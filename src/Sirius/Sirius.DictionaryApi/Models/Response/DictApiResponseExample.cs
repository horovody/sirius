using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Sirius.DictionaryApi.Models.Response
{
    /// <summary>
    /// Usage example of Dictionary Api entry
    /// </summary>
    public class DictApiResponseExample: DictApiResponseElementBase
    {
        /// <summary>
        /// Translations of example
        /// </summary>
        [DataMember(Name = "tr")]
        public ICollection<DictApiResponseExampleTranslation> Translations { get; set; } = new List<DictApiResponseExampleTranslation>();
    }
}
