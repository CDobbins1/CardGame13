using System;
using System.Windows.Input;

namespace CardGame13.GUI
{
    public class Command : ICommand
    {
        #pragma warning disable CS0067
        public event EventHandler CanExecuteChanged;
        #pragma warning restore CS0067

        private Action Method { get; }

        public Command(Action method)
        {
            Method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => Method?.Invoke();
    }
}
