using LearningXamarin.Models.Responses;
using LearningXamarin.ViewModels;
using Xamarin.Forms;

namespace LearningXamarin.Views
{
    public partial class IKEAItemDetailedPage : ContentPage
	{	
		public IKEAItemDetailedPage(StoreProductResponse model)
		{
			InitializeComponent();
			BindingContext = new IKEAItemDetailedViewModel(model);
		}
	}
}

