using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.Core.Objects.Formulas;
using Catrobat.Core.Objects.Variables;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas.Math;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Reactive;
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

            var uiFormula = UiFormulaMappings.CreateFormula(formula.FormulaTree, ((FormulaViewer)d).IsEditEnabled);
            ((FormulaViewer)d).FormulaViewerTreeItemRoot.UiFormula = uiFormula;

            ((FormulaViewer)d).FormulaChanged();
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
            ((FormulaViewer) d).FormulaChanged();
        }

        #endregion


        public FormulaViewer()
        {
            InitializeComponent();
        }

        public void FormulaChanged()
        {
            // TODO: update rows, update size
            var width = FormulaViewerTreeItemRoot.ActualWidth;

            var size = 50.0;

            if (width > 10.0)
            {
                size = FindFontSize(width);
            }

            if (size > 80.0)
                size = 80.0;

            if (size < 20.0)
                size = 20.0;

            if (FormulaViewerTreeItemRoot.UiFormula != null)
                FormulaViewerTreeItemRoot.UiFormula.FontSize = 24; // TODO: add auto size
        }

        private double FindFontSize(double width)
        {
            return 50.0 * 480.0 / width;
        }

        public void KeyPressed(FormulaEditorKey key)
        {
            // TODO: implement me

            var formula = new Formula
            {
                FormulaTree = new FormulaTree
                {
                    VariableType = "random",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "random", 
                        LeftChild = new FormulaTree {VariableType = "NUMBER", VariableValue = "4"},
                        RightChild = new FormulaTree {VariableType = "NUMBER", VariableValue = "2"}
                    },
                    RightChild = new FormulaTree
                    {
                        VariableType = "random",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "random",
                            LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4" },
                            RightChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "2" }
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "random",
                            LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4" },
                            RightChild = new FormulaTree
                            {
                                VariableType = "random",
                                LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "41" },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "random",
                                    LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "42222" },
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "random",
                                        LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "42134141" },
                                        RightChild = new FormulaTree
                                        {
                                            VariableType = "random",
                                            LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "41241" },
                                            RightChild = new FormulaTree
                                            {
                                                VariableType = "random",
                                                LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4124" },
                                                RightChild = new FormulaTree
                                                {
                                                    VariableType = "random",
                                                    LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "42" },
                                                    RightChild = new FormulaTree
                                                    {
                                                        VariableType = "random",
                                                        LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4124142" },
                                                        RightChild = new FormulaTree
                                                        {
                                                            VariableType = "random",
                                                            LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "424" },
                                                            RightChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "21241241412412414" }
                                                        },
                                                    },
                                                },
                                            },
                                        },
                                    },
                                },
                            },
                        },
                    },
                }
            };

            var uiFormula = UiFormulaMappings.CreateFormula(formula.FormulaTree, IsEditEnabled);
            FormulaViewerTreeItemRoot.UiFormula = uiFormula;
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
    }
}
