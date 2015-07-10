using System;
using System.Collections;
using System.Collections.Generic;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model
{
    class BusinessTripCollection:IEnumerable<BusinessTripModel>
    {
        private IEnumerable<BusinessTripModel> businessTripModels;

        public BusinessTripCollection()
        {
            businessTripModels = new List<BusinessTripModel>();
        }

        public IEnumerator<BusinessTripModel> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void LoadBusinessTripForUser(Guid userId)
        {
            BusinesTripsRepository businesTripsRepository=new BusinesTripsRepository();
            businessTripModels = businesTripsRepository.GetByUser(userId);
        }
    }
}
