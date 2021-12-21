using System;
using System.Windows.Input;
using WpfApp2.Annotations;

namespace WpfApp2
{
	public class RelayCommand : ICommand
	{
		public RelayCommand(Action execute)
		{
			_canExecute = _ => true;
			_execute = _ => execute();
		}
		
		public RelayCommand(Predicate<object> canExecute, Action<object> execute)
		{
			_canExecute = canExecute;
			_execute = execute;
		}

		public RelayCommand(Func<bool> canExecute, Action execute)
		{
			_canExecute = _ => canExecute();
			_execute = _ => execute();
		}

		public event EventHandler CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute(parameter);
		}

		public async void Execute(object parameter)
		{
			_execute(parameter);
		}

		private readonly Predicate<object> _canExecute;
		private readonly Action<object> _execute;
	}

	public class RelayCommand<T> : ICommand
	{
		public RelayCommand(Action<T> execute)
		{
			_canExecute = _ => true;
			_execute = execute;
		}
		
		public bool CanExecute(object parameter)
		{
			return _canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			_execute((T)parameter);
		}


		[UsedImplicitly]
		public event EventHandler CanExecuteChanged;
		
		private readonly Predicate<object> _canExecute;
		private readonly Action<T> _execute;
	}
}
