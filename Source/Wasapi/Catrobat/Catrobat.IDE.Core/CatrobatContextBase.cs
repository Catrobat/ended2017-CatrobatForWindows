using System;
using System.ComponentModel;
using System.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core
{
    public abstract class CatrobatContextBase : INotifyPropertyChanged
    {

        #region Constants

        public const string PlayerActiveProjectZipPath = "ActivePlayerProject/ActiveProject.catrobat_from_ide";
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

        #endregion

        #region Private members

        #endregion

        #region Properties

        public LocalSettings LocalSettings { get; set; }

        public string CurrentToken
        {
            get { return LocalSettings.CurrentToken; }

            set
            {
                if (LocalSettings.CurrentToken == value) return;

                LocalSettings.CurrentToken = value;
                RaisePropertyChanged();
            }
        }

        public string CurrentUserEmail
        {
            get { return LocalSettings.CurrentUserEmail; }

            set
            {
                if (LocalSettings.CurrentUserEmail == value) return;

                LocalSettings.CurrentUserEmail = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RaisePropertyChanged<T>(Expression<Func<T>> selector)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyHelper.GetPropertyName(selector)));
            }
        }

        #endregion
    }
}