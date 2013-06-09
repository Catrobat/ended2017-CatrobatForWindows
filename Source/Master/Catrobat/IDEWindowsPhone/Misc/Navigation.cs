using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Reflection;

namespace Catrobat.IDEWindowsPhone.Misc
{
  public class Navigation
  {
    public static void NavigateTo(Type type)
    {
      var assemblyFullName = Assembly.GetExecutingAssembly().FullName;
      var assemblyName = assemblyFullName.Split(',')[0];

      var pathToXaml = type.FullName;

      if (pathToXaml != null) 
        pathToXaml = pathToXaml.Replace(assemblyName, "");
      else
        throw new Exception("Cannot find xaml.");

      pathToXaml = pathToXaml.Replace(".", "/");
      pathToXaml += ".xaml";

      ((PhoneApplicationFrame)Application.Current.RootVisual).Navigate(new Uri(pathToXaml, UriKind.Relative));
    }

    public static void NavigateBack()
    {
      ((PhoneApplicationFrame)Application.Current.RootVisual).GoBack();
    }


    public static void RemoveBackEntry()
    {
      ((PhoneApplicationFrame) Application.Current.RootVisual).RemoveBackEntry();
    }
  }
}
