using Acr.UserDialogs;
using NutritionApp_Android.Models;
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
    public partial class EditExerciseRoutinePage : ContentPage
    {
        private int IdParameter;
        private string state;
        ExerciseRoutine MyLocalExerciseRoutine = new ExerciseRoutine();
        ExerciseRoutineViewModel viewModel { get; set; }
        public EditExerciseRoutinePage(int Id)
        {
            InitializeComponent();
            IdParameter = Id;
            BindingContext = viewModel = new ExerciseRoutineViewModel();
            MyLocalExerciseRoutine = new ExerciseRoutine();
            LoadPlan();
        }
        protected override void OnAppearing() //metodo que al volver a mostrar la pagina, vuelve a refrescar la lista con la nueva DATA
        {
            LoadPlan();
        }
        private async void LoadPlan()
        {
            MyLocalExerciseRoutine = await viewModel.GetExerciseData(IdParameter);
            if (MyLocalExerciseRoutine != null)
            {
                TxtId.Text = Convert.ToString(MyLocalExerciseRoutine.IdRoutine);
                TxtRoutineName.Text = Convert.ToString(MyLocalExerciseRoutine.RoutineName);
                TxtDescription.Text = Convert.ToString(MyLocalExerciseRoutine.Description);
                TxtExerciseXample.Text = Convert.ToString(MyLocalExerciseRoutine.ExerciseXample);
                PickerState.SelectedIndex = MyLocalExerciseRoutine.IdState == 1 ? 0 : 1;
                TxtId.IsEnabled = false;
                LblError.IsVisible = false;
                state = MyLocalExerciseRoutine.IdState == 1 ? "Activate" : "Inactive";
                ImagePreview.Source = Convert.ToString(MyLocalExerciseRoutine.ExerciseXample);
            }
            else
            {
                TxtId.IsVisible = false;
                TxtRoutineName.IsVisible = false;
                TxtDescription.IsVisible = false;
                TxtExerciseXample.IsVisible = false;
                PickerState.IsVisible = false;
            }
        }

        private async void BtnEditName_Clicked(object sender, EventArgs e)
        {
            if (
                !string.IsNullOrEmpty(TxtRoutineName.Text.Trim())
                )
            {
                var action = await DisplayAlert("Are you sure to update the Name?",
                    "Previous Name: " + $"{MyLocalExerciseRoutine.RoutineName}" + Environment.NewLine +
                    "New Name: " + TxtRoutineName.Text.Trim(), "Continue...", "Cancel");
                if (action)
                {
                    try
                    {
                        UserDialogs.Instance.ShowLoading("Updating in progress...");

                        await Task.Delay(2000);

                        MyLocalExerciseRoutine.RoutineName = TxtRoutineName.Text.Trim();

                        bool R = await viewModel.UpdateExerciseName(MyLocalExerciseRoutine.IdRoutine, MyLocalExerciseRoutine);

                        if (R)
                        {

                            await DisplayAlert(":)", "Exercise Routine´s Name Updated Successfully!", "OK");
                            LoadPlan();
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
                await DisplayAlert(":(", "Somenthing went wrong!" + Environment.NewLine + "The name input space can not be null", "OK");
            }
        }
        private async void BtnEditDescription_Clicked(object sender, EventArgs e)
        {
            if (
               !string.IsNullOrEmpty(TxtDescription.Text.Trim())
               )
            {
                var action = await DisplayAlert("Are you sure to update the Description?",
                    "Previous Description: " + $"{MyLocalExerciseRoutine.Description}" + Environment.NewLine +
                    "New: " + TxtDescription.Text.Trim(), "Continue...", "Cancel");
                if (action)
                {
                    try
                    {
                        UserDialogs.Instance.ShowLoading("Updating in progress...");

                        await Task.Delay(2000);

                        MyLocalExerciseRoutine.Description = TxtDescription.Text.Trim();

                        bool R = await viewModel.UpdateExerciseDescription(MyLocalExerciseRoutine.IdRoutine, MyLocalExerciseRoutine);

                        if (R)
                        {

                            await DisplayAlert(":)", "Exercise Routine´s Description Updated Successfully!", "OK");
                            LoadPlan();
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
                await DisplayAlert(":(", "Somenthing went wrong!" + Environment.NewLine + "The Description input space can not be null", "OK");
            }
        }
        private async void BtnEditExerciseXample_Clicked(object sender, EventArgs e)
        {
            if (
               !string.IsNullOrEmpty(TxtExerciseXample.Text.Trim())
               )
            {
                var action = await DisplayAlert("Are you sure to update the Example?",
                    "Previous Example: " + $"{MyLocalExerciseRoutine.ExerciseXample}" + Environment.NewLine +
                    "New: " + TxtExerciseXample.Text.Trim(), "Continue...", "Cancel");
                if (action)
                {
                    try
                    {
                        UserDialogs.Instance.ShowLoading("Updating in progress...");

                        await Task.Delay(2000);

                        MyLocalExerciseRoutine.Description = TxtExerciseXample.Text.Trim();

                        bool R = await viewModel.UpdateExerciseXample(MyLocalExerciseRoutine.IdRoutine, MyLocalExerciseRoutine);

                        if (R)
                        {

                            await DisplayAlert(":)", "Exercise Routine´s Example Updated Successfully!", "OK");
                            LoadPlan();
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
                await DisplayAlert(":(", "Somenthing went wrong!" + Environment.NewLine + "The Example input space can not be null", "OK");
            }
        }
        private async void BtnEditState_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayAlert("Are you sure to update the State?",
                "Previous State: " + $"{state}" + Environment.NewLine +
                "New State: " + PickerState.SelectedItem.ToString(), "Continue...", "Cancel");
            if (action)
            {
                try
                {
                    UserDialogs.Instance.ShowLoading("Updating in progress...");

                    await Task.Delay(2000);

                    MyLocalExerciseRoutine.IdState = Convert.ToInt32(PickerState.SelectedIndex + 1);

                    bool R = await viewModel.UpdateExerciseState(MyLocalExerciseRoutine.IdRoutine, MyLocalExerciseRoutine);

                    if (R)
                    {

                        await DisplayAlert(":)", "Exercise Routine´s State Updated Successfully!", "OK");
                        LoadPlan();
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
        private async void BtnExit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        



    }
}