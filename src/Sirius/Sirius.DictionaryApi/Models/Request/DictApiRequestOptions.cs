using JetBrains.Annotations;

namespace Sirius.DictionaryApi.Models.Request
{
    /// <summary>
    /// Request model to Dictionary API
    /// </summary>
    public class DictApiRequestOptions
    {
        /// <summary>
        /// Create request options
        /// </summary>
        /// <param name="language">Translation direction</param>
        /// Translation direction
        /// <param name="text"></param>
        public DictApiRequestOptions([NotNull] string language,
            [NotNull] string text)
        {
            this.Language = language;
            this.Text = text;
        }

        /// <summary>
        /// Translation direction (for example, "en-ru"). Set as a pair of language codes 
        /// separated by a hyphen. For example, "en-ru" specifies to translate from 
        /// English to Russian.
        /// </summary>
        [NotNull]
        public string Language { get; set; }

        /// <summary>
        /// Translation direction
        /// </summary>
        [NotNull]
        public string Text { get; set; }

        /// <summary>
        /// Optional. The language of the user's interface for displaying names of parts of 
        /// speech in the dictionary entry.
        /// </summary>
        [CanBeNull]
        public string Ui { get; set; }

        /// <summary>
        /// Optional. Search options (bitmask of flags).
        /// </summary>
        [CanBeNull]
        public int? Flags { get; set; }
    }
}
