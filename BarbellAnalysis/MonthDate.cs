using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace BarbellAnalysis
{
    public class MonthDate : ValueObject, IComparable<MonthDate>
    {
        private readonly DateTime dateTime;

        private MonthDate(DateTime dateTime)
        {
            this.dateTime = dateTime;
        }

        public MonthDate(int year, int month) : this(new DateTime(year, month, 1))
        {
        }

        public static MonthDate From(DateTime date)
        {
            return new MonthDate(new DateTime(date.Year, date.Month, 1));
        }

        public int Month => dateTime.Month;
        public int Year => dateTime.Year;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Month;
            yield return Year;
        }

        public MonthDate Next() => From(dateTime.AddMonths(1));

        public override string ToString() => dateTime.ToString("MM/yyyy");

        public int CompareTo(MonthDate other)
        {
            return dateTime.CompareTo(other.dateTime);
        }

        public static bool operator ==(MonthDate left, MonthDate right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        public static bool operator !=(MonthDate left, MonthDate right)
        {
            return !(left == right);
        }

        public static bool operator <(MonthDate left, MonthDate right)
        {
            return left is null ? right is object : left.CompareTo(right) < 0;
        }

        public static bool operator <=(MonthDate left, MonthDate right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        public static bool operator >(MonthDate left, MonthDate right)
        {
            return left is object && left.CompareTo(right) > 0;
        }

        public static bool operator >=(MonthDate left, MonthDate right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }
    }
}
