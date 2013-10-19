using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Sprites;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Sprites
{
    public partial class AddNewSpriteView : PhoneApplicationPage
    {
        private readonly AddNewSpriteViewModel _viewModel = ServiceLocator.Current.GetInstance<AddNewSpriteViewModel>();

        public AddNewSpriteView()
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