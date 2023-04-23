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
        StringBuilder sb { get; set; }

        public ChangePassword()
        {
            InitializeComponent();
            sb = new StringBuilder();
            this.BindingContext = viewModel = new UserViewModel();
        }

        private async void BtnUpdatePassword_Clicked(object sender, EventArgs e)
        {
            bool R = false;

            bool error = ValidateData();


            if (error == false)
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
                await DisplayAlert("Validation Error", string.Format("{0}", sb.ToString()), "OK");
                sb = null;
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
        private bool ValidateData()
        {
            sb = new StringBuilder();

            if (TxtCurrentPassword.Text == null )
            {
                sb.Append( "Current Password can't be empty \n" );
            }

            if (TxtNewPassword.Text == null)
            {
                sb.Append( "New Password can't be empty \n" );
            }

            if (TxtConfirmNewPassword.Text == null)
            {
                sb.Append( "Confirmation Password can't be empty \n" );
            }

            if (TxtNewPassword.Text != null)
            {
                if (TxtNewPassword.Text.Length < 8 || TxtNewPassword.Text.Length > 16)
                {
                    sb.Append("New Password must be between 8 & 16 digits \n");
                }
            }

            if (TxtNewPassword.Text != null && TxtConfirmNewPassword.Text != null)
            {
                if (!TxtNewPassword.Text.Equals(TxtConfirmNewPassword.Text))
                {
                    sb.Append("Confirmation Password Incorrect \n");
                }
            }

            if (sb != null)
            {
                return true;
            }
            else
            {
                return false;
            }
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