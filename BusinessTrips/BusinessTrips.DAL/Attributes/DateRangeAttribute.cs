using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BusinessTrips.DAL.Attributes
{
    public class DateRangeAttribute : RangeAttribute
    {
        private const int NumberOfYears = 10;
        public DateRangeAttribute()
            : base(typeof(DateTime),
            DateTime.Now.AddYears(-NumberOfYears).ToString(CultureInfo.InvariantCulture),
            DateTime.Now.AddYears(NumberOfYears).ToString(CultureInfo.InvariantCulture))
        {
        }
    }
}
