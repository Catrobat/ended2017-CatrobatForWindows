using System.Windows.Media;
using Catrobat.Paint.Phone.Old.ViewModel;
using Microsoft.Phone.Controls;

namespace Catrobat.Paint.Phone.Old.View
{
    public partial class ColorPickerView : PhoneApplicationPage
    {
        public ColorPickerView()
        {
            InitializeComponent();
        }

        private void ColorChanged(object sender, Color color)
        {
            SolidColorBrush _colorBrush = new SolidColorBrush();
            _colorBrush.Color = color;
            
            ((ColorPickerViewModel)DataContext).SelectColorValue.Execute(_colorBrush);

        }
    }
}