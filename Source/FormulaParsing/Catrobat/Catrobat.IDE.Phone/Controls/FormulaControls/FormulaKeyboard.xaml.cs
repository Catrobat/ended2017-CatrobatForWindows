using System.Windows;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.FormulaEditor.Editor;
using Catrobat.IDE.Core.ViewModel.Editor.Formula;

namespace Catrobat.IDE.Phone.Controls.FormulaControls
{
    public delegate void KeyPressed(FormulaKeyEventArgs e);
    public delegate void EvaluatePressed();

    public partial class FormulaKeyboard
    {
        #region DependancyProperties

        public static readonly DependencyProperty GlobalVariablesProperty = DependencyProperty.Register(
            name: "GlobalVariables", 
            propertyType: typeof(UserVariable), 
            ownerType: typeof(FormulaKeyboard),  
            typeMetadata: new PropertyMetadata((d, e) => ((FormulaKeyboard) d).GlobalVariablesChanged(e)));
        public UserVariable GlobalVariables
        {
            get { return (UserVariable)GetValue(GlobalVariablesProperty); }
            set { SetValue(GlobalVariablesProperty, value); }
        }
        private void GlobalVariablesChanged(DependencyPropertyChangedEventArgs e)
        {
            //((FormulaKeyboard) d).ListBoxGlobalVariables.ItemsSource = e.NewValue as IEnumerable;
        }

        public static readonly DependencyProperty LocalVariablesProperty = DependencyProperty.Register(
            name: "LocalVariables", 
            propertyType:  typeof(UserVariable), 
            ownerType: typeof(FormulaKeyboard), 
            typeMetadata: new PropertyMetadata((d, e) => ((FormulaKeyboard) d).LocalVariablesChanged(e)));
        public UserVariable LocalVariables
        {
            get { return (UserVariable)GetValue(LocalVariablesProperty); }
            set { SetValue(LocalVariablesProperty, value); }
        }
        private void LocalVariablesChanged(DependencyPropertyChangedEventArgs e)
        {
            //((FormulaKeyboard)d).ListBoxLocalVariables.ItemsSource = e.NewValue as IEnumerable;
        }

        public static readonly DependencyProperty CanDeleteProperty = DependencyProperty.Register(
            name: "CanDelete",
            propertyType: typeof(bool),
            ownerType: typeof(FormulaKeyboard),
            typeMetadata: new PropertyMetadata(false));
        public bool CanDelete
        {
            get { return (bool)GetValue(CanDeleteProperty); }
            set { SetValue(CanDeleteProperty, value); }
        }

        public static readonly DependencyProperty CanUndoProperty = DependencyProperty.Register(
            name: "CanUndo",
            propertyType: typeof(bool),
            ownerType: typeof(FormulaKeyboard),
            typeMetadata: new PropertyMetadata(false));
        public bool CanUndo
        {
            get { return (bool)GetValue(CanUndoProperty); }
            set { SetValue(CanUndoProperty, value); }
        }

        public static readonly DependencyProperty CanRedoProperty = DependencyProperty.Register(
            name: "CanRedo",
            propertyType: typeof(bool),
            ownerType: typeof(FormulaKeyboard),
            typeMetadata: new PropertyMetadata(false));
        public bool CanRedo
        {
            get { return (bool)GetValue(CanRedoProperty); }
            set { SetValue(CanRedoProperty, value); }
        }

        public static readonly DependencyProperty CanEvaluateProperty = DependencyProperty.Register(
            name: "CanEvaluate",
            propertyType: typeof(bool),
            ownerType: typeof(FormulaKeyboard),
            typeMetadata: new PropertyMetadata(false));
        public bool CanEvaluate
        {
            get { return (bool)GetValue(CanEvaluateProperty); }
            set { SetValue(CanEvaluateProperty, value); }
        }

        #endregion

        #region events

        public KeyPressed KeyPressed;
        private void RaiseKeyPressed(FormulaKeyEventArgs e)
        {
            if(KeyPressed != null) KeyPressed.Invoke(e);
        }
        public void RaiseKeyPressed(FormulaEditorKey key)
        {
            RaiseKeyPressed(new FormulaKeyEventArgs(key, null, null));
       }
        public void RaiseKeyPressed(FormulaEditorKey key, ObjectVariableEntry objectVariable)
        {
            RaiseKeyPressed(new FormulaKeyEventArgs(key, objectVariable, null));
       }
        public void RaiseKeyPressed(UserVariable userVariable)
        {
            RaiseKeyPressed(new FormulaKeyEventArgs(FormulaEditorKey.UserVariable, null, userVariable));
        }

        public EvaluatePressed EvaluatePressed;
        private void RaiseEvaluatePressed()
        {
            if (EvaluatePressed != null)
                EvaluatePressed.Invoke();
        }

        #endregion

        public FormulaKeyboard()
        {
            InitializeComponent();
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
