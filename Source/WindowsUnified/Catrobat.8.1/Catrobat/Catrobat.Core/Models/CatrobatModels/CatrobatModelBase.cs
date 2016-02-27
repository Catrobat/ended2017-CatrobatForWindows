using Windows.UI.Xaml;
namespace Catrobat.IDE.Core.Models.CatrobatModels
{
    public abstract class CatrobatModelBase  : ModelBase
    {
        private int _sensorUnsupported = (int)Visibility.Collapsed;

        public int SensorUnsupported
        {
            get { return _sensorUnsupported; }
            set { Set(ref _sensorUnsupported, value); }
        }
    }
}
