using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Formula;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Formula
{
    public partial class AddNewLocalVariableView
    {
        private readonly AddNewLocalVariableViewModel _viewModel = 
            ServiceLocator.ViewModelLocator.AddNewLocalVariableViewModel;

        

        public AddNewLocalVariableView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            TextBoxVariableName.Focus(FocusState.Keyboard);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TextBoxVariableName.Focus(FocusState.Keyboard);
            base.OnNavigatedTo(e);
        }


        private void TextBoxVariableName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.UserVariableName = TextBoxVariableName.Text;
        }
    }
}