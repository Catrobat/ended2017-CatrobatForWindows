using System;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Store.Services
{
    public class DispatcherServicePhone : IDispatcherService
    {
        public void RunOnMainThread(Action action, DispatcherPriority priority)
        {
            throw new NotImplementedException();

            //Deployment.Current.Dispatcher.BeginInvoke(action);
        }
    }
}
