using NutritionApp_Android.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NutritionApp_Android.ViewModels;
using NutritionApp_Android.Models;

namespace NutritionApp_Android.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyRemindersList : ContentPage
    {

        ReminderViewModel viewModel;

        public MyRemindersList()
        {
            InitializeComponent();

            BindingContext = viewModel = new ReminderViewModel();

            LoadPage();
        }

        private async void LoadPage()
        {

            ReminderListView.ItemsSource = await viewModel.GetReminders();

        }

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {

        }

        private async void BtnAddReminder_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyReminders());
        }

        private async void BtnUpdateReminder_Clicked(object sender, EventArgs e)
        {
            Reminders Reminder = (Reminders) ReminderListView.SelectedItem;

            int IdReminder = Reminder.IdReminder;

            if( IdReminder > 0)
            {

                GlobalObjects.LocalReminder = Reminder;
                await Navigation.PushAsync(new MyReminders());

            }
            else
            {
               await DisplayAlert("Error", "Select a reminder" , "OK");
            }



        }
    }
}