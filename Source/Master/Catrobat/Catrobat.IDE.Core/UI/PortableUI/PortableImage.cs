using System;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.UI.PortableUI
{
    public sealed class PortableImage : INotifyPropertyChanged
    {
        private byte[] _data;
        private int _width;
        private int _height;
        private object _nativeImageSource;
        private MemoryStream _encodedData;

        public byte[] Data
        {
            get { return _data; }
            set
            {
                _data = value;
                RaisePropertyChanged(() => Data);
                RaisePropertyChanged(() => ImageSource);
            }
        }

        public MemoryStream EncodedData
        {
            get { return _encodedData; }
            set
            {
                _encodedData = value;
            }
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public object ImageSource
        {
            get
            {
                if (!IsLoaded)
                    return ServiceLocator.ImageSourceConversionService.ConvertFromEncodedStreeam(null);

                if (EncodedData != null && _nativeImageSource == null)
                    _nativeImageSource = ServiceLocator.ImageSourceConversionService.ConvertFromEncodedStreeam(EncodedData);

                if (_nativeImageSource != null)
                    return _nativeImageSource;



                if (_data != null)
                    return ServiceLocator.ImageSourceConversionService.ConvertToLocalImageSource(_data, _width, _height);

                return null;
            }

            set
            {
                Services.ServiceLocator.ImageSourceConversionService.ConvertToBytes(value, out _data, out _height, out _height);
                RaisePropertyChanged(() => ImageSource);
            }
        }

        public PortableImage()
        {

        }

        public PortableImage(byte[] data, int width, int height)
        {
            this.IsLoaded = true;
            _data = data;
            _width = width;
            _height = height;
        }

        public PortableImage(object nativeImageSource)
        {
            this.IsLoaded = true;
            _nativeImageSource = nativeImageSource;
        }

        public async void LoadAsync(string uri, string alternativeUri = null, bool loadFullImage = true)
        {
            IsLoading = true;

            using (var storage = StorageSystem.GetStorage())
            {
                PortableImage image = null;

                try
                {
                    if (loadFullImage)
                        image = await storage.LoadImageAsync(uri);
                    else
                        image = await storage.LoadImageThumbnailAsync(uri);

                    if (image == null && alternativeUri != null)
                    {
                        if (loadFullImage)
                            image = await storage.LoadImageAsync(alternativeUri);
                        else
                            image = await storage.LoadImageThumbnailAsync(alternativeUri);
                    }
                }
                catch (Exception)
                {

                }

                IsLoaded = true;
                IsLoading = false;

                if (image != null)
                {
                    EncodedData = image.EncodedData;
                    Data = image.Data;
                    Width = image.Width;
                    Height = image.Height;
                    _nativeImageSource = image._nativeImageSource;
                }

                RaisePropertyChanged(() => ImageSource);
                RaisePropertyChanged(() => Data);
                RaisePropertyChanged(() => Width);
                RaisePropertyChanged(() => Height);
                RaisePropertyChanged(() => IsLoaded);
            }
        }

        public bool IsLoaded { get; private set; }

        public bool IsLoading { get; private set; }

        public void WriateAsPng(string path)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                storage.SaveImage(path, this, true, ImageFormat.Png);
            }
        }

        public void WriteAsJpg(string path)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                storage.SaveImage(path, this, true, ImageFormat.Jpg);
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

        public void RaisePropertyChanged<T>(Expression<Func<T>> selector)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyHelper.GetPropertyName(selector)));
            }
        }
        #endregion

        public async Task LoadFromResources(ResourceScope scope, string path)
        {
            using (var loader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
            {
                _nativeImageSource = await loader.LoadImageAsync(scope, path);
            }
        }

        public bool IsEmpty
        {
            get
            {
                return Data == null && _nativeImageSource == null;
            }
        }
    }
}
