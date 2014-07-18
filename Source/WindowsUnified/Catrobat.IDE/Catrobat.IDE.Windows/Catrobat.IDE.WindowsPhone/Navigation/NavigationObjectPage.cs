using Catrobat.IDE.Core.Navigation;
using Catrobat.IDE.WindowsShared.Common;
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
            _navigationHelper.OnGoBack += NavigationHelperOnOnGoBack;
        }

        private void NavigationHelperOnOnGoBack()
        {
            RaiseGoBackRequested();
        }

        private void NavigationHelperOnSaveState(object sender, SaveStateEventArgs args)
        {
            RaiseSaveState(args.PageState);
        }

        void NavigationHelperOnLoadState(object sender, LoadStateEventArgs args)
        {
            RaiseLoadState(args.PageState);
        }

        public override void NavigateBack()
        {
            _navigationHelper.GoBack();
            RaiseNavigatedFrom();
        }
    }
}
