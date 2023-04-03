using Acr.UserDialogs;
using NutritionApp_Android.Models;
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
    public partial class ManageMyAccountDetails : ContentPage
    {

        UserViewModel viewModel;

        public ManageMyAccountDetails()
        {
            InitializeComponent();
            BindingContext = viewModel = new UserViewModel();
            LoadPage();
        }

        public async void LoadPage()
        {
            UserDTO User = GlobalObjects.LocalUser;

            TxtFullName.Text = User.Name;
            TxtPhone.Text = User.PhoneNum;
            TxtEmail.Text = User.EmailAddress;
            TxtWeight.Text = Convert.ToString( User.W );
            TxtHeight.Text = Convert.ToString( User.H );
            TxtAge.Text = Convert.ToString( User.Ages );
            TxtFat.Text = Convert.ToString( User.Fat );
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            bool R = await viewModel.UpdateUser(
                TxtFullName.Text.Trim(),
                TxtPhone.Text.Trim(),
                TxtEmail.Text.Trim(),
                Convert.ToDecimal(TxtWeight.Text.Trim()),
                Convert.ToDecimal(TxtHeight.Text.Trim()),
                Convert.ToInt32(TxtAge.Text.Trim()),
                Convert.ToDecimal(TxtFat.Text.Trim())
                );

            if (R)
            {

                GlobalObjects.LocalUser = await viewModel.GetUserData(TxtEmail.Text.Trim());

                await DisplayAlert(":)", "User Updated Successfully!", "OK");

                await Navigation.PushAsync(new MainMenuPage());
            }
            else
            {
                await DisplayAlert(":(", "Somenthing went wrong!", "OK");
            }
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainMenuPage());
        }

        private async void BtnChangePassword_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePassword());
        }
    }
}