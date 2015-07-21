namespace BusinessTrips.DAL.Model.User
{
    public interface IRandomStringGenerator
    {
        string GetSalt();
    }
}