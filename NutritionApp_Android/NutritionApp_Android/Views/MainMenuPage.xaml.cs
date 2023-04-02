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
		}

        private async void BtnExit_Clicked(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new NutritionLogin();
            await Navigation.PopAsync(); //pushh async almacena aun la data de pagina visitada / en cambio PopAsync elimina la data y retorna la pagina anterior
        }

        private void BtnAllUserEdit_Clicked(object sender, EventArgs e)
        {

        }

        private async void BtnAllNutritionalsEdit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageNutritionalPlansPage());
        }

        private void BtnAllExerciseEdit_Clicked(object sender, EventArgs e)
        {

        }

        private async void BtnUser_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageMyAccountDetails());
        }

        private async void BtnReminder_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyReminders());
        }
    }
}