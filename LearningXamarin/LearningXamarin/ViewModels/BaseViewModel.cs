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
	}
}

