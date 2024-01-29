using System.Globalization;
using Xamarin.Forms;

namespace LearningXamarin.Helpers
{
    public static class ViewModelLocator
    {
        private static readonly ServiceLocator Container;

        //static ViewModelLocator() => Container = new ServiceLocator();
        static ViewModelLocator()
        {
            Container = new ServiceLocator();
        }

        public static TViewModel GetViewModel<TViewModel>(this ContentPage _)
        {
            TViewModel viewModel = Container.Resolve<TViewModel>();
            return viewModel;
        }

        //Other example
        public static string ToTitleCase(this string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
    }
}

