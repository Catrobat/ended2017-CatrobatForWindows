using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using Catrobat.IDE.Core.Resources;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using GalaSoft.MvvmLight;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace Catrobat.IDE.Phone.Services
{
    public class NavigationServicePhone : INavigationService
    {
        public void NavigateTo(Type type)
        {
            if (type == null)
                throw new ArgumentException("The parameter 'type' must nut be null");

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

            if (type.BaseType == typeof(ViewModelBase))
            {
                pathToXaml = pathToXaml.Replace("ViewModel", "Views");

                pathToXaml = pathToXaml.Replace("Views.xaml", "View.xaml");
            }

            try
            {
                ((PhoneApplicationFrame) Application.Current.RootVisual).Navigate(new Uri(pathToXaml, UriKind.Relative));
            }
            catch
            {
                throw new Exception("Navigation not possible: " + pathToXaml);
            }
            
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

        public void NavigateToWebPage(string uri)
        {
            var browser = new WebBrowserTask { Uri = new Uri(uri) };
            browser.Show();
        }
    }
}