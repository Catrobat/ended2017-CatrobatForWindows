using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Editor;

namespace Catrobat.IDEWindowsPhone.ViewModel.Editor.Costumes
{
    public enum ImageSize {Small, Medium, Large, FullSize}



    public class ImageSizeEntry
    {
        private static readonly Dictionary<ImageSize, int> _maxWidthHeights = new Dictionary<ImageSize, int>
        {
            {ImageSize.Small, 500},
            {ImageSize.Medium, 1000},
            {ImageSize.Large, 1500},
            {ImageSize.FullSize, 5000}
        };

        public static int GetNewImageWidth(ImageSize desiredSize, int width, int height)
        {
            int maxWidthHeight = Math.Max(width, height);
            int desiredMaxWidthHeight = _maxWidthHeights[desiredSize];

            if (maxWidthHeight < desiredMaxWidthHeight)
                return width;

            double scale = maxWidthHeight / (double) desiredMaxWidthHeight;

            int newWidth = (int) (width / scale);

            return newWidth;
        }

        public static int GetNewImageHeight(ImageSize desiredSize, int width, int height)
        {
            int maxWidthHeight = Math.Max(width, height);
            int desiredMaxWidthHeight = _maxWidthHeights[desiredSize];

            if (maxWidthHeight < desiredMaxWidthHeight)
                return height;

            double scale = maxWidthHeight / (double) desiredMaxWidthHeight;

            int newHeight = (int) (height / scale);

            return newHeight;
        }


        public ImageSize Size { get; set; }

        public string SizeName
        {
            get
            {
                switch (Size)
                {
                    case ImageSize.Small:
                        return EditorResources.ImageSizeSmall;
                    case ImageSize.Medium:
                        return EditorResources.ImageSizeMedium;
                    case ImageSize.Large:
                        return EditorResources.ImageSizeLarge;
                    case ImageSize.FullSize:
                        return EditorResources.ImageSizeFullSize;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
