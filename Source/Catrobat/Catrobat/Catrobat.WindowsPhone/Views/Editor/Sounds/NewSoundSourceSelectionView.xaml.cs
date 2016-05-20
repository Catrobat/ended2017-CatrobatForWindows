using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Sounds;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Sounds
{
    public partial class NewSoundSourceSelectionView
    {
        private readonly NewSoundSourceSelectionViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).NewSoundSourceSelectionViewModel;

        

        public NewSoundSourceSelectionView()
        {
            InitializeComponent();
        }
    }
}