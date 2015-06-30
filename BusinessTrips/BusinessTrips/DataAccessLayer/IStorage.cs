namespace BusinessTrips.DataAccessLayer
{
    public interface IStorage<T>
    {
        void Add(T element);

        T Get(T element);
    }
}