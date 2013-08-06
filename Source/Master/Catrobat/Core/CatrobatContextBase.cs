using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Catrobat.Core.Annotations;
using Catrobat.Core.Misc;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Scripts;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Objects.Variables;
using Catrobat.Core.Storage;

namespace Catrobat.Core
{
    public abstract class CatrobatContextBase : INotifyPropertyChanged
    {
        public ContextSaving ContextSaving;


        public const string PlayerActiveProjectZipPath = "ActivePlayerProject/ActiveProject.catrobat_from_ide";
        public const string LocalSettingsFilePath = "Settings/settings";
        public const string DefaultProjectPath = "default.catrobat";
        public const string ProjectsPath = "Projects";
        public const string DefaultProjectName = "DefaultProject";
        public const string TempProjectImportZipPath = "Temp/ImportProjectZip";
        public const string TempProjectImportPath = "Temp/ImportProject";
        public const string TempPaintImagePath = "Temp/PaintImage";


        public LocalSettings LocalSettings { get; set; }

        public ObservableCollection<OnlineProjectHeader> OnlineProjects { get; protected set; }

        public string CurrentToken
        {
            get { return LocalSettings.CurrentToken; }

            set
            {
                if (LocalSettings.CurrentToken == value)
                {
                    return;
                }

                LocalSettings.CurrentToken = value;
                RaisePropertyChanged();
            }
        }

        public string CurrentUserEmail
        {
            get { return LocalSettings.CurrentUserEmail; }

            set
            {
                if (LocalSettings.CurrentUserEmail == value)
                {
                    return;
                }

                LocalSettings.CurrentUserEmail = value;
                RaisePropertyChanged();
            }
        }

        protected Project CurrentProjectField;

        public Project CurrentProject
        {
            get { return CurrentProjectField; }
            set
            {
                if (CurrentProjectField == value)
                {
                    return;
                }

                CurrentProjectField = value;
                ProjectHolder.Project = CurrentProjectField;
                RaisePropertyChanged();
                //RaisePropertyChanged(() => );
                //UpdateLocalProjects();LocalProjects
            }
        }

        // To remove:

        //private static IContextHolder _holder;
        //public static CatrobatContextBase GetContext()
        //{
        //    return _holder.GetContext();
        //}

        //public static void SetContextHolder(IContextHolder holder)
        //{
        //    _holder = holder;
        //}

        //public abstract void SetCurrentProject(string projectName);

        //public abstract void CreateNewProject(string projectName);

        //public abstract void DeleteProject(string projectName);

        //public abstract void CopyProject(string projectName);

        //public abstract void UpdateLocalProjects();

        //public abstract void StoreLocalSettings();

        //public abstract bool RestoreLocalSettings();

        //public abstract void Save();

        //public abstract void InitializeLocalSettings();

        //public abstract void RestoreDefaultProject(string projectName);

        //public abstract void CleanUpCostumeReferences(Costume deletedCostume, Sprite selectedSprite);

        //public abstract void CleanUpSoundReferences(Sound deletedSound, Sprite selectedSprite);

        //public abstract void CleanUpSpriteReferences(Sprite deletedSprite);

        //public abstract void CleanUpVariableReferences(UserVariable deletedUserVariable, Sprite selectedSprite);

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
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyNameHelper.GetPropertyNameFromExpression(selector)));
            }
        }

        #endregion
    }
}