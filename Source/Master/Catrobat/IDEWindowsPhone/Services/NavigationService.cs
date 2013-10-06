using System;
using System.Reflection;
using System.Windows;
using Catrobat.Core.Services;
using Microsoft.Phone.Controls;

namespace Catrobat.IDEWindowsPhone.Services
{
    public class NavigationService : INavigationService
    {
        public void NavigateTo(Type type)
        {
            if (type == null) return;

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

        public void NavigateBack()
        {
            ((PhoneApplicationFrame) Application.Current.RootVisual).GoBack();
        }

        public void RemoveBackEntry()
        {
            ((PhoneApplicationFrame) Application.Current.RootVisual).RemoveBackEntry();
        }

        public bool CanGoBack
        {
            get
            {
                return ((PhoneApplicationFrame)Application.Current.RootVisual).CanGoBack;
            }
        }
    }
}