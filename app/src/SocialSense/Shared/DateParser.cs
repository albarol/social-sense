namespace SocialSense.Shared
{
    using System;
    using System.Text.RegularExpressions;

    public class DateParser
    {
        private const string ValidUnits = "year|month|week|day|hour|minute|second";
        private static readonly Regex BasicRelativeRegex = new Regex(@"^(last|next) +(" + ValidUnits + ")$");
        private static readonly Regex SimpleRelativeRegex = new Regex(@"^([+-]?\d+) *(" + ValidUnits + ")s?$");
        private static readonly Regex CompleteRelativeRegex = new Regex(@"^(?: *(\d) *(" + ValidUnits + ")s?)+( +ago)?$");

        public static DateTime FromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }


        public static double ToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        public static DateTime Parse(string input)
        {
            input = input.Trim().ToLower();

            var result = TryParseCommonDateTime(input);
            if (result.HasValue)
            {
                return result.Value;
            }

            result = TryParseLastOrNextCommonDateTime(input);
            if (result.HasValue)
            {
                return result.Value;
            }

            result = TryParseSimpleRelativeDateTime(input);
            if (result.HasValue)
            {
                return result.Value;
            }

            result = TryParseCompleteRelativeDateTime(input);
            if (result.HasValue)
            {
                return result.Value;
            }

            return DateTime.Parse(input);
        }

        private static DateTime? TryParseCommonDateTime(string input)
        {
            switch (input)
            {
                case "now":
                    return DateTime.Now;
                case "today":
                    return DateTime.Today;
                case "tomorrow":
                    return DateTime.Today.AddDays(1);
                case "yesterday":
                    return DateTime.Today.AddDays(-1);
                default:
                    return null;
            }
        }

        private static DateTime? TryParseLastOrNextCommonDateTime(string input)
        {
            var match = BasicRelativeRegex.Match(input);
            if (!match.Success)
            {
                return null;
            }

            var unit = match.Groups[2].Value;
            var sign = string.Compare(match.Groups[1].Value, "next", StringComparison.OrdinalIgnoreCase) == 0 ? 1 : -1;
            return AddOffset(unit, sign);
        }

        private static DateTime? TryParseSimpleRelativeDateTime(string input)
        {
            var match = SimpleRelativeRegex.Match(input);
            if (!match.Success)
            {
                return null;
            }

            var delta = Convert.ToInt32(match.Groups[1].Value);
            var unit = match.Groups[2].Value;
            return AddOffset(unit, delta);
        }

        private static DateTime? TryParseCompleteRelativeDateTime(string input)
        {
            var match = CompleteRelativeRegex.Match(input);
            if (!match.Success)
            {
                return null;
            }

            var values = match.Groups[1].Captures;
            var units = match.Groups[2].Captures;
            var sign = match.Groups[3].Success ? -1 : 1;
            
            if (values.Count != units.Count)
            {
                throw new Exception();
            }

            var dateTime = UnitIncludeTime(units) ? DateTime.Now : DateTime.Today;

            for (int i = 0; i < values.Count; ++i)
            {
                var value = sign * Convert.ToInt32(values[i].Value);
                var unit = units[i].Value;

                dateTime = AddOffset(unit, value, dateTime);
            }

            return dateTime;
        }

        private static DateTime AddOffset(string unit, int value, DateTime dateTime)
        {
            switch (unit)
            {
                case "year":
                    return dateTime.AddYears(value);
                case "month":
                    return dateTime.AddMonths(value);
                case "week":
                    return dateTime.AddDays(value * 7);
                case "day":
                    return dateTime.AddDays(value);
                case "hour":
                    return dateTime.AddHours(value);
                case "minute":
                    return dateTime.AddMinutes(value);
                case "second":
                    return dateTime.AddSeconds(value);
                default:
                    throw new Exception("Internal error: Unhandled relative date/time case.");
            }
        }

        private static DateTime AddOffset(string unit, int value)
        {
            var now = UnitIncludesTime(unit) ? DateTime.Now : DateTime.Today;
            return AddOffset(unit, value, now);
        }

        private static bool UnitIncludeTime(CaptureCollection units)
        {
            foreach (Capture unit in units)
            {
                if (UnitIncludesTime(unit.Value))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool UnitIncludesTime(string unit)
        {
            switch (unit)
            {
                case "hour":
                case "minute":
                case "second":
                    return true;

                default:
                    return false;
            }
        }
    }
}