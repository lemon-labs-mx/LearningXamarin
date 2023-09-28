﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using LearningXamarin.Models.Responses;
using LearningXamarin.Services.APIClientService;
using LearningXamarin.Views;
using Xamarin.Forms;

namespace LearningXamarin.ViewModels
{
    public class IKEAItemsViewModel : BaseViewModel
	{
		private readonly INavigation _navigationService;

		private bool _isRefreshing;
		private string _username;
		private StoreProductResponse _selectedItem;
		private ObservableCollection<StoreProductResponse> _iKEAItems;

		public bool IsRefreshing
		{
			get => _isRefreshing;
			set
			{
				if (value == _isRefreshing)
				{
					return;
                }

				_isRefreshing = value;
				OnPropertyChanged(nameof(IsRefreshing));
            }
        }

		public string Username
		{
			get => _username;
			set
			{
				if (value == _username || value == null)
				{
					return;
                }

				_username = value;
				OnPropertyChanged(nameof(Username));
            }
        }

		public StoreProductResponse SelectedItem
		{
			get => _selectedItem;
			set
			{
				if (value == _selectedItem)
				{
					return;
                }

				_selectedItem = value;
				OnPropertyChanged(nameof(SelectedItem));
            }
        }

		public ObservableCollection<StoreProductResponse> IKEAItems
		{
			get => _iKEAItems;
			set
			{
				if (value == _iKEAItems)
				{
					return;
                }

				_iKEAItems = value;
				OnPropertyChanged(nameof(IKEAItems));
            }
        }

		public ICommand GetIKEAItemsCommand { get; set; }

		public ICommand RefreshViewCommand { get; set; }

		public ICommand ItemSelectedCommand { get; set; }

		public ICommand GetDataFromAPIService { get; set; }

		public IKEAItemsViewModel(INavigation navigation, string username)
		{
			_navigationService = navigation;
			Username = username;
			InitializeCommands();

			GetIKEAItemsCommand.Execute(null);
		}

		private void InitializeCommands()
		{
			GetIKEAItemsCommand = new Command(async () => await ExecuteGetIKEAItemsCommand());
			RefreshViewCommand = new Command(async () => await ExecuteRefreshViewCommand());
			GetDataFromAPIService = new Command(async () => await ExecuteGetDataFromAPIService());
			ItemSelectedCommand = new Command(ExecuteItemSelectedCommand);
        }

		private async Task ExecuteGetIKEAItemsCommand()
		{
			if (IsBusy)
			{
				return;
            }

			IsBusy = true;

			await ExecuteGetDataFromAPIService();

			IsBusy = false;
        }

		private async Task ExecuteRefreshViewCommand()
		{
			if (IsRefreshing)
			{
				return;
            }

			IsRefreshing = true;

			await ExecuteGetDataFromAPIService();

			IsRefreshing = false;
        }

		private async Task ExecuteGetDataFromAPIService()
		{
			APIClientService clientService = new APIClientService();
			var restResponse = await clientService.GetProducts();

			if (restResponse.IsSuccessful && restResponse.Data != null)
			{
				//Inizializando la propiedad IKEAItems para despues meterle datos
				IKEAItems = new ObservableCollection<StoreProductResponse>();

				//Recorrer la Lista "Data", ahi vienen los datos de la api
				foreach (var product in restResponse.Data)
				{
					IKEAItems.Add(product);
				}
			}
        }

		private void ExecuteItemSelectedCommand()
		{
			if (SelectedItem == null)
			{
				return;
            }

			_navigationService.PushAsync(new IKEAItemDetailedPage(SelectedItem));

			SelectedItem = null;
        }
	}
}

