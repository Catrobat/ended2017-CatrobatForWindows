using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Catrobat.IDE.Phone.Annotations;

namespace Catrobat.IDE.Phone.Controls.FormulaControls
{
    public partial class FormulaViewer2 : INotifyPropertyChanged
    {

        #region DependencyProperties

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
                TextChanged();
            }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(FormulaViewer2), new PropertyMetadata(string.Empty, TextChanged));

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FormulaViewer2)d).TextChanged();
        }

        private void TextChanged()
        {
            if (0 <= CaretIndex && CaretIndex <= Text.Length)
            { 
                BeforeCaret.Text = Text.Substring(0, CaretIndex);
                AfterCaret.Text = Text.Substring(CaretIndex);
            }
        }

        public int CaretIndex
        {
            get { return (int)GetValue(CaretIndexProperty); }
            set
            {
                SetValue(CaretIndexProperty, value);
            }
        }

        public static readonly DependencyProperty CaretIndexProperty = DependencyProperty.Register("CaretIndex", typeof(int), typeof(FormulaViewer2), new PropertyMetadata(0, CaretIndexChanged));

        private static void CaretIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FormulaViewer2)d).TextChanged();
        }

        #endregion

        public FormulaViewer2()
        {
            InitializeComponent();
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
