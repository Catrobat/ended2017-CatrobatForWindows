using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Formula;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Formula
{
    public partial class ChangeVariableView
    {
        private readonly ChangeVariableViewModel _viewModel = 
            ServiceLocator.ViewModelLocator.ChangeVariableViewModel;

        

        public ChangeVariableView()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            TextBoxVariableName.SelectAll();
            TextBoxVariableName.Focus(FocusState.Keyboard);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TextBoxVariableName.SelectAll();
            TextBoxVariableName.Focus(FocusState.Keyboard);
            base.OnNavigatedTo(e);
        }

        private void TextBoxVariableName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.UserVariableName = TextBoxVariableName.Text;
        }
    }
}