using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Catrobat.Paint.Data
{
    internal sealed class  GlobalValues
    {
        #region Singleton implementation

        //TODO: threadsafe?! see http://msdn.microsoft.com/en-us/library/ff650316.aspx 

        private static readonly GlobalValues _instance = new GlobalValues();

        private GlobalValues() { }

        public static GlobalValues Instance
        {
            get
            {
                return _instance;
            }
        }

        #endregion

        #region Private Members

        private SolidColorBrush _selectedColor = new SolidColorBrush{Color = Colors.Blue};
        #endregion

        #region Properties
        //TODO who is allowed to set this?
        public SolidColorBrush SelectedColor
        {
            get { return _selectedColor; }
            set { _selectedColor = value; }
        }

        #endregion
    }
}
