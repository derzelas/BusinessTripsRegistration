namespace BusinessTrips.DAL.Exceptions
{
    public class BusinessTripNotFoundException : System.Exception
    {
        public BusinessTripNotFoundException()
            : base("Business Trip Not Found")
        {
        }
    }
}
