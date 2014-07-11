using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Sprites
{
    public partial class AddNewSpriteView : Page
    {
        private readonly AddNewSpriteViewModel _viewModel = 
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).AddNewSpriteViewModel;

        public AddNewSpriteView()
        {
            InitializeComponent();

            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                TextBoxSpriteName.Focus(FocusState.Keyboard);
                TextBoxSpriteName.SelectAll();
            });
        }

        //protected override void OnBackKeyPress(CancelEventArgs e)
        //{
        //    _viewModel.GoBackCommand.Execute(null);
        //    e.Cancel = true;
        //    base.OnBackKeyPress(e);
        //}

        private void TextBoxSpriteName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SpriteName = TextBoxSpriteName.Text;
        }
    }
}