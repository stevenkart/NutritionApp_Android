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
    public partial class ExerciseRoutinePage : ContentPage
    {
     
        ExerciseRoutineViewModel vm;
        public ExerciseRoutinePage()
        {
            InitializeComponent();
            BindingContext = vm = new ExerciseRoutineViewModel();
            if (string.IsNullOrEmpty(Convert.ToString(GlobalObjects.LocalUser.IdRoutines)) || (Convert.ToInt32(GlobalObjects.LocalUser.IdRoutines) == 1004))
            {
                collectionViewRoutine.ItemsSource = null;
                lblNotRoutine.IsEnabled = true;
                lblNotRoutine.IsVisible = true;
                BtnAdd.Text = "Add";
            }
            else
            {
                LoadPage(Convert.ToInt32(GlobalObjects.LocalUser.IdRoutines));
                lblNotRoutine.IsEnabled = false;
                lblNotRoutine.IsVisible = false;
                BtnAdd.Text = "Change";
            }

        }

        protected override void OnAppearing() //metodo que al volver a mostrar la pagina, vuelve a refrescar la lista con la nueva DATA
        {
            if (string.IsNullOrEmpty(Convert.ToString(GlobalObjects.LocalUser.IdRoutines)) || (Convert.ToInt32(GlobalObjects.LocalUser.IdRoutines) == 1004))
            {
                collectionViewRoutine.ItemsSource = null;
                lblNotRoutine.IsEnabled = true;
                lblNotRoutine.IsVisible = true;
                BtnAdd.Text = "Add";
            }
            else
            {
                LoadPage(Convert.ToInt32(GlobalObjects.LocalUser.IdRoutines));
                lblNotRoutine.IsEnabled = false;
                lblNotRoutine.IsVisible = false;
                BtnAdd.Text = "Change";
            }
        }
        private async void LoadPage(int id)
        {
            collectionViewRoutine.ItemsSource = await vm.GetCollectionRoutineData(id);
        }
        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {

            if (BtnAdd.Text == "Change")
            {
                var itemSelected = collectionViewRoutine.SelectedItem as ExerciseRoutine;
                if (itemSelected != null)
                {
                    if (itemSelected != null)
                    {
                        var action = await DisplayAlert("Are you sure to edit/change the Rotine?",
                       "Rotine: " + $"{itemSelected.RoutineName}" + Environment.NewLine + "", "Continue...", "Cancel");

                        if (action)
                        {
                            await Navigation.PushAsync(new UserAddEditExercisePage(itemSelected.IdRoutine));
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Rotine Selected ", "First of all, you need to select  the Item to change", "OK");
                }
            }
            else
            {
                await Navigation.PushAsync(new UserAddEditExercisePage(0));
            }

        }

        private async void collectionViewRoutine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemSelected = e.CurrentSelection[0] as ExerciseRoutine;
            if (itemSelected != null)
            {
                await DisplayAlert("Plan Selected ", $"{itemSelected.RoutineName}" + Environment.NewLine +
                    $"{itemSelected.Description}", "OK");
            }
        }
    }
}