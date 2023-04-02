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
    }
}