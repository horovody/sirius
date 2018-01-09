using Newtonsoft.Json;
using Sirius.Shared.Extentions;

namespace Sirius.Filters.Validation
{
    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        public string Message { get; }

        public ValidationError(string field, string message)
        {
            this.Field = field != string.Empty ? field.ToCamelCase() : null;
            this.Message = message;
        }
    }
}
