using NutritionApp_Android.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NutritionApp_Android
{
    public class GlobalObjects
    {
        public static string MimeType = "application/json";
        public static string PatchType = "application/json-patch+json";
        public static string ContentType = "Content-Type";


        //agregar usuario global igual que en progra 5

        public static UserDTO LocalUser = new UserDTO();

        public static Reminders LocalReminder = new Reminders();
    }
}
