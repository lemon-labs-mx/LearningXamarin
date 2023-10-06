using System.Threading.Tasks;
using LearningXamarin.Models.Enums;
using LearningXamarin.Views.PopupPages;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;

namespace LearningXamarin.Services.PopupNavigationService
{
	public class PopupNavigationService
	{
        private readonly IPopupNavigation _popupNavigation;

        public PopupNavigationService()
        {
            _popupNavigation = Rg.Plugins.Popup.Services.PopupNavigation.Instance;
        }

        public async Task PushPopup(PopupPage popupPage)
        {
            await _popupNavigation.PushAsync(popupPage);
        }

        public async Task<OrderByEnum> ShowOrderByPopup(OrderByEnum orderByStateSelected)
        {
            //Crea un hilo que esta esperando a que el usuario seleccione algo...
            TaskCompletionSource<OrderByEnum> taskCompletionSource = new TaskCompletionSource<OrderByEnum>();

            await _popupNavigation.PushAsync(new OrderItemsPopupPage(orderByStateSelected, taskCompletionSource));

            return await taskCompletionSource.Task;
        }
    }
}

