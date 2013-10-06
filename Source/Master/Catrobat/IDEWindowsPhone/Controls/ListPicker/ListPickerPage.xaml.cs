using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Services;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Costumes;
using Catrobat.IDEWindowsPhone.Views.Editor.Costumes;
using Catrobat.IDEWindowsPhone.Views.Editor.Scripts;
using Catrobat.IDEWindowsPhone.Views.Editor.Sounds;
using Catrobat.IDEWindowsPhone.Views.Editor.Sprites;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Catrobat.IDEWindowsPhone.Controls.ListPicker
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
                AddViewType = typeof(AddNewSpriteView);
            }
            else if (nullItem is Action)
            {
                AddViewType = typeof(AddNewBrickView);
            }
            else if (nullItem is Costume)
            {
                AddViewType = typeof(NewCostumeSourceSelectionView);
            }
            else if (nullItem is Sound)
            {
                AddViewType = typeof(AddNewSoundView);
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