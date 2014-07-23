using System;

namespace Catrobat.IDE.Core.Services
{
    public enum DispatcherPriority{Low, Normal, High, Idle}

    public interface IDispatcherService
    {
        void RunOnMainThread(Action action, DispatcherPriority priority = DispatcherPriority.Normal);
    }
}
