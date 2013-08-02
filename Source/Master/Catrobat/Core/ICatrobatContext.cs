using System.Collections.ObjectModel;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Scripts;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Objects.Variables;
using Catrobat.Core.Storage;

namespace Catrobat.Core
{
    public interface ICatrobatContext
    {
        Project CurrentProject { get; set; }

        ObservableCollection<ProjectDummyHeader> LocalProjects { get; }

        LocalSettings LocalSettings { get; set; }

        void SetCurrentProject(string projectName);

        void CreateNewProject(string projectName);

        void DeleteProject(string projectName);

        void CopyProject(string projectName);

        void UpdateLocalProjects();

        void StoreLocalSettings();

        bool RestoreLocalSettings();

        void Save();

        void InitializeLocalSettings();

        void RestoreDefaultProject(string projectName);

        void CleanUpCostumeReferences(Costume deletedCostume, Sprite selectedSprite);

        void CleanUpSoundReferences(Sound deletedSound, Sprite selectedSprite);

        void CleanUpSpriteReferences(Sprite deletedSprite);

        void CleanUpVariableReferences(UserVariable deletedUserVariable, Sprite selectedSprite);
    }
}