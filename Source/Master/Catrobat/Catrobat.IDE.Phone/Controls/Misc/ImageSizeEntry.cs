using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.Services.Data;
using Catrobat.IDE.Phone.Content.Localization;

namespace Catrobat.IDE.Phone.Controls.Misc
{
    public enum ImageSize {Small, Medium, Large, FullSize}



    public class ImageSizeEntry
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
        private ImageDimention _dimention;
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

        public ImageDimention Dimention
        {
            get { return _dimention; }
            set
            {
                _dimention = value;
                NewWidth = GetNewImageWidth(Size, Dimention.Width, Dimention.Height);
                NewHeight = GetNewImageHeight(Size, Dimention.Width, Dimention.Height);
                Percentage = GetNewImagePercentage(Size, Dimention.Width, Dimention.Height);
                UpdateVisibility();

                RaisePropertyChanged();
            }
        }

        private void UpdateVisibility()
        {
            IsVisible = (Dimention.Height != NewHeight && Dimention.Width != NewWidth);

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
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        # endregion
    }
}
