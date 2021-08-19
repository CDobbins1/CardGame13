using System;
using System.Windows.Input;

namespace CardGame13.GUI.Commands
{
    public class ActionCommand : BaseCommand
    {
        private Action<object> ExecuteDelegate { get; }

        public ActionCommand(Action<object> method, Func<bool> predicate = null) : base(predicate)
        {
            ExecuteDelegate = method ?? throw new ArgumentNullException(nameof(method));
        }

        public override void Execute(object parameter) => ExecuteDelegate.Invoke(parameter);
    }
}
