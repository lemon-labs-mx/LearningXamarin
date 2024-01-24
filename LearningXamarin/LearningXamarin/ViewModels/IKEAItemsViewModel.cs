using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LearningXamarin.Models.Enums;
using LearningXamarin.Models.Responses;
using LearningXamarin.Models.Wrappers;
using LearningXamarin.Services.APIClientService;
using LearningXamarin.Services.NavigationService;
using LearningXamarin.Services.PopupNavigationService;
using LearningXamarin.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace LearningXamarin.ViewModels
{
	public class IKEAItemsViewModel : BaseViewModel
	{
		private readonly NavigationService _navigationService;
		private readonly APIClientService _apiClientService;
		private readonly PopupNavigationService _popupNavigationService;

		//L1 - Setitng the default values of the emtpy view
		private string _defaultEmptyViewMessage = "No Data Found!";
		private ImageSource _defaultEmptyViewImage = new Uri("https://cdn0.iconfinder.com/data/icons/small-n-flat/24/678069-sign-error-512.png");

		//L1 - Setting the values when no internet connection is detected
		private string _noInternetEmptyViewMessage = "No Internet Connection!";
		private ImageSource _noInternetEmptyViewImage = ImageSource.FromResource("LearningXamarin.Images.no-internet.png");

		private bool _isRefreshing;
		private string _username;
		private string _emptyViewMessage;
		private ImageSource _emptyViewImage;
		private OrderByEnum _orderByCategorySelected;
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

		//L1 - Binds the empty view message
		public string EmptyViewMessage
		{
			get => _emptyViewMessage;
			set
			{
				if (value == _emptyViewMessage || value == null)
				{
					return;
				}

				_emptyViewMessage = value;
				OnPropertyChanged(nameof(EmptyViewMessage));
			}
		}

		//L1 - Binds the empty view image source
		public ImageSource EmptyViewImage
		{
			get => _emptyViewImage;
			set
			{
				if (value == _emptyViewImage || value == null)
				{
					return;
				}

				_emptyViewImage = value;
				OnPropertyChanged(nameof(EmptyViewImage));
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

		public ICommand OrderByItemsCommand { get; set; }

		//L3 - Remove the navigation service, also, remove the parameter
		public IKEAItemsViewModel()
		{
			//L3 - We are not using the INavigation service,
			//we are going to use the AppShell navigation
			_navigationService = new NavigationService();
			_apiClientService = new APIClientService();
			_popupNavigationService = new PopupNavigationService();

			//The observable collections (and lists) are null by default
			IKEAItems = new ObservableCollection<StoreProductResponse>();
			ItemCategories = new ObservableCollection<IKEACategoryWrapper>();

			//L3 - We are not going to get the parameter from the constructor,
			//check the method ApplyQueryAttribute
			//Username = username;
			CategorySelected = null;
			//L1 - Sets the default message for empty view
			EmptyViewMessage = _defaultEmptyViewMessage;
			EmptyViewImage = _defaultEmptyViewImage;

			InitializeCommands();
			GetIKEAItemsCommand.Execute(null);
		}

		//L3 - Override the Apply Query method to use a custom behaviour
		public override void ApplyQueryAttributes(IDictionary<string, string> query)
		{
			//L3 - We are checking if the key "username" was sent
			//if (query.ContainsKey("username"))
			//{
			//	//If so, get the value
			//	Username = query["username"];
			//}
		}

        public override void OnNavigating<T>(T param)
        {
			Username = param.ToString();
        }

        private void InitializeCommands()
		{
			GetIKEAItemsCommand = new Command(async () => await ExecuteGetIKEAItemsCommand());
			RefreshViewCommand = new Command(async () => await ExecuteRefreshViewCommand());
			GetDataFromAPIService = new Command(async () => await ExecuteGetDataFromAPIService());
			OrderByItemsCommand = new Command(async () => await ExecuteOrderByItemsCommand());
			ItemSelectedCommand = new Command(async () => await ExecuteItemSelectedCommand());
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

			//L1 - Remember, this propety is in the BaseViewModel, this class inherits from Base,
			//so you can access its public properties and methods
			if (!HasInternetConnection)
			{
				//If the Ikea items list has any item, clear all of its items
				if (IKEAItems.Any())
				{
					IKEAItems.Clear();
				}

				//If the item categories list has any item, clear all of its items
				if (ItemCategories.Any())
				{
					ItemCategories.Clear();
				}

				//Show the no internet connection error
				EmptyViewMessage = _noInternetEmptyViewMessage;
				EmptyViewImage = _noInternetEmptyViewImage;

				//Remember to set the property to false here!
				//The return statement will prevent the IsBusy to deactivate
				IsBusy = false;
				return;
			}

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

			//L1 - Remember, this propety is in the BaseViewModel, this class inherits from Base,
			//so you can access its public properties and methods
			if (!HasInternetConnection)
			{
				//If the Ikea items list has any item, clear all of its items
				if (IKEAItems.Any())
				{
					IKEAItems.Clear();
				}

				//If the item categories list has any item, clear all of its items
				if (ItemCategories.Any())
				{
					ItemCategories.Clear();
				}

				//Show the no internet connection error
				EmptyViewMessage = _noInternetEmptyViewMessage;
				EmptyViewImage = _noInternetEmptyViewImage;

				//Remember to set the property to false here!
				//The return statement will prevent the IsRefreshing to deactivate
				IsRefreshing = false;
				return;
			}

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
				//Added the return statement here so the code won't show the emtpy view
				return;
			}

			//If the rest response is not success or the data is null,
			//show the default empty view message
			EmptyViewMessage = _defaultEmptyViewMessage;
			EmptyViewImage = _defaultEmptyViewImage;
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

		private async Task ExecuteOrderByItemsCommand()
		{
			// Open a PopupPage
			//_orderByCategorySelected = ...;
			if (IsBusy || !IKEAItems.Any())
			{
				return;
			}

			IsBusy = true;

			var oldVal = _orderByCategorySelected;

			_orderByCategorySelected = await _popupNavigationService.ShowOrderByPopup(_orderByCategorySelected);

			if (_orderByCategorySelected == oldVal)
			{
				IsBusy = false;
				return;
			}

			List<StoreProductResponse> itemsSorted = new List<StoreProductResponse>();

			switch (_orderByCategorySelected)
			{
				case OrderByEnum.HighToLow:
					itemsSorted = _backupIKEAItems
						.OrderByDescending(items => items.Price)
						.ToList();
					break;

				case OrderByEnum.LowToHigh:
					itemsSorted = _backupIKEAItems
						.OrderBy(items => items.Price)
						.ToList();
					break;

				case OrderByEnum.TopRated:
					itemsSorted = _backupIKEAItems
						.OrderBy(items => items.Rating.Rate)
						.ToList();
					break;

				case OrderByEnum.Default:
				default:
					IsBusy = false;
					RefreshViewCommand.Execute(null);
					return;
			}

			IKEAItems = new ObservableCollection<StoreProductResponse>(itemsSorted);

			IsBusy = false;
		}

		private async Task ExecuteItemSelectedCommand()
		{
			if (SelectedItem == null)
			{
				return;
			}

			//_navigationService.PushAsync(new IKEAItemDetailedPage(SelectedItem));
			//var jsonStr = JsonConvert.SerializeObject(SelectedItem);
			//var escapedData = Uri.EscapeDataString(jsonStr);
			//await Shell.Current.GoToAsync($"/{nameof(IKEAItemDetailedPage)}?item={escapedData}");
			await _navigationService.NavigateTo($"{nameof(IKEAItemDetailedPage)}", SelectedItem);

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

