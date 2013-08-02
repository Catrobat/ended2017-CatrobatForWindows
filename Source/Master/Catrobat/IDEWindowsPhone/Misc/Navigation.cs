using System;
using System.Reflection;
using System.Windows;
using Microsoft.Phone.Controls;

namespace Catrobat.IDEWindowsPhone.Misc
{
    public class Navigation
    {
        public static void NavigateTo(Type type)
        {
            var assemblyFullName = Assembly.GetExecutingAssembly().FullName;
            var assemblyName = assemblyFullName.Split(',')[0];

            var pathToXaml = type.FullName;

            if (!string.IsNullOrEmpty((pathToXaml)))
            {
                pathToXaml = pathToXaml.Replace(assemblyName, "");
            }
            else
            {
                throw new Exception("Cannot find xaml.");
            }

            pathToXaml = pathToXaml.Replace(".", "/");
            pathToXaml += ".xaml";

            ((PhoneApplicationFrame) Application.Current.RootVisual).Navigate(new Uri(pathToXaml, UriKind.Relative));
        }

        public static void NavigateBack()
        {
            ((PhoneApplicationFrame) Application.Current.RootVisual).GoBack();
        }


        public static void RemoveBackEntry()
        {
            ((PhoneApplicationFrame) Application.Current.RootVisual).RemoveBackEntry();
        }

        public static bool CanGoBack
        {
            get
            {
                return ((PhoneApplicationFrame)Application.Current.RootVisual).CanGoBack;
            }
        }
    }
}