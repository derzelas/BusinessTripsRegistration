using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTrips.DAL.Model
{
    public class BusinessTripFilter
    {
        public string ClientName { get; set; }
        public string Location { get; set; }
        public string MeanOfTransportation { get; set; }
        public string Accommodation { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
    }
}
