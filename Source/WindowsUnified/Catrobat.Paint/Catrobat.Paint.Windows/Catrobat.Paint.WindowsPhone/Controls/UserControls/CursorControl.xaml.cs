using Catrobat.Paint.Phone;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    public sealed partial class CursorControl : UserControl
    {
        private static bool isDrawing;
        public CursorControl()
        {
            this.InitializeComponent();
            isDrawing = false;
            if(PocketPaintApplication.GetInstance().cursorControl == null)
            {
                PocketPaintApplication.GetInstance().cursorControl = this;
            }
        }

        public void setCursorLook()
        {
            isDrawing = !isDrawing;

            if(isDrawing)
            {
                rectColorEven.Color = PocketPaintApplication.GetInstance().PaintData.ColorSelected.Color;
            }
            else
            {
                rectColorEven.Color = Colors.DarkGray;
            }
        }

        public void setCursorColor(Color color)
        {
            if (isDrawing)
            {
                rectColorEven.Color = color;
            }
        }
    }
}
