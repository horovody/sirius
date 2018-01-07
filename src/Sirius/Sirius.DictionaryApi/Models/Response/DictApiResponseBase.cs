using System.Runtime.Serialization;

namespace Sirius.DictionaryApi.Models.Response
{
    /// <summary>
    /// Base class for Dictionary API Response elements
    /// </summary>
    public abstract class DictApiResponseElementBase
    {
        /// <summary>
        /// Text of the element
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Part of speech (may be omitted)
        /// </summary>
        [DataMember(Name = "pos")]
        public string PartOfSpeech { get; set; }

        /// <summary>
        /// Transcription (may be omitted)
        /// </summary>
        [DataMember(Name = "ts")]
        public string Transcription { get; set; }

        /// <summary>
        /// Gender (may be omitted)
        /// </summary>
        [DataMember(Name = "gen")]
        public string Gender { get; set; }

        /// <summary>
        /// Verb tense
        /// </summary>
        [DataMember(Name = "asp")]
        public string Asp { get; set; }
    }
}
