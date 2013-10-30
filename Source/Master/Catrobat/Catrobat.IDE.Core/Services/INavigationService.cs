using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.Services
{
    public interface INavigationService
    {
        void NavigateTo(Type type);

        void NavigateBack(object navigationObject = null);

        void RemoveBackEntry();

        bool CanGoBack { get; }

        void NavigateToWebPage(string p);
    }
}
