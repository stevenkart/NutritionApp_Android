using Acr.UserDialogs;
using NutritionApp_Android.Models;
using NutritionApp_Android.ViewModels;
using RestSharp;
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
	public partial class UserAddEditPlanPage : ContentPage
	{
        private int method = 0;
        NutritionalPlan MyLocalNutritionalPlan { get; set; }
        NutritionalPlanViewModel viewModel { get; set; }

        UserViewModel vmUser { get; set; }

        public UserAddEditPlanPage(int id)
        {
            InitializeComponent();

            BindingContext = viewModel = new NutritionalPlanViewModel();

            vmUser = new UserViewModel();

            method = id;
            if (method == 0 || method == 1004)
            {
                LoadPlanActives();
                lblTitle.Text = "Choose a Plan to Add";
                BtnDelete.IsVisible = false;
                BtnDelete.IsEnabled = false;
                BtnChange.IsVisible = true;
                BtnChange.IsEnabled = true;
                BtnChange.Text = "Add";
            }
            else
            {
                lblTitle.Text = "Change a Plan";
                MyLocalNutritionalPlan = new NutritionalPlan();
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
                MyLocalNutritionalPlan = await viewModel.GetPlanData(method);
                collectionViewPlans.ItemsSource = await viewModel.GetPlansByFilter(1);
                if (collectionViewPlans.ItemsSource == null)
                {
                    lblNotPlan.IsVisible = true;
                }
            }
            else
            {
                collectionViewPlans.ItemsSource = await viewModel.GetPlansByFilter(1);
                if (collectionViewPlans.ItemsSource == null)
                {
                    lblNotPlan.IsVisible = true;
                }
            }
            
        }

        private async void BtnChange_Clicked(object sender, EventArgs e)
        {
            if (method == 0)
            {
                var itemSelected = collectionViewPlans.SelectedItem as NutritionalPlan;
                if (itemSelected != null)
                {
                    if (itemSelected != null)
                    {
                        var action = await DisplayAlert("Are you sure to get this Plan?",
                       "Plan: " + $"{itemSelected.Name}" + Environment.NewLine + "", "Continue...", "Cancel");

                        if (action)
                        {
                            try
                            {
                                UserDialogs.Instance.ShowLoading("Updating in progress...");

                                await Task.Delay(2000);

                                bool R = await vmUser.ChangeNutritionalPlan(GlobalObjects.LocalUser.Id, itemSelected.IdPlan);

                                if (R)
                                {
                                    await DisplayAlert(":)", "User's Nutritional Plan Updated Successfully!", "OK");
                                    GlobalObjects.LocalUser.IdPlans = itemSelected.IdPlan;
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
                    await DisplayAlert("Plan Selected ", "First of all, you need to select  the Item to continue.", "OK");
                }
            }
            else
            {
                var itemSelected = collectionViewPlans.SelectedItem as NutritionalPlan;
                if (itemSelected != null)
                {
                    if (itemSelected.IdPlan == MyLocalNutritionalPlan.IdPlan)
                    {
                        await DisplayAlert(":(", "Somenthing went wrong!" + Environment.NewLine + "You already have the plan selected, choose another plan.", "OK");
                    }
                    else
                    {
                        var action = await DisplayAlert("Are you sure to change the plan to another one?",
                        "Plan Selected: " + $"{itemSelected.Name}" + Environment.NewLine + "", "Continue...", "Cancel");

                        if (action)
                        {
                            try
                            {
                                UserDialogs.Instance.ShowLoading("Updating in progress...");

                                await Task.Delay(2000);

                                bool R = await vmUser.ChangeNutritionalPlan(GlobalObjects.LocalUser.Id, itemSelected.IdPlan);

                                if (R)
                                {
                                    await DisplayAlert(":)", "User's Nutritional Plan Updated Successfully!", "OK");
                                    GlobalObjects.LocalUser.IdPlans = itemSelected.IdPlan;
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
                    await DisplayAlert("Plan Selected ", "First of all, you need to select  the Item to continue.", "OK");
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
                MyLocalNutritionalPlan = await viewModel.GetPlanData(Convert.ToInt32(GlobalObjects.LocalUser.IdPlans));

                var action = await DisplayAlert("Are you sure to delete this Plan?",
                "Plan: " + $"{MyLocalNutritionalPlan.Name}" + Environment.NewLine + "", "Continue...", "Cancel");

                if (action)
                {
                    try
                    {
                        UserDialogs.Instance.ShowLoading("Deleting in progress...");

                        await Task.Delay(2000);

                        bool R = await vmUser.ChangeNutritionalPlan(GlobalObjects.LocalUser.Id, 1004);

                        if (R)
                        {
                            await DisplayAlert(":)", "User's Nutritional Plan Delete Successfully!", "OK");
                            GlobalObjects.LocalUser.IdPlans = 1004;
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

        private async void collectionViewPlans_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemSelected = e.CurrentSelection[0] as NutritionalPlan;
            if (itemSelected != null)
            {
                await DisplayAlert("Plan Selected ", $"{itemSelected.Name}" + Environment.NewLine +
                    $"{itemSelected.Description}", "OK");
            }
        }
    }
}