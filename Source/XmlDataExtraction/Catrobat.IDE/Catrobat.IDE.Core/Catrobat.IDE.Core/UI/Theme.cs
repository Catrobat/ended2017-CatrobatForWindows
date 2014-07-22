using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.UI
{
    public class Theme : INotifyPropertyChanged
    {
        private readonly string _backgroundPath;
        private readonly string _croppedPath;

        private PortableImage _background;

        public PortableImage Background
        {
            get {

                if (_background == null)
                {
                    _background = new PortableImage();

                    Task.Run(async () =>
                    {
                        await _background.LoadFromResources(ResourceScope.Ide, _backgroundPath);
                        ServiceLocator.DispatcherService.RunOnMainThread(() => RaisePropertyChanged(() => CroppedBackground));
                    });
                }

                return _background;
            }
        }

        private PortableImage _croppedBackground;

        public PortableImage CroppedBackground
        {
            get
            {
                if (_croppedBackground == null)
                {
                    _croppedBackground = new PortableImage();

                    Task.Run(async () =>
                    {
                        await _croppedBackground.LoadFromResources(ResourceScope.Ide, _croppedPath);
                        ServiceLocator.DispatcherService.RunOnMainThread(() => RaisePropertyChanged(()=> CroppedBackground));
                    });
                }

                return _croppedBackground;
            }
        }

        public PortableSolidColorBrush AccentColor1 { get; set; }
        public PortableSolidColorBrush AccentColor2 { get; set; }
        public PortableSolidColorBrush AccentColor3 { get; set; }

        public PortableSolidColorBrush AppBarBackgroundBrush { get; set; }

        public PortableSolidColorBrush AppBarButtonBrush { get; set; }

        public PortableSolidColorBrush AppBarButtonClickBrush { get; set; }

        public Theme(string backgroundPath, string croppedPath)
        {
            _backgroundPath = backgroundPath;
            _croppedPath = croppedPath;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

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