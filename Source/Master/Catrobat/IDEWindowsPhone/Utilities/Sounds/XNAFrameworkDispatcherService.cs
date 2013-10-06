using System;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Xna.Framework;

namespace Catrobat.IDEWindowsPhone.Utilities.Sounds
{
    public class XnaFrameworkDispatcherService : IApplicationService
    {
        private readonly DispatcherTimer _frameworkDispatcherTimer;

        public XnaFrameworkDispatcherService()
        {
            _frameworkDispatcherTimer = new DispatcherTimer {Interval = TimeSpan.FromTicks(333333)};
            _frameworkDispatcherTimer.Tick += frameworkDispatcherTimer_Tick;
            FrameworkDispatcher.Update();
        }

        private void frameworkDispatcherTimer_Tick(object sender, EventArgs e)
        {
            FrameworkDispatcher.Update();
        }

        void IApplicationService.StartService(ApplicationServiceContext context)
        {
            _frameworkDispatcherTimer.Start();
        }

        void IApplicationService.StopService()
        {
            _frameworkDispatcherTimer.Stop();
        }
    }
}