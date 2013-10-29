using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Sounds;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Editor.Sounds
{
    public partial class ChangeSoundView : PhoneApplicationPage
    {
        private readonly ChangeSoundViewModel _viewModel = 
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).ChangeSoundViewModel;

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
            _viewModel.GoBackCommand.Execute(null);
        }

        private void TextBoxSoundName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SoundName = TextBoxSoundName.Text;
        }
    }
}