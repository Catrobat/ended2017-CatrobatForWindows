using System.Runtime.CompilerServices;
using System.Windows;
using Catrobat.IDE.Phone.Annotations;
using System.ComponentModel;

namespace Catrobat.IDE.Phone.Controls.StatusIcons
{
  public partial class ProcessIcon : INotifyPropertyChanged
  {
    private bool _transformationIsRunning = false;

    #region Properties
    public bool IsProcessing
    {
      get { return (bool)GetValue(IsProcessingProperty); }
      set { SetValue(IsProcessingProperty, value); }
    }

    public static readonly DependencyProperty IsProcessingProperty = DependencyProperty.Register("IsProcessing", typeof(bool), typeof(ProcessIcon), new PropertyMetadata(IsProcessingChanged));

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
      Dispatcher.BeginInvoke(() =>
      {
        if (isProcessing)
        {
          imageInProcess.Visibility = Visibility.Visible;
          StartTransformationThread();
        }
        else
        {
          imageInProcess.Visibility = Visibility.Collapsed;
          StopTransformationThread();
        }
      });
    }

    #region TransformationThread

    public void StartTransformationThread()
    {
      Dispatcher.BeginInvoke(() => StoryboardAnimation.Begin());
    }

    public void StopTransformationThread()
    {
      Dispatcher.BeginInvoke(() => StoryboardAnimation.Begin());
    }

    public void UpdateTransformation()
    {
      while (_transformationIsRunning)
      {
        Dispatcher.BeginInvoke(() => StoryboardAnimation.Begin());
      }
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
