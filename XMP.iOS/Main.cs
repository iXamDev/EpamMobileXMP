using UIKit;

#pragma warning disable SA1300
namespace XMP.iOS
#pragma warning restore SA1300
{
#pragma warning disable SA1649
    public class Application
#pragma warning restore SA1649
    {
        // This is the main entry point of the application.
#pragma warning disable SA1400
        static void Main(string[] args)
#pragma warning restore SA1400
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
