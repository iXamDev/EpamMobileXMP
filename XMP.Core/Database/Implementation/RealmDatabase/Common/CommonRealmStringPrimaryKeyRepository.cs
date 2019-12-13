using System;
using Realms;
using XMP.Core.Database.Abstract;
using XMP.Core.Services.Abstract;

namespace XMP.Core.Database.Implementation.RealmDatabase.Common
{
    public class CommonRealmStringPrimaryKeyRepository<T, TDto> : CommonRealmRepository<T, TDto>, IDatabasePrimaryKeyRepository<T, string>
        where T : class
        where TDto : RealmObject
    {
        private readonly Func<T, string> _itemKeyResolver;

        public CommonRealmStringPrimaryKeyRepository(Func<T, string> itemKeyResolver, IRealmRepositoryEntriesMapper<T, TDto> realmRepositoryEntriesMapper, IRealmProvider realmProvider)
            : base(realmRepositoryEntriesMapper, realmProvider)
        {
            _itemKeyResolver = itemKeyResolver;
        }

        public T GetByKey(string key)
        {
            using (var realm = GetRealm())
            {
                var item = FindByKey(realm, key);

                return item != null ? RealmRepositoryEntriesMapper.CreateItemForDto(item) : null;
            }
        }

        public void RemoveByKey(string key)
        {
            using (var realm = GetRealm())
            {
                var item = FindByKey(realm, key);

                realm.Write(() => realm.Remove(item));
            }
        }

        protected TDto FindByKey(Realm realm, string key)
        => realm.Find<TDto>(key);

        protected override TDto FindDtoForItem(Realm realm, T item)
        => FindByKey(realm, _itemKeyResolver(item));
    }
}
