using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.UI
{
    public class StaticTheme
    {
        public PortableSolidColorBrush AppBarBorderBrush { get { return new PortableSolidColorBrush("#FF000000"); } }

        public PortableSolidColorBrush ActionsBrush { get { return new PortableSolidColorBrush("#FFEBB613"); } }

        public PortableSolidColorBrush SoundsBrush { get { return new PortableSolidColorBrush("#FFE035D5"); } }
        public PortableSolidColorBrush CostumesBrush{ get { return new PortableSolidColorBrush("#FF8A018C"); } }
        public PortableSolidColorBrush ObjectsBrush{ get { return new PortableSolidColorBrush("#FF891D1D"); } }

        public PortableSolidColorBrush LooksBrickBrush{ get { return new PortableSolidColorBrush("#FF8A018C"); } }
        public PortableSolidColorBrush MotionBrickBrush{ get { return new PortableSolidColorBrush("#FF2E2BD7"); } }
        public PortableSolidColorBrush SoundBrickBrush{ get { return new PortableSolidColorBrush("#FFE035D5"); } }
        public PortableSolidColorBrush ControlBrickBrush{ get { return new PortableSolidColorBrush("#FFEBB613"); } }
        public PortableSolidColorBrush VariableBrickBrush{ get { return new PortableSolidColorBrush("#FF009900"); } }
        public PortableSolidColorBrush BrickBorderBrush{ get { return new PortableSolidColorBrush("#FFFFFFFF"); } }
    }
}
