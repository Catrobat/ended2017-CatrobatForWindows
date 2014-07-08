using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.UI
{
    public interface IPlayPauseButton
    {
        PlayPauseButtonState State { get; set; }
    }
}
