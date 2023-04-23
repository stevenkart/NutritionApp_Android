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
	public partial class MainMenuPage : ContentPage
	{
		public MainMenuPage ()
		{
			InitializeComponent ();
            UserName.Text = GlobalObjects.LocalUser.Name;
            if (GlobalObjects.LocalUser.IdStates == 4)
            {
                BtnAllUserEdit.IsEnabled = true;
                BtnAllUserEdit.IsVisible = true;
                BtnAllNutritionalsEdit.IsEnabled = true;
                BtnAllNutritionalsEdit.IsVisible = true;
                BtnAllExerciseEdit.IsEnabled = true;
                BtnAllExerciseEdit.IsVisible = true;
            }
            else
            {
                BtnAllUserEdit.IsEnabled = false;
                BtnAllUserEdit.IsVisible = false;
                BtnAllNutritionalsEdit.IsEnabled = false;
                BtnAllNutritionalsEdit.IsVisible = false;
                BtnAllExerciseEdit.IsEnabled = false;
                BtnAllExerciseEdit.IsVisible = false;
            }
		}

        private async void BtnExit_Clicked(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new NutritionLogin();
            await Navigation.PopAsync(); //pushh async almacena aun la data de pagina visitada / en cambio PopAsync elimina la data y retorna la pagina anterior
        }

        private async void BtnAllUserEdit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageUsers());
        }

        private async void BtnAllNutritionalsEdit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageNutritionalPlansPage());
        }

        private async void BtnAllExerciseEdit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageExerciseRoutinePage());
        }

        private async void BtnUser_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageMyAccountDetails());
        }

        private async void BtnReminder_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyRemindersList());
        }

        private async void BtnNutritionalPlan_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NutritionalPlansPage());
        }

        private async void BtnExercise_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExerciseRoutinePage());
        }

    }
}