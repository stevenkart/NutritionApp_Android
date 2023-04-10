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
    public partial class ManageExerciseRoutinePage : ContentPage
    {
        ExerciseRoutineViewModel viewModel { get; set; }
        private int filter = 1;
        public ManageExerciseRoutinePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ExerciseRoutineViewModel();
            LoadPlansList();
        }

        private async void collectionViewExercises_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemSelected = e.CurrentSelection[0] as ExerciseRoutine;
            if (itemSelected != null)
            {
                await DisplayAlert("Routine Selected ", $"{itemSelected.RoutineName}", "OK");
                await Navigation.PushAsync(new EditExerciseRoutinePage(itemSelected.IdRoutine));
            }
        }


        protected override void OnAppearing() //metodo que al volver a mostrar la pagina, vuelve a refrescar la lista con la nueva DATA
        {
            LoadPlansList();
            BtnFilter.Text = "All";
        }

        private async void LoadPlansList()
        {
            collectionViewExercises.ItemsSource = await viewModel.GetExercisesAll();
            BtnFilter.Text = "All";

        }

        private async void BtnFilter_Clicked(object sender, EventArgs e)
        {
            if (filter == 2)
            {
                collectionViewExercises.ItemsSource = await viewModel.GetExercisesByFilter(filter);
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
                        collectionViewExercises.ItemsSource = await viewModel.GetExercisesByFilter(filter);
                        BtnFilter.Text = "Active";
                        filter = 2;
                    }
                }
            }

        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddExerciseRoutinePage());
        }

        private async void BtnExit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}