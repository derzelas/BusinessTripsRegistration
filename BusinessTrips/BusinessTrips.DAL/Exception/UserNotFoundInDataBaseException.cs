namespace BusinessTrips.DAL.Exception
{
    public class UserNotFoundException : System.Exception
    {
        public UserNotFoundException()
            : base("User Not Found")
        {
        }
    }
}
