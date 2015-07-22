﻿namespace BusinessTrips.DAL.Exception
{
    public class BusinessTripNotFoundException : System.Exception
    {
        public BusinessTripNotFoundException()
            : base("Business Trip Not Found")
        {
        }
    }
}
