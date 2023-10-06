using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LearningXamarin.Models.Enums;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace LearningXamarin.Views.PopupPages
{
    public partial class OrderItemsPopupPage : PopupPage
	{
        private TaskCompletionSource<OrderByEnum> _taskResult { get; set; }
        private static OrderByEnum _itemSelected;

        public static readonly BindableProperty CurrentStateProperty = BindableProperty
            .Create(
                nameof(CurrentState),
                typeof(OrderByEnum),
                typeof(OrderItemsPopupPage),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    var control = (OrderItemsPopupPage)bindable;
                    var orderEnum = (OrderByEnum)newVal;

                    DeselectAllItems(bindable);

                    switch (orderEnum)
                    {
                        case OrderByEnum.LowToHigh:
                            control.lowToHighState.IsSelected = true;
                            _itemSelected = OrderByEnum.LowToHigh;
                            break;

                        case OrderByEnum.HighToLow:
                            control.highToLowState.IsSelected = true;
                            _itemSelected = OrderByEnum.HighToLow;
                            break;

                        case OrderByEnum.TopRated:
                            control.topRatedState.IsSelected = true;
                            _itemSelected = OrderByEnum.TopRated;
                            break;

                        case OrderByEnum.Default:
                        default:
                            control.defaultState.IsSelected = true;
                            _itemSelected = OrderByEnum.Default;
                            break;
                    }
                });

        public OrderByEnum CurrentState
        {
            get => (OrderByEnum)GetValue(CurrentStateProperty);
            set => SetValue(CurrentStateProperty, value);
        }

        public ICommand OptionSelectedCommand { get; set; }

		public OrderItemsPopupPage(
            OrderByEnum currentItemSelected,
            TaskCompletionSource<OrderByEnum> task)
		{
            //Hay que iniciar el comando antes de inicializar la vista porque si no
            //el custom control OrderByOptionControl no reconoce el comando
            OptionSelectedCommand = new Command<OrderByEnum>(ExecuteOptionSelectedCommand);

			InitializeComponent();
            CurrentState = currentItemSelected;
            _taskResult = task;
		}

        private void ExecuteOptionSelectedCommand(OrderByEnum itemSelected)
        {
            if (itemSelected == _itemSelected)
            {
                return;
            }

            if (PopupNavigation.Instance.PopupStack.Any())
            {
                PopupNavigation.Instance.PopAsync();
                _taskResult.TrySetResult(itemSelected);
            }

        }

        private static void DeselectAllItems(BindableObject bindable)
        {
            var control = (OrderItemsPopupPage)bindable;

            control.defaultState.IsSelected = false;
            control.lowToHighState.IsSelected = false;
            control.highToLowState.IsSelected = false;
            control.topRatedState.IsSelected = false;
        }
	}
}