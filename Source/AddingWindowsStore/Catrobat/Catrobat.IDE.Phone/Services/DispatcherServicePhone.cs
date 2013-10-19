using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
