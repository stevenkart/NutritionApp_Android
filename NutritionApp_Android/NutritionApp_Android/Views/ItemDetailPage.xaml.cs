using NutritionApp_Android.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace NutritionApp_Android.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}