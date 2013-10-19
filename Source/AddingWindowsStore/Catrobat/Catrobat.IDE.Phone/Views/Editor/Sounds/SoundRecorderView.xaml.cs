using System.ComponentModel;
using System.Windows;
using Catrobat.IDE.Phone.ViewModel.Editor.Sounds;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDE.Phone.Views.Editor.Sounds
{
    public partial class SoundRecorderView : PhoneApplicationPage
    {
        private readonly SoundRecorderViewModel _viewModel = ServiceLocator.Current.GetInstance<SoundRecorderViewModel>();

        public SoundRecorderView()
        {
            InitializeComponent();
            _viewModel.PropertyChanged += SoundRecorderViewModel_OnPropertyChanged;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.ResetViewModelCommand.Execute(null);
            base.OnBackKeyPress(e);
        }

        private void SoundRecorderViewModel_OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "IsRecording")
            {
                if (_viewModel.IsRecording)
                {
                    RecordingAnimation.Begin();
                }
                else
                {
                    RecordingAnimation.Stop();
                }
            }
        }

        private void PlayButton_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.PlayPauseCommand.Execute(null);
        }
    }
}