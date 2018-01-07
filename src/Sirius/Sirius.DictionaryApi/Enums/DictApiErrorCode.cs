namespace Sirius.DictionaryApi.Enums
{
    public enum DictApiErrorCode
    {
        KeyInvalid = 401,
        KeyBlocked = 402,
        LimitExceeded = 403,
        TextTooLong = 413,
        LangNotSupported = 501,

        Unknown = 500,
        Known = KeyInvalid & KeyBlocked & LimitExceeded & TextTooLong & LangNotSupported
    }
}
