using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Catrobat.IDEWindowsPhone.Controls.Buttons
{
    public class PlayPauseButtonGroup : FrameworkElement
    {
        private readonly List<PlayPauseButton> _buttons;

        #region DependancyProperties

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(PlayPauseButtonGroup), new PropertyMetadata(CommandChanged));

        private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Code for dealing with your property changes
        }

        #endregion

        public PlayPauseButtonGroup()
        {
            _buttons = new List<PlayPauseButton>();
        }

        public void Register(PlayPauseButton button)
        {
            if(button != null && !_buttons.Contains(button))
            {
                _buttons.Add(button);
                button.PlayStateChanged += ButtonOnPlayStateChanged;
            }
        }


        public void UnRegister(PlayPauseButton button)
        {
            if (button != null && !_buttons.Contains(button))
            {
                _buttons.Add(button);
                button.PlayStateChanged -= ButtonOnPlayStateChanged;
            }
        }

        private void ButtonOnPlayStateChanged(object sender, PlayPauseButtonState state)
        {
            var buttonSender = sender as PlayPauseButton;
            

            if (Command != null && buttonSender != null)
            {
                var arguments = new PlayPauseCommandArguments();
                arguments.CurrentButton = sender as PlayPauseButton;

                if (buttonSender.State == PlayPauseButtonState.Play)
                {
                    
                    foreach (PlayPauseButton button in _buttons)
                    {
                        arguments.ChangedToPlayObject = buttonSender.DataContext;
                        if (button != buttonSender && button.State == PlayPauseButtonState.Play)
                        {
                            arguments.ChangedToPausedObject = button.DataContext;
                            button.State = PlayPauseButtonState.Pause;
                        }
                    }
                }
                else
                {
                    arguments.ChangedToPausedObject = buttonSender.DataContext;
                    //foreach (PlayPauseButton button in _buttons)
                    //{
                    //    if (button != buttonSender && button.State == PlayPauseButtonState.Play)
                    //    {
                    //        button.State = PlayPauseButtonState.Pause; 
                    //    }
                    //}
                }

                Command.Execute(arguments);
            }

        }
    }
}
