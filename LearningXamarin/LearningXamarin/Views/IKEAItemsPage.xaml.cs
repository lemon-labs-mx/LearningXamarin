using LearningXamarin.ViewModels;
using Xamarin.Forms;

namespace LearningXamarin.Views
{
    public partial class IKEAItemsPage : ContentPage
	{	
		public IKEAItemsPage(string username)
		{
			InitializeComponent();
			BindingContext = new IKEAItemsViewModel(Navigation, username);
		}
	}
}

