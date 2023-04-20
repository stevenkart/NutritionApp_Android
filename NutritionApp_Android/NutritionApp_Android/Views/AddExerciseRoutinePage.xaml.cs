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
            if (
              TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim()) &&
              TxtDescription.Text != null && !string.IsNullOrEmpty(TxtDescription.Text.Trim()) &&
              TxtExerciseXample.Text != null && !string.IsNullOrEmpty(TxtExerciseXample.Text.Trim())
              )
            {

                try
                {
                    UserDialogs.Instance.ShowLoading("Adding Exercise Routine...");

                    await Task.Delay(2000);

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
                if (TxtExerciseXample.Text == null || string.IsNullOrEmpty(TxtExerciseXample.Text.Trim()))
                {
                    await DisplayAlert(":(", "Example to show image is required!", "OK");
                    TxtExerciseXample.Focus();
                    return;
                }
            }

        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}