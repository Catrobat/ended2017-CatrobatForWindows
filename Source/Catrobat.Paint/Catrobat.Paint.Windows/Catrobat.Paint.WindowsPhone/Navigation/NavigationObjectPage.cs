using Catrobat.Paint.WindowsPhone.Common;
using Windows.UI.Xaml.Controls;

namespace Catrobat.Paint.WindowsPhone.Navigation
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
            _navigationHelper.OnGoBack += NavigationHelperOnGoBack;
        }

        private void NavigationHelperOnGoBack()
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
            if (_navigationHelper.GoBackCommand.CanExecute(null))
            {
                _navigationHelper.GoBackCommand.Execute(null);
            }
        }
    }
}
