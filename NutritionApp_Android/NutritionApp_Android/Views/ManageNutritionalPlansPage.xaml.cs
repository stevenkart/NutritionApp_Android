﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NutritionApp_Android.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageNutritionalPlansPage : ContentPage
    {
        public ManageNutritionalPlansPage()
        {
            InitializeComponent();
        }

        private async void BtnExit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNutriPlanPage());
           

        }

        private void BtnEdit_Clicked(object sender, EventArgs e)
        {

        }
    }
}