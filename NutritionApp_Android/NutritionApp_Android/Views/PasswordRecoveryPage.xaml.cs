using Acr.UserDialogs;
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
    public partial class PasswordRecoveryPage : ContentPage
    {
        UserViewModel viewModel { get; set; }
        public PasswordRecoveryPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new UserViewModel();
        }
        private async void BtnSendRecoveryCode_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cheking User's RecoveryCode data...");

                await Task.Delay(2000);

                TxtPassword.IsVisible = false;
                TxtPasswordConfirm.IsVisible = false;
                BtnConfirmPassword.IsVisible = false;

                TxtPassword.IsEnabled = false;
                TxtPasswordConfirm.IsEnabled = false;
                BtnConfirmPassword.IsEnabled = false;

                if (!string.IsNullOrEmpty(TxtEmail.Text.Trim()))
                {
                    if (!Validaciones.IsValidEmail(TxtEmail.Text.Trim()))
                    {
                        await DisplayAlert("Email Alert", "Email has not a well format, please check it", "OK");

                    }
                    else
                    {
                        //await DisplayAlert("Validation ok", "Welcome!", "OK");

                        GlobalObjects.LocalUser = await viewModel.GetUserData(TxtEmail.Text.Trim());

                        if (GlobalObjects.LocalUser != null)
                        {
                            bool R = await viewModel.AddRecoveryCode(GlobalObjects.LocalUser.Id);
                            if (R)
                            {
                                TxtEmail.IsEnabled = false;
                                BtnSendRecoveryCode.Text = "Resend Code";
                                TxtRecoveryCode.IsVisible = true;
                                BtnCkRecoveryCode.IsVisible = true;
                                TxtRecoveryCode.IsEnabled = true;
                                BtnCkRecoveryCode.IsEnabled = true;

                                await DisplayAlert(":)", "Code sent successfully", "OK");

                            }
                            else
                            {
                                await DisplayAlert(":(", "Something went wrong!", "OK");
                            }

                        }
                        else
                        {
                            await DisplayAlert("Email Alert", "User not found with the email provided!", "OK");

                        }
                    }
                }

            }
            catch (Exception)
            {
                
                await DisplayAlert("Email Alert", "Email is required!", "OK");
                throw;
            }
            finally
            {
                UserDialogs.Instance.HideLoading();

            }
        }

        private async void BtnCkRecoveryCode_Clicked(object sender, EventArgs e)
        {
            //Check Recovery Code

            bool R = false;

            if (
                TxtEmail.Text != null && !string.IsNullOrEmpty(TxtEmail.Text.Trim()) &&
                TxtRecoveryCode.Text != null && !string.IsNullOrEmpty(TxtRecoveryCode.Text.Trim())
                )
            {
                //si hay datos en el email y recovery code se puede continuar 
                try
                {
                    UserDialogs.Instance.ShowLoading("Cheking User's RecoveryCode data...");

                    await Task.Delay(2000);


                    string email = TxtEmail.Text.Trim();
                    int recoveryCode = Convert.ToInt32(TxtRecoveryCode.Text.Trim());

                    R = await viewModel.UserRecoveryCodeValidation(email, recoveryCode);


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

            //TODO: en caso de que R sea true, entonces cargamos un usuario global igual que en progra 5 

            if (R)
            {
                TxtPasswordConfirm.IsVisible = true;
                TxtPasswordConfirm.IsEnabled = true;
                TxtPassword.IsVisible = true;
                TxtPassword.IsEnabled = true;
                BtnConfirmPassword.IsVisible = true;
                BtnConfirmPassword.IsEnabled = true;

                TxtRecoveryCode.IsEnabled = false;
                BtnCkRecoveryCode.IsEnabled = false;
                await DisplayAlert("Validation Successfulr", "Please Change the password!", "OK");
                return;
            }
            else
            {
                await DisplayAlert("Validation Error", "The RecoveryCode is incorrect!", "OK");
                return;

            }

        }

        private async void BtnConfirmPassword_Clicked(object sender, EventArgs e)
        {
            //Change Password
            bool R = false;
            try
            {
                if (
                   TxtPassword.Text != null && !string.IsNullOrEmpty(TxtEmail.Text.Trim()) &&
                   TxtPassword.Text.Length > 7 && TxtPassword.Text.Length < 16 &&
                   TxtPasswordConfirm.Text.Trim().Equals(TxtPassword.Text.Trim())
                   )
                {
                    UserDialogs.Instance.ShowLoading("Cheking User's RecoveryCode data...");

                    await Task.Delay(2000);



                    R = await viewModel.ChangePassword(GlobalObjects.LocalUser.Id, Convert.ToString(TxtPassword.Text.Trim()));
                }
                else
                {
                    await DisplayAlert("Validation Error", "The Password must be at least 8 to 16 digits, and match with " +
                        "the confirmation password", "OK");
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }

            if (R)
            {
                
                await DisplayAlert("Validation Successfull", "Password has been changed!", "OK");

                GlobalObjects.LocalUser = null;
                await Navigation.PopAsync(); //pushh async almacena aun la data de pagina visitada / en cambio PopAsync elimina la data y retorna la pagina anterior
            }
            else
            {
                await DisplayAlert("Validation Error", "Something went wrong!", "OK");
                return;

            }

        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            GlobalObjects.LocalUser = null;
            await Navigation.PopAsync(); //pushh async almacena aun la data de pagina visitada / en cambio PopAsync elimina la data y retorna la pagina anterior
        }
    }
}