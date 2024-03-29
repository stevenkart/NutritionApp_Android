﻿using Acr.UserDialogs;
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
    public partial class NutritionLogin : ContentPage
    {

        //se debe "anclar el VM correspondiente a la vista"

        UserViewModel viewModel;

        public NutritionLogin()
        {
            InitializeComponent();
           
            labelLogin.FontSize = 20;
            this.BindingContext = viewModel = new UserViewModel();
            GlobalObjects.LocalUser = null;
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            //Login Usuario

            bool R = false;

            if (
                TxtEmail.Text != null && !string.IsNullOrEmpty(TxtEmail.Text.Trim()) &&
                TxtPassword.Text != null && !string.IsNullOrEmpty(TxtPassword.Text.Trim())
                )
            {
                //si hay datos en el usuario y contrasennia se puede continuar 
                if (!Validaciones.IsValidEmail(TxtEmail.Text.Trim()))
                {
                    await DisplayAlert("Email Alert", "Email has not a well format, please check it, it needs @ and correct domain", "OK");
                }
                else
                {
                    try
                    {
                        UserDialogs.Instance.ShowLoading("Checking User Access data...");

                        await Task.Delay(2000);

                        string email = TxtEmail.Text.Trim();
                        string password = TxtPassword.Text.Trim();

                        R = await viewModel.UserAccessValidation(email, password);
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
                await DisplayAlert("Validation Error", "User Email and Password are required!", "OK");
                return;

            }

            //TODO: en caso de que R sea true, entonces cargamos un usuario global igual que en progra 5 

            if (R)
            {
                //await DisplayAlert("Validation ok", "Welcome!", "OK");

                GlobalObjects.LocalUser = await viewModel.GetUserData(TxtEmail.Text.Trim());

                if (GlobalObjects.LocalUser.IdStates == 2)
                {
                    await DisplayAlert("Validation Error", "User is inactive!, contact the administrators to activate", "OK");
                    GlobalObjects.LocalUser = null;
                }
                else
                {
                    await Navigation.PushAsync(new MainMenuPage());
                }
                return;
            }
            else
            {
                await DisplayAlert("Validation Error", "User Email or Password are incorrect!, Access Denied", "OK");
                return;
            }
        }

        private async void BtnSingUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SingUpPage());
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PasswordRecoveryPage());
        }
        private void SwShowPassword_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwShowPassword.IsToggled)
            {
                TxtPassword.IsPassword = false;
            }
            else
            {
                TxtPassword.IsPassword = true;
            }
        }

        private async void BtnCloseApp_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayAlert("Closing...", "Are you sure to close the app?", "Continue...", "Cancel");
            if (action)
            {
                Environment.Exit(0);
            }
        }
    }
}