using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Phone.Services;
using Catrobat.IDE.Phone.Services.Storage;
using GalaSoft.MvvmLight;

namespace Catrobat.IDE.Phone
{
    public class AppDesignStatic
    {
        static AppDesignStatic()
        {
            Core.App.SetNativeApp(new AppPhone());
            Core.App.Initialize();
        }
    }
}
