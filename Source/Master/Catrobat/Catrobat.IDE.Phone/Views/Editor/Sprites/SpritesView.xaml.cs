using System.Windows.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Sprites;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Editor.Sprites
{
    public partial class SpritesView : PhoneApplicationPage
    {
        private readonly SpritesViewModel _viewModel =
            ((ViewModelLocator) ServiceLocator.ViewModelLocator).SpritesViewModel;

        public SpritesView()
        {
            InitializeComponent();
            //_viewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
        }

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
