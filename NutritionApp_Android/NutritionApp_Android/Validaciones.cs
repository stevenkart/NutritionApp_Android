using System;
using System.Collections.Generic;
using System.Text;

namespace NutritionApp_Android
{
    public static class Validaciones
    {

        private static char g_Gen_DecimalSeparator = Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString());

        public static bool CaracteresTexto(Xamarin.Forms.TextChangedEventArgs c, bool Mayusculas = false, bool Minisculas = false)
        {
            bool ret = false;

            if (Mayusculas)
            { c.NewTextValue.ToUpperInvariant(); }

            if (Minisculas)
            { c.NewTextValue.ToLowerInvariant(); }

            if (!(char.IsLetterOrDigit(Convert.ToChar(c.ToString())) & !(char.IsPunctuation(Convert.ToChar(c.ToString())))))
                ret = true;
            else
                ret = false;
            return
            ret;

        }

    
        

        public static bool IsValidEmail(string email)
        {
            try
            {
                var validacion = new System.Net.Mail.MailAddress(email);
                return validacion.Address == email;
            }
            catch
            {
                return false;
            }
        }
        
 

        /*
        public static bool IsValidPass(string pass)
        {
            bool R = true;
            Regex longitud = new Regex(@".{8,16}");
            Regex mayuscula = new Regex(@"[A-Z]+");
            Regex minuscula = new Regex(@"[a-z]+");
            Regex numero = new Regex("[0-9]");
            Regex caracterEspecial = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (pass.Length > 16)
            {
                MessageBox.Show("Contraseña debe ser entre 8 a 16 carácteres",
                    "Error en Contraseña", MessageBoxButtons.OK);
                return false;
            }

            if (!longitud.IsMatch(pass))
            {
                MessageBox.Show("Contraseña debe ser entre 8 a 16 carácteres",
                    "Error en Contraseña", MessageBoxButtons.OK);
                return false;
            }
            if (!mayuscula.IsMatch(pass))
            {
                MessageBox.Show("Contraseña debe tener al menos 1 mayúscula",
                    "Error en Contraseña", MessageBoxButtons.OK);
                return false;
            }
            if (!minuscula.IsMatch(pass))
            {
                MessageBox.Show("Contraseña debe tener al menos 1 minúscula",
                    "Error en Contraseña", MessageBoxButtons.OK);
                return false;
            }
            if (!numero.IsMatch(pass))
            {
                MessageBox.Show("Contraseña debe tener al menos un número",
                    "Error en Contraseña", MessageBoxButtons.OK);
                return false;
            }
            if (!caracterEspecial.IsMatch(pass))
            {
                MessageBox.Show("Contraseña debe tener al menos un carácter especial",
                    "Error en Contraseña", MessageBoxButtons.OK);
                return false;
            }
            return R;
            
        }
        */

    }
}
