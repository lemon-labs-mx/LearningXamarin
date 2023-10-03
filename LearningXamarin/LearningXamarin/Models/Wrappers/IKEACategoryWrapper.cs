using Xamarin.Forms;

namespace LearningXamarin.Models.Wrappers
{
    public class IKEACategoryWrapper : BindableObject
	{
		private bool _isSelected;

		public string Category { get; set; }

		public bool IsSelected
		{
			get => _isSelected;
			set
			{
				if (value == _isSelected)
				{
					return;
                }

				_isSelected = value;
				OnPropertyChanged(nameof(IsSelected));
            }
        }
    }
}

