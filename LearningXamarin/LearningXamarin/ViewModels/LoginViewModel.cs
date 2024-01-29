using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using LearningXamarin.Helpers;
using LearningXamarin.Services.NavigationService;
using LearningXamarin.Views;
using Xamarin.Forms;

namespace LearningXamarin.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		private readonly INavigationService _navigationService;

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

		public LoginViewModel(INavigationService navigationService)
		{
			InitializeCommands();
			//_navigationService = new NavigationService();
            _navigationService = navigationService;

            System.Diagnostics.Debug.WriteLine("have a nice DAY!".ToTitleCase());
            System.Diagnostics.Debug.WriteLine("Flag");
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

			//L3 - Comment this navigation, use shell instead
			//await _navigationService.PushAsync(new IKEAItemsPage(UserName));

			//L3 - The string format is "{route} ? paramName = {yourCustomParam}"
			//Send the Username parameter
			//await Shell.Current.GoToAsync($"/{nameof(IKEAItemsPage)}?username={UserName}");

			//L4 - 
			await _navigationService.NavigateTo($"{nameof(IKEAItemsPage)}", UserName);
		}
	}
}

