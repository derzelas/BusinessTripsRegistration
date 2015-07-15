using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessTrips.Models
{
    public class SearchResultViewModel
    {
        public DateTime StartingDate { get; set; }
        public string Location { get; set; }
        public string UserName { get; set; }
        public string MeanOfTransportation { get; set; }
        public string Accommodation { get; set; }

        public void ToSearchResultViewModel()
        {

        }
    }
}