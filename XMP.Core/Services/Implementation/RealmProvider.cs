using System;
using System.Threading.Tasks;
using Realms;
using Realms.Exceptions;
using XMP.Core.Services.Abstract;

namespace XMP.Core.Services.Implementation
{
    public class RealmProvider : IRealmProvider
    {
        private bool _initialized;

        private RealmConfiguration _configuration;

        public RealmConfiguration Configuration => _configuration ?? (_configuration = new RealmConfiguration());

        public bool Initialized => _initialized;

        public async Task Drop()
        {
            var success = false;

            var i = 0;

            do
            {
                try
                {
                    i++;

                    _initialized = false;

                    var deleteConfig = Configuration;

                    Realm.DeleteRealm(deleteConfig);

                    _configuration = null;

                    success = true;
                }
                catch (RealmPermissionDeniedException)
                {
                    // configuration may be in use
                    await Task.Delay(50);
                }
                catch (Exception)
                {
                }
            }
            while (!success && i < 10);
        }

        public Realm GetRealm()
        => Realm.GetInstance();

        public async Task Init()
        {
            await Realm.GetInstanceAsync();

            _initialized = true;
        }
    }
}
