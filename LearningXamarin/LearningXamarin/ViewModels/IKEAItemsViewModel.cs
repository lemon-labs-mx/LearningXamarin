using System.Collections.ObjectModel;
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

		private bool _isBusy;
		private string _username;
		private StoreProductResponse _selectedItem;
		private ObservableCollection<StoreProductResponse> _iKEAItems;

		public bool IsBusy
		{
			get => _isBusy;
			set
			{
				if (value == _isBusy)
				{
					return;
                }

				_isBusy = value;
				OnPropertyChanged(nameof(IsBusy));
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

		public ICommand RefreshViewCommand { get; set; }

		public ICommand ItemSelectedCommand { get; set; }

		public ICommand GetDataFromAPIService { get; set; }

		public IKEAItemsViewModel(INavigation navigation, string username)
		{
			_navigationService = navigation;
			Username = username;
			InitializeCommands();
		}

		private void InitializeCommands()
		{
			RefreshViewCommand = new Command(async () => await ExecuteRefreshViewCommand());
			ItemSelectedCommand = new Command(ExecuteItemSelectedCommand);
			GetDataFromAPIService = new Command(async () => await ExecuteGetDataFromAPIService());
        }

		private async Task ExecuteRefreshViewCommand()
		{
			if (IsBusy == true)
			{
				return;
            }

			IsBusy = true;

			//Simular llamada a internet
			await Task.Delay(1500);
			GetDataFromAPIService.Execute(null);
			//Añadan mas items a su Lista
			//...

			IsBusy = false;
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
	}
}

