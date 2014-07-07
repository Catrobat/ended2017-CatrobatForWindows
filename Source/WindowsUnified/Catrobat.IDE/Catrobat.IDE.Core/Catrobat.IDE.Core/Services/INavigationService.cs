using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.Services
{
    public enum NavigationPlatform { WindowsStore, WindowsPhone}

    public interface INavigationService
    {
        void NavigateTo(Type type);

        void NavigateTo<T>();

        void NavigateBack(object navigationObject = null);

        void NavigateBackForPlatform(NavigationPlatform platform);

        void RemoveBackEntry();

        void RemoveBackEntryForPlatform(NavigationPlatform platform);

        bool CanGoBack { get; }

        void NavigateToWebPage(string p);
    }
}
