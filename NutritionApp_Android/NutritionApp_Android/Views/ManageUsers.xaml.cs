using NutritionApp_Android.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace NutritionApp_Android.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageUsers : ContentPage
    {

        public int IdStatus { get; set; }

        UserViewModel viewModel;

        public ManageUsers()
        {
            InitializeComponent();

            BindingContext = viewModel = new UserViewModel();

            this.IdStatus = 1;
        }

        protected override void OnAppearing() //metodo que al volver a mostrar la pagina, vuelve a refrescar la lista con la nueva DATA
        {
            LoadPage();
        }


        private async void LoadPage()
        {
            UsersListView.ItemsSource = await viewModel.GetUsersList( this.IdStatus );
        }

        private void BtnUpdateUser_Clicked(object sender, EventArgs e)
        {

        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void SwShowInactiveUsers_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwShowInactiveUsers.IsToggled)
            {
                ActiveSwitch();
            }
            else
            {
                InactiveSwitch();
            }
        }

        private void ActiveSwitch()
        {
            LblUsersStatus.Text = "Active Users";
            this.IdStatus = 1;
            LoadPage();
        }

        private void InactiveSwitch()
        {
            LblUsersStatus.Text = "Inactive Users";
            this.IdStatus = 2;
            LoadPage();
        }


    }
}