using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Catrobat.Paint.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ColorPickerViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the ColorPickerViewModel class.
        /// </summary>
        public ColorPickerViewModel()
        {
            SelectColorValue = new RelayCommand<SolidColorBrush>(SelectColorValueExecute);
        }

        #region Private Members

        private SolidColorBrush _selectedColor = Data.GlobalValues.Instance.SelectedColor;
        #endregion


        #region Properties

        public SolidColorBrush SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                _selectedColor = value;
                RaisePropertyChanged(() => SelectedColor);
            }
        }



        #endregion


        #region Commands
         


        public ICommand SelectColorValue { get; private set; }
        private void SelectColorValueExecute(SolidColorBrush color)
        {
            SelectedColor = color;
        }



        #endregion


    }



}
