using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Catrobat.IDE.Core.Resources.Localization;

namespace Catrobat.IDE.Phone.Controls.AppBar
{
    public enum AppBarTargetType { Object, Script, Costume, Sound }

    public partial class EditorAppBar : UserControl
    {
        #region DependancyProperties

        public AppBarTargetType TargetType
        {
            get { return (AppBarTargetType)GetValue(TargetTypeProperty); }
            set { SetValue(TargetTypeProperty, value); }
        }

        public static readonly DependencyProperty TargetTypeProperty = DependencyProperty.Register("TargetType", typeof(AppBarTargetType), typeof(EditorAppBar), new PropertyMetadata(TargetTypeChanged));

        private static void TargetTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var that = (d as EditorAppBar);
            if (that == null) return;

            Debug.Assert(e.NewValue != null, "e.NewValue != null");
            switch ((AppBarTargetType)e.NewValue)
            {
                case AppBarTargetType.Object:
                    that.StackPanelNew.Visibility = Visibility.Visible;
                    that.StackPanelEdit.Visibility = Visibility.Visible;
                    that.StackPanelCopy.Visibility = Visibility.Visible;
                    that.StackPanelDelete.Visibility = Visibility.Visible;
                    break;

                case AppBarTargetType.Script:
                    that.StackPanelNew.Visibility = Visibility.Visible;
                    that.StackPanelEdit.Visibility = Visibility.Collapsed;
                    that.StackPanelCopy.Visibility = Visibility.Visible;
                    that.StackPanelDelete.Visibility = Visibility.Visible;
                    break;

                case AppBarTargetType.Costume:
                    that.StackPanelNew.Visibility = Visibility.Visible;
                    that.StackPanelEdit.Visibility = Visibility.Visible;
                    that.StackPanelCopy.Visibility = Visibility.Visible;
                    that.StackPanelDelete.Visibility = Visibility.Visible;
                    break;

                case AppBarTargetType.Sound:
                    that.StackPanelNew.Visibility = Visibility.Visible;
                    that.StackPanelEdit.Visibility = Visibility.Visible;
                    that.StackPanelCopy.Visibility = Visibility.Collapsed;
                    that.StackPanelDelete.Visibility = Visibility.Visible;
                    break;
            }

            that.UpdateAddText((AppBarTargetType)e.NewValue);
            that.UpdateSelectedItemsNumberUI((AppBarTargetType)e.NewValue);
        }


        public int NumberOfSelectedItems
        {
            get { return (int)GetValue(NumberOfSelectedItemsProperty); }
            set { SetValue(NumberOfSelectedItemsProperty, value); }
        }

        public static readonly DependencyProperty NumberOfSelectedItemsProperty = DependencyProperty.Register("NumberOfSelectedItems", typeof(int), typeof(EditorAppBar), new PropertyMetadata(0, NumberOfSelectedItemsChanged));
        private static void NumberOfSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EditorAppBar)d).UpdateSelectedItemsNumberUI(((EditorAppBar)d).TargetType);
        }


        public ICommand ClearSelectionCommand
        {
            get { return (ICommand)GetValue(ClearSelectionCommandProperty); }
            set { SetValue(ClearSelectionCommandProperty, value); }
        }
        public static readonly DependencyProperty ClearSelectionCommandProperty = DependencyProperty.Register("ClearSelectionCommand", typeof(ICommand), typeof(EditorAppBar), new PropertyMetadata(ClearSelectionCommandChanged));
        private static void ClearSelectionCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EditorAppBar)d).ButtonClearSelection.Command = e.NewValue as ICommand;
        }


        public ICommand CopyCommand
        {
            get { return (ICommand)GetValue(CopyCommandProperty); }
            set { SetValue(CopyCommandProperty, value); }
        }
        public static readonly DependencyProperty CopyCommandProperty = DependencyProperty.Register("CopyCommand", typeof(ICommand), typeof(EditorAppBar), new PropertyMetadata(CopyCommandChanged));
        private static void CopyCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EditorAppBar)d).ButtonCopy.Command = e.NewValue as ICommand;
        }




        public ICommand EditCommand
        {
            get { return (ICommand)GetValue(EditCommandProperty); }
            set { SetValue(EditCommandProperty, value); }
        }
        public static readonly DependencyProperty EditCommandProperty = DependencyProperty.Register("EditCommand", typeof(ICommand), typeof(EditorAppBar), new PropertyMetadata(EditCommandChanged));
        private static void EditCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EditorAppBar)d).ButtonEdit.Command = e.NewValue as ICommand;
        }




        public ICommand DeleteCommand
        {
            get { return (ICommand)GetValue(DeleteCommandProperty); }
            set { SetValue(DeleteCommandProperty, value); }
        }
        public static readonly DependencyProperty DeleteCommandProperty = DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(EditorAppBar), new PropertyMetadata(DeleteCommandChanged));
        private static void DeleteCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EditorAppBar)d).ButtonDelete.Command = e.NewValue as ICommand;
        }




        public ICommand NewCommand
        {
            get { return (ICommand)GetValue(NewCommandProperty); }
            set { SetValue(NewCommandProperty, value); }
        }
        public static readonly DependencyProperty NewCommandProperty = DependencyProperty.Register("NewCommand", typeof(ICommand), typeof(EditorAppBar), new PropertyMetadata(NewCommandChanged));
        private static void NewCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EditorAppBar)d).ButtonNew.Command = e.NewValue as ICommand;
        }




        public ICommand PlayCommand
        {
            get { return (ICommand)GetValue(PlayCommandProperty); }
            set { SetValue(PlayCommandProperty, value); }
        }
        public static readonly DependencyProperty PlayCommandProperty = DependencyProperty.Register("PlayCommand", typeof(ICommand), typeof(EditorAppBar), new PropertyMetadata(PlayCommandChanged));
        private static void PlayCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EditorAppBar)d).ButtonPlay.Command = e.NewValue as ICommand;
        }
        #endregion


        public EditorAppBar()
        {
            InitializeComponent();
            UpdateAddText(TargetType);
            UpdateSelectedItemsNumberUI(TargetType);
        }

        private void UpdateSelectedItemsNumberUI(AppBarTargetType targetType)
        {
            TextBlockNumberOfSelectedItemsNumber.Text = NumberOfSelectedItems.ToString(CultureInfo.InvariantCulture);

            bool usePlural = NumberOfSelectedItems != 1;

            switch (targetType)
            {
                case AppBarTargetType.Object:
                    TextBlockNumberOfSelectedItemsText.Text = usePlural ? AppResources.Editor_ObjectPlural : AppResources.Editor_ObjectSingular;
                    break;

                case AppBarTargetType.Script:
                    TextBlockNumberOfSelectedItemsText.Text = usePlural ? AppResources.Editor_ActionPlural : AppResources.Editor_ActionSingular;
                    break;

                case AppBarTargetType.Costume:
                    TextBlockNumberOfSelectedItemsText.Text = usePlural ? AppResources.Editor_CostumePlural : AppResources.Editor_CostumeSingular;
                    break;

                case AppBarTargetType.Sound:
                    TextBlockNumberOfSelectedItemsText.Text = usePlural ? AppResources.Editor_SoundPlural : AppResources.Editor_SoundSingular;
                    break;
            }
        }

        private void UpdateAddText(AppBarTargetType targetType)
        {
            var text = "";

            switch (targetType)
            {
                case AppBarTargetType.Object:
                    text = AppResources.Editor_ButtonAddObject;
                    break;

                case AppBarTargetType.Script:
                    text = AppResources.Editor_ButtonAddScript;
                    break;

                case AppBarTargetType.Costume:
                    text = AppResources.Editor_ButtonAddCostume;
                    break;

                case AppBarTargetType.Sound:
                    text = AppResources.Editor_ButtonAddSound;
                    break;
            }

            TextBlockNew.Text =  text;
        }
    }
}
