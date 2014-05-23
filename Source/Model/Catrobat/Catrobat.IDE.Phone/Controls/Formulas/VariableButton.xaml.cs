using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.UI.Converters;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Formula;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Variables;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;

namespace Catrobat.IDE.Phone.Controls.Formulas
{
    public partial class VariableButton : UserControl, INotifyPropertyChanged
    {
        #region DependencyProperties

        public XmlUserVariable Variable
        {
            get { return (XmlUserVariable) GetValue(VariableProperty); }
            set { SetValue(VariableProperty, value); }
        }

        public static readonly DependencyProperty VariableProperty = DependencyProperty.Register("Variable", typeof(XmlUserVariable), typeof(VariableButton), new PropertyMetadata(VariableChanged));

        private static void VariableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((VariableButton)d).RaisePropertyChanged(() => ((VariableButton)d).Variable);
            ((VariableButton)d).VariableChanged((XmlUserVariable) e.NewValue);
        }

        #endregion

        private new static void IsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((VariableButton)d).IsEnabled = (bool)e.NewValue;
        }

        public void VariableChanged(XmlUserVariable newVariable)
        {
            var setVariableBrick = DataContext as XmlSetVariableBrick;
            if (setVariableBrick != null)
                setVariableBrick.UserVariable = newVariable;

            var changeVariableBrick = DataContext as XmlChangeVariableBrick;
            if (changeVariableBrick != null)
                changeVariableBrick.UserVariable = newVariable;

            var isSelected = newVariable != null;
            var converter = new NullVariableConverter();

            newVariable = (XmlUserVariable) converter.Convert(newVariable, null, null, null);

            var viewModel = ((ViewModelLocator)ServiceLocator.ViewModelLocator).VariableSelectionViewModel;

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
            var viewModel = ((ViewModelLocator)ServiceLocator.ViewModelLocator).VariableSelectionViewModel;

            var container = new VariableConteiner { Variable = Variable };
            container.PropertyChanged += ContainerOnPropertyChanged;
            viewModel.SelectedVariableContainer = container;

            Catrobat.IDE.Core.Services.ServiceLocator.NavigationService.NavigateTo<VariableSelectionViewModel>();
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
