using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.WindowsPhone.Controls
{
    public partial class ProcessIcon : INotifyPropertyChanged
    {
        #region Properties

        public bool IsProcessing
        {
            get { return (bool)GetValue(IsProcessingProperty); }
            set { SetValue(IsProcessingProperty, value); }
        }

        public static readonly DependencyProperty IsProcessingProperty =
            DependencyProperty.Register("IsProcessing", typeof(bool), 
            typeof(ProcessIcon), new PropertyMetadata(true, IsProcessingChanged));

        private static void IsProcessingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ProcessIcon)d).OnStateChanged((bool)e.NewValue);
        }

        protected virtual void OnStateChanged(bool isProcessing)
        {
            ProcessingStateChanged(isProcessing);
        }

        #endregion

        public ProcessIcon()
        {
            InitializeComponent();
        }

        private void ProcessingStateChanged(bool isProcessing)
        {
            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                if (isProcessing)
                {
                    ImageInProcess.Visibility = Visibility.Visible;
                    StartTransformationThread();
                }
                else
                {
                    ImageInProcess.Visibility = Visibility.Collapsed;
                    StopTransformationThread();
                }
            });
        }

        #region TransformationThread

        public void StartTransformationThread()
        {
            ServiceLocator.DispatcherService.RunOnMainThread(() => StoryboardAnimation.Begin());
        }

        public void StopTransformationThread()
        {
            ServiceLocator.DispatcherService.RunOnMainThread(() => StoryboardAnimation.Stop());
        }

        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
