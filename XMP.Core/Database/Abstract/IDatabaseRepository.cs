using System.Collections.Generic;

namespace XMP.Core.Database.Abstract
{
    public interface IDatabaseRepository<T>
    where T : class
    {
        IEnumerable<T> GetAll();

        void Add(T item);

        void AddRange(IEnumerable<T> items);

        void Remove(T item);

        void RemoveRange(IEnumerable<T> items);

        void RemoveAll();

        void Update(T item);
    }
}
