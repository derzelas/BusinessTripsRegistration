namespace BusinessTrips.DataAccessLayer
{
    public interface IStorage<T>
    {
        void Add(T element);

        T Get(T element);

        void Update(T element);

        void Remove(T element);

        void Commit();
    }
}