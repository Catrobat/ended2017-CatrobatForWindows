using System;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Core.Tests.Services
{
    public class NavigationServiceTest : INavigationService
    {
        public enum NavigationType
        {
            Initial,
            NavigateTo,
            NavigateBack,
            NavigateToWebPage
        }

        public NavigationType CurrentNavigationType { get; set; }
        public object CurrentView { get; set; }
        public int PageStackCount { get; set; }


        public void NavigateTo(Type type)
        {
            CurrentNavigationType = NavigationType.NavigateTo;
            CurrentView = type;
            PageStackCount++;
        }

        public void NavigateTo<T>()
        {
            NavigateTo(typeof(T));
        }

        public void NavigateBack(object navigationObject = null)
        {
            CurrentNavigationType = NavigationType.NavigateBack;
            CurrentView = null;
            PageStackCount--;
        }

        public void NavigateBackForPlatform(NavigationPlatform platform)
        {
            CurrentNavigationType = NavigationType.NavigateBack;
            CurrentView = null;
            PageStackCount--;
        }

        public void RemoveBackEntry()
        {
            PageStackCount--;
        }

        public void RemoveBackEntryForPlatform(NavigationPlatform platform)
        {
            PageStackCount--;
        }

        public bool CanGoBack
        {
            get { return PageStackCount > 0; }
        }

        public void NavigateToWebPage(string uri)
        {
            CurrentNavigationType = NavigationType.NavigateToWebPage;
            CurrentView = uri;
        }


        public void NavigateBack(Type viewModelType)
        {
            throw new NotImplementedException();
        }

        public void NavigateBack<T>()
        {
            throw new NotImplementedException();
        }
    }
}
