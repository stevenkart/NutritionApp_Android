using NutritionApp_Android.Models;
using NutritionApp_Android.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NutritionApp_Android.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageNutritionalPlansPage : ContentPage
    {
        NutritionalPlanViewModel vm { get; set; }
        private int filter = 1;
        public ManageNutritionalPlansPage()
        {
            InitializeComponent(); 
            BindingContext = vm = new NutritionalPlanViewModel();
            LoadPlansList();
        }

        protected override void OnAppearing() //metodo que al volver a mostrar la pagina, vuelve a refrescar la lista con la nueva DATA
        {
            LoadPlansList();
            BtnFilter.Text = "All";
        }

        private async void LoadPlansList()
        {
            collectionViewPlans.ItemsSource = await vm.GetNutritionalPlansAll();
            BtnFilter.Text = "All";

        }

        private async void BtnExit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNutriPlanPage());

        }

        private async void collectionViewPlans_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemSelected = e.CurrentSelection[0] as NutritionalPlan;
            if (itemSelected != null)
            {
                await DisplayAlert("Plan Selected ", $"{itemSelected.Name}", "OK");
                await Navigation.PushAsync(new EditNutriPlanPage(itemSelected.IdPlan));
            }
        }

        private async void BtnFilter_Clicked(object sender, EventArgs e)
        {
            if (filter == 2)
            {
                collectionViewPlans.ItemsSource = await vm.GetPlansByFilter(filter);
                BtnFilter.Text = "Inactive";
                filter = 0;
            }
            else
            {
                if (filter == 0)
                {
                    LoadPlansList();
                    BtnFilter.Text = "All";
                    filter = 1;
                }
                else
                {
                    if (filter == 1)
                    {
                        collectionViewPlans.ItemsSource = await vm.GetPlansByFilter(filter);
                        BtnFilter.Text = "Active";
                        filter = 2;
                    }
                }
            }
        }
    }
}