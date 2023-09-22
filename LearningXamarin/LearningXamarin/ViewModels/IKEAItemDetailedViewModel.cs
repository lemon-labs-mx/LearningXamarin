using LearningXamarin.Models;

namespace LearningXamarin.ViewModels
{
    public class IKEAItemDetailedViewModel :BaseViewModel
	{
		private IKEAItemModel _item;

		public IKEAItemModel Item
		{
			get => _item;
			set
			{
				if (value == _item)
				{
					return;
                }

				_item = value;
				OnPropertyChanged(nameof(Item));
			}
        }

		public IKEAItemDetailedViewModel(IKEAItemModel itemModel)
		{
			Item = itemModel;
		}
	}
}

