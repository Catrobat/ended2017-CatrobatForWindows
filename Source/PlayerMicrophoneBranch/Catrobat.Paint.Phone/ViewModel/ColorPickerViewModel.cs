using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Controls;

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
            SelectColorDone = new RelayCommand(SelectColorDoneExecute);
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
                Data.GlobalValues.Instance.SelectedColor = value;
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

        public ICommand SelectColorDone { get; private set; }
        private void SelectColorDoneExecute()
        {
            var phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (phoneApplicationFrame != null)
                phoneApplicationFrame.GoBack();
        }
        #endregion
    }
}
