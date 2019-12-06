using System;
using Realms;

namespace XMP.Core.Database.Abstract
{
    public interface IRealmRepositoryEntriesMapper<T, TDto>
        where T : class
        where TDto : RealmObject
    {
        TDto CreateDtoForItem(Realm realm, T item);

        TDto[] CreateDtosForItems(Realm realm, T[] items);

        T CreateItemForDto(TDto dto);

        T[] CreateItemsForDtos(TDto[] dtos);

        void Update(TDto dto, T newData);
    }
}
