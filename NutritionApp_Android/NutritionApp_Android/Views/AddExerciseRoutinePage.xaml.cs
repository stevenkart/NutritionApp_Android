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
    public partial class AddExerciseRoutinePage : ContentPage
    {
        ExerciseRoutineViewModel viewModel;
        public AddExerciseRoutinePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ExerciseRoutineViewModel();
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        { 
            bool R = await viewModel.AddExercise(
                TxtName.Text.Trim(),
                TxtDescription.Text.Trim(),
                TxtExerciseXample.Text.Trim());

            if (R)
            {

                await DisplayAlert(":)", "Exercise Routine Added Successfully!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert(":(", "Somenthing went wrong!", "OK");
            }
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}