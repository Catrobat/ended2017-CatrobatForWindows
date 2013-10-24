using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Annotations;

namespace Catrobat.IDE.Core.Services
{
    public enum DispatcherPriority{Low, Medium, High}

    public interface IDispatcherService
    {
        void RunOnMainThread(Action action, DispatcherPriority priority = DispatcherPriority.Medium);
    }
}
