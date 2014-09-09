using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Editor.Actions;
using Catrobat.IDE.Core.ViewModels.Editor.Looks;
using Catrobat.IDE.Core.ViewModels.Editor.Sounds;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.WindowsShared.Common;
using GalaSoft.MvvmLight.Command;

namespace Catrobat.IDE.WindowsPhone.Controls.ListPickerControl
{
    public partial class ListPickerPage
    {

        private readonly NavigationHelper _navigationHelper;
        public NavigationHelper NavigationHelper
        {
            get { return this._navigationHelper; }
        }


        public static ListPicker ListPicker
        {
            get { return _listPicker; }
            set
            {
                _listPicker = value;
                SetAddViewType(value.NullItem);
            }
        }

        private static void SetAddViewType(object nullItem)
        {
            if (nullItem is Sprite)
            {
                AddViewType = typeof(AddNewSpriteViewModel);
            }
            else if (nullItem is Action)
            {
                AddViewType = typeof(ScriptBrickCategoryViewModel);
            }
            else if (nullItem is Look)
            {
                AddViewType = typeof(NewLookSourceSelectionViewModel);
            }
            else if (nullItem is Sound)
            {
                AddViewType = typeof(NewSoundSourceSelectionViewModel);
            }
            else if (nullItem is BroadcastMessage)
            {
                AddViewType = typeof(NewBroadcastMessageViewModel);
            }
            else
            {
                // Add new type above
                if (Debugger.IsAttached)
                    Debugger.Break();
            }
        }

        public static Type AddViewType { get; set; }

        public ListPickerPage()
        {
            InitializeComponent();

            this._navigationHelper = new NavigationHelper(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._navigationHelper.OnNavigatedTo(e);

            _skipNextSelectionChanged = true;
            ListBoxItems.ItemTemplate = ListPicker.PageItemTemplate;
            ListBoxItems.ItemsSource = null;
            _skipNextSelectionChanged = true;
            ListBoxItems.ItemsSource = ListPicker.NullItemCollection;
            ListBoxItems.SelectedItem = ListPicker.GetItemWithNullItem();

            base.OnNavigatedTo(e);
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this._navigationHelper.OnNavigatedFrom(e);
        }


        private bool _skipNextSelectionChanged = true;
        private static ListPicker _listPicker;

        private void ListBoxItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_skipNextSelectionChanged)
            {
                _skipNextSelectionChanged = false;
                return;
            }

            ListPicker.SelectedItem = ListBoxItems.SelectedItem;
            ServiceLocator.NavigationService.NavigateBack(this.GetType());
        }
    }

    public class ListPickerDummyViewModel
    {
        public RelayCommand<Type> ShowAddViewCommand { get { return new RelayCommand<Type>(ShowViewModel); } }

        private static void ShowViewModel(Type type)
        {
            ServiceLocator.NavigationService.NavigateTo(ListPickerPage.AddViewType);
        }
    }
}