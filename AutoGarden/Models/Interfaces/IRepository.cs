namespace AutoGarden.Models.Interfaces
{
    using System.Collections.Generic;

    using Utility;

    public interface IRepository<T>
    {
        void Delete(int id);

        IEnumerable<T> GetAll();

        Maybe<T> GetOne(int id);

        void Update(T value);

        int Add(Schedule.Schedule schedule);
    }
}