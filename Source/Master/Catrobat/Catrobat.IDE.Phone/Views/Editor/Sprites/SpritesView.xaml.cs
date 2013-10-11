using Catrobat.IDE.Phone.ViewModel.Editor.Sprites;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDE.Phone.Views.Editor.Sprites
{
    public partial class SpritesView : PhoneApplicationPage
    {
        private readonly SpritesViewModel _viewModel = ServiceLocator.Current.GetInstance<SpritesViewModel>();

        public SpritesView()
        {
            InitializeComponent();
            //_viewModel.PropertyChanged += ViewModelOnPropertyChanged;
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
