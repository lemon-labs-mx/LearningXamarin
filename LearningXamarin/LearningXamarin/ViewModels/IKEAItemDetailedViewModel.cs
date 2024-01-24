using System;
using System.Collections.Generic;
using LearningXamarin.Models.Responses;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace LearningXamarin.ViewModels
{
    public class IKEAItemDetailedViewModel : BaseViewModel
	{
		private StoreProductResponse _item;
		private List<string> _carouselViewData;

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

		public List<string> CarouselViewData
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
			CarouselViewData = new List<string>();
		}

        public override void ApplyQueryAttributes(IDictionary<string, string> query)
        {
			if (query.ContainsKey("item"))
			{
				//Item = JsonConvert.DeserializeObject<StoreProductResponse>(query["item"]);
				var unescapedData = Uri.UnescapeDataString(query["item"]);
				Item = JsonConvert.DeserializeObject<StoreProductResponse>(unescapedData);
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

