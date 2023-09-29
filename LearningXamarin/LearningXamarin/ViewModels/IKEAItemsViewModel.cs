using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
		private readonly APIClientService _apiClientService;

		private bool _isRefreshing;
		private string _username;
		private StoreProductResponse _selectedItem;
		private ObservableCollection<StoreProductResponse> _iKEAItems;
		private ObservableCollection<string> _itemCategories;

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

		public ObservableCollection<string> ItemCategories
		{
			get => _itemCategories;
			set
			{
				if (value == _itemCategories)
				{
					return;
                }

				_itemCategories = value;
				OnPropertyChanged(nameof(ItemCategories));
            }
        }

		public ICommand GetIKEAItemsCommand { get; set; }

		public ICommand RefreshViewCommand { get; set; }

		public ICommand ItemSelectedCommand { get; set; }

		public ICommand GetDataFromAPIService { get; set; }

		public IKEAItemsViewModel(INavigation navigation, string username)
		{
			_navigationService = navigation;
			_apiClientService = new APIClientService();
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
            await GetProductCategories();

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
			var restResponse = await _apiClientService.GetProducts();

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

        private async Task GetProductCategories()
        {
			var restResponse = await _apiClientService.GetCategories();

			if (restResponse.IsSuccessful && restResponse.Data != null)
			{
				ItemCategories = new ObservableCollection<string>(restResponse.Data);
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

