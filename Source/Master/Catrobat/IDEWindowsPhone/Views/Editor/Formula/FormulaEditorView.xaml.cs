using System;
using System.ComponentModel;
using Catrobat.Core.Objects.Formulas;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Costumes;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Formula;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Formula
{
    public partial class FormulaEditorView : PhoneApplicationPage
    {
        private readonly FormulaEditorViewModel _viewModel = ServiceLocator.Current.GetInstance<FormulaEditorViewModel>();

        public FormulaEditorView()
        {
            InitializeComponent();
            FormulaKeyboard.KeyPressed += KeyPressed;
            FormulaKeyboard.LocalUserVariableSelected += LocalVariableSelected;
            FormulaKeyboard.GlobalUserVariableSelected += GlobalVariableSelected;
            FormulaKeyboard.ObjectVariableSelected += ObjectVariableSelected;
            FormulaKeyboard.SensorVariableSelected += SensorVariableSelected;
            FormulaKeyboard.EvaluatePresed += EvaluatePresed;
        }

        private string GetKeyPressed(FormulaEditorKey key)
        {
            switch (key)
            {
                case FormulaEditorKey.Number0:
                    return "0";
                case FormulaEditorKey.Number1:
                    return "1";
                case FormulaEditorKey.Number2:
                    return "2";
                case FormulaEditorKey.Number3:
                    return "3";
                case FormulaEditorKey.Number4:
                    return "4";
                case FormulaEditorKey.Number5:
                    return "5";
                case FormulaEditorKey.Number6:
                    return "6";
                case FormulaEditorKey.Number7:
                    return "7";
                case FormulaEditorKey.Number8:
                    return "8";
                case FormulaEditorKey.Number9:
                    return "9";
                default:
                    return null;
            }
        }

        private void KeyPressed(FormulaEditorKey key)
        {
            var selectedFormulaInfo = FormulaViewer.GetSelectedFormula();

            if (selectedFormulaInfo == null) return;

            if (selectedFormulaInfo.SelectedFormula.VariableType == "NUMBER")
            {
                var oldVariableValue = selectedFormulaInfo.SelectedFormula.VariableValue;

                var digitString = GetKeyPressed(key);
                if (digitString != null)
                {
                    selectedFormulaInfo.SelectedFormula.VariableValue += digitString;
                }

                // TODO: handle decimal seperator
                //if(oldVariableValue.Contains())
                
            }
            else
                if (selectedFormulaInfo.SelectedFormulaParent != null)
                {

                    if (selectedFormulaInfo.SelectedFormulaParent.LeftChild == selectedFormulaInfo.SelectedFormula)
                    {
                        //selectedFormulaInfo.SelectedFormulaParent.LeftChild = new FormulaTree
                        //{
                        //    VariableType = "random",
                        //    LeftChild = new FormulaTree
                        //    {
                        //        VariableType = "random",
                        //        LeftChild = new FormulaTree {VariableType = "NUMBER", VariableValue = "4"},
                        //        RightChild = new FormulaTree {VariableType = "NUMBER", VariableValue = "2"}
                        //    }
                        //};
                    }
                    else
                    {
                        //selectedFormulaInfo.SelectedFormulaParent.RightChild = new FormulaTree
                        //{
                        //    VariableType = "random",
                        //    LeftChild = new FormulaTree
                        //    {
                        //        VariableType = "random",
                        //        LeftChild = new FormulaTree {VariableType = "NUMBER", VariableValue = "4"},
                        //        RightChild = new FormulaTree {VariableType = "NUMBER", VariableValue = "2"}
                        //    }
                        //};
                    }
                }
                else
                {
                    _viewModel.Formula.FormulaTree = new FormulaTree
                    {
                        VariableType = "random",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "random",
                            LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4" },
                            RightChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "2" }
                        }
                    };
                }


            //switch (key)
            //{
            //    case FormulaEditorKey.Number0:
            //        var formula1 = new Core.Objects.Formulas.Formula
            //        {
            //            FormulaTree = new FormulaTree
            //            {
            //                VariableType = "random",
            //                LeftChild = new FormulaTree
            //                {
            //                    VariableType = "random",
            //                    LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4" },
            //                    RightChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "2" }
            //                },
            //                RightChild = new FormulaTree
            //                {
            //                    VariableType = "random",
            //                    LeftChild = new FormulaTree
            //                    {
            //                        VariableType = "random",
            //                        LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4" },
            //                        RightChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "2" }
            //                    },
            //                    RightChild = new FormulaTree
            //                    {
            //                        VariableType = "random",
            //                        LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4" },
            //                        RightChild = new FormulaTree
            //                        {
            //                            VariableType = "random",
            //                            LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "41" },
            //                            //RightChild = new FormulaTree
            //                            //{
            //                            //    VariableType = "random",
            //                            //    LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "42222" },
            //                            //    RightChild = new FormulaTree
            //                            //    {
            //                            //        VariableType = "random",
            //                            //        LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "42134141" },
            //                            //        RightChild = new FormulaTree
            //                            //        {
            //                            //            VariableType = "random",
            //                            //            LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "41241" },
            //                            //            RightChild = new FormulaTree
            //                            //            {
            //                            //                VariableType = "random",
            //                            //                LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4124" },
            //                            //                RightChild = new FormulaTree
            //                            //                {
            //                            //                    VariableType = "random",
            //                            //                    LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "42" },
            //                            //                    RightChild = new FormulaTree
            //                            //                    {
            //                            //                        VariableType = "random",
            //                            //                        LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4124142" },
            //                            //                        RightChild = new FormulaTree
            //                            //                        {
            //                            //                            VariableType = "random",
            //                            //                            LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "424" },
            //                            //                            RightChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "21241241412412414" }
            //                            //                        },
            //                            //                    },
            //                            //                },
            //                            //            },
            //                            //        },
            //                            //    },
            //                            //},
            //                        },
            //                    },
            //                },
            //            }
            //        };

            //        FormulaViewer.Formula = formula1;
            //        break;
            //    case FormulaEditorKey.Number1:
            //        var formula2 = new Core.Objects.Formulas.Formula
            //        {
            //            FormulaTree = new FormulaTree
            //            {
            //                VariableType = "random",
            //                LeftChild = new FormulaTree
            //                {
            //                    VariableType = "random",
            //                    LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4" },
            //                    RightChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "2" }
            //                },
            //                RightChild = new FormulaTree
            //                {
            //                    VariableType = "random",
            //                    LeftChild = new FormulaTree
            //                    {
            //                        VariableType = "random",
            //                        LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4" },
            //                        RightChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "2" }
            //                    },
            //                    RightChild = new FormulaTree
            //                    {
            //                        VariableType = "random",
            //                        LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4" },
            //                        RightChild = new FormulaTree
            //                        {
            //                            VariableType = "random",
            //                            LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "41" },
            //                            RightChild = new FormulaTree
            //                            {
            //                                VariableType = "random",
            //                                LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "42222" },
            //                                RightChild = new FormulaTree
            //                                {
            //                                    VariableType = "random",
            //                                    LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "42134141" },
            //                                    RightChild = new FormulaTree
            //                                    {
            //                                        VariableType = "random",
            //                                        LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "41241" },
            //                                        //RightChild = new FormulaTree
            //                                        //{
            //                                        //    VariableType = "random",
            //                                        //    LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4124" },
            //                                        //    RightChild = new FormulaTree
            //                                        //    {
            //                                        //        VariableType = "random",
            //                                        //        LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "42" },
            //                                        //        RightChild = new FormulaTree
            //                                        //        {
            //                                        //            VariableType = "random",
            //                                        //            LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4124142" },
            //                                        //            RightChild = new FormulaTree
            //                                        //            {
            //                                        //                VariableType = "random",
            //                                        //                LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "424" },
            //                                        //                RightChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "21241241412412414" }
            //                                        //            },
            //                                        //        },
            //                                        //    },
            //                                        //},
            //                                    },
            //                                },
            //                            },
            //                        },
            //                    },
            //                },
            //            }
            //        };

            //        FormulaViewer.Formula = formula2;
            //        break;
            //    case FormulaEditorKey.Number2:
            //        var formula3 = new Core.Objects.Formulas.Formula
            //        {
            //            FormulaTree = new FormulaTree
            //            {
            //                VariableType = "random",
            //                LeftChild = new FormulaTree
            //                {
            //                    VariableType = "random",
            //                    LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4" },
            //                    RightChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "2" }
            //                },
            //                RightChild = new FormulaTree
            //                {
            //                    VariableType = "random",
            //                    LeftChild = new FormulaTree
            //                    {
            //                        VariableType = "random",
            //                        LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4" },
            //                        RightChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "2" }
            //                    },
            //                    RightChild = new FormulaTree
            //                    {
            //                        VariableType = "random",
            //                        LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4" },
            //                        RightChild = new FormulaTree
            //                        {
            //                            VariableType = "random",
            //                            LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "41" },
            //                            RightChild = new FormulaTree
            //                            {
            //                                VariableType = "random",
            //                                LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "42222" },
            //                                RightChild = new FormulaTree
            //                                {
            //                                    VariableType = "random",
            //                                    LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "42134141" },
            //                                    RightChild = new FormulaTree
            //                                    {
            //                                        VariableType = "random",
            //                                        LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "41241" },
            //                                        RightChild = new FormulaTree
            //                                        {
            //                                            VariableType = "random",
            //                                            LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4124" },
            //                                            RightChild = new FormulaTree
            //                                            {
            //                                                VariableType = "random",
            //                                                LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "42" },
            //                                                RightChild = new FormulaTree
            //                                                {
            //                                                    VariableType = "random",
            //                                                    LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "4124142" },
            //                                                    RightChild = new FormulaTree
            //                                                    {
            //                                                        VariableType = "random",
            //                                                        LeftChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "424" },
            //                                                        RightChild = new FormulaTree { VariableType = "NUMBER", VariableValue = "21241241412412414" }
            //                                                    },
            //                                                },
            //                                            },
            //                                        },
            //                                    },
            //                                },
            //                            },
            //                        },
            //                    },
            //                },
            //            }
            //        };

            //        FormulaViewer.Formula = formula3;
            //        break;
            //    case FormulaEditorKey.Number3:
            //        break;
            //    case FormulaEditorKey.Number4:
            //        break;
            //    case FormulaEditorKey.Number5:
            //        break;
            //    case FormulaEditorKey.Number6:
            //        break;
            //    case FormulaEditorKey.Number7:
            //        break;
            //    case FormulaEditorKey.Number8:
            //        break;
            //    case FormulaEditorKey.Number9:
            //        break;
            //    case FormulaEditorKey.NumberDot:
            //        break;
            //    case FormulaEditorKey.KeyEquals:
            //        break;
            //    case FormulaEditorKey.KeyDelete:
            //        break;
            //    case FormulaEditorKey.KeyUndo:
            //        break;
            //    case FormulaEditorKey.KeyRedo:
            //        break;
            //    case FormulaEditorKey.KeyOpenBrecket:
            //        break;
            //    case FormulaEditorKey.KeyClosedBrecket:
            //        break;
            //    case FormulaEditorKey.KeyPlus:
            //        break;
            //    case FormulaEditorKey.KeyMinus:
            //        break;
            //    case FormulaEditorKey.KeyMult:
            //        break;
            //    case FormulaEditorKey.KeyDivide:
            //        break;
            //    case FormulaEditorKey.KeyLogicEqual:
            //        break;
            //    case FormulaEditorKey.KeyLogicNotEqual:
            //        break;
            //    case FormulaEditorKey.KeyLogicSmaller:
            //        break;
            //    case FormulaEditorKey.KeyLogicSmallerEqual:
            //        break;
            //    case FormulaEditorKey.KeyLogicGreater:
            //        break;
            //    case FormulaEditorKey.KeyLogicGreaterEqual:
            //        break;
            //    case FormulaEditorKey.KeyLogicAnd:
            //        break;
            //    case FormulaEditorKey.KeyLogicOr:
            //        break;
            //    case FormulaEditorKey.KeyLogicNot:
            //        break;
            //    case FormulaEditorKey.KeyLogicTrue:
            //        break;
            //    case FormulaEditorKey.KeyLogicFalse:
            //        break;
            //    case FormulaEditorKey.KeyMathSin:
            //        break;
            //    case FormulaEditorKey.KeyMathCos:
            //        break;
            //    case FormulaEditorKey.KeyMathTan:
            //        break;
            //    case FormulaEditorKey.KeyMathArcSin:
            //        break;
            //    case FormulaEditorKey.KeyMathArcCos:
            //        break;
            //    case FormulaEditorKey.KeyMathArcTan:
            //        break;
            //    case FormulaEditorKey.KeyMathExp:
            //        break;
            //    case FormulaEditorKey.KeyMathLn:
            //        break;
            //    case FormulaEditorKey.KeyMathLog:
            //        break;
            //    case FormulaEditorKey.KeyMathAbs:
            //        break;
            //    case FormulaEditorKey.KeyMathRound:
            //        break;
            //    case FormulaEditorKey.KeyMathMod:
            //        break;
            //    case FormulaEditorKey.KeyMathMin:
            //        break;
            //    case FormulaEditorKey.KeyMathMax:
            //        break;
            //    case FormulaEditorKey.KeyMathSqrt:
            //        break;
            //    case FormulaEditorKey.KeyMathPi:
            //        break;
            //    case FormulaEditorKey.KeyMathRandom:
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException("key");
            //}

            FormulaViewer.Formula = _viewModel.Formula;
        }

        private void SensorVariableSelected(Controls.FormulaControls.SensorVariable variable)
        {
            throw new NotImplementedException();

        }

        private void ObjectVariableSelected(Controls.FormulaControls.ObjectVariable variable)
        {
            throw new NotImplementedException();
        }

        private void GlobalVariableSelected(Core.Objects.Variables.UserVariable variable)
        {
            throw new NotImplementedException();
        }

        private void LocalVariableSelected(Core.Objects.Variables.UserVariable variable)
        {
            throw new NotImplementedException();
        }

        private void Value()
        {
            var formula = new Core.Objects.Formulas.Formula
            {
                FormulaTree = new FormulaTree
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
        }


        private void EvaluatePresed()
        {
            throw new NotImplementedException();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            //_viewModel.ResetViewModelCommand.Execute(null);
            base.OnBackKeyPress(e);
        }
    }
}