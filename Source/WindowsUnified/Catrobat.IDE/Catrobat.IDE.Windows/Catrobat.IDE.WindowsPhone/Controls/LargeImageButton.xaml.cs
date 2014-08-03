using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Catrobat.IDE.WindowsPhone.Controls
{
    public sealed partial class LargeImageButton : UserControl
    {
        #region DependancyProperties


        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command",
            typeof(ICommand), typeof(LargeImageButton),
            new PropertyMetadata(null, CommandChanged));

        private static void CommandChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var instance = (d as LargeImageButton);
            if (instance == null) return;

            instance.ButtonMain.Command = ((ICommand)e.NewValue);
        }


        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", 
            typeof(ImageSource), typeof(LargeImageButton), 
            new PropertyMetadata(null, ImageChanged));

        private static void ImageChanged(DependencyObject d, 
            DependencyPropertyChangedEventArgs e)
        {
            var instance = (d as LargeImageButton);
            if (instance == null) return;

            instance.LeftImage.Source = ((ImageSource) e.NewValue);

            instance.LeftImage.Visibility = Visibility.Visible;
            instance.ContentControlImageLeft.Visibility = Visibility.Collapsed;

        }


        public DataTemplate ImageTemplate
        {
            get { return (DataTemplate)GetValue(ImageTemplateProperty); }
            set { SetValue(ImageTemplateProperty, value); }
        }

        public static readonly DependencyProperty ImageTemplateProperty =
            DependencyProperty.Register("ImageTemplate",
            typeof(DataTemplate), typeof(LargeImageButton),
            new PropertyMetadata(null, ImageTemplateChanged));

        private static void ImageTemplateChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var instance = (d as LargeImageButton);
            if (instance == null) return;

            instance.ContentControlImageLeft.ContentTemplate = ((DataTemplate)e.NewValue);

            instance.LeftImage.Visibility = Visibility.Collapsed;
            instance.ContentControlImageLeft.Visibility = Visibility.Visible;
        }


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text",
            typeof(string), typeof(LargeImageButton),
            new PropertyMetadata(null, TextChanged));

        private static void TextChanged(DependencyObject d, 
            DependencyPropertyChangedEventArgs e)
        {
            var instance = (d as LargeImageButton);
            if (instance == null) return;

            instance.TextBlockTextRight.Text = ((string) e.NewValue);
        }

        #endregion


        public LargeImageButton()
        {
            this.InitializeComponent();
        }
    }
}
