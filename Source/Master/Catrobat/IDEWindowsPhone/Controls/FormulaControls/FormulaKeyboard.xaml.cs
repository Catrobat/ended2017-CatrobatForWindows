using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.Core.Objects.Variables;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls
{
    public enum FormulaEditorKey
    {
        Number0, Number1, Number2, Number3, Number4, Number5, Number6, Number7, Number8, Number9, NumberDot,
        KeyEquals, KeyDelete, KeyUndo, KeyRedo, KeyOpenBrecket, KeyClosedBrecket, KeyPlus, KeyMinus, KeyMult, KeyDevide, KeyCompute,
        KeyLogicEqual, KeyLogicNotEqual, KeyLogicSmaller, KeyLogicSmallerEqual, KeyLogiGreater, KeyLogicGreaterEqual, 
        KeyLogicAnd, KeyLogicOr, KeyLogicNot, KeyLogicTrue, KeyLogicFalse
    }

    public enum ObjectVariable
    {
        PositionX, PositionY, Transparency, Brightness, Size, Direction, Layer
    }

    public enum SensorVariable
    {
        AccelerationX, AccelerationY, AccelerationZ, CompassDirection, InclinationX, InclinationY
    }

    public delegate void KeyPressed(FormulaEditorKey key);
    public delegate void ObjectVariableSelected(ObjectVariable variable);
    public delegate void SensorVariableSelected(SensorVariable variable);
    public delegate void LocalUserVariableSelected(UserVariable variable);
    public delegate void GlobalUserVariableSelected(UserVariable variable);

    public partial class FormulaKeyboard : UserControl
    {
        #region DependancyProperties

        public UserVariable GlobalVariables
        {
            get { return (UserVariable)GetValue(GlobalVariablesProperty); }
            set { SetValue(GlobalVariablesProperty, value); }
        }

        public static readonly DependencyProperty GlobalVariablesProperty = DependencyProperty.Register("GlobalVariables", typeof(UserVariable), typeof(FormulaKeyboard), new PropertyMetadata(GlobalVariablesChanged));

        private static void GlobalVariablesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FormulaKeyboard) d).ListBoxGlobalVariables.ItemsSource = e.NewValue as IEnumerable;
        }




        public UserVariable LocalVariables
        {
            get { return (UserVariable)GetValue(LocalVariablesProperty); }
            set { SetValue(LocalVariablesProperty, value); }
        }

        public static readonly DependencyProperty LocalVariablesProperty = DependencyProperty.Register("LocalVariables", typeof(UserVariable), typeof(FormulaKeyboard), new PropertyMetadata(LocalVariablesChanged));

        private static void LocalVariablesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FormulaKeyboard)d).ListBoxLocalVariables.ItemsSource = e.NewValue as IEnumerable;
        }

        #endregion


        public KeyPressed KeyPressed;
        public ObjectVariableSelected ObjectVariableSelected;
        public SensorVariableSelected SensorVariableSelected;
        public LocalUserVariableSelected LocalUserVariableSelected;
        public GlobalUserVariableSelected GlobalUserVariableSelected;

        public void RaiseKeyPressed(FormulaEditorKey key)
        {
            if(KeyPressed != null)
                KeyPressed.Invoke(key);
        }

        public void RaiseObjectVariableSelected(ObjectVariable variable)
        {
            if (ObjectVariableSelected != null)
                ObjectVariableSelected.Invoke(variable);
        }

        public void RaiseSensorVariableSelected(SensorVariable variable)
        {
            if (SensorVariableSelected != null)
                SensorVariableSelected.Invoke(variable);
        }

        public void RaiseLocalUserVariableSelected(UserVariable variable)
        {
            if (LocalUserVariableSelected != null)
                LocalUserVariableSelected.Invoke(variable);
        }

        public void RaiseGlobalUserVariableSelected(UserVariable variable)
        {
            if (GlobalUserVariableSelected != null)
                GlobalUserVariableSelected.Invoke(variable);
        }

        public FormulaKeyboard()
        {
            InitializeComponent();
        }

        private void ButtonVariable_OnClick(object sender, RoutedEventArgs e)
        {
            ShowVariable();
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

        private void ShowMain()
        {
            GridMain.Visibility = Visibility.Visible;
            GridMath.Visibility = Visibility.Collapsed;
            GridVariable.Visibility = Visibility.Collapsed;
        }
        private void ShowMath()
        {
            GridMain.Visibility = Visibility.Collapsed;
            GridMath.Visibility = Visibility.Visible;
            GridVariable.Visibility = Visibility.Collapsed;
        }

        private void ShowVariable()
        {
            GridMain.Visibility = Visibility.Collapsed;
            GridMath.Visibility = Visibility.Collapsed;
            GridVariable.Visibility = Visibility.Visible;
        }

        private void KeyButton_OnClick(object sender, RoutedEventArgs e)
        {
            var key = (FormulaEditorKey)(uint)((FrameworkElement)sender).DataContext;
            RaiseKeyPressed(key);
        }
    }
}
