using LearningXamarin.Helpers;
using LearningXamarin.ViewModels;
using Xamarin.Forms;

namespace LearningXamarin.Views
{
    public partial class IKEAItemDetailedPage : ContentPage
	{	
		public IKEAItemDetailedPage()
		{
			InitializeComponent();
			//BindingContext = new IKEAItemDetailedViewModel();

			BindingContext = this.GetViewModel<IKEAItemDetailedViewModel>();
		}
	}
}

