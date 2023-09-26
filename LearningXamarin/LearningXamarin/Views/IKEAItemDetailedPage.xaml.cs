using LearningXamarin.Models;
using LearningXamarin.ViewModels;
using Xamarin.Forms;

namespace LearningXamarin.Views
{
    public partial class IKEAItemDetailedPage : ContentPage
	{	
		//Cambien aqui IKEAItemModel a StoreProductResponse
		public IKEAItemDetailedPage(IKEAItemModel model)
		{
			InitializeComponent();
			//Cambien aqui IKEAItemModel a StoreProductResponse
			BindingContext = new IKEAItemDetailedViewModel(model);
		}
	}
}

