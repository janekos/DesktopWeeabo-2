using System;

namespace DesktopWeeabo2.ViewModels.Shared {
	public class DelegateCommand : Command {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;
        private readonly Action _executeNoVar;

        /// <summary>
        /// Constructs an instance of <c>DelegateCommand</c>.
        /// </summary>
        /// <remarks>
        /// This constructor creates the command without a delegate for determining whether the command can execute. Therefore, the
        /// command will always be eligible for execution.
        /// </remarks>
        /// <param name="execute">
        /// The delegate to invoke when the command is executed.
        /// </param>
        public DelegateCommand(Action<object> execute)
            : this(execute, null) {
        }

        /// <summary>
        /// Constructs an instance of <c>DelegateCommand</c>.
        /// </summary>
        /// <param name="execute">
        /// The delegate to invoke when the command is executed.
        /// </param>
        /// <param name="canExecute">
        /// The delegate to invoke to determine whether the command can execute.
        /// </param>
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

		/// <summary>
		/// Determines whether this command can execute.
		/// </summary>
		/// <remarks>
		/// If there is no delegate to determine whether the command can execute, this method will return <see langword="true"/>. If a delegate was provided, this
		/// method will invoke that delegate.
		/// </remarks>
		/// <param name="parameter">
		/// The command parameter.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the command can execute, otherwise <see langword="false"/>.
		/// </returns>
		public override bool CanExecute(object parameter) {
            if (_canExecute == null) {
                return true;
            }

            return _canExecute(parameter);
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <remarks>
        /// This method invokes the provided delegate to execute the command.
        /// </remarks>
        /// <param name="parameter">
        /// The command parameter.
        /// </param>
        public override void Execute(object parameter) {
			if (parameter == null) _executeNoVar();
			else _execute(parameter);
        }
	}
}
