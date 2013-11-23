using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Costumes;
using Catrobat.IDE.Core.ViewModel.Editor.Sounds;

namespace Catrobat.IDE.Store.Views.Editor.Sounds
{
    public sealed partial class ChangeSoundView : UserControl
    {
        private readonly ChangeSoundViewModel _viewModel =
            ServiceLocator.ViewModelLocator.ChangeSoundViewModel;

        public ChangeSoundView()
        {
            this.InitializeComponent();
        }

        private void TextBoxSoundName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SoundName = TextBoxSoundName.Text;
        }
    }
}
