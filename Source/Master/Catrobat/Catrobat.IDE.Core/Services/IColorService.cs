using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.Services
{
    public interface IColorConversionService
    {
        object ConvertToLocalSolidColorBrush(PortableSolidColorBrush portableSolidColorBrush);

        PortableSolidColorBrush ConvertFromLocalSolidColorBrush(object localSolidColorBrush);

        object ConvertToLocalSolidColor(PortableSolidColorBrush portableSolidColor);
    }
}
