using System.Threading.Tasks;
using LearningXamarin.Models.Enums;
using Rg.Plugins.Popup.Pages;

namespace LearningXamarin.Services.PopupNavigationService
{
    public interface IPopupNavigationService
    {
        Task PushPopup(PopupPage popupPage);

        Task<OrderByEnum> ShowOrderByPopup(OrderByEnum orderByStateSelected);
    }
}