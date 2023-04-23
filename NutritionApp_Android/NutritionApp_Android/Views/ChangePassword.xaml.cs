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

            string error = ValidateData();

            if ( string.IsNullOrEmpty( error ) )
            {
                try
                {
                    UserDialogs.Instance.ShowLoading("Cheking User's Password...");

                    await Task.Delay(2000);

                    string CurrentPassword = TxtCurrentPassword.Text.Trim();
                    string Email = GlobalObjects.LocalUser.EmailAddress.Trim();

                    // method -> validate email and password
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
                await DisplayAlert("Validation Error", string.Format("{0}", error), "OK");
                return;
            }
            

            if (R)
            {
                string NewPassword = TxtNewPassword.Text.Trim();

                // method -> update password
                R = await viewModel.UpdatePassword(NewPassword);

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
                await DisplayAlert("Validation Error", "Password Incorrect!", "OK");
                return;
            }

        }

        // method -> data can't be empty
        private string ValidateData()
        {
            StringBuilder sb = new StringBuilder();

            if ( string.IsNullOrEmpty( TxtCurrentPassword.Text.Trim() ) )
            {
                sb.Append( "Current Password can't be empty \n" );
            }

            if ( string.IsNullOrEmpty( TxtNewPassword.Text.Trim() ) )
            {
                sb.Append( "New Password can't be empty \n" );
            }

            if ( string.IsNullOrEmpty( TxtConfirmNewPassword.Text.Trim() ) )
            {
                sb.Append( "Confirmation Password can't be empty \n" );
            }
            
            if ( TxtNewPassword.Text.Length < 8 || TxtNewPassword.Text.Length > 16 )
            {
                sb.Append( "New Password must be between 8 & 16 digits \n" );
            }

            if ( !TxtNewPassword.Text.Equals( TxtConfirmNewPassword.Text ) )
            {
                sb.Append( "Confirmation Password Incorrect \n" );
            }

            return sb.ToString();
        }

        // button -> exit
        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        // switch -> show the password text
        private void SwShowPassword_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwShowPassword.IsToggled)
            {
                ActiveSwitch();
            }
            else
            {
                InactiveSwitch();
            }
        }

        private void ActiveSwitch()
        {
            TxtCurrentPassword.IsPassword = false;
            TxtNewPassword.IsPassword = false;
            TxtConfirmNewPassword.IsPassword = false;
        }

        private void InactiveSwitch()
        {
            TxtCurrentPassword.IsPassword = true;
            TxtNewPassword.IsPassword = true;
            TxtConfirmNewPassword.IsPassword = true;
        }

    }
}