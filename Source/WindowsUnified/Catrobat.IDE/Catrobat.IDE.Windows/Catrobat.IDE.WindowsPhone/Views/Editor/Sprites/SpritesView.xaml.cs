using System.ComponentModel;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Sprites
{
    public partial class SpritesView : Page
    {
        private readonly SpritesViewModel _viewModel =
            ((ViewModelLocator) ServiceLocator.ViewModelLocator).SpritesViewModel;

        public SpritesView()
        {
            InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
            e.Handled = true;
        }

        //protected override void OnBackKeyPress(CancelEventArgs e)
        //{
        //    _viewModel.GoBackCommand.Execute(null);
        //    e.Cancel = true;
        //    base.OnBackKeyPress(e);
        //}

        //private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        //{
        //    if (args.PropertyName == PropertyNameHelper.GetPropertyNameFromExpression(() => _viewModel.SelectedSprite))
        //    {
        //        var selectedSprite = _viewModel.SelectedSprite;

        //        ReorderListBoxSprites.SelectedItem = selectedSprite;
        //    }
        //}
    }
}
