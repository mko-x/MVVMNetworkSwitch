using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NetProfileSwitcher.Interfaces.Tasks;

namespace NetProfileSwitcher.Util
{
    public class DelegateBaseCommand<T> : ICommand, IValueUpdateHandler<T>
    {
        private Action<T> _action;
        private IList<IValueUpdateHandler<T>> _handlers;

        #region - public interface
        /// <summary>
        /// Generic constuctor for typed delegates
        /// </summary>
        /// <param name="action"></param>
        public DelegateBaseCommand(Action<T> action)
        {
            this._handlers = new List<IValueUpdateHandler<T>>();
            this._action = action;
            this.register(this);
        }

        /// <summary>
        /// DelegateBaseCommand can always execute. Derived types may override CanReallyExecute and decide to not execute.
        /// </summary>
        /// <param name="parameter">Parameter to evaluate</param>
        /// <returns>True if command is allowed to execute</returns>
        public bool CanExecute(object parameter)
        {
            return CanReallyExecute(parameter);
        }

        /// <summary>
        /// Notice if allowed to execute. In base class not implemented.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Executes the given action only if the type of object fits to T
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            this.executeCheckedType(parameter);
        }
        
        /// <summary>
        /// Update registered handlers
        /// </summary>
        /// <param name="value">New value</param>
        public void update(T value)
        {
            if (_handlers != null)
            {
                foreach (IValueUpdateHandler<T> handler in _handlers)
                {
                    if (handler != null && handler != this)
                    {
                        handler.update(value);
                    }
                    else if (handler == this)
                    {
                        this.executeCheckedType(value);
                    }
                }
            }
        }

        /// <summary>
        /// Register for updates
        /// </summary>
        /// <param name="handler"></param>
        public void register(IValueUpdateHandler<T> handler)
        {
            this._handlers.Add(handler);
        }
        #endregion - public interface

        #region - protected area
        /// <summary>
        /// Derived check for execution allowed. Override for special treatment.
        /// </summary>
        /// <param name="parameter">Parameter to evaluate</param>
        /// <returns>True if command can be executed</returns>
        protected bool CanReallyExecute(object parameter)
        {
            CanExecuteChanged(this, new EventArgs());
            return true;
        }
        #endregion - protected area

        #region - internal methods
        private void executeCheckedType(object parameter)
        {
            if (parameter.GetType() == typeof(T))
            {
                this._action((T)parameter);
                //this.update((T)parameter);
            }
        }
        #endregion - internal methods


    }
}
