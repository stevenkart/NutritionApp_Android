﻿using NutritionApp_Android.Models;
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
	public partial class NutritionalPlansPage : ContentPage
	{
		NutritionalPlanViewModel vm;
		public NutritionalPlansPage ()
		{
			InitializeComponent ();
			BindingContext = vm = new NutritionalPlanViewModel ();
			if (string.IsNullOrEmpty(Convert.ToString(GlobalObjects.LocalUser.IdPlans)) || (Convert.ToInt32(GlobalObjects.LocalUser.IdPlans) == 1004))
			{
                collectionViewPlan.ItemsSource = null;
				lblNotPlan.IsEnabled = true;
                lblNotPlan.IsVisible = true;
                BtnAdd.Text = "Add";
            }
			else
			{
                LoadPage(Convert.ToInt32(GlobalObjects.LocalUser.IdPlans));
                lblNotPlan.IsEnabled = false;
                lblNotPlan.IsVisible = false;
                BtnAdd.Text = "Change";
            }

		}

        protected override void OnAppearing() //metodo que al volver a mostrar la pagina, vuelve a refrescar la lista con la nueva DATA
        {
            if (string.IsNullOrEmpty(Convert.ToString(GlobalObjects.LocalUser.IdPlans)) || (Convert.ToInt32(GlobalObjects.LocalUser.IdPlans) == 1004))
            {
                collectionViewPlan.ItemsSource = null;
                lblNotPlan.IsEnabled = true;
                lblNotPlan.IsVisible = true;
                BtnAdd.Text = "Add";
            }
            else
            {
                LoadPage(Convert.ToInt32(GlobalObjects.LocalUser.IdPlans));
                lblNotPlan.IsEnabled = false;
                lblNotPlan.IsVisible = false;
                BtnAdd.Text = "Change";
            }
        }
        private async void LoadPage(int id)
		{
            collectionViewPlan.ItemsSource = await vm.GetCollectionPlanData(id);     
        }
        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
			await Navigation.PopAsync();
        }
        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            
            if (BtnAdd.Text == "Change")
            {
                var itemSelected = collectionViewPlan.SelectedItem as NutritionalPlan;
                if (itemSelected != null)
                {
                    if (itemSelected != null)
                    {
                        var action = await DisplayAlert("Are you sure to edit/change the Plan?",
                       "Plan: " + $"{itemSelected.Name}" + Environment.NewLine + "", "Continue...", "Cancel");

                        if (action)
                        {
                            await Navigation.PushAsync(new UserAddEditPlanPage(itemSelected.IdPlan));
                        } 
                    }
                }
                else
                {
                    await DisplayAlert("Plan Selected ", "First of all, you need to select  the Item to change", "OK");
                }              
            }
            else
            {
                await Navigation.PushAsync(new UserAddEditPlanPage(0));
            } 
           
        }

        private async void collectionViewPlan_SelectionChanged(object sender, SelectionChangedEventArgs e)
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