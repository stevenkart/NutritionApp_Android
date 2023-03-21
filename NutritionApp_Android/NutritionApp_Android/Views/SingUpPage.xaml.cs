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
	public partial class SingUpPage : ContentPage
	{

        UserViewModel viewModel;

        public SingUpPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new UserViewModel();

        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NutritionLogin());
        }

        private async void BtnApply_Clicked(object sender, EventArgs e)
        {

            //CAPTURAR el valor del id del role seleccionado en el picker

            //PickerUserGenre selectedUserRole = PickerUserRole.SelectedItem as UserRole;

            bool R = await viewModel.AddUser(
                TxtFullName.Text.Trim(),
                TxtPhone.Text.Trim(),
                TxtEmail.Text.Trim(),
                TxtPassword.Text.Trim(),
                Convert.ToDecimal(TxtWeight.Text.Trim()),
                Convert.ToDecimal(TxtHight.Text.Trim()),
                Convert.ToInt32(TxtAge.Text.Trim()),
                Convert.ToDecimal(TxtFat.Text.Trim()),
                PickerUserGenre.SelectedItem.ToString());

            if (R)
            {

                await DisplayAlert(":)", "User Added Successfully!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert(":(", "Somenthing went wrong!", "OK");
            }


        }
    }
}