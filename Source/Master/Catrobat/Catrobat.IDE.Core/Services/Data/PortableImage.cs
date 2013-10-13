using System;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Services.Data
{
    public sealed class PortableImage : INotifyPropertyChanged
    {
        private byte[] _data;
        private int _width;
        private int _height;

        public byte[] Data
        {
            get { return _data; }
            private set
            {
                _data = value;
                RaisePropertyChanged(() => Data);
                RaisePropertyChanged(() => ImageSource);
            }
        }

        public int Width
        {
            get { return _width; }
            private set { _width = value; }
        }

        public int Height
        {
            get { return _height; }
            private set { _height = value; }
        }

        public object ImageSource
        {
            get
            {
                if (_nativeImageSource != null)
                    return _nativeImageSource;

                return Services.ServiceLocator.ImageSourceConversionService.ConvertToLocalImageSource(_data, _width, _height);
            }

            set
            {
                Services.ServiceLocator.ImageSourceConversionService.ConvertToBytes(value, out _data, out _height, out _height);
                RaisePropertyChanged(()=> ImageSource);
            }
        }

        public PortableImage()
        {
            
        }

        public PortableImage(byte[] data, int width, int height)
        {
            _data = data;
            _width = width;
            _height = height;
        }

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


        private object _nativeImageSource;
        public void LoadFromResources(ResourceScope scope, string path)
        {
            using (var loader = ServiceLocator.ResourceLoaderFactory.CreateResourceLoader())
            {
                _nativeImageSource = loader.LoadImage(scope, path);

                //object portableImage = loader.LoadImage(scope, path);

                //if (portableImage != null)
                //{
                //    this.Data = portableImage.Data;
                //    this.Height = portableImage.Height;
                //    this.Width = portableImage.Width;
                //}
            }
        }
    }
}
