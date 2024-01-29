using LearningXamarin.Helpers;
using LearningXamarin.ViewModels;
using Xamarin.Essentials; //Add this using
using Xamarin.Forms;

namespace LearningXamarin.Views
{	
	public partial class LoginPage : ContentPage
	{	
		public LoginPage()
		{
			InitializeComponent();
			//BindingContext = new LoginViewModel();
			BindingContext = this.GetViewModel<LoginViewModel>();
			ChangeIcon();
		}

		//L1-Essentials
		private void ChangeIcon()
		{
			//Using Xamarin.Essentials you can access the DeviceInfo.Platform
			//There you can check which platform the user currently is
			//The platforms available are:
			//-Android
			//-iOS
			//-macOS
			//-Tizen
			//-tvOS
			//-Unkown
			//-UWP
			//-watchOS
			//More info here: https://learn.microsoft.com/en-us/xamarin/essentials/
			if (DeviceInfo.Platform == DevicePlatform.Android)
			{
				icon.Source = ImageSource.FromResource("LearningXamarin.Images.android.png");
			}
			else
			{
				icon.Source = ImageSource.FromResource("LearningXamarin.Images.iOs.png");
			}
		}
	}
}
