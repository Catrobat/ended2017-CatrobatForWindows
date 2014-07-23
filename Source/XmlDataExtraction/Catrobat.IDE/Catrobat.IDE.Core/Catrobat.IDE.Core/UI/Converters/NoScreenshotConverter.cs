using System;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.UI.Converters
{
    public class NoScreenshotConverter : IPortableValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                return value;
            }
            else
            {
                return ManualImageCache.NoScreenshotImage;

                //try
                //{
                //    var image = new PortableImage();
                //    using (var loader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
                //    {
                //        image.LoadFromResources(ResourceScope.IdePhone, "Content/Images/Screenshot/NoScreenshot.png");
                //    }
                //    return image.ImageSource;
                //}
                //catch (Exception)
                //{
                //    return null;
                //}
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // not used
            return null;
        }
    }
}