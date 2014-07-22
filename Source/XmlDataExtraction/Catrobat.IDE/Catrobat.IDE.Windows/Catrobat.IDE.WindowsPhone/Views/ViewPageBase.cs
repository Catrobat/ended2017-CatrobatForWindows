using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.WindowsPhone.Navigation;
using System;
using System.Reflection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Catrobat.IDE.WindowsPhone.Views
{
    public abstract class ViewPageBase : Page
    {
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

                _viewModel = (ViewModelBase)ServiceLocator.GetInstance(type);

                if (_viewModel == null)
                    throw new Exception("The Page's DataContext has to be of type ViewModelBase");

                return _viewModel;
            }
        }

        protected ViewPageBase()
        {
            if (!ViewModelBase.IsInDesignModeStatic)
                ViewModel.NavigationObject = new NavigationObjectPage(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.NavigationObject.RaiseNavigatedTo();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModel.NavigationObject.RaiseNavigatedFrom();
        }
    }
}
