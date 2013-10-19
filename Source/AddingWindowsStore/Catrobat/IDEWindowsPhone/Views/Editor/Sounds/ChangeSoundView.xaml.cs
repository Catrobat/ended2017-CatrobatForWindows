using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Sounds;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Sounds
{
    public partial class ChangeSoundView : PhoneApplicationPage
    {
        private readonly ChangeSoundViewModel _viewModel = ServiceLocator.Current.GetInstance<ChangeSoundViewModel>();

        public ChangeSoundView()
        {
            InitializeComponent();

            Dispatcher.BeginInvoke(() =>
                {
                    TextBoxSoundName.Focus();
                    TextBoxSoundName.SelectAll();
                });
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.ResetViewModelCommand.Execute(null);
            base.OnNavigatedFrom(e);
        }

        private void TextBoxSoundName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SoundName = TextBoxSoundName.Text;
        }
    }
}