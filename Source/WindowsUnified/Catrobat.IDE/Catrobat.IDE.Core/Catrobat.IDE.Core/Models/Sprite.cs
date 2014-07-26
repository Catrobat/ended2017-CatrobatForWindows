using System;
using System.Collections.Specialized;
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
    public partial class Sprite : Model, IAsyncCloneable<Program>
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
            set
            {
                var oldValue = _scripts;
                if (!Set(ref _scripts, value)) return;
                RaisePropertyChanged(() => ActionsCount);
                if (oldValue != null)
                {
                    oldValue.CollectionChanged -= Scripts_OnCollectionChanged;
                    foreach (var script in oldValue)
                    {
                        script.Bricks.CollectionChanged -= Bricks_OnCollectionChanged;
                    }
                }
                if (value != null)
                {
                    foreach (var script in value)
                    {
                        script.Bricks.CollectionChanged += Bricks_OnCollectionChanged;
                    }
                    value.CollectionChanged += Scripts_OnCollectionChanged;
                }
            }
        }
        private void Scripts_OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Script script in e.NewItems)
                    {
                        script.Bricks.CollectionChanged += Bricks_OnCollectionChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    // Scripts stay the same
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (Script script in e.OldItems)
                    {
                        script.Bricks.CollectionChanged -= Bricks_OnCollectionChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    foreach (Script script in e.OldItems)
                    {
                        script.Bricks.CollectionChanged -= Bricks_OnCollectionChanged;
                    }
                    foreach (Script script in e.NewItems)
                    {
                        script.Bricks.CollectionChanged += Bricks_OnCollectionChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    // unsubscribing from old bricks not possible
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void Bricks_OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            switch (notifyCollectionChangedEventArgs.Action)
            {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Reset:
                    RaisePropertyChanged(() => ActionsCount);
                    break;
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                    // Bricks.Count stays the same
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public int ActionsCount
        {
            get
            {
                return _scripts.Sum(script => script.Bricks.Count + 1);
            }
        }

        #endregion

        public async Task Delete(Program project)
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

        async Task<object> IAsyncCloneable<Program>.CloneInstance(Program project)
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
