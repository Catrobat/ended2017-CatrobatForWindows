using System.Collections.ObjectModel;
using System.Windows.Navigation;
using Catrobat.Core.Objects;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Scripts;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Scripts
{
    public class BrickCollection : ObservableCollection<DataObject> {}

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


        private void Movement_Tap(object sender, GestureEventArgs e)
        {
            _viewModel.MovementCommand.Execute(null);
        }

        private void Looks_Tap(object sender, GestureEventArgs e)
        {
            _viewModel.LooksCommand.Execute(null);
        }

        private void Sound_Tap(object sender, GestureEventArgs e)
        {
            _viewModel.SoundCommand.Execute(null);
        }

        private void Control_Tap(object sender, GestureEventArgs e)
        {
            _viewModel.ControlCommand.Execute(null);
        }

        private void Variable_Tap(object sender, GestureEventArgs e)
        {
            _viewModel.VariablesCommand.Execute(null);
        }
    }
}