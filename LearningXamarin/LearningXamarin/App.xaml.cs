using LearningXamarin.Styles;
using LearningXamarin.Views;
using Xamarin.Forms;

namespace LearningXamarin
{
    public partial class App : Application
    {
        public App ()
        {
            //Xamarin.Forms method to create the app
            InitializeComponent();
            ApplyAppResources();
            RegisterRoutes();

            //L3 - Comment this
            //MainPage = new NavigationPage(new LoginPage());
            //Use this instead to use Shell
            MainPage = new AppShell();
        }

        private void ApplyAppResources()
        {
            Resources.MergedDictionaries.Add(new Colors());
            Resources.MergedDictionaries.Add(new CustomStyles());
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(IKEAItemsPage), typeof(IKEAItemsPage));
            Routing.RegisterRoute(nameof(IKEAItemDetailedPage), typeof(IKEAItemDetailedPage));
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

