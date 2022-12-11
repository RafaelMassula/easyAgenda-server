using EasyAgenda.Exceptions;
using EasyAgenda.Model.DTO;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.RegularExpressions;

namespace EasyAgenda.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static void CheckDatesLessCurrentDay(this IList<AgendaDTO> agendas)
        {
            for (int i = 0; i < agendas.Count; i++)
            {
                DateTime agendaDay = DateTime.SpecifyKind(agendas[i].Date, DateTimeKind.Local);
                if (agendaDay.Date < DateTime.Now.Date)
                {
                    throw new ScheduleException("There dates older than the current day.");
                }
            }
        }
        public static void CheckConflictingSchedules(this IList<AgendaDTO> agendas)
        {
            if (!agendas.Any())
                throw new ScheduleException("List of empty times.");

            for (int i = 1; i < agendas.Count; i++)
            {
                int l = i - 1;
                while (l >= 0)
                {
                    if (GetTimeSpan(agendas[i].Start) > GetTimeSpan(agendas[l].Start) &&
                         GetTimeSpan(agendas[i].Start) < GetTimeSpan(agendas[l].End))
                    {
                        throw new ScheduleException("There are conflicts between schedules.");
                    }
                    l--;
                }
            }
        }
        public static void SortAgenda(this IList<AgendaDTO> agendas)
        {
            if (!agendas.Any())
                return;

            for (int j = 1; j < agendas.Count; j++)
            {
                AgendaDTO chave = agendas[j];
                int i = j - 1;

                while (i >= 0 && agendas[i].Date > chave.Date)
                {
                    agendas[i + 1] = agendas[i];
                    i--;
                }
                agendas[i + 1] = chave;
            }

            for (int j = 1; j < agendas.Count; j++)
            {
                AgendaDTO chave = agendas[j];
                int i = j - 1;
                while (i >= 0 && GetTimeSpan(agendas[i].Start) > GetTimeSpan(chave.Start) &&
                agendas[i].Date == chave.Date)
                {
                    agendas[i + 1] = agendas[i];
                    i--;
                }
                agendas[i + 1] = chave;
            }
        }

        public static string TableName<T>(this T entity)
        {
            var type = entity?.GetType();

            var tableNameAttribute = type?.GetCustomAttributes<TableAttribute>().First();

            if (tableNameAttribute?.Name != null)
                return tableNameAttribute.Name;

            return "";

        }
        public static string RemoveMaskPhone(this string phone)
        {
            return Regex.Replace(phone, @"[A-z\W]", "");
        }
        private static TimeSpan GetTimeSpan(string time)
        {
            if (string.IsNullOrEmpty(time))
                throw new TimeoutException("Time is not valid!");

            string patternHour = @"^[0-9]{2}";
            string patternMinute = @"[0-9]{2}$";

            int hour = int.Parse(Regex.Match(time, patternHour).Value);
            int minute = int.Parse(Regex.Match(time, patternMinute).Value);

            return new TimeSpan(hour, minute, 0);
        }
    }
}
