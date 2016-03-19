using Windows.UI.Xaml;
namespace Catrobat.IDE.Core.Models.CatrobatModels
{
    public abstract class CatrobatModelBase  : ModelBase
    {
        private Visibility _sensorUnsupported = Visibility.Collapsed;

        public Visibility SensorUnsupported
        {
            get { return _sensorUnsupported; }
            set { Set(ref _sensorUnsupported, value); }
        }
    }
}
