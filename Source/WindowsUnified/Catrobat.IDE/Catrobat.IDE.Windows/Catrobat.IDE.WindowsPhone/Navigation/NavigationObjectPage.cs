using Catrobat.IDE.Core.Navigation;
using Catrobat.IDE.WindowsShared.Common;
using System.Reflection;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Navigation
{
    public class NavigationObjectPage : NavigationObject
    {
        private readonly Page _page;
        private readonly NavigationHelper _navigationHelper;

        public NavigationObjectPage(Page page)
        {
            _page = page;
            _navigationHelper = new NavigationHelper(page);
            _navigationHelper.LoadState += NavigationHelperOnLoadState;
            _navigationHelper.SaveState += NavigationHelperOnSaveState;
        }

        private void NavigationHelperOnSaveState(object sender, SaveStateEventArgs args)
        {
            RaiseSaveState(args.PageState);
        }

        void NavigationHelperOnLoadState(object sender, LoadStateEventArgs args)
        {
            RaiseLoadState(args.PageState);
        }

        public override void OnNavigateBack()
        {
            _navigationHelper.GoBackCommand.Execute(null);
        }

        public override void OnNavigateTo()
        {
            _page.Frame.Navigate(_page.GetType().GetTypeInfo().BaseType.GetTypeInfo().BaseType);
        }
    }
}
