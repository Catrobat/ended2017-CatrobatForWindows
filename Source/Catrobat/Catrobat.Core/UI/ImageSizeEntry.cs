using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.Core.Resources.Localization;
using System.Diagnostics;

namespace Catrobat.IDE.Core.UI
{
    public enum ImageSize {Small, Medium, Large, FullSize}

    public sealed class ImageSizeEntry : ISelectable, INotifyPropertyChanged
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
                RaisePropertyChanged(() => Size);
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

                RaisePropertyChanged(() => Dimension);
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
                RaisePropertyChanged(() => NewWidth);
            }
        }

        public int NewHeight
        {
            get { return _newHeight; }
            set
            {
                _newHeight = value;
                RaisePropertyChanged(() => NewHeight);
            }
        }

        public int Percentage
        {
            get { return _percentage; }
            set
            {
                _percentage = value;
                RaisePropertyChanged(() => Percentage);
            }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                RaisePropertyChanged(() => IsVisible);
            }
        }

        public string SizeName
        {
            get
            {
                switch (Size)
                {
                    case ImageSize.Small:
                        return AppResourcesHelper.Get("Editor_ImageSizeSmall");
                    case ImageSize.Medium:
                        return AppResourcesHelper.Get("Editor_ImageSizeMedium");
                    case ImageSize.Large:
                        return AppResourcesHelper.Get("Editor_ImageSizeLarge");
                    case ImageSize.FullSize:
                        return AppResourcesHelper.Get("Editor_ImageSizeFullSize");
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }

        #region INotifyPropertyChanged region

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged<T>(Expression<Func<T>> selector)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(
                    PropertyHelper.GetPropertyName(selector)));
            }
        }

        #endregion
    }
}
