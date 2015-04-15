using System.Collections.Generic;

namespace Catrobat.Paint.WindowsPhone.Navigation
{
    public delegate void GoBackRequested();

    public delegate void LoadState(Dictionary<string, object> loadData);
    public delegate void SaveState(Dictionary<string, object> loadData);

    public delegate void NavigateTo();
    public delegate void NavigateFrom();

    public abstract class NavigationObject
    {
        public event GoBackRequested GoBackRequested;
        protected virtual void RaiseGoBackRequested()
        {
            GoBackRequested handler = GoBackRequested;
            if (handler != null) handler();
        }

        public event LoadState LoadState;
        protected virtual void RaiseLoadState(Dictionary<string, object> loaddata)
        {
            LoadState handler = LoadState;
            if (handler != null) handler(loaddata);
        }

        public event LoadState SaveState;
        protected virtual void RaiseSaveState(Dictionary<string, object> loaddata)
        {
            LoadState handler = SaveState;
            if (handler != null) handler(loaddata);
        }

        public event NavigateTo NavigateTo;
        public event NavigateFrom NavigateFrom;

        public abstract void NavigateBack();

        public void RaiseNavigatedTo()
        {
            if(NavigateTo != null)
                NavigateTo.Invoke();
        }

        public void RaiseNavigatedFrom()
        {
            if (NavigateFrom != null)
                NavigateFrom.Invoke();
        }
    }
}
