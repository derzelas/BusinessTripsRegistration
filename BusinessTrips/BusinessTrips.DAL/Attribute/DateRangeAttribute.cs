using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessTrips.DAL.Model
{
    public class DateRangeAttribute : RangeAttribute
    {
        public DateRangeAttribute()
            : base(typeof(DateTime), DateTime.Now.AddYears(-1).ToString(), DateTime.Now.AddYears(20).ToString())
        { }
    }
}
