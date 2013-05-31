using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.ComponentModel;

namespace Catrobat.IDEWindowsPhone.Controls.StatusIcons
{
  public partial class ProcessIcon : UserControl, INotifyPropertyChanged
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

    public event PropertyChangedEventHandler PropertyChanged;

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

    protected virtual void OnPropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
