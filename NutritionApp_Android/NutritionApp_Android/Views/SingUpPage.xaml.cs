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
            await Navigation.PopAsync();
        }   
        private async void BtnApply_Clicked(object sender, EventArgs e)
        {   
            if (
                TxtFullName.Text != null &&
                TxtPhone.Text != null &&
                TxtEmail.Text != null &&
                TxtPassword.Text != null &&
                TxtWeight.Text != null &&
                TxtHight.Text != null &&
                TxtAge.Text != null &&
                TxtFat.Text != null &&
                PickerUserGenre.SelectedIndex != -1
                )
            {
                if (TxtPassword.Text.Length > 7 && TxtPassword.Text.Length < 16)
                {
                    if (!Validaciones.IsValidEmail(TxtEmail.Text.Trim()))
                    {
                        await DisplayAlert("Email Alert", "Email has not a well format, please check it, it needs @ and correct domain", "OK");
                    }
                    else
                    {
                        try
                        {
                            UserDialogs.Instance.ShowLoading("Adding the user...");

                            await Task.Delay(2000);

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
                        catch (Exception)
                        {

                            throw;
                        }
                        finally
                        {
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                }
                else
                {
                    await DisplayAlert(":(", "Password is required to be between 7 and 16 characters!", "OK");
                }
            }
            else
            {
                //estas validaciones deben ser puntuales para informar al usuario que falla 

                if (TxtFullName.Text == null)
                {
                    await DisplayAlert(":(", "Name is required!", "OK");
                    TxtFullName.Focus();
                    return;
                }
                if (TxtPhone.Text == null)
                {
                    await DisplayAlert(":(", "Phone is required!", "OK");
                    TxtPhone.Focus();
                    return;
                }
                if (TxtEmail.Text == null)
                {
                    await DisplayAlert(":(", "Email is required!", "OK");
                    TxtEmail.Focus();
                    return;
                }
                if (TxtPassword.Text == null)
                {
                    await DisplayAlert(":(", "Password is required!", "OK");
                    TxtPassword.Focus();
                    return;
                }
                if (TxtWeight.Text == null)
                {
                    await DisplayAlert(":(", "Weight is required!", "OK");
                    TxtWeight.Focus();
                    return;
                }
                if (TxtHight.Text == null)
                {
                    await DisplayAlert(":(", "Hight is required!", "OK");
                    TxtHight.Focus();
                    return;
                }
                if (TxtAge.Text == null)
                {
                    await DisplayAlert(":(", "Age is required!", "OK");
                    TxtAge.Focus();
                    return;
                }
                if (TxtFat.Text == null)
                {
                    await DisplayAlert(":(", "Fat is required!", "OK");
                    TxtFat.Focus();
                    return;
                }
                if (PickerUserGenre.SelectedIndex == -1)
                {
                    await DisplayAlert(":(", "A Genre is required!", "OK");
                    PickerUserGenre.Focus();
                    return;
                }
            }
        }
    }
}