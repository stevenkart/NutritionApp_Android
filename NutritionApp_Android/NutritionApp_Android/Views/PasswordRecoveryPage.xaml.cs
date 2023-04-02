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
            if (!string.IsNullOrEmpty(TxtEmail.Text.Trim()))
            {
                //bool R = await viewModel.AddRecoveryCode(Convert.ToDecimal(TxtEmail.Text.Trim()));
                bool R = await viewModel.AddRecoveryCode();
                if (R)
                {
                    TxtEmail.IsEnabled = false;
                    await DisplayAlert(":)", "Code sent successfully", "OK");
                }
                else
                {
                    await DisplayAlert(":(", "Something went wrong!", "OK");
                }
            }
        }
    }
}