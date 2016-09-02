using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Looks;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Looks
{
    public partial class LookNameChooserView
    {
        private readonly LookNameChooserViewModel _viewModel =
            ServiceLocator.ViewModelLocator.LookNameChooserViewModel;


        public LookNameChooserView()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            TextBoxLookName.Focus(FocusState.Keyboard);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TextBoxLookName.Focus(FocusState.Keyboard);
            base.OnNavigatedTo(e);
        }

        private void TextBoxLookName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.LookName = TextBoxLookName.Text;
        }

        //private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var listView = (ListViewBase)sender;

        //    if (e.AddedItems != null && e.AddedItems.Count >= 1)
        //        ((ISelectable)e.AddedItems[0]).IsSelected = true;

        //    if (e.RemovedItems != null && e.RemovedItems.Count >= 1)
        //        ((ISelectable)e.RemovedItems[0]).IsSelected = false;

        //    if (!listView.SelectedItems.Contains(_viewModel.SelectedSize))
        //    {
        //        Debugger.Break();
        //    }
        //}
    }
}