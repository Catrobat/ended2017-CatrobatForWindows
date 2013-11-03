using System.Threading.Tasks;
using System.Windows.Input;
using System;

namespace Catrobat.IDE.Core.ViewModel
{
    public class AsyncCommand : ICommand
    {
        private readonly Action _finished;
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        private Action _testCallback;

        public AsyncCommand(Action execute, Action finished)
        {
            _execute = execute;
            _finished = finished;
            _canExecute = () => true;
        }

        public AsyncCommand(Action execute, Action finished, Func<bool> canExecute)
        {
            _execute = execute;
            _finished = finished;
            _canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                var callback = new AsyncCallback(CommandFinished);
                _execute.BeginInvoke(callback, parameter);
            }
        }

        public async Task ExecuteAsync(object parameter)
        {
            if (CanExecute(parameter))
            {
                var callback = new AsyncCallback(CommandFinished);
                _execute.BeginInvoke(callback, parameter);
            }
        }

        private void CommandFinished(IAsyncResult ar)
        {
            _execute.EndInvoke(ar);
            if(_finished != null)
                _finished.Invoke();
            if(_testCallback != null)
                _testCallback.Invoke();
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged.Invoke(this, new EventArgs());
        }

        public void SetTestCallback(Action callback)
        {
            _testCallback = callback;
        }

        public event EventHandler CanExecuteChanged;
    }
}
