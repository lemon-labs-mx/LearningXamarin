using LearningXamarin.Models;
using LearningXamarin.ViewModels;
using Xamarin.Forms;

namespace LearningXamarin.Views
{
    public partial class IKEAItemDetailedPage : ContentPage
	{	
		public IKEAItemDetailedPage(IKEAItemModel model)
		{
			InitializeComponent();
			BindingContext = new IKEAItemDetailedViewModel(model);
		}
	}
}

