using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LearningXamarin.Models.Responses;
using LearningXamarin.Models.Wrappers;
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
		private IKEACategoryWrapper _categorySelected;
		private StoreProductResponse _selectedItem;
		private List<StoreProductResponse> _backupIKEAItems;
		private ObservableCollection<StoreProductResponse> _iKEAItems;
		private ObservableCollection<IKEACategoryWrapper> _itemCategories;

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

		public IKEACategoryWrapper CategorySelected
		{
			get => _categorySelected;
			set
			{
				if (value == _categorySelected)
				{
					return;
                }

				_categorySelected = value;
				OnPropertyChanged(nameof(CategorySelected));
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

		public ObservableCollection<IKEACategoryWrapper> ItemCategories
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

		public ICommand CategorySelectedCommand { get; set; }

		public ICommand SearchCommand { get; set; }

		public IKEAItemsViewModel(INavigation navigation, string username)
		{
			_navigationService = navigation;
			_apiClientService = new APIClientService();
			Username = username;
			CategorySelected = null;

			InitializeCommands();
			GetIKEAItemsCommand.Execute(null);
		}

		private void InitializeCommands()
		{
			GetIKEAItemsCommand = new Command(async () => await ExecuteGetIKEAItemsCommand());
			RefreshViewCommand = new Command(async () => await ExecuteRefreshViewCommand());
			GetDataFromAPIService = new Command(async () => await ExecuteGetDataFromAPIService());
			ItemSelectedCommand = new Command(ExecuteItemSelectedCommand);
			CategorySelectedCommand = new Command(ExecuteCategorySelectedCommand);
			SearchCommand = new Command<string>((searched) => ExecuteSearchCommand(searched));
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
            await GetProductCategories();

			IsRefreshing = false;
        }

		private async Task ExecuteGetDataFromAPIService()
		{
			var restResponse = await _apiClientService.GetProducts();

			if (restResponse.IsSuccessful && restResponse.Data != null)
			{
				//Inizializando la propiedad IKEAItems para despues meterle datos
				IKEAItems = new ObservableCollection<StoreProductResponse>(restResponse.Data);
				_backupIKEAItems = restResponse.Data;
			}
        }

        private async Task GetProductCategories()
        {
			var restResponse = await _apiClientService.GetCategories();

			if (restResponse.IsSuccessful && restResponse.Data != null)
			{
				//ItemCategories = new ObservableCollection<string>(restResponse.Data);
				ItemCategories = new ObservableCollection<IKEACategoryWrapper>();

				foreach (var category in restResponse.Data)
				{
					ItemCategories.Add(new IKEACategoryWrapper
					{
						Category = category,
						IsSelected = false,
					});
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

        private void ExecuteCategorySelectedCommand()
        {
			if (CategorySelected == null || IsBusy)
			{
				return;
            }

			IsBusy = true;
			DeselectAllCategories();
			HighlightSelectCategory();

            //Filtrar mi lista por categoria de la manera "larga"
            //List<StoreProductResponse> filteredList = new List<StoreProductResponse>();
            //foreach (var item in _backupIKEAItems)
            //{
            //	if (item.Category == CategorySelected)
            //	{
            //		filteredList.Add(item);
            //	}
            //}

            //Filtrar mi lista por categoria de la manera "corta"
            var filteredList = _backupIKEAItems.Where(item => item.Category == CategorySelected.Category);
			//Usar _backupIKEAItems para siempre tener una lista con 
			//los items completos obtenidos en el endpoint

			IKEAItems = new ObservableCollection<StoreProductResponse>(filteredList);

			CategorySelected = null;

			IsBusy = false;
        }

		private void ExecuteSearchCommand(string textSearched)
		{
			if (IsBusy || string.IsNullOrEmpty(textSearched))
			{
				return;
            }

			IsBusy = true;

			var textSearchedLowerCase = textSearched.ToLower();

			var filteredList = _backupIKEAItems.Where(item =>
			{
				return item.Title.ToLower().Contains(textSearchedLowerCase)
					|| item.Description.ToLower().Contains(textSearchedLowerCase);
            });

			IKEAItems = new ObservableCollection<StoreProductResponse>(filteredList);

			IsBusy = false;
        }

		private void HighlightSelectCategory()
		{
			CategorySelected.IsSelected = true;
        }

		private void DeselectAllCategories()
		{
			foreach (var cat in ItemCategories)
			{
				cat.IsSelected = false;
			}
        }
	}
}

