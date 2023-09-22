using System.Collections.Generic;
using LearningXamarin.Models;

namespace LearningXamarin.ViewModels
{
    public class IKEAItemDetailedViewModel :BaseViewModel
	{
		private IKEAItemModel _item;
		private List<string> _carouselViewData;

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

		public IKEAItemDetailedViewModel(IKEAItemModel itemModel)
		{
			Item = itemModel;
			CarouselViewData = new List<string>();
            InitializeCarouselViewData();
		}

        private void InitializeCarouselViewData()
        {
			//Prevenimos que si el usuario no agrego las imagenes
			//nosotros no las mostremos
			if (!string.IsNullOrEmpty(Item.Image))
			{
				CarouselViewData.Add(Item.Image);
            }

			if (!string.IsNullOrEmpty(Item.Image2))
			{
				CarouselViewData.Add(Item.Image2);
            }

			if (!string.IsNullOrEmpty(Item.Image3))
			{
				CarouselViewData.Add(Item.Image3);
            }

			if (!string.IsNullOrEmpty(Item.Image4))
			{
				CarouselViewData.Add(Item.Image4);
            }

			//Vamos a ponerlo por si acaso
			//esto le avisa a la vista que se actualice
			OnPropertyChanged(nameof(CarouselViewData));
        }
    }
}

