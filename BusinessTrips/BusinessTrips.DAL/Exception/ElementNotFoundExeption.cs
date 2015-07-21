namespace BusinessTrips.DAL.Exception
{
    public class UserNotFoundInDataBaseException : System.Exception
    {
        public UserNotFoundInDataBaseException()
            : base("User Not Found In DataBase")
        {
        }
    }
}
