using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Formulas.Tokens;
using Catrobat.IDE.WindowsPhone.Controls.Formulas.Templates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace Catrobat.IDE.WindowsPhone.Controls.Formulas
{
    public delegate void DoubleTap(int index);

    public partial class FormulaViewer
    {
        public bool _lastContainerAdded;

        public new DoubleTap DoubleTap;
        private void RaiseDoubleTap(int index)
        {
            if (DoubleTap != null) DoubleTap.Invoke(index);
        }

        #region Dependency properties

        public static readonly DependencyProperty TokensProperty = DependencyProperty.Register(
            name: "Tokens",
            propertyType: typeof(ObservableCollection<IFormulaToken>),
            ownerType: typeof(FormulaViewer),
            typeMetadata: new PropertyMetadata(null, (d, e) => ((FormulaViewer)d).TokensPropertyChanged(e)));
        public ObservableCollection<IFormulaToken> Tokens
        {
            get { return (ObservableCollection<IFormulaToken>)GetValue(TokensProperty); }
            set { SetValue(TokensProperty, value); }
        }
        private void TokensPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            var oldValue = (ObservableCollection<IFormulaToken>) e.OldValue;
            var newValue = (ObservableCollection<IFormulaToken>) e.NewValue;

            InitContainers();

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
                        AddContainer((IFormulaToken)e.NewItems[relativeIndex], absoluteIndex);
                    }
                    //UpdateFontSize();
                    UpdateStyles();
                    break;
                case NotifyCollectionChangedAction.Remove:
                    for (var relativeIndex = 0; relativeIndex < e.OldItems.Count; relativeIndex++)
                    {
                        var absoluteIndex = e.OldStartingIndex + relativeIndex;
                        RemoveContainer(absoluteIndex);
                    }
                    //UpdateFontSize();
                    UpdateStyles();
                    break;
                default:
                    Debug.Assert(false, "NotifyCollectionChangedAction \"" + e.Action + "\" not implemented. ");
                    break;
            }
        }

        public static readonly DependencyProperty CaretIndexProperty = DependencyProperty.Register(
            name: "CaretIndex",
            propertyType: typeof (int),
            ownerType: typeof (FormulaViewer),
            typeMetadata: new PropertyMetadata(0, (d, e) => ((FormulaViewer) d).CaretIndexChanged(e)));
        public int CaretIndex
        {
            get { return (int)GetValue(CaretIndexProperty); }
            set {  SetValue(CaretIndexProperty, value); }
        }
        private void CaretIndexChanged(DependencyPropertyChangedEventArgs e)
        {
            if (Tokens == null) return;
            // CoerceValueCallback is not available in Windows Phone (FrameworkPropertyMetadata is not available)
            MoveCaret((int) e.OldValue, (int) e.NewValue);
            if (CaretIndex != SelectionEnd)
            {
                SelectionLength = 0;
                CaretIndex = (int) e.NewValue;
            }
        }

        public static readonly DependencyProperty SelectionStartProperty = DependencyProperty.Register(
            name: "SelectionStart",
            propertyType: typeof (int),
            ownerType: typeof (FormulaViewer),
            typeMetadata: new PropertyMetadata(0, (d, e) => ((FormulaViewer) d).SelectionChanged()));
        public int SelectionStart
        {
            get { return (int)GetValue(SelectionStartProperty); }
            set { SetValue(SelectionStartProperty, value); }
        }

        public static readonly DependencyProperty SelectionLengthProperty = DependencyProperty.Register(
            name: "SelectionLength",
            propertyType: typeof (int),
            ownerType: typeof (FormulaViewer),
            typeMetadata: new PropertyMetadata(0, (d, e) => ((FormulaViewer) d).SelectionChanged()));
        public int SelectionLength
        {
            get { return (int)GetValue(SelectionLengthProperty); }
            set { SetValue(SelectionLengthProperty, value); }
        }
        private void SelectionChanged()
        {
            if (Tokens == null) return;
            // CoerceValueCallback is not available in Windows Phone (FrameworkPropertyMetadata is not available)
            CaretIndex = SelectionEnd;
            UpdateStyles();
        }

        //public static readonly DependencyProperty IsEditEnabledProperty = DependencyProperty.Register(
        //    name: "IsEditEnabled",
        //    propertyType: typeof(bool),
        //    ownerType: typeof(FormulaViewer),
        //    typeMetadata: new PropertyMetadata(false, (d, e) => ((FormulaViewer)d).UpdateContainers()));
        //public bool IsEditEnabled
        //{
        //    get { return (bool)GetValue(IsEditEnabledProperty); }
        //    set { SetValue(IsEditEnabledProperty, value); }
        //}

        //public static readonly DependencyProperty IsMultilineProperty = DependencyProperty.Register(
        //    name: "IsMultiline",
        //    propertyType: typeof(bool),
        //    ownerType: typeof(FormulaViewer),
        //    typeMetadata: new PropertyMetadata(false, (d, e) => ((FormulaViewer)d).UpdateContainers()));
        //public bool IsMultiline
        //{
        //    get { return (bool)GetValue(IsMultilineProperty); }
        //    set { SetValue(IsMultilineProperty, value); }
        //}

        public static readonly DependencyProperty NormalFontSizeProperty = DependencyProperty.Register(
            name: "NormalFontSize",
            propertyType: typeof(double),
            ownerType: typeof(FormulaViewer),
            typeMetadata: new PropertyMetadata(1.0, (d, e) => ((FormulaViewer)d).UpdateFontSize()));
        public double NormalFontSize
        {
            get { return (double)GetValue(NormalFontSizeProperty); }
            set { SetValue(NormalFontSizeProperty, value); }
        }

        public static readonly DependencyProperty MinFontSizeProperty = DependencyProperty.Register(
            name: "MinFontSize",
            propertyType: typeof(double),
            ownerType: typeof(FormulaViewer),
            typeMetadata: new PropertyMetadata(0.0, (d, e) => ((FormulaViewer)d).UpdateFontSize()));
        public double MinFontSize
        {
            get { return (double)GetValue(MinFontSizeProperty); }
            set { SetValue(MinFontSizeProperty, value); }
        }

        public static readonly DependencyProperty MaxFontSizeProperty = DependencyProperty.Register(
            name: "MaxFontSize",
            propertyType: typeof(double),
            ownerType: typeof(FormulaViewer),
            typeMetadata: new PropertyMetadata(42.0, (d, e) => ((FormulaViewer)d).UpdateFontSize()));
        public double MaxFontSize
        {
            get { return (double)GetValue(MaxFontSizeProperty); }
            set { SetValue(MaxFontSizeProperty, value); }
        }

        public static readonly DependencyProperty CharactersInOneLineNormalFontSizeProperty = DependencyProperty.Register(
                name: "CharactersInOneLineNormalFontSize",
                propertyType: typeof(int),
                ownerType: typeof(FormulaViewer),
                typeMetadata: new PropertyMetadata(0, (d, e) => ((FormulaViewer)d).UpdateFontSize()));
        public int CharactersInOneLineNormalFontSize
        {
            get { return (int)GetValue(CharactersInOneLineNormalFontSizeProperty); }
            set { SetValue(CharactersInOneLineNormalFontSizeProperty, value); }
        }

        public static readonly DependencyProperty LinesNormalFontSizeProperty = DependencyProperty.Register(
            name: "LinesNormalFontSize",
            propertyType: typeof(int),
            ownerType: typeof(FormulaViewer),
            typeMetadata: new PropertyMetadata(0, (d, e) => ((FormulaViewer)d).UpdateFontSize()));
        public int LinesNormalFontSize
        {
            get { return (int)GetValue(LinesNormalFontSizeProperty); }
            set { SetValue(LinesNormalFontSizeProperty, value); }
        }

        public static readonly DependencyProperty IsAutoFontSizeProperty = DependencyProperty.Register(
            name: "IsAutoFontSize",
            propertyType: typeof(bool),
            ownerType: typeof(FormulaViewer),
            typeMetadata: new PropertyMetadata(false, (d, e) => ((FormulaViewer)d).UpdateFontSize()));
        public bool IsAutoFontSize
        {
            get { return (bool)GetValue(IsAutoFontSizeProperty); }
            set { SetValue(IsAutoFontSizeProperty, value); }
        }

        #endregion

        #region Properties
        public double ActualFontSize { get; private set; }

        private static IDictionary<Type, FormulaTokenTemplate> _formulaTokenTemplates;
        private static IDictionary<Type, FormulaTokenTemplate> FormulaTokenTemplates
        {
            get
            {
                if (_formulaTokenTemplates == null)
                {
                    var formulaTokenTemplates = Application.Current.Resources["FormulaTokenTemplates"] as FormulaTokenTemplateCollection;
                    if (formulaTokenTemplates == null)
                    {
                        Debug.WriteLine("Please add FormulaTokenTemplates.xaml to App resources.");
                        _formulaTokenTemplates = new Dictionary<Type, FormulaTokenTemplate>();
                    }
                    else
                    {
                        _formulaTokenTemplates = formulaTokenTemplates.ToDictionary();
                    }
                }
                return _formulaTokenTemplates;
            }
        }

        private Panel Panel
        {
            get { return MultilinePanelContent; }
            //get { return IsMultiline ? (Panel) MultilinePanelContent : SingleLinePanelContent; }
        }

        private UIElementCollection Children
        {
            get { return Panel.Children; }
        }

        private IEnumerable<Grid> Containers
        {
            get { return Children.Cast<Grid>().Where(child => child != Caret); }
        }

        public int SelectionEnd
        {
            get { return SelectionStart + SelectionLength; }
        }

        private Grid Caret { get; set; }

        #endregion

        public FormulaViewer()
        {
            InitializeComponent();
        }

        public bool IsLoaded { get; private set; }
        private void FormulaViewer_OnLoaded(object sender, RoutedEventArgs e)
        {
            ActualFontSize = 42.0;

            IsLoaded = true;

            Caret = CreateCaret();
            InitCaret();
            InitContainers();
        }

        #region Containers

        private static Grid CreateContainer(IFormulaToken token)
        {
            // find template in FormulaTokenTemplates.xaml
            FormulaTokenTemplate template;
            if (!FormulaTokenTemplates.TryGetValue(token.GetType(), out template))
            {
                Debug.WriteLine("Please add template for \"" + token.GetType().Name +
                                "\" to FormulaTokenTemplates.xaml. ");
                return null;
            }

            return template.CreateContainer(token);
        }

        private void InitContainer(Grid container)
        {
            container.Tapped += (sender, e) => Container_OnTap((Grid)sender, e);
            container.DoubleTapped += (sender, e) => Container_OnDoubleTap((Grid)sender, e);
            FormulaTokenTemplate.SetStyle(container, false);
            FormulaTokenTemplate.SetFontSize(container, ActualFontSize);
        }

        private void InitContainers()
        {
            // see FormulaViewer_OnLoaded
            if (!IsLoaded) return;

            Children.Clear();
            if (Tokens != null)
            {
                Children.AddRange(Tokens.Select(CreateContainer).Where(container => container != null));
                foreach (var container in Containers)
                {
                    InitContainer(container);
                }
                //UpdateStyles();
            }

            var caretIndex = Tokens == null ? 0 : Tokens.Count;
            MoveCaret(-1, caretIndex);
            CaretIndex = caretIndex;

            UpdateFontSize();
        }

        private void AddContainer(IFormulaToken token, int index)
        {
            _lastContainerAdded = true;

            if (index > CaretIndex)
            {
                index++;
            }

            var container = CreateContainer(token);
            if (container == null) return;
            Children.Insert(index, container);
            InitContainer(container);

            if (index <= CaretIndex)
            {
                CaretIndex++;
            }
            if (SelectionStart <= index && index < SelectionEnd)
            {
                SelectionLength++;
            }
        }

        private void RemoveContainer(int index)
        {
            _lastContainerAdded = false;

            if (index >= CaretIndex)
            {
                index++;
            }

            Children.RemoveAt(index);

            if (index < CaretIndex)
            {
                CaretIndex--;
            }
            if (SelectionStart <= index && index < SelectionEnd)
            {
                SelectionLength--;
            }
        }

        #endregion

        #region Event handlers

        private void Container_OnTap(Grid container, TappedRoutedEventArgs e)
        {
            // find container
            var index = Containers.IndexOf(container);
            if (index == -1) return;
            e.Handled = true;

            if (e.GetPosition(container).X > container.ActualWidth / 2) index++;

            SelectionLength = 0;
            CaretIndex = index;
        }

        private void Container_OnDoubleTap(Grid container, DoubleTappedRoutedEventArgs e)
        {
            // find container
            var index = Containers.IndexOf(container);
            if (index == -1) return;

            RaiseDoubleTap(index);
            e.Handled = true;
        }

        private void Panel_OnTap(object sender, RoutedEventArgs e)
        {
            if (Tokens == null) return;

            CaretIndex = Tokens.Count;
        }

        private void MultilinePanelContent_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            const double decreaseMultiplyer = 0.95;
            const double increaseMultiplyer = 1.05;

            var decreaseOffset = ActualFontSize * 1.0;
            var increaseOffset = ActualFontSize*2.2;

            if (_lastContainerAdded)
            {
                if (MultilinePanelContent.ActualHeight > ScrollViewerContent.ActualHeight - decreaseOffset)
                {
                    if (ActualFontSize > MinFontSize)
                    {
                        ActualFontSize *= decreaseMultiplyer;
                        UpdateFontSize();
                    }
                    else
                    {
                        ScrollViewerContent.ScrollToVerticalOffset(100000);
                    }
                }
            }
            else
            {
                if (MultilinePanelContent.ActualHeight < ScrollViewerContent.ActualHeight - increaseOffset)
                {
                    if (ActualFontSize < MaxFontSize)
                    {
                        ActualFontSize *= increaseMultiplyer;
                        UpdateFontSize();
                    }

                }
            }
        }

        #endregion

        #region FontSize

        //private double CalculateFontSize(IList<int> characterWidths)
        //{
        //    var fontSize = NormalFontSize;
        //    if (!IsAutoFontSize) return fontSize;

        //    double oldFontSize;
        //    int maxSinglePartWidth;
        //    var trials = 0;
        //    do
        //    {
        //        trials++;
        //        maxSinglePartWidth = 0;
        //        oldFontSize = fontSize;

        //        var oldMaxLinesUsed = LinesNormalFontSize * (NormalFontSize / fontSize);

        //        var currentCharactersPerLine = CharactersInOneLineNormalFontSize * (NormalFontSize / fontSize);
        //        double linesUsedWithCurrentFontSize = 1;
        //        double currentLineCharacters = 0;
        //        foreach (var width in characterWidths)
        //        {
        //            currentLineCharacters += width;

        //            if (currentLineCharacters > currentCharactersPerLine)
        //            {
        //                currentLineCharacters = width;
        //                linesUsedWithCurrentFontSize++;
        //            }

        //            maxSinglePartWidth = Math.Max(maxSinglePartWidth, width);
        //        }

        //        fontSize = (0.5) * oldFontSize + 0.5 * (oldFontSize * (oldMaxLinesUsed / linesUsedWithCurrentFontSize));

        //    } while (Math.Abs(fontSize - oldFontSize) > 7.0 && trials < 10);

        //    var singleLineFontSize = maxSinglePartWidth == 0 ? MaxFontSize : NormalFontSize * (((double) CharactersInOneLineNormalFontSize) / maxSinglePartWidth);

        //    fontSize = Math.Min(fontSize, singleLineFontSize);

        //    if (fontSize < MinFontSize) fontSize = MinFontSize;
        //    if (fontSize > MaxFontSize) fontSize = MaxFontSize;
        //    return fontSize;
        //}


        private void UpdateFontSize()
        {
            //var oldFontSize = ActualFontSize;
            //var characterWidths = Children.OfType<Grid>().Select(FormulaTokenTemplate.GetCharacterWidth).ToList();
            //var fontSize = CalculateFontSize(characterWidths);
            //if (Math.Abs(fontSize - oldFontSize) <= 0.1) return;
            //ActualFontSize = fontSize;

            foreach (var container in Containers)
            {
                FormulaTokenTemplate.SetFontSize(container, ActualFontSize);
            }
            SetCaretFontSize(ActualFontSize);
        }



        #endregion

        #region Styles

        private void UpdateStyles()
        {
            var index = 0;
            foreach (var container in Containers)
            {
                var isSelected = SelectionStart <= index && index < SelectionEnd;
                FormulaTokenTemplate.SetStyle(container, isSelected);
                index++;
            }
        }

        #endregion

        #region Caret

        private void MoveCaret(int oldIndex, int newIndex)
        {
            // see FormulaViewer_OnLoaded
            if (!IsLoaded) return;

            if (oldIndex == newIndex) return;
            
            if (oldIndex != -1)
            {
                var element = Children.ElementAtOrDefault(newIndex);
                if (element == Caret) return;
                if (oldIndex != -1) Children.RemoveAt(oldIndex);
            }

            Children.Insert(newIndex, Caret);
        }

        private void SetCaretFontSize(double fontSize)
        {
            if (Caret == null) return;
            var textBlock = (TextBlock) Caret.Children[0];
            textBlock.FontSize = fontSize;

            // trigger SizeChanged event (see UpdateCaretMargin)
            Caret.Margin = new Thickness(0);
        }

        private void UpdateCaretMargin()
        {
            var width = Caret.ActualWidth;
            Caret.Margin = new Thickness(
                left: -width / 2,
                top: 0,
                right: -width / 2,
                bottom: 0);
        }

        private void InitCaret()
        {
            Caret.SizeChanged += (sender, e) => UpdateCaretMargin();
            SetCaretFontSize(ActualFontSize);
        }

        /// <remarks>Caret cannot be taken from XAML Resources, because it can not be child of Resources and Panel.Children. </remarks>
        private Grid CreateCaret()
        {
            var container = new Grid {Name = "Caret"};

            var style = new Style(typeof(TextBlock));
            style.Setters.Add(new Setter(TextBlock.FontFamilyProperty, new FontFamily("Courier New")));
            style.Setters.Add(new Setter(TextBlock.ForegroundProperty, Application.Current.Resources["PhoneForegroundBrush"]));

            var textBlock = new TextBlock
            {
                Style = style,
                Text = "|"
            };

            // TODO: 8.1
            var storyboard = CreateBlinkingEffect(textBlock);
            textBlock.Loaded += (sender, e) => storyboard.Begin(); 

            container.Children.Add(textBlock);

            return container;
        }

        private static Storyboard CreateBlinkingEffect(DependencyObject target)
        {
            var storyboard = new Storyboard
            {
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = TimeSpan.FromSeconds(1.2)
            };

            var animation = new ObjectAnimationUsingKeyFrames();
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame
            {
                KeyTime = TimeSpan.FromSeconds(0.0),
                Value = 0.0
            });
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame
            {
                KeyTime = TimeSpan.FromSeconds(0.6),
                Value = 1.0
            });

            storyboard.Children.Add(animation);

            Storyboard.SetTarget(storyboard, target);
            Storyboard.SetTargetProperty(storyboard, "Opacity");

            return storyboard;
        }

        #endregion
    }
}
