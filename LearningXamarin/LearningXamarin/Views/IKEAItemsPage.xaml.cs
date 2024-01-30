using LearningXamarin.Helpers;
using LearningXamarin.ViewModels;
using Xamarin.Forms;

namespace LearningXamarin.Views
{
	public partial class IKEAItemsPage : ContentPage
	{	
		//L3 - As we are now using Shell, we won't be 
		//passing arguments in the view anymore.
		public IKEAItemsPage()
		{
			InitializeComponent();
			//L3 - Don't use this call
			//BindingContext = new IKEAItemsViewModel(Navigation, username);

			//L3 - Use this simpler one
			//BindingContext = new IKEAItemsViewModel();

			//L4 - 
			BindingContext = this.GetViewModel<IKEAItemsViewModel>();
		}
	}
}

