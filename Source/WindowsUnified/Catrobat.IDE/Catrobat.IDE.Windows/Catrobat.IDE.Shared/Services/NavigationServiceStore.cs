using System;
using System.Linq;
using System.Reflection;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class NavigationServiceStore : INavigationService
    {
        private Frame _frame;
        private int _removedBackEntryCount;

        public NavigationServiceStore(Frame frame)
        {
            _frame = frame;
        }

        public void NavigateTo<T>()
        {
            NavigateTo(typeof(T));
        }

        public void NavigateTo(Type type)
        {
            NavigateBack();

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
            else if (type.GetTypeInfo().BaseType == typeof (Page))
            {
                pageType = type;
            }

            if (pageType.GetTypeInfo().BaseType == typeof(Page)) // this is not true for flyouts (UserControls)
                _frame.Navigate(pageType);
        }

        public void NavigateBack(object navigationObject)
        {
            var flyout = navigationObject as Flyout;
            if (flyout != null)
                flyout.Hide();
            else
            {
                _removedBackEntryCount++;
                NavigateBack();
            }
        }

        public void NavigateBackForPlatform(NavigationPlatform platform)
        {
            if (platform == NavigationPlatform.WindowsStore)
            {
                _removedBackEntryCount++;
                NavigateBack();
            }
        }

        private void NavigateBack()
        {
            while (_removedBackEntryCount > 0)
            {
                _removedBackEntryCount--;
                if (CanGoBack)
                    _frame.GoBack();
            }
        }

        public void RemoveBackEntry()
        {
            _removedBackEntryCount++;
        }


        public void RemoveBackEntryForPlatform(NavigationPlatform platform)
        {
            if (platform == NavigationPlatform.WindowsStore)
                _removedBackEntryCount++;
        }


        public bool CanGoBack
        {
            get { return _frame.CanGoBack; }
        }

        public void NavigateToWebPage(string uri)
        {
            Launcher.LaunchUriAsync(new Uri(uri));
        }
    }
}