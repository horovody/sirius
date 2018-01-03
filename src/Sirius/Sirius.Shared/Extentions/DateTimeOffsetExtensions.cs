using System;

namespace Sirius.Shared.Extentions
{
    /// <summary>
    /// Расширения для Даты/времени с часовым поясом (абсолютное смещение времени).
    /// </summary>
    public static class DateTimeOffsetExtensions
    {
        /// <summary>
        /// Изменить компоненту времени.
        /// </summary>
        /// <param name="dt">Исходная Дата/время.</param>
        /// <param name="ts">Новая компонента времени.</param>
        /// <returns>Новое Дата/время и изменённой компонентой времени.</returns>
        public static DateTimeOffset SetTime(this DateTimeOffset dt, TimeSpan ts)
        {
            return new DateTimeOffset(dt.Year, dt.Month, dt.Day,
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds, dt.Offset);
        }

        /// <summary>
        /// Изменить компоненту времени по-отдельности.
        /// </summary>
        /// <param name="dt">Исходная Дата/время.</param>
        /// <param name="hours">Часы. Если не указано или null, то остаётся как есть.</param>
        /// <param name="minutes">Минуты. Если не указано или null, то остаётся как есть.</param>
        /// <param name="seconds">Секунды. Если не указано или null, то остаётся как есть.</param>
        /// <returns>Новое Дата/время с изменённой компонентой времени.</returns>
        public static DateTimeOffset SetTime(this DateTimeOffset dt, int? hours = null, int? minutes = null, int? seconds = null)
        {
            return new DateTimeOffset(dt.Year, dt.Month, dt.Day,
                hours ?? dt.Hour, minutes ?? dt.Minute, seconds ?? dt.Second, 0, dt.Offset);
        }

        /// <summary>
        /// Извлечь локализованное для России значение дня недели (первый день недели — понедельник).
        /// </summary>
        /// <param name="dt">Исходная Дата/время.</param>
        /// <returns>День недели, начиная от нуля (0 == понедельник, 6 == воскресенье).</returns>
        public static int GetDayOfWeekFromMonday(this DateTimeOffset dt)
        {
            var w = (int)dt.DayOfWeek;
            if (w == 0) return 6;
            return w - 1;
        }
    }
}
