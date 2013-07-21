using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Catrobat.Paint.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            /// 
            IncrementValue = new RelayCommand(() => IncrementValueExecute(), () => true);
            ExampleValue = 0;
        }


        public ICommand IncrementValue { get; private set; }
        private void IncrementValueExecute()
        {
            ExampleValue += 1;
        }


        int _exampleValue;

        public int ExampleValue
        {
            get
            {
                return _exampleValue;
            }
            set
            {
                _exampleValue = value;
                RaisePropertyChanged("ExampleValue");
            }
        }

        public ViewModelBase CurrentViewModel { get; set; }

    }
}