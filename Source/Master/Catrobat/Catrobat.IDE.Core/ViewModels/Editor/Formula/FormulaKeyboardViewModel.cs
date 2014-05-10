using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Formulas.Editor;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Editor.Formula
{
    public class FormulaKeyboardViewModel : ViewModelBase
    {
        private const int MaxHistoryCount = 20;

        #region Properties

        private Project Project { get; set; }

        private Sprite Sprite { get; set; }

        private ObservableCollection<UserVariable> _localVariablesSource;
        private ObservableCollection<UserVariable> LocalVariablesSource
        {
            get { return _localVariablesSource; }
            set
            {
                if (_localVariablesSource == value) return;
                if (_localVariablesSource != null) _localVariablesSource.CollectionChanged -= LocalVariablesSource_OnCollectionChanged;
                _localVariablesSource = value;
                if (_localVariablesSource != null) _localVariablesSource.CollectionChanged += LocalVariablesSource_OnCollectionChanged;

                LocalVariables = _localVariablesSource == null ? null : _localVariablesSource.ObservableSelect(
                    selector: variable => new FormulaKey(FormulaEditorKey.LocalVariable, variable), 
                    inverseSelector: data => data.Variable);
            }
        }
        private void LocalVariablesSource_OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    var variables = e.OldItems.Cast<UserVariable>().ToLookup(variable => variable);
                    Favorites.RemoveAll(data => data.Key == FormulaEditorKey.LocalVariable && !variables.Contains(data.Variable));
                    RecentlyUsed.RemoveAll(data => data.Key == FormulaEditorKey.LocalVariable && !variables.Contains(data.Variable));
                    break;
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                    var deletedVariables = e.OldItems.Cast<UserVariable>().ToLookup(variable => variable);
                    Favorites.RemoveAll(data => data.Key == FormulaEditorKey.LocalVariable && deletedVariables.Contains(data.Variable));
                    RecentlyUsed.RemoveAll(data => data.Key == FormulaEditorKey.LocalVariable && deletedVariables.Contains(data.Variable));
                    break;
            }
        }

        public IObservableCollection<FormulaKey> LocalVariables { get; private set; }

        private ObservableCollection<UserVariable> _globalVariablesSource;
        private ObservableCollection<UserVariable> GlobalVariablesSource
        {
            get { return _globalVariablesSource; }
            set
            {
                if (_globalVariablesSource == value) return;
                if (_globalVariablesSource != null) _globalVariablesSource.CollectionChanged -= GlobalVariablesSource_OnCollectionChanged;
                _globalVariablesSource = value;
                if (_globalVariablesSource != null) _globalVariablesSource.CollectionChanged += GlobalVariablesSource_OnCollectionChanged;

                GlobalVariables = _globalVariablesSource.ObservableSelect(
                    selector: variable => new FormulaKey(FormulaEditorKey.GlobalVariable, variable),
                    inverseSelector: data => data.Variable);
            }
        }
        private void GlobalVariablesSource_OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    var variables = e.OldItems.Cast<UserVariable>().ToLookup(variable => variable);
                    Favorites.RemoveAll(data => data.Key == FormulaEditorKey.GlobalVariable && !variables.Contains(data.Variable));
                    RecentlyUsed.RemoveAll(data => data.Key == FormulaEditorKey.GlobalVariable && !variables.Contains(data.Variable));
                    break;
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                    var deletedVariables = e.OldItems.Cast<UserVariable>().ToLookup(variable => variable);
                    Favorites.RemoveAll(data => data.Key == FormulaEditorKey.GlobalVariable && deletedVariables.Contains(data.Variable));
                    RecentlyUsed.RemoveAll(data => data.Key == FormulaEditorKey.GlobalVariable && deletedVariables.Contains(data.Variable));
                    break;
            }
        }

        public IObservableCollection<FormulaKey> GlobalVariables { get; set; }

        private readonly ObservableCollection<FormulaKey> _favorites;
        public ObservableCollection<FormulaKey> Favorites
        {
            get { return _favorites; }
        }

        private readonly ObservableCollection<FormulaKey> _recentlyUsed;

        public ObservableCollection<FormulaKey> RecentlyUsed
        {
            get { return _recentlyUsed; }
        }

        public bool IsFavoritesEmpty
        {
            get { return Favorites == null || Favorites.Count == 0; }
        }

        private bool _isAddLocalVariableButtonVisible;
        public bool IsAddLocalVariableButtonVisible
        {
            get { return _isAddLocalVariableButtonVisible; }
            set
            {
                if (_isAddLocalVariableButtonVisible == value) return;
                _isAddLocalVariableButtonVisible = value;
                RaisePropertyChanged(() => IsAddLocalVariableButtonVisible);
            }
        }

        private bool _isAddGlobalVariableButtonVisible;
        public bool IsAddGlobalVariableButtonVisible
        {
            get { return _isAddGlobalVariableButtonVisible; }
            set
            {
                if (_isAddGlobalVariableButtonVisible == value) return;
                _isAddGlobalVariableButtonVisible = value;
                RaisePropertyChanged(() => IsAddGlobalVariableButtonVisible);
            }
        }

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
            var message = new GenericMessage<UserVariable>(data.Variable);
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

        private void CurrentProjectChangedMessageAction(GenericMessage<Project> message)
        {
            Project = message.Content;

            GlobalVariablesSource = Project == null 
                ? null 
                : VariableHelper.GetGlobalVariableList(Project);
        }

        private void CurrentSpriteChangedMesageAction(GenericMessage<Sprite> message)
        {
            Sprite = message.Content;

            if (Project == null) return;

            LocalVariablesSource = Project == null || Sprite == null 
                ? null 
                : VariableHelper.GetLocalVariableList(Project, Sprite);
            Favorites.RemoveAll(data => data.Key == FormulaEditorKey.LocalVariable);
        }

        #endregion

        public FormulaKeyboardViewModel()
        {
            // TODO: synchronize with settings
            _favorites = new ObservableCollection<FormulaKey>();
            _favorites.CollectionChanged += (sender, e) => RaisePropertyChanged(() => IsFavoritesEmpty);
            _recentlyUsed = new ObservableCollection<FormulaKey>();
            AddFavoriteCommand = new RelayCommand<FormulaKey>(AddFavoriteAction);
            RemoveFavoriteCommand = new RelayCommand<FormulaKey>(RemoveFavoriteAction);
            KeyPressedCommand = new RelayCommand<FormulaKey>(KeyPressedAction);
            DeleteVariableCommand = new RelayCommand<FormulaKey>(DeleteVariableAction);

            AddLocalVariableCommand = new RelayCommand<FormulaKey>(AddLocalVariableAction);
            AddGlobalVariableCommand = new RelayCommand<FormulaKey>(AddGlobalVariableAction);
            RenameVariableCommand = new RelayCommand<FormulaKey>(RenameVariableAction);
            DeleteVariableCommand = new RelayCommand<FormulaKey>(DeleteVariableAction, DeleteVariableCanExecute);

            Messenger.Default.Register<GenericMessage<Project>>(this, ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);
            Messenger.Default.Register<GenericMessage<Sprite>>(this, ViewModelMessagingToken.CurrentSpriteChangedListener, CurrentSpriteChangedMesageAction);

        }

        internal void ResetViewModel()
        {
            IsAddLocalVariableButtonVisible = false;
            IsAddGlobalVariableButtonVisible = false;
        }
    }
}