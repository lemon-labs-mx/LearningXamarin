using System;
using System.Threading.Tasks;
using System.Windows.Input;
using LearningXamarin.Views;
using Xamarin.Forms;

namespace LearningXamarin.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		private readonly INavigation _navigationService;

		private string _userName;
		private string _passwordText;

		public string UserName
		{
			get => _userName;
			set
			{
				if (value == _userName || value == null)
				{
					return;
                }

				_userName = value;
				OnPropertyChanged(nameof(UserName));
            }
        }

		public string PasswordText
		{
			get => _passwordText;
			set
			{
				if (value == _passwordText || value == null)
				{
					return;
                }

				_passwordText = value;
				OnPropertyChanged(nameof(PasswordText));
            }
        }

        public ICommand LoginCommand { get; set; }
		public ICommand SyncCommand { get; set; }

		public LoginViewModel(INavigation navigation)
		{
			_navigationService = navigation;
			InitializeCommands();
		}

		private void InitializeCommands()
		{
			//Async Command
			LoginCommand = new Command(async () => await ExecuteLoginCommand());

			//Sync Command
			SyncCommand = new Command(ExecuteSyncCommand);
        }

        private void ExecuteSyncCommand()
        {
			Console.WriteLine("Hello!");
			UserName = "Adios!";
        }

        private async Task ExecuteLoginCommand()
		{
			if (UserName == null || PasswordText == null)
			{
				//TODO: Show error message
				return;
            }

			await _navigationService.PushAsync(new IKEAItemsPage(UserName));
        }
	}
}

