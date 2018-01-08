namespace Sirius.DictionaryApi
{
    /// <summary>
    /// Options of dictionary api.
    /// You can configure this class using DI from appsettings like this:
    /// services.Configure&lt;DictionaryApiOptions&gt;(Configuration.GetSection("DictionaryApi"));
    /// </summary>
    public class DictionaryApiOptions
    {
        public string BaseUrl { get; set; }

        public string ApiKey { get; set; }
    }
}
