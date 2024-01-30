using Xamarin.Forms;

namespace LearningXamarin.Helpers
{
    public static class ViewModelLocator
    {
        private static readonly ServiceLocator Container;

        static ViewModelLocator() => Container = new ServiceLocator();

        public static TViewModel GetViewModel<TViewModel>(this ContentPage _)
        {
            TViewModel viewModel = Container.Resolve<TViewModel>();
            return viewModel;
        }
    }
}

