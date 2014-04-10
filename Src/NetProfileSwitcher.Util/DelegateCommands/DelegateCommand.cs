using System;
using System.Windows.Input;

namespace NetProfileSwitcher.Util
{
    public class DelegateCommand : ICommand
    {
        Action _action;

        public DelegateCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }

    }
}