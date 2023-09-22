using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using LearningXamarin.Models;
using LearningXamarin.Views;
using Xamarin.Forms;

namespace LearningXamarin.ViewModels
{
    public class IKEAItemsViewModel : BaseViewModel
	{
		private readonly INavigation _navigationService;

		private bool _isBusy;
		private string _username;
		private IKEAItemModel _selectedItem;
		private ObservableCollection<IKEAItemModel> _iKEAItems;

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

		public IKEAItemModel SelectedItem
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

		public ObservableCollection<IKEAItemModel> IKEAItems
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

		public ICommand LoadDataCommand { get; set; }

		public ICommand RefreshViewCommand { get; set; }

		public ICommand ItemSelectedCommand { get; set; }

		public IKEAItemsViewModel(INavigation navigation, string username)
		{
			_navigationService = navigation;
			Username = username;
			InitializeCommands();
		}

		private void InitializeCommands()
		{
			LoadDataCommand = new Command(ExecuteLoadDataCommand);
			RefreshViewCommand = new Command(async () => await ExecuteRefreshViewCommand());
			ItemSelectedCommand = new Command(ExecuteItemSelectedCommand);
        }

		private void ExecuteLoadDataCommand()
		{
			IKEAItems = new ObservableCollection<IKEAItemModel>
			{
				new IKEAItemModel
				{
					Image = "https://www.gaiadesign.com.mx/media/catalog/product/cache/28cb47c806b746a91bc25b380c9673fa/m/e/mesa_lateral_xocos_cafe_still1_v1.jpg",
					Name = "Mesa de noche",
					Description = "Linda mesa de noche de 30 x 30 cm",
					Price = 120.50M,
				},

				new IKEAItemModel
				{
					Image = "https://http2.mlstatic.com/D_NQ_NP_812901-MLM46460205115_062021-O.webp",
					Name = "Taza horrible",
					Description = "Regala a tus amigos esta cochinada",
					Price = 99.50M,
				}
			};
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
			LoadDataCommand.Execute(null);
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

			//Do your stuff here!
			_navigationService.PushAsync(new IKEAItemDetailedPage(SelectedItem));

			SelectedItem = null;
        }
	}
}

