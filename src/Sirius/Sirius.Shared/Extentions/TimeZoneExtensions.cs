using System;

namespace Sirius.Shared.Extentions
{
    public static class TimeZoneExtensions
    {
        /// <summary>
        /// Системное имя часового пояса, используемого по умолчанию (если пользователь не установил 
        /// своё значение часового пояса с помощью метода <see cref="SetTimeZone"/>).
        /// </summary>
        public const string DefaultTimeZone = "Russian Standard Time";

        /// <summary>
        /// Значение часового пояса для текущего потока. Напрямую не обращаться!
        /// </summary>
        [ThreadStatic]
        public static TimeZoneInfo TimeZone;

        /// <summary>
        /// Получить часовой пояс для текущего потока (Thread).
        /// </summary>
        /// <returns></returns>
        public static TimeZoneInfo GetTimeZone()
        {
            return TimeZone ?? (TimeZone = TimeZoneInfo.FindSystemTimeZoneById(DefaultTimeZone));
        }

        /// <summary>
        /// Задать часовой пояс для текущего потока (Thread).
        /// </summary>
        /// <param name="timeZone"></param>
        public static void SetTimeZone(string timeZone)
        {
            TimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone ?? DefaultTimeZone);
        }

        /// <summary>
        /// Преобразовывает дату/время с любым смещением в дату/время со смещением в текущем часовом поясе пользователя.
        /// </summary>
        /// <param name="dt">Дата и время со смещением.</param>
        /// <returns>Дата и время со смещением, равным смещению времени в текущем часовом поясе пользователя.</returns>
        public static DateTimeOffset ToCurrentTimeZone(this DateTimeOffset dt)
        {
            return TimeZoneInfo.ConvertTime(dt, GetTimeZone());
        }

        public static DateTimeOffset? ToCurrentTimeZone(this DateTimeOffset? dt)
        {
            return dt.HasValue ? ToCurrentTimeZone(dt.Value) : (DateTimeOffset?)null;
        }

        /// <summary>
        /// Создаёт дату/время в текущем часовом поясе пользователя на основе переданных
        /// даты и времени без учёта значения параметра <see cref="DateTime.Kind" />.
        /// </summary>
        /// <param name="dt">Дата и время без смещения времени и учёта часовой зоны.</param>
        /// <returns>Дата и время со смещением на основе текущего часового пояса пользователя.</returns>
        public static DateTimeOffset AsCurrentTimeZone(this DateTime dt)
        {
            return new DateTimeOffset(dt.Year, dt.Month, dt.Day,
                dt.Hour, dt.Minute, dt.Second, dt.Millisecond,
                GetTimeZone().BaseUtcOffset);
        }
    }
}
