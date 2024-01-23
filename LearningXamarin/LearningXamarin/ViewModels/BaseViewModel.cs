using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LearningXamarin.ViewModels
{
	public class BaseViewModel : BindableObject, IQueryAttributable
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

		//L3 - Add IQueryAttributable to pass data using Shell
		//Set this method as public virtual to enable overwritting
		//in your own view models
		public virtual void ApplyQueryAttributes(IDictionary<string, string> query)
		{
		}

		private bool IsNetworkConnected()
		{
			return Connectivity.NetworkAccess == NetworkAccess.Internet;
		}
	}
}

