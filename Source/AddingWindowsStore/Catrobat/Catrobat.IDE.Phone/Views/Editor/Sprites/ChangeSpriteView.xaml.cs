using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDE.Phone.ViewModel.Editor.Sprites;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDE.Phone.Views.Editor.Sprites
{
    public partial class ChangeSpriteView : PhoneApplicationPage
    {
        private readonly ChangeSpriteViewModel _viewModel = ServiceLocator.Current.GetInstance<ChangeSpriteViewModel>();

        public ChangeSpriteView()
        {
            InitializeComponent();

            Dispatcher.BeginInvoke(() =>
                {
                    TextBoxSpriteName.Focus();
                    TextBoxSpriteName.SelectAll();
                });
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.ResetViewModelCommand.Execute(null);
            base.OnNavigatedFrom(e);
        }

        private void TextBoxSpriteName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SpriteName = TextBoxSpriteName.Text;
        }
    }
}