using System;
using System.Reflection;
using System.Windows;
using Catrobat.IDE.Core.Services;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using ViewModelBase = Catrobat.IDE.Core.ViewModel.ViewModelBase;

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
                //Catrobat/IDE/Phone/Views/Main/MainView.xaml
                pathToXaml = pathToXaml.Replace("Catrobat/IDE/Core/ViewModel", "/Views");

                pathToXaml = pathToXaml.Replace("ViewModel.xaml", "View.xaml");
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