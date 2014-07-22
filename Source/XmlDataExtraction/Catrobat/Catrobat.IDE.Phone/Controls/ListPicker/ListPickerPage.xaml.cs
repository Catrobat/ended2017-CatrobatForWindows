using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Editor.Costumes;
using Catrobat.IDE.Core.ViewModels.Editor.Scripts;
using Catrobat.IDE.Core.ViewModels.Editor.Sounds;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Controls.ListPicker
{
    public partial class ListPickerPage : PhoneApplicationPage
    {
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
            else if (nullItem is Costume)
            {
                AddViewType = typeof(NewCostumeSourceSelectionViewModel);
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
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _firstSelectionChanged = true;
            ListBoxItems.ItemTemplate = ListPicker.PageItemTemplate;
            ListBoxItems.ItemsSource = null;
            _firstSelectionChanged = true;
            ListBoxItems.ItemsSource = ListPicker.NullItemCollection;
            ListBoxItems.SelectedItem = ListPicker.GetItemWithNullItem();

            base.OnNavigatedTo(e);
        }

        private bool _firstSelectionChanged = true;
        private static ListPicker _listPicker;

        private void ListBoxItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_firstSelectionChanged)
            {
                _firstSelectionChanged = false;
                return;
            }

            ListPicker.SelectedItem = ListBoxItems.SelectedItem;
            var phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (phoneApplicationFrame != null)
                phoneApplicationFrame.GoBack();
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