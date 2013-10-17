using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.UI.Converters;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects.Bricks;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Phone.Converters;
using Catrobat.IDE.Phone.ViewModel.Editor.Formula;
using Catrobat.IDE.Phone.Views.Editor.Formula;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDE.Phone.Controls.FormulaControls
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

            Catrobat.IDE.Core.Services.ServiceLocator.NavigationService.NavigateTo(typeof(VariableSelectionView));
        }

        private void ContainerOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var container = (VariableConteiner)sender;

            if (args.PropertyName == PropertyHelper.
                GetPropertyName(() => container.Variable))
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
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyHelper.GetPropertyName(selector)));
            }
        }

        #endregion
    }
}
