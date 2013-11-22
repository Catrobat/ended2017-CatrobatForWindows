using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core.Services
{
    public interface IPlayerLauncherService
    {
        Task LaunchPlayer(Project project, bool isLaunchedFromTile = false);

        Task LaunchPlayer(string projectName, bool isLaunchedFromTile = false);
    }
}
