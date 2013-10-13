using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services.Data;

namespace Catrobat.IDE.Core.UI
{
    public enum ImageSize {Small, Medium, Large, FullSize}

    public sealed class ImageSizeEntry
    {
        private static readonly Dictionary<ImageSize, int> MaxWidthHeights = new Dictionary<ImageSize, int>
        {
            {ImageSize.Small, 400},
            {ImageSize.Medium, 800},
            {ImageSize.Large, 1200},
            {ImageSize.FullSize, 5000}
        };

        private int _newWidth;
        private int _newHeight;
        private ImageSize _size;
        private ImageDimension _dimension;
        private bool _isVisible;
        private int _percentage;

        public static int GetNewImageWidth(ImageSize desiredSize, int width, int height)
        {
            int maxWidthHeight = Math.Max(width, height);
            int desiredMaxWidthHeight = MaxWidthHeights[desiredSize];

            if (maxWidthHeight < desiredMaxWidthHeight)
                return width;

            double scale = desiredMaxWidthHeight / (double)maxWidthHeight;

            int newWidth = (int) (width * scale);

            return newWidth;
        }

        public static int GetNewImageHeight(ImageSize desiredSize, int width, int height)
        {
            int maxWidthHeight = Math.Max(width, height);
            int desiredMaxWidthHeight = MaxWidthHeights[desiredSize];

            if (maxWidthHeight < desiredMaxWidthHeight)
                return height;

            double scale = desiredMaxWidthHeight / (double)maxWidthHeight;

            int newHeight = (int) (height * scale);

            return newHeight;
        }

        public static int GetNewImagePercentage(ImageSize desiredSize, int width, int height)
        {
            int newWidth = GetNewImageWidth(desiredSize, width, height);
            int newHeight = GetNewImageHeight(desiredSize, width, height);

            return (int)Math.Round((newWidth * (double)newHeight) / (width * (double)height) * 100);
        }


        public ImageSize Size
        {
            get { return _size; }
            set
            {
                _size = value;
                RaisePropertyChanged();
            }
        }

        public ImageDimension Dimension
        {
            get { return _dimension; }
            set
            {
                _dimension = value;
                NewWidth = GetNewImageWidth(Size, Dimension.Width, Dimension.Height);
                NewHeight = GetNewImageHeight(Size, Dimension.Width, Dimension.Height);
                Percentage = GetNewImagePercentage(Size, Dimension.Width, Dimension.Height);
                UpdateVisibility();

                RaisePropertyChanged();
            }
        }

        private void UpdateVisibility()
        {
            IsVisible = (Dimension.Height != NewHeight && Dimension.Width != NewWidth);

            if (Size == ImageSize.FullSize)
                IsVisible = true;
        }

        public int NewWidth
        {
            get { return _newWidth; }
            set
            {
                _newWidth = value;
                RaisePropertyChanged();
            }
        }

        public int NewHeight
        {
            get { return _newHeight; }
            set
            {
                _newHeight = value; 
                RaisePropertyChanged();
            }
        }

        public int Percentage
        {
            get { return _percentage; }
            set
            {
                _percentage = value;
                RaisePropertyChanged();
            }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                RaisePropertyChanged();
            }
        }

        public string SizeName
        {
            get
            {
                switch (Size)
                {
                    case ImageSize.Small:
                        return AppResources.Editor_ImageSizeSmall;
                    case ImageSize.Medium:
                        return AppResources.Editor_ImageSizeMedium;
                    case ImageSize.Large:
                        return AppResources.Editor_ImageSizeLarge;
                    case ImageSize.FullSize:
                        return AppResources.Editor_ImageSizeFullSize;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        # endregion
    }
}
