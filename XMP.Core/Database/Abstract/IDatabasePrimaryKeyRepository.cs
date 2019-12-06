using System;
namespace XMP.Core.Database.Abstract
{
    public interface IDatabasePrimaryKeyRepository<T, K> : IDatabaseRepository<T>
        where T : class
    {
        T GetByKey(K key);

        void RemoveByKey(K key);
    }
}
