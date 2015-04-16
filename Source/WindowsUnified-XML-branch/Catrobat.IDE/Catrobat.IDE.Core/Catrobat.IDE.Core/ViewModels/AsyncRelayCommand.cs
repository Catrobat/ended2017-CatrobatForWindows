using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Catrobat.IDE.Core.ViewModels
{
    public class AsyncRelayCommand : ICommand
    {
        private readonly Action _finished;
        private readonly Func<object, Task> _executeWithParameter;
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;
        private readonly Func<object, bool> _canExecuteWithParameter;

        public AsyncRelayCommand(Func<Task> execute, Action finished)
        {
            _execute = execute;
            _finished = finished;
        }

        public AsyncRelayCommand(Func<Task> execute, Action finished, Func<bool> canExecute)
        {
            _execute = execute;
            _finished = finished;
            _canExecute = canExecute;
        }


        public AsyncRelayCommand(Func<object, Task> execute, Action finished)
        {
            _executeWithParameter = execute;
            _finished = finished;
        }

        public AsyncRelayCommand(Func<object, Task> execute, Action finished, Func<object, bool> canExecute)
        {
            _executeWithParameter = execute;
            _finished = finished;
            _canExecuteWithParameter = canExecute;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                Task.Run(async () =>
                {
                    if(_executeWithParameter != null)
                        await _executeWithParameter(parameter);

                    if (_execute != null)
                    {
                        try
                        {
                            await _execute();
                        }
                        catch (Exception exc)
                        {
                            if(Debugger.IsAttached)
                                Debugger.Break();
                        }
                    }
                    
                    _finished.Invoke();
                });
            }
        }

        public async Task ExecuteAsync(object parameter)
        {
            if (_executeWithParameter != null)
                await _executeWithParameter(parameter);

            if (_execute != null)
                await _execute();

            _finished.Invoke();
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute();
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged.Invoke(this, new EventArgs());
        }

        public event EventHandler CanExecuteChanged;
    }
}
