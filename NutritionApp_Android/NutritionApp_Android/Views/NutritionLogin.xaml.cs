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
                try
                {
                    UserDialogs.Instance.ShowLoading("Cheking User Access data...");

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
  

                await Navigation.PushAsync(new MainMenuPage());

                //Application.Current.MainPage = new MainMenuPage();

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
    }
}