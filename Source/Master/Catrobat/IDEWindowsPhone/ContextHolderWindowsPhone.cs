using Catrobat.Core;
using Catrobat.IDEWindowsPhone.ViewModel;
using IDEWindowsPhone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.IDEWindowsPhone
{
  public class ContextHolderWindowsPhone : IContextHolder
  {
    public CatrobatContext GetContext()
    {
      MainViewModel mainViewModel = (App.Current.Resources["Locator"] as ViewModelLocator).Main;
      return mainViewModel.Context;
    }
  }
}
