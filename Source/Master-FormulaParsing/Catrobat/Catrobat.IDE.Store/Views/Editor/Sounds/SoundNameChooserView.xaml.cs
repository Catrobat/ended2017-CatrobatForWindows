using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel.Editor.Sounds;

namespace Catrobat.IDE.Store.Views.Editor.Sounds
{
    public sealed partial class SoundNameChooserView : Page
    {
        private readonly SoundNameChooserViewModel _viewModel =
            ServiceLocator.ViewModelLocator.SoundNameChooserViewModel;

        public SoundNameChooserView()
        {
            this.InitializeComponent();
        }

        private void TextBoxSoundName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SoundName = TextBoxSoundName.Text;
        }
    }
}
