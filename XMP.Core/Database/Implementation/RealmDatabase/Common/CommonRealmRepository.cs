using System;
using System.Collections.Generic;
using System.Linq;
using ExpressMapper.Extensions;
using Realms;
using XMP.Core.Database.Abstract;
using XMP.Core.Services.Abstract;

namespace XMP.Core.Database.Implementation.RealmDatabase.Common
{
    public abstract class CommonRealmRepository<T, TDto> : IDatabaseRepository<T>
        where T : class
        where TDto : RealmObject
    {
        public CommonRealmRepository(IRealmRepositoryEntriesMapper<T, TDto> realmRepositoryEntriesMapper, IRealmProvider realmProvider)
        {
            RealmProvider = realmProvider;

            RealmRepositoryEntriesMapper = realmRepositoryEntriesMapper;
        }

        protected IRealmProvider RealmProvider { get; }

        protected IRealmRepositoryEntriesMapper<T, TDto> RealmRepositoryEntriesMapper { get; }

        protected abstract TDto FindDtoForItem(Realm realm, T item);

        protected Realm GetRealm()
        => RealmProvider.GetRealm();

        protected virtual void Update(TDto dto, T item)
        => RealmRepositoryEntriesMapper.Update(dto, item);

        protected virtual void AddEntry(Realm realm, TDto dto)
        => realm.Write(() => realm.Add(dto));

        protected virtual void AddEntries(Realm realm, TDto[] dtos)
        => realm.Write(() =>
        {
            foreach (var dto in dtos)
                realm.Add(dto);
        });

        public virtual void Add(T item)
        {
            using (var realm = GetRealm())
            {
                var dto = RealmRepositoryEntriesMapper.CreateDtoForItem(realm, item);

                if (dto != null)
                    AddEntry(realm, dto);
            }
        }

        public virtual void AddRange(IEnumerable<T> items)
        {
            using (var realm = GetRealm())
            {
                var dtos = RealmRepositoryEntriesMapper.CreateDtosForItems(realm, items.ToArray());

                if (dtos != null)
                    AddEntries(realm, dtos);
            }
        }

        public virtual IEnumerable<T> GetAll()
        {
            using (var realm = GetRealm())
            {
                var all = realm.All<TDto>().ToArray();

                return RealmRepositoryEntriesMapper.CreateItemsForDtos(all);
            }
        }

        public virtual void Remove(T item)
        {
            using (var realm = GetRealm())
            {
                var dto = FindDtoForItem(realm, item);

                if (dto != null)
                    realm.Write(() => realm.Remove(dto));
            }
        }

        public virtual void Update(T item)
        {
            using (var realm = GetRealm())
            {
                var dto = FindDtoForItem(realm, item);

                if (dto != null)
                    realm.Write(() => Update(dto, item));
            }
        }

        public virtual void RemoveRange(IEnumerable<T> items)
        {
            using (var realm = GetRealm())
            {
                var dtos = items?.Select((arg) => FindDtoForItem(realm, arg))?.Where(p => p != null)?.ToArray();

                if (dtos != null)
                    realm.Write(() =>
                    {
                        foreach (var dto in dtos)
                            realm.Remove(dto);
                    });
            }
        }

        public virtual void RemoveAll()
        {
            using (var realm = GetRealm())
            {
                realm.Write(() => realm.RemoveAll<TDto>());
            }
        }
    }
}
