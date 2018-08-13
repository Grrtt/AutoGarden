namespace AutoGarden.Models.Interfaces
{
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        List<T> GetAll();
    }
}