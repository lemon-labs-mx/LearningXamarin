using System.Collections.Generic;
using System.Collections.ObjectModel;
using LearningXamarin.Models.Responses;

namespace LearningXamarin.ViewModels
{
    public class IKEAItemDetailedViewModel : BaseViewModel
	{
		private StoreProductResponse _item;
		private ObservableCollection<string> _carouselViewData;

		public StoreProductResponse Item
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

		public ObservableCollection<string> CarouselViewData
		{
			get => _carouselViewData;
			set
			{
				if (value == _carouselViewData)
				{
					return;
                }

				_carouselViewData = value;
				OnPropertyChanged(nameof(CarouselViewData));
            }
        }

		public IKEAItemDetailedViewModel()
		{
			//Item = itemModel;
			CarouselViewData = new ObservableCollection<string>();
		}

        public override void ApplyQueryAttributes(IDictionary<string, string> query)
        {
			//if (query.ContainsKey("item"))
			//{
			//	//Item = JsonConvert.DeserializeObject<StoreProductResponse>(query["item"]);
			//	var unescapedData = Uri.UnescapeDataString(query["item"]);
			//	Item = JsonConvert.DeserializeObject<StoreProductResponse>(unescapedData);
				//InitializeCarouselViewData();
			//}
		}

		public override void OnNavigating<T>(T param)
        {
			if (param is StoreProductResponse product)
			{
				Item = product;
				InitializeCarouselViewData();
			}
		}

		private void InitializeCarouselViewData()
        {
			//Prevenimos que si el usuario no agrego las imagenes
			//nosotros no las mostremos
			if (!string.IsNullOrEmpty(Item.Image))
			{
				CarouselViewData.Add(Item.Image);
            }
        }
    }
}

