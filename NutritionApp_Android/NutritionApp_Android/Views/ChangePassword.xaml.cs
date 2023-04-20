using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NutritionApp_Android.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NutritionApp_Android.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePassword : ContentPage
    {

        UserViewModel viewModel;

        public ChangePassword()
        {
            InitializeComponent();

            this.BindingContext = viewModel = new UserViewModel();
        }

        private async void BtnUpdatePassword_Clicked(object sender, EventArgs e)
        {

            
            bool R = false;

            if (
                !string.IsNullOrEmpty(TxtCurrentPassword.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtNewPassword.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtConfirmNewPassword.Text.Trim())
                )
            {
                //si hay datos en el usuario y contrasennia se puede continuar 
                try
                {
                    UserDialogs.Instance.ShowLoading("Cheking User's Password...");

                    await Task.Delay(2000);


                    string CurrentPassword = TxtCurrentPassword.Text.Trim();
                    string Email = GlobalObjects.LocalUser.EmailAddress.Trim();

                    R = await viewModel.UserAccessValidation(Email, CurrentPassword);


                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    UserDialogs.Instance.HideLoading();

                }
            }
            else
            {
                await DisplayAlert("Validation Error", "You must fill the blank fields!", "OK");
                return;

            }


            if (R)
            {

                string NewPassword = TxtNewPassword.Text.Trim();
                string ConfirmNewPassword = TxtConfirmNewPassword.Text.Trim();

                if ( NewPassword.Equals( ConfirmNewPassword ) )
                {

                    R = await viewModel.UpdatePassword( NewPassword );

                    if (R)
                    {

                        await DisplayAlert(":)", "Password Updated Successfully!", "OK");

                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert(":(", "Somenthing went wrong!", "OK");
                    }

                }
                else
                {

                    await DisplayAlert("Validation Error", "Confirmation Password Incorrect!", "OK");
                    return;

                }

            }
            else
            {
                await DisplayAlert("Validation Error", "Password Incorrect!", "OK");
                return;
            }

        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void SwShowPassword_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwShowPassword.IsToggled)
            {
                TxtCurrentPassword.IsPassword = false;
                TxtNewPassword.IsPassword = false;
                TxtConfirmNewPassword.IsPassword = false;
            }
            else
            {
                TxtCurrentPassword.IsPassword = true;
                TxtNewPassword.IsPassword = true;
                TxtConfirmNewPassword.IsPassword = true;
            }
        }
    }
}