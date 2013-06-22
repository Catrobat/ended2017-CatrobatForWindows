using System;
using Catrobat.Core.Objects;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.ViewModel;
using Catrobat.IDEWindowsPhone.ViewModel.Settings;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using Microsoft.Practices.ServiceLocation;
using Catrobat.IDEWindowsPhone.ViewModel.Scripts;
using System.Windows.Navigation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Scripts
{
    public class BrickCollection : ObservableCollection<DataObject> { }
    public enum BrickCategory { Motion, Looks, Sounds, Control }

    public partial class AddNewScriptView : PhoneApplicationPage
    {
        private readonly AddNewScriptBrickViewModel _viewModel = ServiceLocator.Current.GetInstance<AddNewScriptBrickViewModel>();

        public AddNewScriptView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.ResetViewModelCommand.Execute(null);
            base.OnNavigatedFrom(e);
        }


        private void Movement_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            _viewModel.MovementCommand.Execute(null);
        }

        private void Looks_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            _viewModel.LooksCommand.Execute(null);
        }

        private void Sound_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            _viewModel.SoundCommand.Execute(null);
        }

        private void Control_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            _viewModel.ControlCommand.Execute(null);
        }
    }
}