using System.Windows.Navigation;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Sounds;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Sounds
{
    public partial class AddNewSoundView : PhoneApplicationPage
    {
        private readonly AddNewSoundViewModel _viewModel = ServiceLocator.Current.GetInstance<AddNewSoundViewModel>();

        public AddNewSoundView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.ResetViewModelCommand.Execute(null);
        }
    }
}