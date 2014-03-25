using System.Windows.Media;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Phone.Annotations;
using Catrobat.IDE.Phone.Controls.FormulaControls.Formulas;
using Catrobat.IDE.Phone.Controls.FormulaControls.PartControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Catrobat.IDE.Phone.Controls.FormulaControls
{
    public partial class FormulaViewer3
    {
        #region DependencyProperties

        public static readonly DependencyProperty TokensProperty = DependencyProperty.Register(
            name: "Tokens",
            propertyType: typeof(ObservableCollection<IFormulaToken>),
            ownerType: typeof(FormulaViewer3),
            typeMetadata: new PropertyMetadata(null, (d, e) => ((FormulaViewer3)d).TokensPropertyChanged(e)));
        public ObservableCollection<IFormulaToken> Tokens
        {
            get { return (ObservableCollection<IFormulaToken>)GetValue(TokensProperty); }
            set { SetValue(TokensProperty, value); }
        }
        private void TokensPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            var oldValue = (ObservableCollection<IFormulaToken>)e.OldValue;
            var newValue = (ObservableCollection<IFormulaToken>)e.NewValue;

            UpdateControls();

            if (oldValue != null) oldValue.CollectionChanged -= Tokens_CollectionChanged;
            if (newValue != null) newValue.CollectionChanged += Tokens_CollectionChanged;

        }
        private void Tokens_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    for (var relativeIndex = 0; relativeIndex < e.NewItems.Count; relativeIndex++)
                    {
                        var absoluteIndex = e.NewStartingIndex + relativeIndex;
                        AddControl((IFormulaToken)e.NewItems[relativeIndex], absoluteIndex);
                    }
                    UpdateFontSize();
                    UpdateStyles();
                    break;
                case NotifyCollectionChangedAction.Remove:
                    for (var relativeIndex = 0; relativeIndex < e.OldItems.Count; relativeIndex++)
                    {
                        var absoluteIndex = e.OldStartingIndex + relativeIndex;
                        RemoveControl(absoluteIndex);
                    }
                    UpdateFontSize();
                    UpdateStyles();
                    break;
                default:
                    Debug.Assert(false, "NotifyCollectionChangedAction \"" + e.Action.ToString() + "\" not implemented. ");
                    UpdateControls();
                    break;
            }
        }

        public static readonly DependencyProperty CaretIndexProperty = DependencyProperty.Register(
            name: "CaretIndex",
            propertyType: typeof(int),
            ownerType: typeof(FormulaViewer3),
            typeMetadata: new PropertyMetadata(0, (d, e) => ((FormulaViewer3)d).CaretIndexChanged(e)));
        public int CaretIndex
        {
            get { return (int)GetValue(CaretIndexProperty); }
            set { SetValue(CaretIndexProperty, value); }
        }

        private void CaretIndexChanged(DependencyPropertyChangedEventArgs e)
        {
            var oldIndex = (int)e.OldValue;
            var newIndex = (int)e.NewValue;

            // CoerceValueCallback in Windows Phone not available (FrameworkPropertyMetadata is not available)
            if (!(0 <= oldIndex && oldIndex <= Tokens.Count)) return;
            if (!(0 <= newIndex))
            {
                CaretIndex = 0;
                newIndex = 0;
            }
            if (!(newIndex <= Tokens.Count))
            {
                CaretIndex = Tokens.Count;
                newIndex = Tokens.Count;
            }
            if (oldIndex == newIndex) return;

            MoveCaret(oldIndex, newIndex);
        }

        public static readonly DependencyProperty SelectionStartProperty = DependencyProperty.Register(
            name: "SelectionStart",
            propertyType: typeof(int),
            ownerType: typeof(FormulaViewer3),
            typeMetadata: new PropertyMetadata(-1, (d, e) => ((FormulaViewer3)d).SetSelection()));
        public int SelectionStart
        {
            get { return (int)GetValue(SelectionStartProperty); }
            set { SetValue(SelectionStartProperty, value); }
        }

        public static readonly DependencyProperty SelectionLengthProperty = DependencyProperty.Register(
            name: "SelectionLength",
            propertyType: typeof(int),
            ownerType: typeof(FormulaViewer3),
            typeMetadata: new PropertyMetadata(0, (d, e) => ((FormulaViewer3)d).SetSelection()));
        public int SelectionLength
        {
            get { return (int)GetValue(SelectionLengthProperty); }
            set
            {
                SetValue(SelectionLengthProperty, value);
            }
        }

        public static readonly DependencyProperty IsEditEnabledProperty = DependencyProperty.Register(
            name: "IsEditEnabled",
            propertyType: typeof(bool),
            ownerType: typeof(FormulaViewer3),
            typeMetadata: new PropertyMetadata(false, (d, e) => ((FormulaViewer3)d).UpdateControls()));
        public bool IsEditEnabled
        {
            get { return (bool)GetValue(IsEditEnabledProperty); }
            set { SetValue(IsEditEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsMultilineProperty = DependencyProperty.Register(
            name: "IsMultiline",
            propertyType: typeof(bool),
            ownerType: typeof(FormulaViewer3),
            typeMetadata: new PropertyMetadata(false, (d, e) => ((FormulaViewer3)d).UpdateControls()));
        public bool IsMultiline
        {
            get { return (bool)GetValue(IsMultilineProperty); }
            set { SetValue(IsMultilineProperty, value); }
        }

        public static readonly DependencyProperty NormalFontSizeProperty = DependencyProperty.Register(
            name: "NormalFontSize",
            propertyType: typeof(double),
            ownerType: typeof(FormulaViewer3),
            typeMetadata: new PropertyMetadata(1.0, (d, e) => ((FormulaViewer3)d).UpdateControls()));
        public double NormalFontSize
        {
            get { return (double)GetValue(NormalFontSizeProperty); }
            set { SetValue(NormalFontSizeProperty, value); }
        }

        public static readonly DependencyProperty MinFontSizeProperty = DependencyProperty.Register(
            name: "MinFontSize",
            propertyType: typeof(double),
            ownerType: typeof(FormulaViewer3),
            typeMetadata: new PropertyMetadata(0.0, (d, e) => ((FormulaViewer3)d).UpdateControls()));
        public double MinFontSize
        {
            get { return (double)GetValue(MinFontSizeProperty); }
            set { SetValue(MinFontSizeProperty, value); }
        }

        public static readonly DependencyProperty MaxFontSizeProperty = DependencyProperty.Register(
            name: "MaxFontSize",
            propertyType: typeof(double),
            ownerType: typeof(FormulaViewer3),
            typeMetadata: new PropertyMetadata(42.0, (d, e) => ((FormulaViewer3)d).UpdateControls()));
        public double MaxFontSize
        {
            get { return (double)GetValue(MaxFontSizeProperty); }
            set { SetValue(MaxFontSizeProperty, value); }
        }

        public static readonly DependencyProperty CharactersInOneLineNormalFontSizeProperty = DependencyProperty.Register(
                name: "CharactersInOneLineNormalFontSize",
                propertyType: typeof(int),
                ownerType: typeof(FormulaViewer3),
                typeMetadata: new PropertyMetadata(0, (d, e) => ((FormulaViewer3)d).UpdateControls()));
        public int CharactersInOneLineNormalFontSize
        {
            get { return (int)GetValue(CharactersInOneLineNormalFontSizeProperty); }
            set { SetValue(CharactersInOneLineNormalFontSizeProperty, value); }
        }

        public static readonly DependencyProperty LinesNormalFontSizeProperty = DependencyProperty.Register(
            name: "LinesNormalFontSize",
            propertyType: typeof(int),
            ownerType: typeof(FormulaViewer3),
            typeMetadata: new PropertyMetadata(0, (d, e) => ((FormulaViewer3)d).UpdateControls()));
        public int LinesNormalFontSize
        {
            get { return (int)GetValue(LinesNormalFontSizeProperty); }
            set { SetValue(LinesNormalFontSizeProperty, value); }
        }

        public static readonly DependencyProperty IsAutoFontSizeProperty = DependencyProperty.Register(
            name: "IsAutoFontSize",
            propertyType: typeof(bool),
            ownerType: typeof(FormulaViewer3),
            typeMetadata: new PropertyMetadata(false, (d, e) => ((FormulaViewer3)d).UpdateControls()));
        public bool IsAutoFontSize
        {
            get { return (bool)GetValue(IsAutoFontSizeProperty); }
            set
            {
                SetValue(IsAutoFontSizeProperty, value);
            }
        }

        #endregion

        #region Members

        private double _actualFontSize;
        public double ActualFontSize
        {
            get { return _actualFontSize; }
            private set { _actualFontSize = value; }
        }

        private static Dictionary<Type, FormulaPartControl> _formulaTokenDefinitions;
        private static Dictionary<Type, FormulaPartControl> FormulaTokenDefinitions
        {
            get
            {
                if (_formulaTokenDefinitions == null)
                {
                    var formulaDefinitions = Application.Current.Resources["FormulaTokenDefinitions"] as UiFormulaTokenDefinitionCollection;
                    if (formulaDefinitions == null) throw new NotImplementedException("Please add FormulaTokenTemaplates.xaml to App resources. ");

                    _formulaTokenDefinitions = formulaDefinitions.ToDictionary(
                        keySelector: definition => definition.TokenType,
                        elementSelector: definition => definition.Template);
                }
                return _formulaTokenDefinitions;
            }
        }

        private List<FormulaPartControl> _templates;

        #endregion

        private Panel GetPanel()
        {
            return IsMultiline ? (Panel)MultilinePanelContent : SingleLinePanelContent;
        }

        private UIElementCollection Children
        {
            get { return GetPanel().Children; }
        }

        private FormulaPartControl CreateTemplate(IFormulaToken token)
        {
            var template = FormulaTokenDefinitions[token.GetType()];
            Debug.Assert(template != null, "Please add template for \"" + token.GetType().Name + "\" to FormulaTokenTemplates.xaml. ");
            return template.CreateUiTokenTemplate(token);
        }

        private Grid CreateUiControl(FormulaPartControl template)
        {
            return template.CreateUiControls(ActualFontSize, false, false, false);
        }

        private void MoveCaret(int oldIndex, int newIndex)
        {
            // check if already at the right position
            if (((Grid) Children[newIndex]).Name == "Caret") return;

            var caret = (Grid)Children[oldIndex];
            Debug.Assert(caret.Name == "Caret");

            Children.RemoveAt(oldIndex);
            Children.Insert(newIndex, caret);
            Debug.Assert(((Grid)Children[newIndex]).Name == "Caret");
        }

        private void AddControl(IFormulaToken token, int index)
        {
            var template = CreateTemplate(token);
            _templates.Insert(index, template);
            if (index > CaretIndex)
            {
                index++;
            }
            Children.Insert(index, CreateUiControl(template));
            if (index <= CaretIndex)
            {
                CaretIndex++;
            }
        }

        private void RemoveControl(int index)
        {
            _templates.RemoveAt(index);
            if (index >= CaretIndex)
            {
                index++;
            }
            Children.RemoveAt(index);
            if (index < CaretIndex)
            {
                CaretIndex--;
            }
        }

        private void UpdateControls()
        {
            if (Tokens == null)
            {
                Children.Clear();
                return;
            }

            _templates = Tokens.Select(CreateTemplate).ToList();

            UpdateActualFontSize();

            Children.Clear();
            foreach (var child in _templates.Select(CreateUiControl))
            {
                Children.Add(child);
            }

            if (0 <= CaretIndex && CaretIndex <= Children.Count) Children.Insert(CaretIndex, CreateCaret(ActualFontSize));

            UpdateStyles();
        }

        private void UpdateStyles()
        {
            // TODO: set selection
            SetSelection(SelectionStart, SelectionLength);

            // TODO: set error
        }

        private void UpdateActualFontSize()
        {
            var fontSize = NormalFontSize;
            if (IsAutoFontSize)
            {
                double oldFontSize;
                int maxSinglePartWidth;
                var trials = 0;
                do
                {
                    trials++;
                    maxSinglePartWidth = 0;
                    oldFontSize = fontSize;

                    var oldMaxLinesUsed = LinesNormalFontSize * (NormalFontSize / fontSize);

                    var currentCharactersPerLine = CharactersInOneLineNormalFontSize * (NormalFontSize / fontSize);
                    double linesUsedWithCurrentFontSize = 1;
                    double currentLineCharacters = 0;
                    foreach (var template in _templates)
                    {
                        var width = template.GetCharacterWidth();

                        currentLineCharacters += width;

                        if (currentLineCharacters > currentCharactersPerLine)
                        {
                            currentLineCharacters = width;
                            linesUsedWithCurrentFontSize++;
                        }

                        maxSinglePartWidth = Math.Max(maxSinglePartWidth, width);
                    }

                    fontSize = (0.5) * oldFontSize + 0.5 * (oldFontSize * (oldMaxLinesUsed / linesUsedWithCurrentFontSize));

                } while (Math.Abs(fontSize - oldFontSize) > 7.0 && trials < 10);

                var singleLineFontSize = maxSinglePartWidth == 0 ? MaxFontSize : NormalFontSize * (((double) CharactersInOneLineNormalFontSize) / maxSinglePartWidth);

                fontSize = Math.Min(fontSize, singleLineFontSize);

                if (fontSize < MinFontSize) fontSize = MinFontSize;
                if (fontSize > MaxFontSize) fontSize = MaxFontSize;
            }
            ActualFontSize = fontSize;
        }

        private void UpdateFontSize()
        {
            var oldFontSize = ActualFontSize;
            UpdateActualFontSize();
            if (Math.Abs(ActualFontSize - oldFontSize) <= double.Epsilon) return;
            UpdateControls();
        }

        public void SetCaretIndex(Grid sender)
        {
            CaretIndex = Children.IndexOf(sender);
        }

        private void SetSelection()
        {
            SetSelection(SelectionStart, SelectionLength);
        }

        private void SetSelection(int startIndex, int count)
        {
            var endIndex = startIndex + count;
            for (var i = 0; i < Children.Count; i++)
            {
                var child = (Grid)Children[i];
                var partControl = child.DataContext as FormulaPartControl;
                if (partControl == null) continue;

                var styles = partControl.Style;
                if (styles == null) continue;

                var isSelected = startIndex <= i && i <= endIndex;
                ApplyStyle(
                    control: child,
                    containerStyle: isSelected ? styles.SelectedContainerStyle : styles.ContainerStyle,
                    textStyle: isSelected ? styles.SelectedTextStyle : styles.TextStyle);
            }
        }

        private void SetError(int startIndex, int count)
        {
            throw new NotImplementedException();
            // TODO: create error styles
            var endIndex = startIndex + count;
            for (var i = 0; i < Children.Count; i++)
            {
                var child = (Grid)Children[i];
                var partControl = child.DataContext as FormulaPartControl;
                if (partControl == null) continue;

                var styles = partControl.Style;
                if (styles == null) continue;


                var isError = startIndex <= i && i <= endIndex;
                //ApplyStyle(
                //    control: child,
                //    containerStyle: isError ? styles.ErrorContainerStyle : styles.ContainerStyle,
                //    textStyle: isError ? styles.ErrorTextStyle : styles.TextStyle);
            }
        }

        internal void ApplyStyle(Grid control, Style containerStyle, Style textStyle)
        {
            control.Style = containerStyle;
            foreach (var textBlock in control.Children.OfType<TextBlock>())
            {
                textBlock.Style = textStyle;
            }
        }


        private static Storyboard CreateBlinkingEffect(DependencyObject target)
        {
            //<Storyboard RepeatBehavior="Forever" AutoReverse="True" Duration="0:0:0.6">
            //  <ObjectAnimationUsingKeyFrames Storyboard.TargetName="Caret" Storyboard.TargetProperty="TextBlock.Visibility">
            //    <DiscreteObjectKeyFrame KeyTime="0:0:0.3">
            //      <DiscreteObjectKeyFrame.Value>
            //        <Visibility>Collapsed</Visibility>
            //      </DiscreteObjectKeyFrame.Value>
            //    </DiscreteObjectKeyFrame>
            //  </ObjectAnimationUsingKeyFrames>
            //</Storyboard>

            var storyboard = new Storyboard
            {
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true,
                Duration = TimeSpan.FromSeconds(0.6)
            };

            var animation = new ObjectAnimationUsingKeyFrames();
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame
            {
                KeyTime = TimeSpan.FromSeconds(0.3),
                Value = Visibility.Collapsed
            });

            storyboard.Children.Add(animation);

            Storyboard.SetTarget(storyboard, target);
            Storyboard.SetTargetProperty(storyboard, new PropertyPath(VisibilityProperty));

            return storyboard;
        }

        private Grid CreateCaret(double fontSize)
        {
            //<Grid>
            //  <TextBlock Name="Caret" Text="|" Margin="-7,0,-7,0">
            //    <TextBlock.Style>
            //      <Style TargetType="TextBlock">
            //        <Style.Setters>
            //          <Setter Property="FontFamily" Value="Courier New"/>
            //          <Setter Property="Foreground" Value="{StaticResource PhoneForegroundBrush}"/>
            //        </Style.Setters>
            //      </Style>
            //    </TextBlock.Style>
            //    <!-- blinking effect -->
            //    <TextBlock.Triggers>
            //      <EventTrigger RoutedEvent="TextBlock.Loaded">
            //        <EventTrigger.Actions>
            //          <BeginStoryboard>
            //            ...
            //          </BeginStoryboard>
            //        </EventTrigger.Actions>
            //      </EventTrigger>
            //    </TextBlock.Triggers>
            //  </TextBlock>
            //</Grid>

            var container = new Grid
            {
                Name = "Caret"
            };

            var style = new Style(typeof(TextBlock));
            style.Setters.Add(new Setter(TextBlock.FontFamilyProperty, new FontFamily("Courier New")));
            style.Setters.Add(new Setter(TextBlock.ForegroundProperty, Application.Current.Resources["PhoneForegroundBrush"]));

            var textBlock = new TextBlock
            {
                Style = style,
                FontSize = fontSize,
                Text = "|"
            };
            textBlock.Margin = new Thickness(-textBlock.ActualWidth / 2, 0, -textBlock.ActualWidth / 2, 0);

            var storyboard = CreateBlinkingEffect(textBlock);
            textBlock.Loaded += (sender, e) => storyboard.Begin();

            container.Children.Add(textBlock);

            return container;
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
