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
	public partial class EditNutriPlanPage : ContentPage
	{
        private int IdParameter;
        private string state;
        NutritionalPlan MyLocalNutritionalPlan = new NutritionalPlan();
        NutritionalPlanViewModel viewModel { get; set; }
        public EditNutriPlanPage (int Id)
		{
			InitializeComponent ();
            IdParameter = Id;
            BindingContext = viewModel = new NutritionalPlanViewModel();
            MyLocalNutritionalPlan = new NutritionalPlan();
            LoadPlan();

        }
        protected override void OnAppearing() //metodo que al volver a mostrar la pagina, vuelve a refrescar la lista con la nueva DATA
        {
            LoadPlan();
        }
        private async void LoadPlan()
        {
            MyLocalNutritionalPlan = await viewModel.GetPlanData(IdParameter);
            if (MyLocalNutritionalPlan != null)
            {
                TxtId.Text = Convert.ToString(MyLocalNutritionalPlan.IdPlan);
                TxtName.Text = Convert.ToString(MyLocalNutritionalPlan.Name);
                TxtDescription.Text = Convert.ToString(MyLocalNutritionalPlan.Description);
                TxtPlan.Text = Convert.ToString(MyLocalNutritionalPlan.PlanXample);
                PickerState.SelectedIndex = MyLocalNutritionalPlan.IdState == 1 ? 0 : 1;
                TxtId.IsEnabled = false;
                LblError.IsVisible = false;
                state = MyLocalNutritionalPlan.IdState == 1 ? "Activate" : "Inactive";
                ImagePreview.Source = Convert.ToString(MyLocalNutritionalPlan.PlanXample);
            }
            else
            {
                TxtId.IsVisible = false;
                TxtName.IsVisible = false;
                TxtDescription.IsVisible = false;
                TxtPlan.IsVisible = false;
                PickerState.IsVisible = false;
            }
        }
        private async void BtnExit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

        }
        private async void BtnEditName_Clicked(object sender, EventArgs e)
        {
            if (
               !string.IsNullOrEmpty(TxtName.Text.Trim())
               )
            {
                var action = await DisplayAlert("Are you sure to update the Name?",
                    "Previous Name: " + $"{MyLocalNutritionalPlan.Name}" + Environment.NewLine +
                    "New Name: " + TxtName.Text.Trim(), "Continue...", "Cancel");
                if (action)
                {
                    try
                    {
                        UserDialogs.Instance.ShowLoading("Updating in progress...");

                        await Task.Delay(2000);

                        MyLocalNutritionalPlan.Name = TxtName.Text.Trim();

                        bool R = await viewModel.UpdatePlanName(MyLocalNutritionalPlan.IdPlan, MyLocalNutritionalPlan);

                        if (R)
                        {

                            await DisplayAlert(":)", "Nutritional Plan Name Updated Successfully!", "OK");
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
                    "Previous Description: " + $"{MyLocalNutritionalPlan.Description}" + Environment.NewLine +
                    "New Description: " + TxtDescription.Text.Trim(), "Continue...", "Cancel");
                if (action)
                {
                    try
                    {
                        UserDialogs.Instance.ShowLoading("Updating in progress...");

                        await Task.Delay(2000);

                        MyLocalNutritionalPlan.Description = TxtDescription.Text.Trim();


                        bool R = await viewModel.UpdatePlanDescription(MyLocalNutritionalPlan.IdPlan, MyLocalNutritionalPlan);

                        if (R)
                        {
                            await DisplayAlert(":)", "Nutritional Plan's Description Updated Successfully!", "OK");
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

        private async void BtnEditPlan_Clicked(object sender, EventArgs e)
        {
            if (
               !string.IsNullOrEmpty(TxtPlan.Text.Trim())
               )
            {
                var action = await DisplayAlert("Are you sure to update the Example?",
                    "Previous Example: " + $"{MyLocalNutritionalPlan.PlanXample}" + Environment.NewLine +
                    "New Example: " + TxtPlan.Text.Trim(), "Continue...", "Cancel");
                if (action)
                {
                    try
                    {
                        UserDialogs.Instance.ShowLoading("Updating in progress...");

                        await Task.Delay(2000);

                        MyLocalNutritionalPlan.PlanXample = TxtPlan.Text.Trim();

                        bool R = await viewModel.UpdatePlanXample(MyLocalNutritionalPlan.IdPlan, MyLocalNutritionalPlan);

                        if (R)
                        {

                            await DisplayAlert(":)", "Nutritional Plan's Example Updated Successfully!", "OK");
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

                    MyLocalNutritionalPlan.IdState = Convert.ToInt32(PickerState.SelectedIndex + 1);

                    bool R = await viewModel.UpdatePlanState(MyLocalNutritionalPlan.IdPlan, MyLocalNutritionalPlan);

                    if (R)
                    {

                        await DisplayAlert(":)", "Nutritional Plan's State Updated Successfully!", "OK");
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






    }
}