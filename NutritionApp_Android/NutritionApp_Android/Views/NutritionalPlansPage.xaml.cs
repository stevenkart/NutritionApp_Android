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
		public NutritionalPlansPage ()
		{
			InitializeComponent ();
		}

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
			await Navigation.PopAsync();
        }
    }
}