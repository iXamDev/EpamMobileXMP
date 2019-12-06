using System;
using System.Threading.Tasks;
using Realms;
using Realms.Exceptions;
using XMP.Core.Services.Abstract;
namespace XMP.Core.Services.Implementation
{
    public class RealmProvider : IRealmProvider
    {
        private RealmConfiguration configuration;
        public RealmConfiguration Configuration => configuration ?? (configuration = new RealmConfiguration());

        private bool initialized;
        public bool Initialized => initialized;

        public async Task Drop()
        {
            var success = false;

            var i = 0;

            do
            {
                try
                {
                    i++;

                    initialized = false;

                    var deleteConfig = Configuration;

                    Realm.DeleteRealm(deleteConfig);

                    configuration = null;

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
            } while (!success && i < 10);
        }

        public Realm GetRealm()
        => Realm.GetInstance();

        public async Task Init()
        {
            await Realm.GetInstanceAsync();

            initialized = true;
        }
    }
}
