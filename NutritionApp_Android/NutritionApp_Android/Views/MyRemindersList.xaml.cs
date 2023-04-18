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
using System.ComponentModel.Design;

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

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void BtnAddReminder_Clicked(object sender, EventArgs e)
        {
            GlobalObjects.LocalReminder.IdReminder = 0;

            await Navigation.PushAsync(new MyReminders());
        }

        private async void BtnUpdateReminder_Clicked(object sender, EventArgs e)
        {

            if (ReminderListView.SelectedItem != null)
            {
                Reminders Reminder = (Reminders)ReminderListView.SelectedItem;

                if (Reminder.IdReminder > 0)
                {

                    GlobalObjects.LocalReminder = Reminder;

                    await Navigation.PushAsync(new MyReminders());

                }
                else
                {
                    await DisplayAlert("Error", "Select a reminder", "OK");
                }

            }
            else
            {
                await DisplayAlert("Error", "Select a reminder", "OK");
            }
        }
    }
}