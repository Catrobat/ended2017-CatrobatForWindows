using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.Core.Objects.Formulas;
using Catrobat.Core.Objects.Variables;
using Catrobat.IDEWindowsPhone.Annotations;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas.Math;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Reactive;
using Microsoft.Phone.Shell;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls
{
    public partial class FormulaViewer : UserControl, INotifyPropertyChanged
    {
        private UiFormula _uiFormula;
        private FormulaTree _selectedFormula;

        #region DependencyProperties

        public int NormalFontSize
        {
            get { return (int)GetValue(NormalFontSizeProperty); }
            set { SetValue(NormalFontSizeProperty, value); }
        }

        public static readonly DependencyProperty NormalFontSizeProperty = DependencyProperty.Register("NormalFontSize", typeof(int), typeof(FormulaViewer), new PropertyMetadata(0, NormalFontSizeChanged));

        private static void NormalFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Code for dealing with your property changes
        }



        public int MinFontSize
        {
            get { return (int)GetValue(MinFontSizeProperty); }
            set { SetValue(MinFontSizeProperty, value); }
        }

        public static readonly DependencyProperty MinFontSizeProperty = DependencyProperty.Register("MinFontSize", typeof(int), typeof(FormulaViewer), new PropertyMetadata(0, MinFontSizeChanged));

        private static void MinFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Code for dealing with your property changes
        }


        public int MaxFontSize
        {
            get { return (int)GetValue(MaxFontSizeProperty); }
            set { SetValue(MaxFontSizeProperty, value); }
        }

        public static readonly DependencyProperty MaxFontSizeProperty = DependencyProperty.Register("MaxFontSize", typeof(int), typeof(FormulaViewer), new PropertyMetadata(0, MaxFontSizeChanged));

        private static void MaxFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Code for dealing with your property changes
        }


        public int CharactersOnNormalFontSize
        {
            get { return (int)GetValue(CharactersOnNormalFontSizeProperty); }
            set { SetValue(CharactersOnNormalFontSizeProperty, value); }
        }

        public static readonly DependencyProperty CharactersOnNormalFontSizeProperty = DependencyProperty.Register("CharactersOnNormalFontSize", typeof(int), typeof(FormulaViewer), new PropertyMetadata(0, CharactersOnNormalFontSizeChanged));

        private static void CharactersOnNormalFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Code for dealing with your property changes
        }


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
            set
            {
                SetValue(FormulaProperty, value);
                FormulaChanged();
            }
        }

        public static readonly DependencyProperty FormulaProperty = DependencyProperty.Register("Formula", typeof(Formula), typeof(FormulaViewer), new PropertyMetadata(FormulaChanged));

        private static void FormulaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var formula = e.NewValue as Formula;
            if (formula == null) return;

            ((FormulaViewer)d).FormulaChanged();


            //((FormulaViewer)d).FormulaViewerTreeItemRoot.UiFormula = uiFormula;



            //((FormulaViewer)d).FormulaChanged();
        }



        public bool IsAutoFontSize
        {
            get { return (bool)GetValue(IsAutoFontSizeProperty); }
            set
            {
                SetValue(IsAutoFontSizeProperty, value);
                FormulaChanged();
            }
        }

        public static readonly DependencyProperty IsAutoFontSizeProperty = DependencyProperty.Register("IsAutoFontSize", typeof(bool), typeof(FormulaViewer), new PropertyMetadata(IsAutoFontSizeChanged));

        private static void IsAutoFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FormulaViewer)d).FormulaChanged();
        }

        #endregion


        public FormulaViewer()
        {
            InitializeComponent();
        }

        public void SelectedFormulaChanged(FormulaTree formula)
        {
            _selectedFormula = formula;
        }

        public void FormulaChanged()
        {
            if (Formula == null)
                return;

            _uiFormula = UiFormulaMappings.CreateFormula(Formula, this, Formula.FormulaTree, IsEditEnabled, _selectedFormula);
            var allParts = _uiFormula.GetAllParts();

            var fontSize = NormalFontSize;

            if (IsAutoFontSize)
            {
                var charactersWidth = allParts.Sum(control => control.GetCharacterWidth());

                if (charactersWidth < 50)
                    charactersWidth += 10;

                if (charactersWidth < 100)
                    charactersWidth += 10;

                if (charactersWidth < 200)
                    charactersWidth += 10;

                fontSize = (int) (NormalFontSize * ((double)CharactersOnNormalFontSize / charactersWidth));

                if (fontSize < MinFontSize)
                    fontSize = MinFontSize;
                if (fontSize > MaxFontSize)
                    fontSize = MaxFontSize;
            }

            var allControls = allParts.Select(part => part.CreateUiControls(fontSize, false, false)).ToList();
            _uiFormula.UpdateStyles(false);


            PanelContent.Children.Clear();
            foreach (var part in allControls)
                if (part != null)
                    PanelContent.Children.Add(part);

            var scrollViewerHeight = ScrollViewerContent.ActualWidth;
            var contentPanelHeight = PanelContent.ActualWidth;
            ScrollViewerContent.InvalidateScrollInfo();
            var scrollInfo = ScrollViewerContent.ScrollableHeight;
        }

        public void KeyPressed(FormulaEditorKey key)
        {
            // TODO: implement me

            //var uiFormula = UiFormulaMappings.CreateFormula(formula.FormulaTree, IsEditEnabled);
            //FormulaViewerTreeItemRoot.UiFormula = uiFormula;
            FormulaChanged();
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

        private void ListBoxParts_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((ListBox)sender).SelectedItem = null;
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public SelectedFormulaInformation GetSelectedFormula()
        {
            return _uiFormula.GetSelectedFormula();
        }
    }
}
