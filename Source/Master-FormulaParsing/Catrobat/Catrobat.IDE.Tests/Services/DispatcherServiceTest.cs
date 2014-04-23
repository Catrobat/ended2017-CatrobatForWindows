using System;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Tests.Services
{
    public class DispatcherServiceTest : IDispatcherService
    {
        public void RunOnMainThread(Action action, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            action.Invoke();
        }
    }
}
