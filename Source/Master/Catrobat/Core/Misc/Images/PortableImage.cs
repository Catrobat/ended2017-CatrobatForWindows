using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core.Annotations;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Misc.Images
{
    public class PortableImage : INotifyPropertyChanged
    {
        private byte[] _data;
        private int _width;
        private int _height;

        internal byte[] Data
        {
            get { return _data; }
            private set
            {
                _data = value;
                RaisePropertyChanged(() => Data);
                RaisePropertyChanged(() => ImageSource);
            }
        }

        internal int Width
        {
            get { return _width; }
            private set { _width = value; }
        }

        internal int Height
        {
            get { return _height; }
            private set { _height = value; }
        }

        public object ImageSource
        {
            get
            {
                return Services.ServiceLocator.ImageSourceConversionService.ConvertToLocalImageSource(_data, _width, _height);
            }

            set
            {
                Services.ServiceLocator.ImageSourceConversionService.ConvertToBytes(value, out _data, out _height, out _height);
                RaisePropertyChanged(()=> ImageSource);
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
