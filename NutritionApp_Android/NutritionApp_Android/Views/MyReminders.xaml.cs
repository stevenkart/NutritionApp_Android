using NutritionApp_Android.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NutritionApp_Android.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyReminders : ContentPage
    {

        ReminderViewModel viewModel;

        public MyReminders()
        {
            InitializeComponent();

            this.BindingContext = viewModel = new ReminderViewModel();

            LoadPage();
        }

        private void LoadPage()
        {

            int IdReminder = GlobalObjects.LocalReminder.IdReminder;

            if (IdReminder > 0) 
            {

                string Detail = GlobalObjects.LocalReminder.Detail;
                DateTime Date = Convert.ToDateTime( GlobalObjects.LocalReminder.Date );
                string H = GlobalObjects.LocalReminder.Hour;

                TimeSpan Hour = new TimeSpan(int.Parse(H.Split(':')[0]),    // hours
                       int.Parse(H.Split(':')[1]),    // minutes
                       int.Parse(H.Split(':')[2].Split('.')[0])   // seconds
                       );

                TxtDetail.Text = Detail;
                TxtDate.Date = Date.Date;
                TxtTime.Time = Hour;
            }

        }

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {

        }

        private async void BtnSaveReminder_Clicked(object sender, EventArgs e)
        {
            bool R = false;

            string Detail = TxtDetail.Text.Trim();

            if ( !string.IsNullOrEmpty( Detail ) )
            {

                string Date = TxtDate.Date.ToString();
                string Hour = TxtTime.Time.ToString();

                R = await viewModel.AddReminder(Detail, Date, Hour);
            }
            else
            {

                await DisplayAlert("Validation Error", "You must type a detail!", "OK");
                return;

            }


            if (R)
            {
                await DisplayAlert(":)", "Reminder Save Successfully!", "OK");

                await Navigation.PushAsync(new MainMenuPage());
            }
            else
            {
                await DisplayAlert(":(", "Somenthing went wrong!", "OK");
            }

        }
    }
}