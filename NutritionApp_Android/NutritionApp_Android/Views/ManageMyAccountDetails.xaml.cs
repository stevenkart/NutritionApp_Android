using Acr.UserDialogs;
using NutritionApp_Android.Models;
using NutritionApp_Android.ViewModels;
using System;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NutritionApp_Android.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageMyAccountDetails : ContentPage
    {

        UserViewModel viewModel { get; set; }
        UserDTO MyUser { get; set; }

        public ManageMyAccountDetails()
        {
            InitializeComponent();
            BindingContext = viewModel = new UserViewModel();
            LoadPage();
            MyUser = new UserDTO();
        }

        // method -> load data 
        public void LoadPage()
        {
            UserDTO User = GlobalObjects.LocalUser;

            TxtFullName.Text = User.Name;
            TxtPhone.Text = User.PhoneNum;
            TxtEmail.Text = User.EmailAddress;
            TxtWeight.Text = Convert.ToString(Convert.ToDouble(User.W));
            TxtHeight.Text = Convert.ToString(Convert.ToDouble(User.H));
            TxtAge.Text = Convert.ToString(Convert.ToDouble(User.Ages));
            TxtFat.Text = Convert.ToString(Convert.ToDouble(User.Fat));
        }

        // button -> modified user personal data
        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            string error = ValidateData();

            if (
                 /*
                TxtFullName.Text != null && !string.IsNullOrEmpty(TxtFullName.Text.Trim()) &&
                TxtPhone.Text != null && !string.IsNullOrEmpty(TxtPhone.Text.Trim()) &&
                TxtEmail.Text != null && !string.IsNullOrEmpty(TxtEmail.Text.Trim()) &&
                TxtWeight.Text != null && !string.IsNullOrEmpty(TxtWeight.Text.Trim()) &&
                TxtHeight.Text != null && !string.IsNullOrEmpty(TxtHeight.Text.Trim()) &&
                TxtAge.Text != null && !string.IsNullOrEmpty(TxtAge.Text.Trim()) &&
                TxtFat.Text != null && !string.IsNullOrEmpty(TxtFat.Text.Trim())
                 */
                 string.IsNullOrEmpty(error)
               )
            {

                if (!Validaciones.IsValidEmail(TxtEmail.Text.Trim()))
                {
                    await DisplayAlert("Email Alert", "Email has not a well format, please check it, it needs @ and correct domain", "OK");
                }
                else
                {
                    MyUser = await viewModel.GetUserData(TxtEmail.Text.Trim());
                    if (MyUser == null)
                    {
                        try
                        {
                            UserDialogs.Instance.ShowLoading("Adding the user...");

                            await Task.Delay(2000);

                            bool R = await viewModel.UpdateUser(
                               TxtFullName.Text.Trim(),
                               TxtPhone.Text.Trim(),
                               TxtEmail.Text.Trim(),
                               Convert.ToDecimal(TxtWeight.Text),
                               Convert.ToDecimal(TxtHeight.Text),
                               Convert.ToInt32(TxtAge.Text),
                               Convert.ToDecimal(TxtFat.Text)
                               );

                            if (R)
                            {

                                GlobalObjects.LocalUser = await viewModel.GetUserData(TxtEmail.Text.Trim());

                                await DisplayAlert(":)", "User Updated Successfully!", "OK");
                                MyUser = null;
                                //await Navigation.PushAsync(new MainMenuPage());
                            }
                            else
                            {
                                MyUser = null;
                                await DisplayAlert(":(", "Somenthing went wrong!", "OK");
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
                    }
                    else
                    {
                        if ((MyUser.EmailAddress == GlobalObjects.LocalUser.EmailAddress) && (MyUser.Id == GlobalObjects.LocalUser.Id))
                        {
                            try
                            {
                                UserDialogs.Instance.ShowLoading("Adding the user...");

                                await Task.Delay(2000);


                                bool R = await viewModel.UpdateUser(
                                   TxtFullName.Text.Trim(),
                                   TxtPhone.Text.Trim(),
                                   TxtEmail.Text.Trim(),
                                   Convert.ToDecimal(TxtWeight.Text),
                                   Convert.ToDecimal(TxtHeight.Text),
                                   Convert.ToInt32(TxtAge.Text),
                                   Convert.ToDecimal(TxtFat.Text)
                                   );

                                if (R)
                                {

                                    GlobalObjects.LocalUser = await viewModel.GetUserData(TxtEmail.Text.Trim());

                                    await DisplayAlert(":)", "User Updated Successfully!", "OK");
                                    MyUser = null;
                                    //await Navigation.PushAsync(new MainMenuPage());
                                }
                                else
                                {
                                    MyUser = null;
                                    await DisplayAlert(":(", "Somenthing went wrong!", "OK");
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
                        }
                        else
                        {
                            if ((MyUser.EmailAddress != GlobalObjects.LocalUser.EmailAddress) && (MyUser.Id != GlobalObjects.LocalUser.Id))
                            {
                                MyUser = null;
                                await DisplayAlert(":(", "The email typed already exists, you cannot change the email, try another one.", "OK");
                            }
                        }
                    }
                }
            }
            else
            {
                await DisplayAlert("Error", $"The {error}", "OK");
            }
            /*
            else
            {
                //estas validaciones deben ser puntuales para informar al usuario que falla 

                if (TxtFullName.Text == null || string.IsNullOrEmpty(TxtFullName.Text.Trim()))
                {
                    await DisplayAlert(":(", "Name is required!", "OK");
                    TxtFullName.Focus();
                    return;
                }
                if (TxtPhone.Text == null || string.IsNullOrEmpty(TxtPhone.Text.Trim()))
                {
                    await DisplayAlert(":(", "Phone is required!", "OK");
                    TxtPhone.Focus();
                    return;
                }
                if (TxtEmail.Text == null || string.IsNullOrEmpty(TxtEmail.Text.Trim()))
                {
                    await DisplayAlert(":(", "Email is required!", "OK");
                    TxtEmail.Focus();
                    return;
                }
                if (TxtWeight.Text == null || string.IsNullOrEmpty(TxtWeight.Text.Trim()))
                {
                    await DisplayAlert(":(", "Weight is required!", "OK");
                    TxtWeight.Focus();
                    return;
                }
                if (TxtHeight.Text == null || string.IsNullOrEmpty(TxtHeight.Text.Trim()))
                {
                    await DisplayAlert(":(", "Hight is required!", "OK");
                    TxtHeight.Focus();
                    return;
                }
                if (TxtAge.Text == null || string.IsNullOrEmpty(TxtAge.Text.Trim()))
                {
                    await DisplayAlert(":(", "Age is required!", "OK");
                    TxtAge.Focus();
                    return;
                }
                if (TxtFat.Text == null || string.IsNullOrEmpty(TxtFat.Text.Trim()))
                {
                    await DisplayAlert(":(", "Fat is required!", "OK");
                    TxtFat.Focus();
                    return;
                }  
            }
            */
        }

        // method -> data can't be empty
        private string ValidateData()
        {
            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrEmpty(TxtFullName.Text.Trim()))
            {
                sb.Append("Full Name field is empty \n");
            }

            if (string.IsNullOrEmpty(TxtPhone.Text.Trim()))
            {
                sb.Append("Phone field is empty \n");
            }

            if (string.IsNullOrEmpty(TxtEmail.Text.Trim()))
            {
                sb.Append("Email field is empty \n");
            }

            if (string.IsNullOrEmpty(TxtWeight.Text.Trim()))
            {
                sb.Append("Weight field is empty \n");
            }

            if (string.IsNullOrEmpty(TxtHeight.Text.Trim()))
            {
                sb.Append("Height field is empty \n");
            }

            if (string.IsNullOrEmpty(TxtAge.Text.Trim()))
            {
                sb.Append("Age field is empty \n");
            }

            if (string.IsNullOrEmpty(TxtFat.Text.Trim()))
            {
                sb.Append("Fat field is empty \n");
            }

            //if ( Validaciones.IsValidEmail( TxtEmail.Text.Trim() ) )
            //{
            //    sb.Append( "Email has not a well format \n" );
            //}

            return sb.ToString();
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void BtnChangePassword_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePassword());
        }
    }
}