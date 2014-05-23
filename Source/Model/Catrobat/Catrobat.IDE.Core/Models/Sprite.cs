using System.Linq;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Scripts;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Catrobat.IDE.Core.Models
{
    [DebuggerDisplay("Name = {Name}")]
    public partial class Sprite : Model, IAsyncCloneable<Project>
    {
        #region Properties

        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        private ObservableCollection<Costume> _costumes = new ObservableCollection<Costume>();
        public ObservableCollection<Costume> Costumes
        {
            get { return _costumes; }
            set { Set(ref _costumes, value); }
        }

        private ObservableCollection<Sound> _sounds = new ObservableCollection<Sound>();
        public ObservableCollection<Sound> Sounds
        {
            get { return _sounds; }
            set { Set(ref _sounds, value); }
        }

        private ObservableCollection<LocalVariable> _localVariables = new ObservableCollection<LocalVariable>();
        public ObservableCollection<LocalVariable> LocalVariables
        {
            get { return _localVariables; }
            set { Set(ref _localVariables, value); }
        }

        private ObservableCollection<Script> _scripts = new ObservableCollection<Script>();
        public ObservableCollection<Script> Scripts
        {
            get { return _scripts; }
            set { Set(ref _scripts, value); }
        }

        #endregion

        public async Task Delete(Project project)
        {
            foreach (var costume in Costumes)
            {
                await costume.Delete(project);
            }
            foreach (var sound in Sounds)
            {
                await sound.Delete(project);
            }
        }

        #region Implements TestEquatable

        protected override bool TestEquals(Model other)
        {
            return base.TestEquals(other) && TestEquals((Sprite) other);
        }

        protected bool TestEquals(Sprite other)
        {
            return CollectionExtensions.TestEquals(_costumes, other._costumes) &&
                CollectionExtensions.TestEquals(_localVariables, other._localVariables) && 
                string.Equals(_name, other._name) &&
                CollectionExtensions.TestEquals(_scripts, other._scripts) &&
                CollectionExtensions.TestEquals(_sounds, other._sounds);
        }

        #endregion

        #region Implements IAsyncCloneable

        async Task<object> IAsyncCloneable<Project>.CloneInstance(Project project)
        {
            var costumes = Costumes == null ? null : await Costumes.ToReadOnlyDictionaryAsync(
                keySelector: costume => costume, 
                elementSelector: costume => costume.CloneAsync(project));
            var sounds = Sounds == null ? null : await Sounds.ToReadOnlyDictionaryAsync(
                keySelector: sound => sound,
                elementSelector: sound => sound.CloneAsync(project));
            var localVariables = LocalVariables == null ? null : LocalVariables.ToReadOnlyDictionary(
                keySelector: variable => variable,
                elementSelector: variable => variable.Clone());
            var context = new CloneSpriteContext(costumes, sounds, localVariables);
            return new Sprite
            {
                Name = Name,
                Costumes = costumes == null ? null : costumes.Values.ToObservableCollection(),
                Sounds = sounds == null ? null : sounds.Values.ToObservableCollection(),
                LocalVariables = localVariables == null ? null : localVariables.Values.ToObservableCollection(),
                Scripts = Scripts.Select(script => script.Clone(context)).ToObservableCollection()
            };
        }

        #endregion
    }
}
