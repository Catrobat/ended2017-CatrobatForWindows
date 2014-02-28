using System;
using System.Windows;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Phone.Services
{
    public class DispatcherServicePhone : IDispatcherService
    {
        public void RunOnMainThread(Action action, DispatcherPriority priority)
        {
            Deployment.Current.Dispatcher.BeginInvoke(action);
        }
    }
}
