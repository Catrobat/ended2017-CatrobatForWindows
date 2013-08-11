using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Catrobat.Core.Annotations;
using Catrobat.Core.Misc;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Formulas;
using Catrobat.Core.Objects.Variables;
using Catrobat.IDEWindowsPhone.Converters;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.ViewModel;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Formula;
using Catrobat.IDEWindowsPhone.Views.Editor.Formula;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls
{
    public partial class VariableButton : UserControl, INotifyPropertyChanged
    {
        #region DependencyProperties

        public UserVariable Variable
        {
            get { return (UserVariable)GetValue(VariableProperty); }
            set { SetValue(VariableProperty, value); }
        }

        public static readonly DependencyProperty VariableProperty = DependencyProperty.Register("Variable", typeof(UserVariable), typeof(VariableButton), new PropertyMetadata(VariableChanged));

        private static void VariableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((VariableButton)d).RaisePropertyChanged(() => ((VariableButton)d).Variable);
            ((VariableButton)d).VariableChanged((UserVariable)e.NewValue);
        }

        #endregion

        private new static void IsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((VariableButton)d).IsEnabled = (bool)e.NewValue;
        }

        public void VariableChanged(UserVariable newVariable)
        {
            var setVariableBrick = DataContext as SetVariableBrick;
            if (setVariableBrick != null)
                setVariableBrick.UserVariable = newVariable;

            var changeVariableBrick = DataContext as ChangeVariableBrick;
            if (changeVariableBrick != null)
                changeVariableBrick.UserVariable = newVariable;

            var isSelected = newVariable != null;
            var converter = new NullVariableConverter();

            newVariable = (UserVariable)converter.Convert(newVariable, null, null, null);

            var viewModel = ServiceLocator.Current.GetInstance<VariableSelectionViewModel>();

            TextBlockVariableName.Text = newVariable.Name;

            if (isSelected)
            {
                TextBlockVariableName.Foreground = VariableHelper.IsVariableLocal(viewModel.CurrentProject, newVariable) ?
                    new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.Gray);
            }
            else
            {
                TextBlockVariableName.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        public VariableButton()
        {
            InitializeComponent();
            VariableChanged(null);
        }

        private void ButtonFormula_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = ServiceLocator.Current.GetInstance<VariableSelectionViewModel>();

            var container = new VariableConteiner { Variable = Variable };
            container.PropertyChanged += ContainerOnPropertyChanged;
            viewModel.SelectedVariableContainer = container;

            Navigation.NavigateTo(typeof(VariableSelectionView));
        }

        private void ContainerOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var container = (VariableConteiner)sender;

            if (args.PropertyName == PropertyNameHelper.
                GetPropertyNameFromExpression(() => container.Variable))
            {
                VariableChanged(container.Variable);
                //SetValue(VariableProperty, container.Variable);
            }
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RaisePropertyChanged<T>(Expression<Func<T>> selector)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyNameHelper.GetPropertyNameFromExpression(selector)));
            }
        }

        #endregion
    }
}
