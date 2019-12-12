using System;

namespace XMP.Core.Database.Abstract
{
    public interface IDatabasePrimaryKeyRepository<TItem, TKey> : IDatabaseRepository<TItem>
        where TItem : class
    {
        TItem GetByKey(TKey key);

        void RemoveByKey(TKey key);
    }
}
