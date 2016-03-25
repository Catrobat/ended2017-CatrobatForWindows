using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Catrobat.IDE.Core.Formulas.Editor;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Formula;
using GalaSoft.MvvmLight.Messaging;
using System;

namespace Catrobat.IDE.WindowsPhone.Controls.FormulaControls
{
    public delegate void KeyPressed(FormulaKeyEventArgs e);
    public delegate void EvaluatePressed();
    public delegate void ShowErrorPressed();

    public partial class FormulaKeyboard
    {
        private readonly FormulaKeyboardViewModel _viewModel = ServiceLocator.ViewModelLocator.FormulaKeyboardViewModel;
        public FormulaKeyboardViewModel ViewModel
        {
            get { return _viewModel; }
        }

        #region Dependency properties

        public static readonly DependencyProperty ProgramProperty = DependencyProperty.Register(
            name: "Program",
            propertyType: typeof(Program),
            ownerType: typeof(FormulaKeyboard),
            typeMetadata: new PropertyMetadata(null, (d, e) => ((FormulaKeyboard)d).ProgramChanged(e)));
        public Program Project
        {
            get { return (Program)GetValue(ProgramProperty); }
            set { SetValue(ProgramProperty, value); }
        }
        private void ProgramChanged(DependencyPropertyChangedEventArgs e)
        {
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

        public static readonly DependencyProperty CanLeftProperty = DependencyProperty.Register(
            name: "CanLeft",
            propertyType: typeof(bool),
            ownerType: typeof(FormulaKeyboard),
            typeMetadata: new PropertyMetadata(false));
        public bool CanLeft
        {
            get { return (bool)GetValue(CanLeftProperty); }
            set { SetValue(CanLeftProperty, value); }
        }

        public static readonly DependencyProperty CanRightProperty = DependencyProperty.Register(
            name: "CanRight",
            propertyType: typeof(bool),
            ownerType: typeof(FormulaKeyboard),
            typeMetadata: new PropertyMetadata(false));
        public bool CanRight
        {
            get { return (bool)GetValue(CanRightProperty); }
            set { SetValue(CanRightProperty, value); }
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

        public static readonly DependencyProperty HasErrorProperty = DependencyProperty.Register(
            name: "HasError",
            propertyType: typeof(bool),
            ownerType: typeof(FormulaKeyboard),
            typeMetadata: new PropertyMetadata(false));
        public bool HasError
        {
            get { return (bool)GetValue(HasErrorProperty); }
            set { SetValue(HasErrorProperty, value); }
        }

        public static readonly DependencyProperty DecimalSeparatorProperty = DependencyProperty.Register("DecimalSeparator", typeof(string), typeof(FormulaKeyboard), new PropertyMetadata("."));
        public string DecimalSeparator
        {
            get { return (string)GetValue(DecimalSeparatorProperty); }
            private set { SetValue(DecimalSeparatorProperty, value); }
        }

        #endregion

        #region Events

        public KeyPressed KeyPressed;
        private void RaiseKeyPressed(FormulaKeyEventArgs e)
        {
            if (KeyPressed != null) KeyPressed.Invoke(e);
        }
        public void RaiseKeyPressed(FormulaKey data)
        {
            RaiseKeyPressed(new FormulaKeyEventArgs(data));
        }

        //public EvaluatePressed EvaluatePressed;
        //private void RaiseEvaluatePressed()
        //{
        //    if (EvaluatePressed != null) EvaluatePressed.Invoke();
        //}

        #endregion

        public FormulaKeyboard()
        {
            InitializeComponent();

            ShowMain();

            DecimalSeparator = ServiceLocator.CultureService.GetCulture().NumberFormat.NumberDecimalSeparator;

            Messenger.Default.Register<FormulaEvaluationResult>(this, ViewModelMessagingToken.FormulaEvaluated, FormulaChangedMessageAction);

            this.CheckSensorKeys();
        }

        private void CheckSensorKeys()
        {
            if (KeysSensors == null || KeysSensors.Items == null || KeysSensors.Items.Count <= 0)
            {
                return;
            }

            foreach (FormulaKey key in KeysSensors.Items)
            {
                if (key.Key.Equals(FormulaEditorKey.AccelerationX) ||
                        key.Key.Equals(FormulaEditorKey.AccelerationY) ||
                        key.Key.Equals(FormulaEditorKey.AccelerationZ))
                {
                    key.Enabled = ServiceLocator.SensorService.IsAccelarationEnabled();
                }
                else if (key.Key.Equals(FormulaEditorKey.InclinationX) ||
                        key.Key.Equals(FormulaEditorKey.InclinationY))
                {
                    key.Enabled = ServiceLocator.SensorService.IsInclinationEnabled();
                }
                else if (key.Key.Equals(FormulaEditorKey.Loudness))
                {
                    key.Enabled = ServiceLocator.SensorService.IsMicrophoneEnabled();
                }
                else if (key.Key.Equals(FormulaEditorKey.Compass))
                {
                    key.Enabled = ServiceLocator.SensorService.IsCompassEnabled();
                }
            }
        }

        private void FormulaChangedMessageAction(FormulaEvaluationResult result)
        {
            ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    TextBlockEvaluationValue.Text = result.Value ?? "";
                    TextBlockEvaluationError.Text = result.Error ?? "";
                });
        }

        #region VisualStateManager

        private void ShowMain()
        {
            GridMain.Visibility = Visibility.Visible;
            GridMore.Visibility = Visibility.Collapsed;
            Pivot_OnSelectionChanged(null, null);
        }

        private void ShowMore()
        {
            GridMain.Visibility = Visibility.Collapsed;
            GridMore.Visibility = Visibility.Visible;
            Pivot_OnSelectionChanged(null, null);
        }

        #endregion

        private void BtnMore_OnClick(object sender, RoutedEventArgs e)
        {
            ShowMore();
        }

        private void ButtonMoreBack_Click(object sender, RoutedEventArgs e)
        {
            ShowMain();
        }

        private void KeyButton_OnClick(object sender, RoutedEventArgs e)
        {
            var key = (FormulaEditorKey)(int)((FrameworkElement)sender).DataContext;
            ShowMain();
            RaiseKeyPressed(new FormulaKey(key, null));
        }

        private void ListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var source = (Selector)sender;
            if (source.SelectedItem == null) return;
            //_viewModel.IsAddLocalVariableButtonVisible = false;
            //_viewModel.IsAddGlobalVariableButtonVisible = false;
            ShowMain();
            var data = (FormulaKey)source.SelectedItem;

            if (!CheckSensorKeys(data))
            {
                return;
            }

            RaiseKeyPressed(data);
            _viewModel.KeyPressedCommand.Execute(data);
            source.SelectedItem = null;
        }

        private bool CheckSensorKeys(FormulaKey key)
        {
            if (key.Key.Equals(FormulaEditorKey.AccelerationX) ||
                key.Key.Equals(FormulaEditorKey.AccelerationY) ||
                key.Key.Equals(FormulaEditorKey.AccelerationZ))
            {
                if (!ServiceLocator.SensorService.IsAccelarationEnabled())
                {
                    return false;
                }

                return true;
            }

            if (key.Key.Equals(FormulaEditorKey.Compass))
            {
                if (!ServiceLocator.SensorService.IsCompassEnabled())
                {
                    return false;
                }

                return true;
            }

            if (key.Key.Equals(FormulaEditorKey.InclinationX) ||
                key.Key.Equals(FormulaEditorKey.InclinationY))
            {
                if (!ServiceLocator.SensorService.IsInclinationEnabled())
                {
                    return false;
                }

                return true;
            }

            return true;
        }

        private void Pivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var parentVisible = GridMore.Visibility == Visibility.Visible && PivotMore.SelectedItem == PivotItemVariables;
            //ViewModel.IsAddLocalVariableButtonVisible = parentVisible && PivotVariables.SelectedItem == PivotItemLocalVariables;
            //ViewModel.IsAddGlobalVariableButtonVisible = parentVisible && PivotVariables.SelectedItem == PivotItemGlobalVariables;
        }

        //private void ButtonEvaluate_OnClick(object sender, RoutedEventArgs e)
        //{
        //    RaiseEvaluatePressed();
        //}
    }
}
