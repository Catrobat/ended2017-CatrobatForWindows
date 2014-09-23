using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Utilities.Helpers;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.UI.PortableUI
{
    public sealed class PortableImage : INotifyPropertyChanged
    {
        private int _width;
        private int _height;
        private object _nativeImageSource;
        private Stream _encodedData;
        private bool _isEmpty= false;

        public Stream EncodedData
        {
            get { return _encodedData; }
            set
            {
                if (value != null)
                    IsLoaded = true;

                _encodedData = value;
            }
        }

        string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                if (value != null)
                {
                    string path = Path.Combine("ms-appdata:///local", value);
                    _imagePath = path.Replace('\\', '/');
                    //RaisePropertyChanged(() => ImagePath);
                    ImageUri = new Uri(_imagePath);
                }
            }
        }

        Uri _imageUri;
        public Uri ImageUri
        {
            get { return _imageUri; }
            set
            {
                _imageUri = value;
                RaisePropertyChanged(() => ImageUri);
            }
        }

        public bool IsEmpty
        {
            get { return _isEmpty; }
            set { _isEmpty = value; }
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
                if (EncodedData != null)
                    IsLoaded = true;

                if (!IsLoaded)
                    return ServiceLocator.ImageSourceConversionService.ConvertFromEncodedStream(null, 0, 0);

                //if (EncodedData != null && _nativeImageSource == null)
                //    _nativeImageSource = ServiceLocator.ImageSourceConversionService.
                //        ConvertFromEncodedStream(EncodedData);

                if (_nativeImageSource != null)
                    return _nativeImageSource;

                return null;
            }

            set
            {
                RaisePropertyChanged(() => ImageSource);
            }
        }

        public PortableImage()
        {

        }

        public PortableImage(int width, int height)
        {
            this.IsLoaded = true;
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
            loadFullImage = true; // never try to load thumbnails
            
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
                    Width = image.Width;
                    Height = image.Height;
                    ImageUri = image.ImageUri;

                    if (image._nativeImageSource != null)
                    {
                        _nativeImageSource = image._nativeImageSource;
                    }
                    //else
                    //{
                    //    _nativeImageSource = await ServiceLocator.ImageSourceConversionService.
                    //        ConvertFromEncodedStream(EncodedData, Width, Height);
                    //}
                }

                _isEmpty = _encodedData == null && _nativeImageSource == null;

                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    RaisePropertyChanged(() => ImageSource);
                    RaisePropertyChanged(() => IsEmpty);
                    RaisePropertyChanged(() => Width);
                    RaisePropertyChanged(() => Height);
                    RaisePropertyChanged(() => IsLoaded);
                });
            }
        }

        private bool IsLoaded { get; set; }

        private bool IsLoading { get; set; }

        public async Task WriteAsPng(string path)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                await storage.SaveImageAsync(path, this, true, ImageFormat.Png);
            }
        }

        public async Task WriteAsJpg(string path)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                await storage.SaveImageAsync(path, this, true, ImageFormat.Jpg);
            }
        }

        public async Task LoadFromResources(ResourceScope scope, string path)
        {
            using (var loader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
            {
                var image = await loader.LoadImageAsync(scope, path);

                _nativeImageSource = image.ImageSource;
                _encodedData = image._encodedData;
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


    }
}
