namespace Catrobat.IDE.Store
{
    public class AppStoreDesignStatic
    {
        static AppStoreDesignStatic()
        {
            var app = new AppStore();
            app.InitializeInterfaces();
            Core.App.SetNativeApp(app);
        }
    }
}
