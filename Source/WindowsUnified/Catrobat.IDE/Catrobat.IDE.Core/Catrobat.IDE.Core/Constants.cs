using System.Collections.Generic;

namespace Catrobat.IDE.Core
{
    public static class Constants
    {
        // Application Info
        public const string ApplicationName = "Pocket Code";

        public const string CurrentAppVersion = "0.20";

        public const int CurrentAppBuildNumber = 1;

        public const string CurrentAppBuildName = "0.20";

        // Vatrobat XML Version

        public const double MinimumCodeVersion = 0.91;

        public const string TargetIDEVersion = "Win0.91";

        public const string TargetOutputVersion = "0.91";

        // Languages

        public static readonly string[] SupportedLanguageCodes =
        {
            "DE", "EN"
        };

        public static List<string> CatrobatFileNames = 
            new List<string> { ".catrobat", ".pocketcode" };
    }
}
