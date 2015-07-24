using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BusinessTrips.DAL.Attributes
{
    public class DateRangeAttribute : RangeAttribute
    {
        public DateRangeAttribute()
            : base(typeof(DateTime), 
            DateTime.Now.AddYears(-1).ToString(CultureInfo.InvariantCulture), 
            DateTime.Now.AddYears(20).ToString(CultureInfo.InvariantCulture))
        {
        }
    }
}
