using System.Text.RegularExpressions;

namespace Sirius.Shared.Utils
{
    /// <summary>
    ///     Справочник паттернов регулярных выражений и методы для проверки значений на соответствие регулярным выражениям.
    /// </summary>
    public static class RegexShared
    {
        /// <summary>
        ///     Адрес электронной почты, повторяющийся до 31 раза
        /// </summary>
        public const string EmailRepeated = @"^(\S+@\S+)(\s?;\s?\S+@\S+){0,31}$";

        /// <summary>
        ///     Проверить, является ли значение адресом электронной почты, повторяющийся до 31 раза
        /// </summary>
        /// <param name="value">
        ///     Значение для проверки. </param>
        /// <returns>
        ///     True, если значение подходит условию. </returns>
        public static bool IsEmailRepeated(string value)
        {
            return Regex.IsMatch(value, EmailRepeated);
        }

        /// <summary>
        ///     Адрес электронной почты (паттерн взят из реализации <see cref="EmailAddressAttribute"/>).
        /// </summary>
        public const string Email = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";

        /// <summary>
        ///     Проверить, является ли значение адресом электронной почты.
        /// </summary>
        /// <param name="value">
        ///     Значение для проверки. </param>
        /// <returns>
        ///     True, если значение подходит условию. </returns>
        public static bool IsEmail(string value)
        {
            return Regex.IsMatch(value, Email);
        }

        /// <summary>
        ///     Логин (email или номер телефона)
        /// </summary>
        public const string Login = @"^(((([+7]{2})|([8]{1}))[-\s\.]{0,1}[(]{0,1}[0-9]{3}[)]{0,1}[-\s\.]{0,1}[0-9]{3}[-\s\.]{0,1}[0-9]{2}[-\s\.]{0,1}[0-9]{2})|(((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?))$";

        /// <summary>
        ///     Проверить, подходит ли значение в качестве логина (email или номер телефона).
        /// </summary>
        /// <param name="value">
        ///     Значение для проверки. </param>
        /// <returns>
        ///     True, если значение подходит условию. </returns>
        public static bool IsLogin(string value)
        {
            return Regex.IsMatch(value, Login);
        }

        /// <summary>
        ///     Технический псевдоним, исользуется для доступа к ресурсам системы независимо
        ///     от их сквозных идентификаторов. Псевдонимы должны быть короткими, простыми
        ///     и не должны меняться на протяжение жизни системы.
        /// </summary>
        public const string Alias = @"[_\p{L}][_\p{L}\d]{0,127}";

        /// <summary>
        ///     Полное соответствие технического псевдонима для всей строки.
        /// </summary>
        public static readonly Regex AliasPattern = new Regex("^" + Alias + "$", RegexOptions.Compiled);

        /// <summary>
        ///     Проверить, подходит ли переданное значение в качестве технического псевдонима.
        /// </summary>
        /// <param name="value">
        ///     Значение для проверки. </param>
        /// <returns>
        ///     True, если значение соответствует условию проверки. </returns>
        public static bool IsAlias(string value)
        {
            return AliasPattern.IsMatch(value);
        }
    }
}
