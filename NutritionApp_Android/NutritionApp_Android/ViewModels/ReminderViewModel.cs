using NutritionApp_Android.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NutritionApp_Android.ViewModels
{
    public class ReminderViewModel : BaseViewModel
    {

        public Reminders MyReminder { get; set; }

        public ReminderViewModel()
        {
            MyReminder = new Reminders();
        }



        public async Task<bool> AddReminder(
                                        string pDetail,
                                        string pDate,
                                        string pHour
                                        )
        {

            if (IsBusy)
            {
                return false;
            }
            else
            {
                IsBusy = true;
            }

            try
            {

                int pIdUser = GlobalObjects.LocalUser.Id;

                MyReminder.Detail = pDetail;
                MyReminder.Date = Convert.ToString( pDate );
                MyReminder.Hour = Convert.ToString( pHour );
                MyReminder.Done = true;
                MyReminder.IdUser = pIdUser;

                bool R = await MyReminder.AddReminder();

                return R;

            }
            catch (Exception)
            {
                return false;

                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }



        public async Task<List<Reminders>> GetReminders()
        {
            try
            {

                List<Reminders> reminders = new List<Reminders>();

                reminders = await MyReminder.GetUserReminders();

                return reminders == null ? null : reminders;

            }
            catch (Exception)
            {
                throw;
            }
        }














    }
}
