using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDE.Phone.ViewModel.Editor.Sounds;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDE.Phone.Views.Editor.Sounds
{
    public partial class SoundNameChooserView : PhoneApplicationPage
    {
        private readonly SoundNameChooserViewModel _viewModel = ServiceLocator.Current.GetInstance<SoundNameChooserViewModel>();

        public SoundNameChooserView()
        {
            InitializeComponent();

            Dispatcher.BeginInvoke(() =>
                {
                    TextBoxSoundName.Focus();
                    TextBoxSoundName.SelectAll();
                });
        }

        private void TextBoxSoundName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SoundName = TextBoxSoundName.Text;
        }
    }
}