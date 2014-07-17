using System.Windows.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Sounds;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Editor.Sounds
{
    public partial class SoundNameChooserView : PhoneApplicationPage
    {
        private readonly SoundNameChooserViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).SoundNameChooserViewModel;

        public SoundNameChooserView()
        {
            InitializeComponent();

            Dispatcher.BeginInvoke(() =>
                {
                    TextBoxSoundName.Focus();
                    TextBoxSoundName.SelectAll();
                });
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
        }

        private void TextBoxSoundName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SoundName = TextBoxSoundName.Text;
        }
    }
}