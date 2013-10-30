using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Sprites;
using Catrobat.IDE.Store.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Catrobat.IDE.Store.Views.Editor.Sprites
{
    public sealed partial class AddNewSpriteView : UserControl
    {
        private readonly AddNewSpriteViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).AddNewSpriteViewModel;

        public AddNewSpriteView()
        {
            this.InitializeComponent();
        }

        private void TextBoxSpriteName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SpriteName = TextBoxSpriteName.Text;
        }
    }
}
