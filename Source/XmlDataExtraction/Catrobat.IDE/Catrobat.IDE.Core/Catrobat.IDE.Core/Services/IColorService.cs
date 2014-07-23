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
