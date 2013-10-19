using System.Diagnostics;

namespace Catrobat.IDE.Core
{
    public class LocalSettings
    {
        public bool IsInDevelopingMode { get; set; }

        public string CurrentProjectName { get; set; }

        public string CurrentLanguageString { get; set; }

        public int CurrentThemeIndex { get; set; }

        public string CurrentUserEmail { get; set; }

        public string CurrentToken { get; set; }

        public LocalSettings ()
        {
            if (Debugger.IsAttached)
                IsInDevelopingMode = true;
        }
    }
}