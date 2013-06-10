using Catrobat.Core;
using Catrobat.IDEWindowsPhone.ViewModel;
using Catrobat.IDEWindowsPhone.ViewModel.Main;
using Catrobat.IDEWindowsPhone.ViewModel.Settings;
using IDEWindowsPhone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone
{
  public class ContextHolderWindowsPhone : IContextHolder
  {
    public CatrobatContext GetContext()
    {
      MainViewModel mainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
      return mainViewModel.Context;
    }
  }
}
