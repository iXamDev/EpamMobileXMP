using System.Threading;
using FlexiMvvm.Bootstrappers;
using FlexiMvvm.Ioc;
using Foundation;
using UIKit;
using XMP.Core.Bootstrapper;
using XMP.iOS.Bootstrapper;
using XMP.iOS.Views.Splash;
using XMP.API.Bootstrappers;

namespace XMP.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIResponder, IUIApplicationDelegate
    {
        [Export("window")]
        public static UIWindow Window { get; set; }

        [Export("application:didFinishLaunchingWithOptions:")]
        public bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            InitFramework();

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");

            Theme.SetupGrobalStyle();

            Window = new UIWindow(UIScreen.MainScreen.Bounds)
            {
                RootViewController = new SplashViewController()
            };

            Window.MakeKeyAndVisible();

            return true;
        }

        private void InitFramework()
        {
            var config = new BootstrapperConfig();
            config.SetSimpleIoc(new SimpleIoc());

            var compositeBootstrapper = new CompositeBootstrapper(new IosBootstrapper(), new CoreBootstrapper());
            compositeBootstrapper.Execute(config);
        }
    }
}

