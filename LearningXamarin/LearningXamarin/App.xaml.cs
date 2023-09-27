using System;
using LearningXamarin.Styles;
using LearningXamarin.Views;
using Xamarin.Forms;

namespace LearningXamarin
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent();
            ApplyAppResources();

            MainPage = new NavigationPage(new LoginPage());
        }

        private void ApplyAppResources()
        {
            Resources.MergedDictionaries.Add(new Colors());
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

