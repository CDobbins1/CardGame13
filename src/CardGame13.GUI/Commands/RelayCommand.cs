using System;
using System.Windows.Input;

namespace CardGame13.GUI.Commands
{
    public class RelayCommand : BaseCommand
    {
        private Action ExecuteDelegate { get; }

        public RelayCommand(Action method, Func<bool> predicate = null) : base(predicate)
        {
            ExecuteDelegate = method ?? throw new ArgumentNullException(nameof(method));
        }

        public override void Execute(object parameter) => ExecuteDelegate.Invoke();
    }
}
