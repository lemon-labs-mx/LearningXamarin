using Xamarin.Essentials;
using Xamarin.Forms;

namespace LearningXamarin.ViewModels
{
	public class BaseViewModel : BindableObject
	{
		private bool _isBusy;

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

		//L1 - Add a property to check if the user has internet connection
		//using Xamarin.Essentials
		public bool HasInternetConnection => IsNetworkConnected();

        private bool IsNetworkConnected()
        {
			return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }
	}
}

