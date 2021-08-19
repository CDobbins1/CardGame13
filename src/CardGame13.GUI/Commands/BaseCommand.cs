using System;
using System.Windows.Input;

namespace CardGame13.GUI.Commands
{
    public abstract class BaseCommand : ICommand
    {
        private Func<bool> Predicate { get; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => Predicate?.Invoke() ?? true;

        public abstract void Execute(object parameter);

        public BaseCommand(Func<bool> predicate)
        {
            Predicate = predicate;
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
