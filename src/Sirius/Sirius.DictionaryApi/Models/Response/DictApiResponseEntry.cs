using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Sirius.DictionaryApi.Models.Response
{
    /// <summary>
    /// Entry of Dictionary API response (the definition and translations are here)
    /// </summary>
    [DataContract(Name = "def")]
    public class DictApiResponseEntry: DictApiResponseElementBase
    {
        /// <summary>
        /// Entry translations
        /// </summary>
        [DataMember(Name = "tr")]
        public ICollection<DictApiResponseTranslation> Translations { get; set; } = new List<DictApiResponseTranslation>();
    }
}
