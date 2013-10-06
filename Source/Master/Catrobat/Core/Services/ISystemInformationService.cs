using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.Core.Misc.Helpers
{
    public interface ISystemInformationService
    {
        string PlatformName { get; }

        string PlatformVersion { get; }

        string DeviceName { get; }

        int ScreenWidth { get; }

        int ScreenHeight { get; }

        string CurrentApplicationVersion { get; }
    }
}
