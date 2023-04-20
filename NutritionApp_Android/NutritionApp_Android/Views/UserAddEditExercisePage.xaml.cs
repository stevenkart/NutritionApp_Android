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
	public partial class UserAddEditExercisePage : ContentPage
	{
        private int method = 0;
        ExerciseRoutine MyLocalExerciseRoutine { get; set; }
        ExerciseRoutineViewModel viewModel { get; set; }
        UserViewModel vmUser { get; set; }

        public UserAddEditExercisePage(int id)
        {
            InitializeComponent();

            BindingContext = viewModel = new ExerciseRoutineViewModel();

            vmUser = new UserViewModel();

            method = id;
            if (method == 0 || method == 1004)
            {
                LoadPlanActives();
                lblTitle.Text = "Choose a Routine to Add";
                BtnDelete.IsVisible = false;
                BtnDelete.IsEnabled = false;
                BtnChange.IsVisible = true;
                BtnChange.IsEnabled = true;
                BtnChange.Text = "Add";
            }
            else
            {
                lblTitle.Text = "Change a Routine";
                MyLocalExerciseRoutine = new ExerciseRoutine();
                LoadPlanActives();
                BtnChange.IsVisible = true;
                BtnChange.IsEnabled = true;
                BtnChange.Text = "Change";
            }

        }

        protected override void OnAppearing() //metodo que al volver a mostrar la pagina, vuelve a refrescar la lista con la nueva DATA
        {
            LoadPlanActives();
        }

        private async void LoadPlanActives()
        {
            if (method != 0)
            {
                MyLocalExerciseRoutine = await viewModel.GetExerciseData(method);
                collectionViewRoutines.ItemsSource = await viewModel.GetExercisesByFilter(1);
                if (collectionViewRoutines.ItemsSource == null)
                {
                    lblNotPlan.IsVisible = true;
                }
            }
            else
            {
                collectionViewRoutines.ItemsSource = await viewModel.GetExercisesByFilter(1);
                if (collectionViewRoutines.ItemsSource == null)
                {
                    lblNotPlan.IsVisible = true;
                }
            }

        }

        private async void BtnChange_Clicked(object sender, EventArgs e)
        {
            if (method == 0)
            {
                var itemSelected = collectionViewRoutines.SelectedItem as ExerciseRoutine;
                if (itemSelected != null)
                {
                    if (itemSelected != null)
                    {
                        var action = await DisplayAlert("Are you sure to get this Routine?",
                       "Routine: " + $"{itemSelected.RoutineName}" + Environment.NewLine + "", "Continue...", "Cancel");

                        if (action)
                        {
                            try
                            {
                                UserDialogs.Instance.ShowLoading("Updating in progress...");

                                await Task.Delay(2000);

                                bool R = await vmUser.ChangeRoutine(GlobalObjects.LocalUser.Id, itemSelected.IdRoutine);

                                if (R)
                                {
                                    await DisplayAlert(":)", "User's Routine Exercises Updated Successfully!", "OK");
                                    GlobalObjects.LocalUser.IdRoutines = itemSelected.IdRoutine;
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
                }
                else
                {
                    await DisplayAlert("Routine Selected ", "First of all, you need to select  the Item to continue.", "OK");
                }
            }
            else
            {
                var itemSelected = collectionViewRoutines.SelectedItem as ExerciseRoutine;
                if (itemSelected != null)
                {
                    if (itemSelected.IdRoutine == MyLocalExerciseRoutine.IdRoutine)
                    {
                        await DisplayAlert(":(", "Somenthing went wrong!" + Environment.NewLine + "You already have the Routine selected, choose another plan.", "OK");
                    }
                    else
                    {
                        var action = await DisplayAlert("Are you sure to change the Routine to another one?",
                        "Routine Selected: " + $"{itemSelected.RoutineName}" + Environment.NewLine + "", "Continue...", "Cancel");

                        if (action)
                        {
                            try
                            {
                                UserDialogs.Instance.ShowLoading("Updating in progress...");

                                await Task.Delay(2000);

                                bool R = await vmUser.ChangeRoutine(GlobalObjects.LocalUser.Id, itemSelected.IdRoutine);

                                if (R)
                                {
                                    await DisplayAlert(":)", "User's Routine Exercises Updated Successfully!", "OK");
                                    GlobalObjects.LocalUser.IdRoutines = itemSelected.IdRoutine;
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
                }
                else
                {
                    await DisplayAlert("Routine Selected ", "First of all, you need to select  the Item to continue.", "OK");
                }
            }
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (method != 0)
            {
                MyLocalExerciseRoutine = await viewModel.GetExerciseData(Convert.ToInt32(GlobalObjects.LocalUser.IdRoutines));

                var action = await DisplayAlert("Are you sure to delete this Routine?",
                "Routine: " + $"{MyLocalExerciseRoutine.RoutineName}" + Environment.NewLine + "", "Continue...", "Cancel");

                if (action)
                {
                    try
                    {
                        UserDialogs.Instance.ShowLoading("Deleting in progress...");

                        await Task.Delay(2000);

                        bool R = await vmUser.ChangeNutritionalPlan(GlobalObjects.LocalUser.Id, 1004);

                        if (R)
                        {
                            await DisplayAlert(":)", "User's Routine Exercises Delete Successfully!", "OK");
                            GlobalObjects.LocalUser.IdRoutines = 1004;
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
        }

        private async void collectionViewRoutines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemSelected = e.CurrentSelection[0] as ExerciseRoutine;
            if (itemSelected != null)
            {
                await DisplayAlert("Routine Selected ", $"{itemSelected.RoutineName}" + Environment.NewLine +
                    $"{itemSelected.Description}", "OK");
            }
        }





    }
}