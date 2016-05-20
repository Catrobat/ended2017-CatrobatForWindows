using Windows.UI.Xaml;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Sounds;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Sounds
{
    public partial class SoundNameChooserView
    {
        private readonly SoundNameChooserViewModel _viewModel =
            ServiceLocator.ViewModelLocator.SoundNameChooserViewModel;

        public SoundNameChooserView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            TextBoxSoundName.Focus(FocusState.Keyboard);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TextBoxSoundName.Focus(FocusState.Keyboard);
            base.OnNavigatedTo(e);
        }

        private void TextBoxSoundName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SoundName = TextBoxSoundName.Text;
        }
    }
}