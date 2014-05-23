using System.ComponentModel;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Sounds;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Editor.Sounds
{
    public partial class NewSoundSourceSelectionView : PhoneApplicationPage
    {
        private readonly NewSoundSourceSelectionViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).NewSoundSourceSelectionViewModel;

        public NewSoundSourceSelectionView()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
            base.OnBackKeyPress(e);
        }
    }
}