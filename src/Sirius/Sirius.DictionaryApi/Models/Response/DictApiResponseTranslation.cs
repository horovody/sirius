using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Sirius.DictionaryApi.Models.Response
{
    /// <summary>
    /// Translation of Dictionaty Api entry
    /// </summary>
    public class DictApiResponseTranslation: DictApiResponseElementBase
    {
        /// <summary>
        /// Synonyms
        /// </summary>
        [DataMember(Name = "syn")]
        public ICollection<DictApiResponseSynonym> Synonyms { get; set; } = new List<DictApiResponseSynonym>();

        /// <summary>
        /// Meanings
        /// </summary>
        [DataMember(Name = "mean")]
        public ICollection<DictApiResponseMean> Meanings { get; set; } = new List<DictApiResponseMean>();

        /// <summary>
        /// Examples
        /// </summary>
        [DataMember(Name = "ex")]
        public ICollection<DictApiResponseExample> Examples { get; set; } = new List<DictApiResponseExample>();
    }
}
