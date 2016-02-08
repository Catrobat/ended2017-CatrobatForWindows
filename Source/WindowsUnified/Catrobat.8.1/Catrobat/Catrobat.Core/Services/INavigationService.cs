using System;

namespace Catrobat.IDE.Core.Services
{
    public enum NavigationPlatform { WindowsStore, WindowsPhone}

    public interface INavigationService
    {
        void NavigateTo(Type viewModelType);

        void NavigateTo<T>();

        void NavigateBack(Type viewModelType);

        void NavigateBack<T>();


        void RemoveBackEntry();

        bool CanGoBack { get; }

        void NavigateToWebPage(string p);
    }
}
