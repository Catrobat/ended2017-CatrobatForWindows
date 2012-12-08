namespace Catrobat.Core.Helpers
{
    public static class StaticApplicationSettings
    {
        public static double MaxSupportedXMLVersion
        {
            get { return 1.0; }
        }

        public static double CurrentApplicationVersion
        {
            get
            {
                //String appVersion = System.Reflection.Assembly.GetExecutingAssembly().FullName.Split('=')[1].Split(',')[0];
                return 0.1;
            }
        }

        public static string CurrentApplicationVersionName
        {
            get { return "MetroCat v0.1"; }
        }

        public static int ThumbnailWidth
        {
            get { return 50; }
        }
    }
}