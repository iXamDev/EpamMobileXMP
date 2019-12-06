using System;
using ExpressMapper.Extensions;
using Realms;
using XMP.Core.Database.Abstract;
namespace XMP.Core.Mapping
{
    public class ExpressMapperRealmRepositoryEntriesMapper<T, TDto> : IRealmRepositoryEntriesMapper<T, TDto>
        where T : class
        where TDto : RealmObject
    {
        public TDto CreateDtoForItem(Realm realm, T item)
        => item.Map<T, TDto>();

        public TDto[] CreateDtosForItems(Realm realm, T[] items)
        => items.Map<T[], TDto[]>();

        public T CreateItemForDto(TDto dto)
        => dto.Map<TDto, T>();

        public T[] CreateItemsForDtos(TDto[] dtos)
        => dtos.Map<TDto[], T[]>();

        public void Update(TDto dto, T newData)
        => newData.Map<T, TDto>(dto);
    }
}
