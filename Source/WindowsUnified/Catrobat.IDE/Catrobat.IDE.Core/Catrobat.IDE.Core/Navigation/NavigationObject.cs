
using System.Collections.Generic;

namespace Catrobat.IDE.Core.Navigation
{
    public delegate void LoadState(Dictionary<string, object> loadData);
    public delegate void SaveState(Dictionary<string, object> loadData);

    public delegate void NavigateTo();
    public delegate void NavigateFrom();

    public abstract class NavigationObject
    {
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

        public abstract void OnNavigateBack();

        public abstract void OnNavigateTo();

        public void OnNavigatedTo(object args)
        {
            NavigateTo.Invoke();
        }

        public void OnNavigatedFrom(object args)
        {
            NavigateFrom.Invoke();
        }
    }
}
