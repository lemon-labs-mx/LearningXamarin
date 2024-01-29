using System.Threading.Tasks;
using LearningXamarin.ViewModels;
using Xamarin.Forms;

namespace LearningXamarin.Services.NavigationService
{
    public class NavigationService : INavigationService
    {
        public async Task NavigateTo(string route)
        {
            await Shell.Current.GoToAsync($"/{route}");
        }

        public async Task NavigateTo<T>(string route, T param)
        {
            await Shell.Current.GoToAsync($"/{route}");

            if (Shell.Current.CurrentPage == null)
            {
                //Await to shell to load the page
                await Task.Delay(100);
            }

            var currentPage = Shell.Current.CurrentPage;

            if (currentPage != null
                && currentPage.BindingContext is BaseViewModel bvm)
            {
                bvm.OnNavigating(param);
            }
        }
    }
}

