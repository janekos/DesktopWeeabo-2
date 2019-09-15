using System;

namespace DesktopWeeabo2.ViewModels.Shared {
	public class DelegateCommand : Command {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;
        private readonly Action _executeNoVar;

        public DelegateCommand(Action<object> execute) : this(execute, null) {}

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute) {
            //execute.AssertNotNull("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

		public DelegateCommand(Action executeNoVar, Predicate<object> canExecute) {
			_executeNoVar = executeNoVar;
			_canExecute = canExecute;
		}

		public DelegateCommand(Action executeNoVar) {
			_executeNoVar = executeNoVar;
		}

		public override bool CanExecute(object parameter) =>
			_canExecute == null ? true : _canExecute(parameter);

        public override void Execute(object parameter) {
			if (parameter == null) _executeNoVar();
			else _execute(parameter);
        }
	}
}
