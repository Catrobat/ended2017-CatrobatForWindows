using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Sounds;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Sounds
{
    public partial class SoundNameChooserView : PhoneApplicationPage
    {
        private readonly SoundRecorderViewModel _soundRecorderViewModel = ServiceLocator.Current.GetInstance<SoundRecorderViewModel>();

        public SoundNameChooserView()
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
            //DON'T RESET VIEWMODEL
        }

        private void TextBoxSoundName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _soundRecorderViewModel.SoundName = TextBoxSoundName.Text;
        }
    }
}