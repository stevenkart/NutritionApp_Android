using NutritionApp_Android.Services;
using NutritionApp_Android.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NutritionApp_Android
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            //MainPage = new AppShell();
            MainPage = new NavigationPage(new NutritionLogin());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
