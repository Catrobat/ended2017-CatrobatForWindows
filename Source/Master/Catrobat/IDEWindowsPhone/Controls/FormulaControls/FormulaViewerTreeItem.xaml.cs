using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.Core.Objects.Formulas;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls
{
    public partial class FormulaViewerTreeItem : UserControl
    {
        #region DependencyProperties

        public FormulaTree TreeItem
        {
            get { return (FormulaTree)GetValue(TreeItemProperty); }
            set { SetValue(TreeItemProperty, value); }
        }

        public static readonly DependencyProperty TreeItemProperty = DependencyProperty.Register("TreeItem", typeof(FormulaTree), typeof(FormulaViewerTreeItem), new PropertyMetadata(TreeItemChanged));

        private static void TreeItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var that = (FormulaViewerTreeItem)d;

            that.UpdateUI();

            if (e.NewValue == null) return;

            ((FormulaTree)e.NewValue).PropertyChanged -= that.OnPropertyChanged;
            ((FormulaTree)e.NewValue).PropertyChanged += that.OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateUI();
        }

        #endregion

        private void UpdateUI()
        {
            if (TreeItem == null)
            {
                TextBlockParent.Text = "";
            }
            else
            {
                TextBlockParent.Text = TreeItem.VariableValue;

                GridLeftChild.Children.Clear();
                var leftChild = new FormulaViewerTreeItem {TreeItem = TreeItem.LeftChild};
                GridLeftChild.Children.Add(leftChild);

                GridRightChild.Children.Clear();
                var rightChild = new FormulaViewerTreeItem {TreeItem = TreeItem.RightChild};
                GridRightChild.Children.Add(rightChild);
            }
        }

        public FormulaViewerTreeItem()
        {
            InitializeComponent();
        }
    }
}
