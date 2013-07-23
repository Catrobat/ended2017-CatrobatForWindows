using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.Core.Objects.Formulas;
using Catrobat.Core.Objects.Variables;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls
{
    public partial class FormulaViewer : UserControl
    {
        #region DependencyProperties

        public bool IsEditEnabled
        {
            get { return (bool)GetValue(IsEditEnabledProperty); }
            set { SetValue(IsEditEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsEditEnabledProperty = DependencyProperty.Register("IsEditEnabled", typeof(bool), typeof(FormulaViewer), new PropertyMetadata(IsEditEnabledChanged));

        private static void IsEditEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO: implement me
            // Code for dealing with your property changes
        }



        public bool IsMultiline
        {
            get { return (bool)GetValue(IsMultilineProperty); }
            set { SetValue(IsMultilineProperty, value); }
        }

        public static readonly DependencyProperty IsMultilineProperty = DependencyProperty.Register("IsMultiline", typeof(bool), typeof(FormulaViewer), new PropertyMetadata(IsMultilineChanged));

        private static void IsMultilineChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO: implement me
            // Code for dealing with your property changes
        }



        public Formula Formula
        {
            get { return (Formula)GetValue(FormulaProperty); }
            set { SetValue(FormulaProperty, value); }
        }

        public static readonly DependencyProperty FormulaProperty = DependencyProperty.Register("Formula", typeof(Formula), typeof(FormulaViewer), new PropertyMetadata(FormulaChanged));

        private static void FormulaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO: implement me
            // Code for dealing with your property changes
        }


        #endregion


        public FormulaViewer()
        {
            InitializeComponent();
        }

        public void KeyPressed(FormulaEditorKey key)
        {
            // TODO: implement me
            throw new NotImplementedException();
        }

        public void ObjectVariableSelected(ObjectVariable variable)
        {
            // TODO: implement me
            throw new NotImplementedException();
        }

        public void SensorVariableSelected(SensorVariable variable)
        {
            // TODO: implement me
            throw new NotImplementedException();
        }

        public void LocalVariableSelected(UserVariable variable)
        {
            // TODO: implement me
            throw new NotImplementedException();
        }

        public void GlobalVariableSelected(UserVariable variable)
        {
            // TODO: implement me
            throw new NotImplementedException();
        }
    }
}
