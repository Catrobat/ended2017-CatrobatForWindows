using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Phone.Utilities
{
    public class AsyncRunner
    {
        public static Task RunAsyncOnMainThread(Action action)
        {
            var semaphore = new BSemaphore(0, 1);

            ServiceLocator.DispatcherService.RunOnMainThread(async () =>
            {
                action.Invoke();
                semaphore.Release();
            });

            return Task.Run(() => semaphore.WaitOne());
        }
    }
}
