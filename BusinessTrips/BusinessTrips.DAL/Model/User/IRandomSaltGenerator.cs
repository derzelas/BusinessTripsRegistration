namespace BusinessTrips.DAL.Model.User
{
    public interface IRandomSaltGenerator
    {
        string GetSalt();
    }
}