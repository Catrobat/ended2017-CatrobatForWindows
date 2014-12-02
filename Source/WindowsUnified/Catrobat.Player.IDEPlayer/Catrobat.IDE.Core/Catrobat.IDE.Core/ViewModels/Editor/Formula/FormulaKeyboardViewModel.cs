using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Formulas.Editor;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Catrobat.IDE.Core.ViewModels.Editor.Formula
{
    public class FormulaKeyboardViewModel : ViewModelBase
    {
        private const int MaxHistoryCount = 20;

        #region Properties

        private Program _currentProgram;
        public Program CurrentProgram
        {
            get { return _currentProgram; }
            private set
            {
                if (ReferenceEquals(_currentProgram, value)) return;
                _currentProgram = value;
                GlobalVariablesSource = CurrentProgram == null
                    ? null
                    : VariableHelper.GetGlobalVariableList(CurrentProgram);
            }
        }

        private Sprite _sprite;
        private Sprite Sprite
        {
            get { return _sprite; }
            set
            {
                if (ReferenceEquals(_sprite, value)) return;
                _sprite = value;
                LocalVariablesSource = CurrentProgram == null || Sprite == null
                    ? null
                    : VariableHelper.GetLocalVariableList(CurrentProgram, Sprite);
            }
        }

        private ObservableCollection<LocalVariable> _localVariablesSource;
        private ObservableCollection<LocalVariable> LocalVariablesSource
        {
            get { return _localVariablesSource; }
            set
            {
                if (_localVariablesSource == value) return;
                if (_localVariablesSource != null) _localVariablesSource.CollectionChanged -= LocalVariablesSource_OnCollectionChanged;
                _localVariablesSource = value;
                if (_localVariablesSource != null) _localVariablesSource.CollectionChanged += LocalVariablesSource_OnCollectionChanged;

                if (Favorites != null) Favorites.RemoveAll(data => data.Key == FormulaEditorKey.LocalVariable);
                LocalVariables = LocalVariablesSource == null ? null : LocalVariablesSource.ObservableSelect(
                    selector: variable => new FormulaKey(FormulaEditorKey.LocalVariable, variable), 
                    inverseSelector: data => data.LocalVariable);
            }
        }
        private void LocalVariablesSource_OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Favorites == null && RecentlyUsed == null) return;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    var variables = LocalVariablesSource.ToSet();
                    if (Favorites != null) Favorites.RemoveAll(data => data.Key == FormulaEditorKey.LocalVariable && !variables.Contains(data.LocalVariable));
                    if (RecentlyUsed != null) RecentlyUsed.RemoveAll(data => data.Key == FormulaEditorKey.LocalVariable && !variables.Contains(data.LocalVariable));
                    break;
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                    var deletedVariables = e.OldItems.Cast<LocalVariable>().ToSet();
                    if (Favorites != null) Favorites.RemoveAll(data => data.Key == FormulaEditorKey.LocalVariable && deletedVariables.Contains(data.LocalVariable));
                    if (RecentlyUsed != null) RecentlyUsed.RemoveAll(data => data.Key == FormulaEditorKey.LocalVariable && deletedVariables.Contains(data.LocalVariable));
                    break;
            }
        }

        public IObservableCollection<FormulaKey> LocalVariables { get; private set; }

        private ObservableCollection<GlobalVariable> _globalVariablesSource;
        private ObservableCollection<GlobalVariable> GlobalVariablesSource
        {
            get { return _globalVariablesSource; }
            set
            {
                if (_globalVariablesSource == value) return;
                if (_globalVariablesSource != null) _globalVariablesSource.CollectionChanged -= GlobalVariablesSource_OnCollectionChanged;
                _globalVariablesSource = value;
                if (_globalVariablesSource != null) _globalVariablesSource.CollectionChanged += GlobalVariablesSource_OnCollectionChanged;

                if (Favorites != null) Favorites.RemoveAll(data => data.Key == FormulaEditorKey.GlobalVariable);
                GlobalVariables = GlobalVariablesSource == null ? null : GlobalVariablesSource.ObservableSelect(
                    selector: variable => new FormulaKey(FormulaEditorKey.GlobalVariable, globalVariable: variable),
                    inverseSelector: data => data.GlobalVariable);
            }
        }
        private void GlobalVariablesSource_OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Favorites == null && RecentlyUsed == null) return;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    var variables = GlobalVariablesSource.ToSet();
                    if (Favorites != null) Favorites.RemoveAll(data => data.Key == FormulaEditorKey.GlobalVariable && !variables.Contains(data.GlobalVariable));
                    if (RecentlyUsed != null) RecentlyUsed.RemoveAll(data => data.Key == FormulaEditorKey.GlobalVariable && !variables.Contains(data.GlobalVariable));
                    break;
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                    var deletedVariables = e.OldItems.Cast<GlobalVariable>().ToSet();
                    if (Favorites != null) Favorites.RemoveAll(data => data.Key == FormulaEditorKey.GlobalVariable && deletedVariables.Contains(data.GlobalVariable));
                    if (RecentlyUsed != null) RecentlyUsed.RemoveAll(data => data.Key == FormulaEditorKey.GlobalVariable && deletedVariables.Contains(data.GlobalVariable));
                    break;
            }
        }

        public IObservableCollection<FormulaKey> GlobalVariables { get; set; }

        private ObservableCollection<FormulaKey> _favorites;
        public ObservableCollection<FormulaKey> Favorites
        {
            get { return _favorites; }
            private set
            {
                if (_favorites == value) return;
                if (_favorites != null) _favorites.CollectionChanged -= Favorites_OnCollectionChanged;
                _favorites = value;
                if (_favorites != null) _favorites.CollectionChanged += Favorites_OnCollectionChanged;
                RaisePropertyChanged(() => Favorites);
            }
        }
        private void Favorites_OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            RaisePropertyChanged(() => IsFavoritesEmpty);
        }

        private ObservableCollection<FormulaKey> _recentlyUsed;
        public ObservableCollection<FormulaKey> RecentlyUsed
        {
            get { return _recentlyUsed; }
            private set
            {
                if (_recentlyUsed == value) return;
                _recentlyUsed = value;
                RaisePropertyChanged(() => RecentlyUsed);
            }
        }

        public bool IsFavoritesEmpty
        {
            get { return Favorites == null || Favorites.Count == 0; }
        }

        //private bool _isAddLocalVariableButtonVisible;
        //public bool IsAddLocalVariableButtonVisible
        //{
        //    get { return _isAddLocalVariableButtonVisible; }
        //    set
        //    {
        //        if (_isAddLocalVariableButtonVisible == value) return;
        //        _isAddLocalVariableButtonVisible = value;
        //        RaisePropertyChanged(() => IsAddLocalVariableButtonVisible);
        //    }
        //}

        //private bool _isAddGlobalVariableButtonVisible;

        //public bool IsAddGlobalVariableButtonVisible
        //{
        //    get { return _isAddGlobalVariableButtonVisible; }
        //    set
        //    {
        //        if (_isAddGlobalVariableButtonVisible == value) return;
        //        _isAddGlobalVariableButtonVisible = value;
        //        RaisePropertyChanged(() => IsAddGlobalVariableButtonVisible);
        //    }
        //}

        #endregion

        #region Commands

        public RelayCommand<FormulaKey> AddFavoriteCommand { get; private set; }
        private void AddFavoriteAction(FormulaKey data)
        {
            if (!_favorites.Contains(data)) _favorites.Add(data);
        }

        public RelayCommand<FormulaKey> RemoveFavoriteCommand { get; private set; }
        private void RemoveFavoriteAction(FormulaKey data)
        {
            _favorites.Remove(data);
        }

        public RelayCommand<FormulaKey> KeyPressedCommand { get; private set; }
        private void KeyPressedAction(FormulaKey data)
        {
            if (_recentlyUsed.Contains(data)) _recentlyUsed.Remove(data);
            _recentlyUsed.Insert(0, data);
            if (_recentlyUsed.Count > MaxHistoryCount) _recentlyUsed.RemoveAt(_recentlyUsed.Count - 1);
        }

        public RelayCommand<FormulaKey> AddLocalVariableCommand { get; private set; }
        private void AddLocalVariableAction(FormulaKey data)
        {
            ServiceLocator.NavigationService.NavigateTo<AddNewLocalVariableViewModel>();
        }

        public RelayCommand<FormulaKey> AddGlobalVariableCommand { get; private set; }
        private void AddGlobalVariableAction(FormulaKey data)
        {
            ServiceLocator.NavigationService.NavigateTo<AddNewGlobalVariableViewModel>();
        }

        public RelayCommand<FormulaKey> RenameVariableCommand { get; private set; }
        private void RenameVariableAction(FormulaKey data)
        {
            var message = new GenericMessage<Variable>((Variable) data.LocalVariable ?? data.GlobalVariable);
            Messenger.Default.Send(message, ViewModelMessagingToken.SelectedUserVariableChangedListener);

            ServiceLocator.NavigationService.NavigateTo<ChangeVariableViewModel>();
        }

        public RelayCommand<FormulaKey> DeleteVariableCommand { get; private set; }
        private void DeleteVariableAction(FormulaKey data)
        {
            switch (data.Key)
            {
                case FormulaEditorKey.LocalVariable:
                    LocalVariables.Remove(data);
                    break;
                case FormulaEditorKey.GlobalVariable:
                    GlobalVariables.Remove(data);
                    break;
            }
        }
        private bool DeleteVariableCanExecute(FormulaKey formulaKey)
        {
            // TODO: check if this variable is used in formulas
            return true;
        }

        #endregion

        #region Messenger

        private void LoadSettingsAction(LocalSettings settings)
        {
            var localVariables = (LocalVariables ?? Enumerable.Empty<FormulaKey>()).ToLookup(variable => variable.LocalVariable == null ? null : variable.LocalVariable.Name);
            var globalVariables = (GlobalVariables ?? Enumerable.Empty<FormulaKey>()).ToLookup(variable => variable.GlobalVariable == null ? null : variable.GlobalVariable.Name);
            RecentlyUsed = (DeserializeFormulaKeyCollection(settings.RecentlyUsed, localVariables, globalVariables) ?? Enumerable.Empty<FormulaKey>()).ToObservableCollection();
            Favorites = (DeserializeFormulaKeyCollection(settings.Favorites, localVariables, globalVariables) ?? Enumerable.Empty<FormulaKey>()).ToObservableCollection();
        }

        private void SaveSettingsAction(LocalSettings settings)
        {
            settings.RecentlyUsed = SerializeFormulaKeyCollection(_recentlyUsed).ToList();
            settings.Favorites = SerializeFormulaKeyCollection(_favorites).ToList();
        }

        private static IEnumerable<SerializableTuple<int, string>> SerializeFormulaKeyCollection(IEnumerable<FormulaKey> collection)
        {
            return collection == null ? null : collection.Select(data => (SerializableTuple<int, string>) Tuple.Create(
                item1: (int) data.Key,
                item2: data.LocalVariable != null ? data.LocalVariable.Name : data.GlobalVariable != null ? data.GlobalVariable.Name : null));
        }

        private static IEnumerable<FormulaKey> DeserializeFormulaKeyCollection(
            IEnumerable<SerializableTuple<int, string>> graph, 
            ILookup<string, FormulaKey> localVariables, 
            ILookup<string, FormulaKey> globalVariables)
        {
            return graph == null ? null : graph.Select(g => (Tuple<int, string>) g).Select(g =>
            {
                var key = (FormulaEditorKey) g.Item1;
                switch (key)
                {
                    case FormulaEditorKey.LocalVariable: return localVariables[g.Item2].FirstOrDefault();
                    case FormulaEditorKey.GlobalVariable: return globalVariables[g.Item2].FirstOrDefault();
                    default: return new FormulaKey(key, null);
                }
            }).Where(data => data != null);
        }

        private void CurrentProgramChangedAction(Program program)
        {
            CurrentProgram = program;
        }

        private void CurrentSpriteChangedAction(Sprite sprite)
        {
            Sprite = sprite;
        }

        #endregion

        public FormulaKeyboardViewModel()
        {
            AddFavoriteCommand = new RelayCommand<FormulaKey>(AddFavoriteAction);
            RemoveFavoriteCommand = new RelayCommand<FormulaKey>(RemoveFavoriteAction);
            KeyPressedCommand = new RelayCommand<FormulaKey>(KeyPressedAction);
            DeleteVariableCommand = new RelayCommand<FormulaKey>(DeleteVariableAction);

            AddLocalVariableCommand = new RelayCommand<FormulaKey>(AddLocalVariableAction);
            AddGlobalVariableCommand = new RelayCommand<FormulaKey>(AddGlobalVariableAction);
            RenameVariableCommand = new RelayCommand<FormulaKey>(RenameVariableAction);
            DeleteVariableCommand = new RelayCommand<FormulaKey>(DeleteVariableAction, DeleteVariableCanExecute);

            Messenger.Default.Register<GenericMessage<LocalSettings>>(this, ViewModelMessagingToken.LoadSettings, message => LoadSettingsAction(message.Content));
            Messenger.Default.Register<GenericMessage<LocalSettings>>(this, ViewModelMessagingToken.SaveSettings, message => SaveSettingsAction(message.Content));
            Messenger.Default.Register<GenericMessage<Program>>(this, ViewModelMessagingToken.CurrentProgramChangedListener, message => CurrentProgramChangedAction(message.Content));
            Messenger.Default.Register<GenericMessage<Sprite>>(this, ViewModelMessagingToken.CurrentSpriteChangedListener, message => CurrentSpriteChangedAction(message.Content));
        }

        internal void ResetViewModel()
        {
            //IsAddLocalVariableButtonVisible = false;
            //IsAddGlobalVariableButtonVisible = false;
        }
    }
}