
using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.WindowsShared.Common;

namespace Catrobat.IDE.WindowsPhone.Views
{
    public abstract class ViewPageBase : Page
    {
        private const string ViewModelStateKey = "ViewModelState";

        private ViewModelBase _viewModel;
        protected ViewModelBase ViewModel
        {
            get
            {
                if (_viewModel != null) return _viewModel;

                var viewFullName = this.GetType().GetTypeInfo().FullName;

                var viewModelName = viewFullName.Replace("Catrobat.IDE.WindowsPhone.Views", "Catrobat.IDE.Core.ViewModels");

                viewModelName += "Model";

                var viewModelBaseAssemblyName = typeof(ViewModelBase).AssemblyQualifiedName;

                var viewModelAssemblyName = viewModelBaseAssemblyName.Replace(
                    "Catrobat.IDE.Core.ViewModels.ViewModelBase",
                    viewModelName);

                var type = Type.GetType(viewModelAssemblyName);

                ServiceLocator.RegisterByType(type, TypeCreationMode.Normal);
                _viewModel = (ViewModelBase)ServiceLocator.GetInstance(type);

                if (_viewModel == null)
                    throw new Exception("The Page's DataContext has to be of type ViewModelBase");

                return _viewModel;
            }
        }

        private readonly NavigationHelper _navigationHelper;

        protected ViewPageBase()
        {
            _navigationHelper = new NavigationHelper(this);
            _navigationHelper.LoadState += InternalLoadState;
            _navigationHelper.SaveState += InternalSaveState;
        }

        private void InternalSaveState(object sender, SaveStateEventArgs e)
        {
            var viewModelState = new Dictionary<string, object>();
            ViewModel.SaveState(viewModelState);
            e.PageState.Add(ViewModelStateKey, viewModelState);

            SaveState(sender, e);
        }

        private void InternalLoadState(object sender, LoadStateEventArgs e)
        {
            if (e.PageState != null && e.PageState.ContainsKey(ViewModelStateKey))
            {
                var viewModelState = e.PageState[ViewModelStateKey] as Dictionary<string, object>;
                ViewModel.LoadState(viewModelState);
            }
            else
            {
                ViewModel.LoadState(null);
            }

            LoadState(sender, e);
        }

        protected virtual void LoadState(object sender, LoadStateEventArgs e) { }


        protected virtual void SaveState(object sender, SaveStateEventArgs e) { }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
        }

    }
}
