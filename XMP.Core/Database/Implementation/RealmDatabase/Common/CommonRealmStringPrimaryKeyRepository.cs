﻿using System;
using Realms;
using XMP.Core.Database.Abstract;
using XMP.Core.Services.Abstract;

namespace XMP.Core.Database.Implementation.RealmDatabase.Common
{
    public class CommonRealmStringPrimaryKeyRepository<T, TDto> : CommonRealmRepository<T, TDto>, IDatabasePrimaryKeyRepository<T, string>
        where T : class
        where TDto : RealmObject
    {
        public CommonRealmStringPrimaryKeyRepository(Func<T, string> itemKeyResolver, IRealmRepositoryEntriesMapper<T, TDto> realmRepositoryEntriesMapper, IRealmProvider realmProvider) : base(realmRepositoryEntriesMapper, realmProvider)
        {
            this.itemKeyResolver = itemKeyResolver;
        }

        private readonly Func<T, string> itemKeyResolver;

        protected TDto FindByKey(Realm realm, string key)
        => realm.Find<TDto>(key);

        protected override TDto FindDtoForItem(Realm realm, T item)
        => FindByKey(realm, itemKeyResolver(item));

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
    }
}