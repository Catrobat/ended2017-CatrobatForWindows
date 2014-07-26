using System.Collections.Generic;
using Catrobat.IDE.Core.Resources.Localization;

namespace Catrobat.IDE.Core
{
    public static class StorageConstants
    {
        public const string ApplicationName = "Pocket Code";
        public const string PlayerActiveProjectZipPath = 
            "ActivePlayerProject/ActiveProject.catrobat_from_ide";
        public const string LocalSettingsFilePath = "Settings/settings";
        public const string DefaultProjectPath = "default.catrobat";
        public const string ProjectsPath = "Projects";
        public const string TempProjectImportZipPath = "Temp/ImportProjectZip";
        public const string TempProjectImportPath = "Temp/ImportProject";
        public const string TempPaintImagePath = "Temp/PaintImage";

        public const string ImageThumbnailExtension = "_thumb.png";

        public static string DefaultProjectName
        {
            get
            {
                return AppResources.Main_DefaultProjectName;
            }
        }


        // TODO: add constants from "Project" class
    }
}
