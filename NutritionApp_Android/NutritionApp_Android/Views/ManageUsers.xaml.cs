using NutritionApp_Android.Models;
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

        private async void UsersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersListView.SelectedItem != null)
            {
                User user = (User)UsersListView.SelectedItem;
                int IdUser = user.IdUser;
                int IdState = user.IdState;
                string UserName = user.FullName;

                int NewIdState = ChangeState(IdState);

                if(IdUser > 0)
                {
                    bool R = await viewModel.UpdateUserState(IdUser, NewIdState);

                    if (R)
                    {
                        await DisplayAlert(":)", string.Format("User {0} Updated Successfully!", UserName), "OK");
                        
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert(":(", string.Format("User Updated Unsuccessfully", UserName), "OK");
                    }

                }
                else
                {
                    await DisplayAlert("Error", "Select a user", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Select a user", "OK");
            }
        }

        private int ChangeState(int IdState)
        {
            int R = 0;

            if ( IdState == 1 ) R = 2;

            if ( IdState == 2 ) R = 1;

            return R;
        }

    }
}