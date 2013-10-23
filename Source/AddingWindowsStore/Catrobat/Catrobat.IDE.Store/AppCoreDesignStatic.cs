namespace Catrobat.IDE.Store
{
    public class AppStoreDesignStatic
    {
        static AppStoreDesignStatic()
        {
            Core.App.SetNativeApp(new AppStore());
            Core.App.Initialize();
        }
    }
}
