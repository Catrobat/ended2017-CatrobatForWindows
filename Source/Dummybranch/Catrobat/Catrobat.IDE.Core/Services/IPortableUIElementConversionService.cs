namespace Catrobat.IDE.Core.Services
{
    public interface IPortableUIElementConversionService
    {
        object ConvertToNativeUIElement(object portableUIElement);

        object ConvertToPortableUIElement(object nativeUIElement);
    }
}
