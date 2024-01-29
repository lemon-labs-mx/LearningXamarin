using System.Threading.Tasks;

namespace LearningXamarin.Services.NavigationService
{
    public interface INavigationService
    {
        Task NavigateTo(string route);

        Task NavigateTo<T>(string route, T param);
    }
}

