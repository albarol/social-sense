namespace SocialSense.Shared
{
    using System;

    public struct Period
    {
        private const int TotalSecondsInDay = 86399;

        public Period(DateTime begin, DateTime end)
            : this()
        {
            this.Begin = begin;
            this.End = end;
        }

        public static Period Today
        {
            get
            {
                return new Period(DateTime.Today, DateTime.Today.AddSeconds(TotalSecondsInDay));
            }
        }

        public static Period Week
        {
            get
            {
                return new Period(DateTime.Today.AddDays(-7), DateTime.Today.AddSeconds(TotalSecondsInDay));
            }
        }

        public static Period Month
        {
            get
            {
                return new Period(DateTime.Today.AddMonths(-1), DateTime.Today.AddSeconds(TotalSecondsInDay));
            }
        }

        public DateTime Begin { get; private set; }
        public DateTime End { get; private set; }
    }
}
