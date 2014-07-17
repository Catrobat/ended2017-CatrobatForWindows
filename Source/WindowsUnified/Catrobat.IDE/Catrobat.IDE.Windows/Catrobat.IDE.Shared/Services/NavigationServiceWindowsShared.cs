using System;
using System.Linq;
using System.Reflection;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.WindowsPhone.Views;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class NavigationServiceWindowsShared : INavigationService
    {
        private readonly Frame _frame;

        public NavigationServiceWindowsShared(Frame frame)
        {
            _frame = frame;
        }


        public void NavigateTo<T>()
        {
            NavigateTo(typeof(T));
        }
        
        public void NavigateTo(Type type)
        {
            Type pageType = null;

            if (type.GetTypeInfo().BaseType == typeof(ViewModelBase))
            {
                var viewModelInstance = (ViewModelBase)ServiceLocator.GetInstance(type);

                if (viewModelInstance.SkipAndNavigateTo != null)
                    type = viewModelInstance.SkipAndNavigateTo;

                pageType = viewModelInstance.PresenterType;

                if (pageType == null)
                {
                    var viewModelName = type.GetTypeInfo().AssemblyQualifiedName.Split(',').First();
                    var viewName = viewModelName.Replace("Catrobat.IDE.Core.ViewModels", "Catrobat.IDE.WindowsPhone.Views");
                    viewName = viewName.Replace("ViewModel", "View");
                    pageType = Type.GetType(viewName);
                }
            }
            else if (type.GetTypeInfo().BaseType == typeof (ViewPageBase))
            {
                pageType = type;
            }

            if (pageType.GetTypeInfo().BaseType == typeof(ViewPageBase)) // this is not true for flyouts (UserControls)
                _frame.Navigate(pageType);
        }


        public void NavigateBack<T>()
        {
            NavigateBack(typeof(T));
        }

        public void NavigateBack(Type viewModelType)
        {
            if (viewModelType.GetTypeInfo().BaseType != typeof(ViewModelBase))
            {
                throw new Exception("The type has to have 'ViewModelBase' as it's base type.");
            }

            var viewModel = (ViewModelBase)ServiceLocator.GetInstance(viewModelType);
            var navigationObject = viewModel.NavigationObject;


            if (navigationObject == null)
                throw new Exception("The navigation object cannot be null.");

            navigationObject.OnNavigateBack();
        }


        public void RemoveBackEntry()
        {
            _frame.BackStack.RemoveAt(_frame.BackStack.Count - 1);
        }

        public bool CanGoBack
        {
            get { return _frame != null && _frame.CanGoBack; }
        }

        public void NavigateToWebPage(string uri)
        {
            Launcher.LaunchUriAsync(new Uri(uri));
        }
    }
}