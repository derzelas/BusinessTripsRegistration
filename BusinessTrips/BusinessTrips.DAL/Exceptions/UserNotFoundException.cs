namespace BusinessTrips.DAL.Exceptions
{
    public class UserNotFoundException : System.Exception
    {
        public UserNotFoundException()
            : base("User Not Found")
        {
        }
    }
}
