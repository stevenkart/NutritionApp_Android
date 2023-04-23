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
    public partial class AddNutriPlanPage : ContentPage
    {

        NutritionalPlanViewModel viewModel;
        public AddNutriPlanPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new NutritionalPlanViewModel();
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            if (
               TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim()) &&
               TxtDescription.Text != null && !string.IsNullOrEmpty(TxtDescription.Text.Trim()) &&
               TxtPlanXample.Text != null && !string.IsNullOrEmpty(TxtPlanXample.Text.Trim())
               )
            {
                try
                {
                    UserDialogs.Instance.ShowLoading("Adding Nutritional Plan...");

                    await Task.Delay(2000);

                    bool R = await viewModel.AddPlan(
                    TxtName.Text.Trim(),
                    TxtDescription.Text.Trim(),
                    TxtPlanXample.Text.Trim());

                    if (R)
                    {

                        await DisplayAlert(":)", "Nutritional Plan Added Successfully!", "OK");
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
            else
            {
                if (TxtName.Text == null || string.IsNullOrEmpty(TxtName.Text.Trim()))
                {
                    await DisplayAlert(":(", "Name is required!", "OK");
                    TxtName.Focus();
                    return;
                }
                if (TxtDescription.Text == null || string.IsNullOrEmpty(TxtDescription.Text.Trim()))
                {
                    await DisplayAlert(":(", "Description is required!", "OK");
                    TxtDescription.Focus();
                    return;
                }
                if (TxtPlanXample.Text == null || string.IsNullOrEmpty(TxtPlanXample.Text.Trim()))
                {
                    await DisplayAlert(":(", "Example to show image is required!", "OK");
                    TxtPlanXample.Focus();
                    return;
                }
            }     
        }
    }
}