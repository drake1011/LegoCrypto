using System;
using System.Windows.Input;

namespace LegoCrypto.WPF.App
{
    public class RelayCommand : ICommand
    {
        private readonly Action _TargetExecuteMethod;
        private readonly Func<bool> _TargetCanExecuteMethod;

        public RelayCommand(Action executeMethod) => _TargetExecuteMethod = executeMethod;

        public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged(this, EventArgs.Empty);
        #region ICommand Members

        public bool CanExecute(object parameter) => _TargetCanExecuteMethod != null ? _TargetCanExecuteMethod() : _TargetExecuteMethod != null ? true : false;

        public event EventHandler CanExecuteChanged = delegate { };

        public void Execute(object parameter) => _TargetExecuteMethod?.Invoke();
        #endregion
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _TargetExecuteMethod;
        private readonly Func<T, bool> _TargetCanExecuteMethod;

        public RelayCommand(Action<T> executeMethod) => _TargetExecuteMethod = executeMethod;

        public RelayCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged(this, EventArgs.Empty);
        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            if (_TargetCanExecuteMethod != null)
            {
                var tparm = (T)parameter;
                return _TargetCanExecuteMethod(tparm);
            }
            return _TargetExecuteMethod != null ? true : false;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public void Execute(object parameter) => _TargetExecuteMethod?.Invoke((T)parameter);
        #endregion
    }
}
