using System;

namespace WebBar.Site.Models
{
    public class DateTimeInterval
    {
        public DateTimeInterval(DateTime dateStart, DateTime dateEnd)
        {
            DateStart = dateStart;
            DateEnd = dateEnd;
        }

        public DateTime DateStart { get; }
        public DateTime DateEnd { get; }
    }
}