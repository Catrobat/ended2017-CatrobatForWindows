using System.ComponentModel;
using System.Windows;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.FormulaEditor.Editor;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.ViewModel.Editor.Formula;

namespace Catrobat.IDE.Phone.Controls.FormulaControls
{
    public delegate void KeyPressed(FormulaKeyEventArgs e);
    public delegate void EvaluatePressed();

    public partial class FormulaKeyboard
    {
        #region Dependency properties

        public static readonly DependencyProperty ProjectProperty = DependencyProperty.Register(
            name: "Project",
            propertyType: typeof(Project),
            ownerType: typeof(FormulaKeyboard),
            typeMetadata: new PropertyMetadata((d, e) => ((FormulaKeyboard)d).ProjectChanged(e)));
        public Project Project
        {
            get { return (Project) GetValue(ProjectProperty); }
            set { SetValue(ProjectProperty, value); }
        }
        private void ProjectChanged(DependencyPropertyChangedEventArgs e)
        {
        }

        public static readonly DependencyProperty CanDeleteProperty = DependencyProperty.Register(
            name: "CanDelete",
            propertyType: typeof(bool),
            ownerType: typeof(FormulaKeyboard),
            typeMetadata: new PropertyMetadata(false));
        public bool CanDelete
        {
            get { return (bool) GetValue(CanDeleteProperty); }
            set { SetValue(CanDeleteProperty, value); }
        }

        public static readonly DependencyProperty CanUndoProperty = DependencyProperty.Register(
            name: "CanUndo",
            propertyType: typeof(bool),
            ownerType: typeof(FormulaKeyboard),
            typeMetadata: new PropertyMetadata(false));
        public bool CanUndo
        {
            get { return (bool) GetValue(CanUndoProperty); }
            set { SetValue(CanUndoProperty, value); }
        }

        public static readonly DependencyProperty CanRedoProperty = DependencyProperty.Register(
            name: "CanRedo",
            propertyType: typeof(bool),
            ownerType: typeof(FormulaKeyboard),
            typeMetadata: new PropertyMetadata(false));
        public bool CanRedo
        {
            get { return (bool) GetValue(CanRedoProperty); }
            set { SetValue(CanRedoProperty, value); }
        }

        public static readonly DependencyProperty CanLeftProperty = DependencyProperty.Register(
            name: "CanLeft",
            propertyType: typeof(bool),
            ownerType: typeof(FormulaKeyboard),
            typeMetadata: new PropertyMetadata(false));
        public bool CanLeft
        {
            get { return (bool) GetValue(CanLeftProperty); }
            set { SetValue(CanLeftProperty, value); }
        }

        public static readonly DependencyProperty CanRightProperty = DependencyProperty.Register(
            name: "CanRight",
            propertyType: typeof(bool),
            ownerType: typeof(FormulaKeyboard),
            typeMetadata: new PropertyMetadata(false));
        public bool CanRight
        {
            get { return (bool) GetValue(CanRightProperty); }
            set { SetValue(CanRightProperty, value); }
        }

        public static readonly DependencyProperty CanEvaluateProperty = DependencyProperty.Register(
            name: "CanEvaluate",
            propertyType: typeof(bool),
            ownerType: typeof(FormulaKeyboard),
            typeMetadata: new PropertyMetadata(false));
        public bool CanEvaluate
        {
            get { return (bool) GetValue(CanEvaluateProperty); }
            set { SetValue(CanEvaluateProperty, value); }
        }

        public static readonly DependencyProperty HasErrorProperty = DependencyProperty.Register(
            name: "HasError",
            propertyType: typeof(bool),
            ownerType: typeof(FormulaKeyboard),
            typeMetadata: new PropertyMetadata(false));
        public bool HasError
        {
            get { return (bool) GetValue(HasErrorProperty); }
            set { SetValue(HasErrorProperty, value); }
        }

        public static readonly DependencyProperty ParsingErrorProperty = DependencyProperty.Register(
            name: "ParsingError",
            propertyType: typeof(string),
            ownerType: typeof(FormulaKeyboard),
            typeMetadata: new PropertyMetadata(null));
        public string ParsingError
        {
            get { return (string) GetValue(ParsingErrorProperty); }
            set { SetValue(ParsingErrorProperty, value); }
        }

        public static readonly DependencyProperty DecimalSeparatorProperty = DependencyProperty.Register("DecimalSeparator", typeof(string), typeof(FormulaKeyboard), new PropertyMetadata("."));
        public string DecimalSeparator
        {
            get { return (string) GetValue(DecimalSeparatorProperty); }
            private set { SetValue(DecimalSeparatorProperty, value); }
        }

        #endregion

        #region Events

        public KeyPressed KeyPressed;
        private void RaiseKeyPressed(FormulaKeyEventArgs e)
        {
            if(KeyPressed != null) KeyPressed.Invoke(e);
        }
        public void RaiseKeyPressed(FormulaEditorKey key, UserVariable  variable = null)
        {
            RaiseKeyPressed(new FormulaKeyEventArgs(key, variable));
        }
 
        public EvaluatePressed EvaluatePressed;
        private void RaiseEvaluatePressed()
        {
            if (EvaluatePressed != null)
                EvaluatePressed.Invoke();
        }

        #endregion

        private readonly VariableConteiner _variableContainer = new VariableConteiner();

        public FormulaKeyboard()
        {
            InitializeComponent();

            DecimalSeparator = ServiceLocator.CulureService.GetCulture().NumberFormat.NumberDecimalSeparator;
            _variableContainer.PropertyChanged += VariableContainer_OnPropertyChanged;
            ServiceLocator.ViewModelLocator.VariableSelectionViewModel.SelectedVariableContainer = _variableContainer;
        }
        private void FormulaKeyboard_OnUnloaded(object sender, RoutedEventArgs e)
        {
            ServiceLocator.ViewModelLocator.VariableSelectionViewModel.SelectedVariableContainer = null;
            _variableContainer.PropertyChanged -= VariableContainer_OnPropertyChanged;
        }

        private void VariableContainer_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var variable = _variableContainer.Variable;
            if (variable == null) return;
            ShowMain();
            RaiseKeyPressed(
                key: VariableHelper.IsVariableLocal(Project, variable) ? FormulaEditorKey.LocalVariable : FormulaEditorKey.GlobalVariable,
                variable: variable);
        }

        private void ButtonVariable_OnClick(object sender, RoutedEventArgs e)
        {
            ShowVariable();
        }

        private void ButtonSensors_OnClick(object sender, RoutedEventArgs e)
        {
            ShowSensors();
        }

        private void ButtonMath_OnClick(object sender, RoutedEventArgs e)
        {
            ShowMath();
        }

        private void ButtonMathBack_Click(object sender, RoutedEventArgs e)
        {
            ShowMain();
        }

        private void ButtonVariablesBack_Click(object sender, RoutedEventArgs e)
        {
            ShowMain();
        }

        private void KeyButton_OnClick(object sender, RoutedEventArgs e)
        {
            var key = (FormulaEditorKey)(uint)((FrameworkElement)sender).DataContext;
            ShowMain();
            RaiseKeyPressed(key);
        }

        private void SensorButton_OnClick(object sender, RoutedEventArgs e)
        {
            var key = (FormulaEditorKey)(uint)((FrameworkElement)sender).DataContext;
            ShowMain();
            RaiseKeyPressed(key);
        }

        private void ObjectButton_OnClick(object sender, RoutedEventArgs e)
        {
            var key = (FormulaEditorKey)(uint)((FrameworkElement)sender).DataContext;
            ShowMain();
            RaiseKeyPressed(key);
        }

        private void ButtonEvaluate_OnClick(object sender, RoutedEventArgs e)
        {
            RaiseEvaluatePressed();
        }

        private void ButtonError_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: pretty up toast notification
            ServiceLocator.NotifictionService.ShowToastNotification(
                title: "", 
                message: ParsingError,  
                timeTillHide:ToastNotificationTime.Medeum);
        }

        private void ShowMain()
        {
            GridMain.Visibility = Visibility.Visible;
            GridMath.Visibility = Visibility.Collapsed;
            GridVariable.Visibility = Visibility.Collapsed;
            GridSensors.Visibility = Visibility.Collapsed;
        }
        private void ShowMath()
        {
            GridMain.Visibility = Visibility.Collapsed;
            GridMath.Visibility = Visibility.Visible;
            GridVariable.Visibility = Visibility.Collapsed;
            GridSensors.Visibility = Visibility.Collapsed;
        }

        private void ShowVariable()
        {
            var variableSelectionViewModel = ServiceLocator.ViewModelLocator.VariableSelectionViewModel;
            variableSelectionViewModel.SelectedLocalVariable = null;
            variableSelectionViewModel.SelectedGlobalVariable = null;
            GridMain.Visibility = Visibility.Collapsed;
            GridMath.Visibility = Visibility.Collapsed;
            GridVariable.Visibility = Visibility.Visible;
            GridSensors.Visibility = Visibility.Collapsed;
        }

        private void ShowSensors()
        {
            GridMain.Visibility = Visibility.Collapsed;
            GridMath.Visibility = Visibility.Collapsed;
            GridVariable.Visibility = Visibility.Collapsed;
            GridSensors.Visibility = Visibility.Visible;
        }
    }
}
