using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Sounds;
using System.ComponentModel;
using Windows.UI.Xaml;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Sounds
{
    public partial class SoundRecorderView
    {
        private readonly SoundRecorderViewModel _viewModel = 
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).SoundRecorderViewModel;

        protected override ViewModelBase GetViewModel() { return _viewModel; }

        public SoundRecorderView()
        {
            InitializeComponent();
            _viewModel.PropertyChanged += SoundRecorderViewModel_OnPropertyChanged;
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