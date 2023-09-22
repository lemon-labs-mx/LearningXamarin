using LearningXamarin.ViewModels;
using Xamarin.Forms;

namespace LearningXamarin.Views
{	
	public partial class LoginPage : ContentPage
	{	
		public LoginPage()
		{
			InitializeComponent();
			BindingContext = new LoginViewModel(Navigation);
		}
	}
}
