using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Phone.Converters;

namespace Catrobat.IDE.Phone.Controls.Formulas.Templates
{
    public class FormulaTokenTemplate
    {
        #region Members

        [TypeConverter(typeof(NamespaceTypeConverter))]
        public Type TokenType { get; set; }

        public FormulaTokenStyleCollection TokenStyle { get; set; }

        private double _relativeLeftMargin = 0.5;
        /// <summary>
        /// Adjusts <see cref="Thickness.Left" /> of <see cref="Grid.Margin" /> measured in percentage of <see cref="Grid.ActualWidth" />. 
        /// </summary>
        public double RelativeLeftMargin
        {
            get { return _relativeLeftMargin; }
            set { _relativeLeftMargin = value; }
        }

        private double _relativeRightMargin = 0.5;
        /// <summary>
        /// Adjusts <see cref="Thickness.Right" /> of <see cref="Grid.Margin" /> measured in percentage of <see cref="Grid.ActualWidth" />. 
        /// </summary>
        public double RelativeRightMargin
        {
            get { return _relativeRightMargin; }
            set { _relativeRightMargin = value; }
        }

        #endregion

        public Grid CreateContainer(IFormulaToken token)
        {
            // DataContext is misused to hold the template
            var container = new Grid {HorizontalAlignment = HorizontalAlignment.Left, DataContext = this};
            container.Children.Add(new TextBlock {Text = FormulaSerializer.Serialize(token)});
            container.SizeChanged += (sender, e) => UpdateMargin(container);
            return container;
        }

        public static int GetCharacterWidth(Grid container)
        {
            return container.Children.OfType<TextBlock>()
                .Where(textBlock => textBlock.Text != null)
                .Sum(textBlock => textBlock.Text.Length);
        }

        public static void SetFontSize(Grid container, double fontSize)
        {
            ((TextBlock) container.Children[0]).FontSize = fontSize;

            // trigger SizeChanged event (see UpdateMargin)
            container.Margin = new Thickness(0);
        }

        private void UpdateMargin(Grid container)
        {
            if (Math.Abs(RelativeLeftMargin - 0.5) <= double.Epsilon && 
                Math.Abs(RelativeRightMargin - 0.5) <= double.Epsilon) return;

            // apply Math.Round to avoid endless loops (Margin becomes rounded to screen pixels)
            var width = container.ActualWidth;
            container.Margin = new Thickness(
                left: -Math.Round(width * (0.5 - RelativeLeftMargin)),
                top: 0,
                right: -Math.Round(width * (0.5 - RelativeRightMargin)),
                bottom: 0);
        }

        public static void SetStyle(Grid container, bool isSelected)
        {
            // see CreateContainer
            var template = (FormulaTokenTemplate)container.DataContext;

            var style = template.TokenStyle;
            var containerStyle = isSelected ? style.SelectedContainerStyle : style.ContainerStyle;
            var textStyle = isSelected ? style.SelectedTextStyle : style.TextStyle;

            container.Style = containerStyle;
            ((TextBlock) container.Children[0]).Style = textStyle;
        }
    }
}
