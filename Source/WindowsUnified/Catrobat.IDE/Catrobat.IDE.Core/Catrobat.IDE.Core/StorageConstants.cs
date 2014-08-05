using System.Collections.Generic;
using System.Dynamic;
using Catrobat.IDE.Core.Resources.Localization;

namespace Catrobat.IDE.Core
{
    public static class StorageConstants
    {
        // supported file formats
        public static string PaintImageExportFileExtension = ".catrobat_paint_png";
        public static string PaintImageImportFileExtension = ".catrobat_ide_png";

        public static IEnumerable<string> SupportedImageFileTypes
        {
            get
            {
                return new List<string> { ".jpg", ".jpeg", ".png" };
            }
        }

        public static IEnumerable<string> SupportedCatrobatFileTypes
        { // first one is used for export
            get
            {
                return new List<string> { ".catrobat", ".pocketcode"};
            }
        }


        // Program temporary paths
        public const string TempProgramImportZipPath = "Temp/ImportProgramZip";
        public const string TempProgramImportPath = "Temp/ImportProgram";
        public const string TempProgramExportZipPath = "Temp/ExportProgramZip";
        public const string TempProgramExportPath = "Temp/ExportProgram";

        public static string TempPaintImagePath
        { get { return "Temp/temp" + PaintImageExportFileExtension; } }

        // Program related constants
        public const string ProgramsPath = "Projects";
        public const string ProgramCodePath = "code.xml";
        public const string ProgramManualScreenshotPath = "manual_screenshot.png‏";
        public const string ProgramAutomaticScreenshotPath = "automatic_screenshot.png";
        public const string ProgramLooksPath = "images";
        public const string ProgramSoundsPath = "sounds";

        // other constants
        public const string LocalSettingsFilePath = "Settings/settings";
        public const string ImageThumbnailExtension = "_thumb.png";
    }
}
