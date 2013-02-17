using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using System.ComponentModel;
using Microsoft.Phone.Info;
using System.Diagnostics;

namespace KBB.Mobile.Controls
{    /// <summary>
    /// This is a tool that will monitor the memory usage of your application. It provides
    /// various functionality that helps you identify memory threshholds, including
    /// tracking of current and peak usage, properties that let you inspect memory usage
    /// at a given point in time, events that let you to respond to memory usage
    /// warnings, defining thresholds for warning and critical memory usage
    /// and a visualization control that shows memory usage on screen while the application
    /// is running.
    /// </summary>
    public sealed class MemoryMonitor : IDisposable
    {
        #region Private members
        private bool _isEnabled;
        private bool _showVisualization;
        private bool _isInitialized;
        private bool _currentWarningExceeded;
        private bool _currentCriticalExceeded;
        private DispatcherTimer _timer;
        private Popup _popup = new Popup();
        private TextBlock _txtAppCurrent = new TextBlock();
        private TextBlock _txtTotalMemory = new TextBlock();
        private TextBlock _txtAppPeak = new TextBlock();
        #endregion

        #region Public properties
        /// <summary>
        /// How much memory is available on the device. Make decisions based
        /// on whether the device meets or exceeds minimum requirements.
        /// </summary>
        public long TotalMemory { get; private set; }
        /// <summary>
        /// How much memory your application is currently using.
        /// </summary>
        public long CurrentMemory { get; private set; }
        /// <summary>
        /// The most memory your application has used.
        /// </summary>
        public long PeakMemory { get; private set; }
        /// <summary>
        /// How much memory the application can use before it starts to become a problem. 
        /// Recommended value is 60MB.
        /// </summary>
        public long WarningMemoryThreshold { get; set; }
        /// <summary>
        /// How much memory the application can use before it is in danger of being closed by the system. 
        /// Recommended value is 80MB, actual is somewhere around 90MB, more depending on the device total memory.
        /// </summary>
        public long CriticalMemoryThreshold { get; set; }
        /// <summary>
        /// Indicates whether or not an alert will be presented when the memory warning threshold has been exceeded.
        /// </summary>
        public bool AlertWhenWarningThresholdsExceeded { get; set; }
        /// <summary>
        /// Indicates whether or not an alert will be presented when the max memory threshold has been exceeded.
        /// </summary>
        public bool AlertWhenCriticalThresholdsExceeded { get; set; }
        /// <summary>
        /// Indicates whether or not the peak memory usage of the application has exceeded the warning threshold value.
        /// </summary>
        public bool PeakWarningExceeded { get; private set; }
        /// <summary>
        /// Indicates whether or not the current memory usage of the application has exceeded the warning threshold value.
        /// </summary>
        public bool CurrentWarningExceeded
        {
            get { return this._currentWarningExceeded; }
            private set
            {
                if (this._currentWarningExceeded != value)
                {
                    this._currentWarningExceeded = value;
                    this.OnWarningMemoryThresholdExceeded();
                }
            }
        }
        /// <summary>
        /// Indicates whether or not the peak memory usage of the application has exceeded the critical threshold value.
        /// </summary>
        public bool PeakCriticalExceeded { get; private set; }
        /// <summary>
        /// Indicates whether or not the current memory usage of the application has exceeded the critical threshold value.
        /// </summary>
        public bool CurrentCriticalExceeded
        {
            get { return this._currentCriticalExceeded; }
            private set
            {
                if (this._currentCriticalExceeded != value)
                {
                    this._currentCriticalExceeded = value;
                    this.OnCriticalMemoryThresholdExceeded();
                }
            }
        }
        /// <summary>
        /// Indicates whether or not the <see cref="MemoryMonitor"/> will monitor memory usage of the application.
        /// </summary>
        public bool IsEnabled
        {
            get { return this._isEnabled; }
            set
            {
                if (this._isEnabled != value)
                {
                    this._isEnabled = value;
                    if (this._isEnabled)
                        this.startTimer();
                    else
                        this.stopTimer();
                }
            }
        }
        /// <summary>
        /// Indicates whether or not the <see cref="MemoryMonitor"/> will display a visualization of memory usage of the application.
        /// </summary>
        public bool ShowVisualization
        {
            get { return this._showVisualization; }
            set
            {
                if (this._showVisualization != value)
                {
                    this._showVisualization = value;
                    if (this._showVisualization)
                        this.show();
                    else
                        this.hide();
                }
            }
        }
        #endregion

        #region Constructors
        private MemoryMonitor()
        {
            this.WarningMemoryThreshold = 60;
            this.CriticalMemoryThreshold = 80;

            if (this.IsEnabled)
                this.startTimer();
            if (this.ShowVisualization)
                this.show();
        }
        /// <summary>
        /// Create a new <see cref="MemoryMonitor"/> instance with the specified initial values.
        /// </summary>
        /// <param name="isEnabled">Indicates whether or not the <see cref="MemoryMonitor"/> will monitor memory usage of the application</param>
        /// <param name="showVisualization">Indicates whether or not the <see cref="MemoryMonitor"/> will display a visualization of memory usage of the application.</param>
        public MemoryMonitor(bool isEnabled, bool showVisualization)
            : this()
        {
            this.IsEnabled = isEnabled;
            this.ShowVisualization = showVisualization;
        }
        #endregion

        #region Events
        /// <summary>
        /// This event is raised when the current memory usage of the application exceeds the critical threshold.
        /// </summary>
        public event EventHandler CriticalMemoryThresholdExceeded;
        private void OnCriticalMemoryThresholdExceeded()
        {
            if (this.CriticalMemoryThresholdExceeded != null)
                this.CriticalMemoryThresholdExceeded(this, EventArgs.Empty);
        }
        /// <summary>
        /// This event is raised when the current memory usage of the application exceeds the warning threshold.
        /// </summary>
        public event EventHandler WarningMemoryThresholdExceeded;
        private void OnWarningMemoryThresholdExceeded()
        {
            if (this.WarningMemoryThresholdExceeded != null)
                this.WarningMemoryThresholdExceeded(this, EventArgs.Empty);
        }
        #endregion

        #region Private methods
        private void _timer_Tick(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerAsync();
        }
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            long deviceTotalMemory = (long)DeviceExtendedProperties.GetValue("DeviceTotalMemory");
            long applicationCurrentMemoryUsage = (long)DeviceExtendedProperties.GetValue("ApplicationCurrentMemoryUsage");
            long applicationPeakMemoryUsage = (long)DeviceExtendedProperties.GetValue("ApplicationPeakMemoryUsage");

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                this.TotalMemory = deviceTotalMemory / 1024 / 1024;
                this.CurrentMemory = applicationCurrentMemoryUsage / 1024 / 1024;
                this.PeakMemory = applicationPeakMemoryUsage / 1024 / 1024;

                this._txtAppCurrent.Text = this.CurrentMemory.ToString("N2");
                this._txtTotalMemory.Text = this.TotalMemory.ToString("N2");
                this._txtAppPeak.Text = this.PeakMemory.ToString("N2");

                if (this.CurrentMemory > this.CriticalMemoryThreshold)
                {
                    this._txtAppCurrent.Foreground = new SolidColorBrush(Colors.Red);
                    if (!this.CurrentCriticalExceeded)
                    {
                        this.CurrentCriticalExceeded = true;
                        if (this.AlertWhenCriticalThresholdsExceeded)
                            MessageBox.Show(String.Format("Current memory usage threshold for this application has exceeded the critical threshold of {0} MB.", this.CriticalMemoryThreshold));
                    }
                }
                else
                {
                    this.CurrentCriticalExceeded = false;
                    this._txtAppCurrent.Foreground = new SolidColorBrush(Colors.White);
                }

                if (this.CurrentMemory > this.WarningMemoryThreshold)
                {
                    if (!this.CurrentCriticalExceeded)
                        this._txtAppCurrent.Foreground = new SolidColorBrush(Colors.Yellow);

                    if (!this.CurrentWarningExceeded)
                    {
                        this.CurrentWarningExceeded = true;
                        if (this.AlertWhenWarningThresholdsExceeded)
                            MessageBox.Show(String.Format("Current memory usage threshold for this application has exceeded the warning threshold of {0} MB.", this.WarningMemoryThreshold));
                    }
                }
                else
                {
                    this.CurrentWarningExceeded = false;
                    this._txtAppCurrent.Foreground = new SolidColorBrush(Colors.White);
                }

                if (this.PeakMemory > this.CriticalMemoryThreshold)
                {
                    this._txtAppPeak.Foreground = new SolidColorBrush(Colors.Red);
                    if (!this.PeakCriticalExceeded)
                    {
                        this.PeakCriticalExceeded = true;
                        if (this.AlertWhenCriticalThresholdsExceeded)
                            MessageBox.Show(String.Format("Peak memory usage threshold for this application has exceeded the critical threshold of {0} MB.", this.CriticalMemoryThreshold));
                    }
                }
                else
                {
                    this.PeakCriticalExceeded = false;
                    this._txtAppPeak.Foreground = new SolidColorBrush(Colors.White);
                }

                if (this.PeakMemory > this.WarningMemoryThreshold)
                {
                    if (!this.PeakCriticalExceeded)
                        this._txtAppPeak.Foreground = new SolidColorBrush(Colors.Yellow);
                    if (!this.PeakWarningExceeded)
                    {
                        this.PeakWarningExceeded = true;
                        if (this.AlertWhenWarningThresholdsExceeded)
                            MessageBox.Show(String.Format("Peak memory usage threshold for this application has exceeded the warning threshold of {0} MB.", this.WarningMemoryThreshold));
                    }
                }
                else
                {
                    this.PeakWarningExceeded = false;
                    this._txtAppPeak.Foreground = new SolidColorBrush(Colors.White);
                }
            });
        }

        private void initializeVisualization()
        {
            try
            {
                Border child = new Border();
                child.Width = 480;
                child.Background = new SolidColorBrush(Color.FromArgb(200, 50, 50, 50));
                child.IsHitTestVisible = false;

                StackPanel childPanel = new StackPanel();
                childPanel.Margin = new Thickness(0, 30, 0, 0);
                childPanel.Orientation = Orientation.Horizontal;
                child.Child = childPanel;
                childPanel.IsHitTestVisible = false;

                SolidColorBrush white = new SolidColorBrush(Colors.White);

                TextBlock lblTotalMemory = new TextBlock();
                childPanel.Children.Add(lblTotalMemory);
                lblTotalMemory.Text = "Device: ";
                lblTotalMemory.SetValue(Grid.ColumnProperty, 0);
                lblTotalMemory.SetValue(Grid.RowProperty, 0);
                lblTotalMemory.FontSize = 17;
                lblTotalMemory.Foreground = white;
                lblTotalMemory.IsHitTestVisible = false;

                childPanel.Children.Add(_txtTotalMemory);
                _txtTotalMemory.Text = this.TotalMemory.ToString();
                _txtTotalMemory.SetValue(Grid.ColumnProperty, 1);
                _txtTotalMemory.SetValue(Grid.RowProperty, 0);
                _txtTotalMemory.FontSize = 17;
                _txtTotalMemory.FontWeight = FontWeights.Bold;
                _txtTotalMemory.Foreground = white;
                _txtTotalMemory.Margin = new Thickness(0, 0, 20, 0);
                _txtTotalMemory.IsHitTestVisible = false;

                TextBlock lblAppCurrent = new TextBlock();
                childPanel.Children.Add(lblAppCurrent);
                lblAppCurrent.Text = "Current: ";
                lblAppCurrent.SetValue(Grid.ColumnProperty, 0);
                lblAppCurrent.SetValue(Grid.RowProperty, 1);
                lblAppCurrent.FontSize = 17;
                lblAppCurrent.Foreground = white;
                lblAppCurrent.IsHitTestVisible = false;

                childPanel.Children.Add(_txtAppCurrent);
                _txtAppCurrent.Text = this.CurrentMemory.ToString();
                _txtAppCurrent.SetValue(Grid.ColumnProperty, 1);
                _txtAppCurrent.SetValue(Grid.RowProperty, 1);
                _txtAppCurrent.FontSize = 17;
                _txtAppCurrent.FontWeight = FontWeights.Bold;
                _txtAppCurrent.Foreground = white;
                _txtAppCurrent.Margin = new Thickness(0, 0, 20, 0);
                _txtAppCurrent.IsHitTestVisible = false;

                TextBlock lblAppPeak = new TextBlock();
                childPanel.Children.Add(lblAppPeak);
                lblAppPeak.Text = "Peak: ";
                lblAppPeak.SetValue(Grid.ColumnProperty, 0);
                lblAppPeak.SetValue(Grid.RowProperty, 2);
                lblAppPeak.FontSize = 17;
                lblAppPeak.Foreground = white;
                lblAppPeak.IsHitTestVisible = false;

                childPanel.Children.Add(_txtAppPeak);
                _txtAppPeak.Text = this.PeakMemory.ToString();
                _txtAppPeak.SetValue(Grid.ColumnProperty, 1);
                _txtAppPeak.SetValue(Grid.RowProperty, 2);
                _txtAppPeak.FontSize = 17;
                _txtAppPeak.FontWeight = FontWeights.Bold;
                _txtAppPeak.Foreground = white;
                _txtAppPeak.Margin = new Thickness(0, 0, 20, 0);
                _txtAppPeak.IsHitTestVisible = false;

                _popup.Child = child;
                _popup.IsHitTestVisible = false;
                this._isInitialized = true;
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }
        }

        private void startTimer()
        {
            try
            {
                this._timer = new DispatcherTimer();
                this._timer.Interval = TimeSpan.FromSeconds(2);
                this._timer.Tick += new EventHandler(_timer_Tick);
                this._timer.Start();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }
        private void stopTimer()
        {
            if (this._timer != null)
            {
                if (this._timer.IsEnabled)
                    this._timer.Stop();
                this._timer = null;
            }
        }

        private void show()
        {
            if (!this._isInitialized)
                this.initializeVisualization();
            this._popup.IsOpen = true;
        }
        private void hide()
        {
            this._popup.IsOpen = false;
        }
        #endregion

        #region IDisposable implementation
        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        public void Dispose()
        {
            dispose(true);
        }
        private void dispose(bool disposing)
        {
            if (disposing)
            {
                this.stopTimer();
            }
        }
        #endregion
    }
}
