using NutritionApp_Android.Models;
using NutritionApp_Android.ViewModels;
using System;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NutritionApp_Android.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageMyAccountDetails : ContentPage
    {

        UserViewModel viewModel { get; set; }

        public ManageMyAccountDetails()
        {
            InitializeComponent();
            BindingContext = viewModel = new UserViewModel();
            LoadPage();
        }

        // method -> load data 
        public void LoadPage()
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

        // button -> modified user personal data
        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {

            string error = ValidateData();

            if ( string.IsNullOrEmpty( error ) )
            {
                // method -> modified user personal data
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
                    // method -> refresh global user
                    GlobalObjects.LocalUser = await viewModel.GetUserData(TxtEmail.Text.Trim());

                    await DisplayAlert(":)", "User Updated Successfully!", "OK");

                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert(":(", "Somenthing went wrong!", "OK");
                }

            }
            else
            {
                await DisplayAlert(":(", string.Format("{0}", error), "OK");
            }

        }


        // method -> data can't be empty
        private string ValidateData()
        {
            StringBuilder sb = new StringBuilder();

            if ( string.IsNullOrEmpty( TxtFullName.Text.Trim() ) ) 
            {
                sb.Append( "Full Name field is empty \n" );
            }

            if ( string.IsNullOrEmpty( TxtPhone.Text.Trim() ) )
            {
                sb.Append( "Phone field is empty \n" );
            }

            if ( string.IsNullOrEmpty( TxtEmail.Text.Trim() ) )
            {
                sb.Append( "Email field is empty \n" );
            }

            if ( string.IsNullOrEmpty( TxtWeight.Text.Trim() ) )
            {
                sb.Append( "Weight field is empty \n" );
            }

            if ( string.IsNullOrEmpty( TxtHeight.Text.Trim() ) )
            {
                sb.Append( "Height field is empty \n" );
            }

            if ( string.IsNullOrEmpty( TxtAge.Text.Trim() ) )
            {
                sb.Append( "Age field is empty \n" );
            }

            if (  string.IsNullOrEmpty( TxtFat.Text.Trim() ) )
            {
                sb.Append( "Fat field is empty \n" );
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