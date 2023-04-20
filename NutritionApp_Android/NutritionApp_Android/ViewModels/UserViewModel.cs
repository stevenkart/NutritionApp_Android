using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NutritionApp_Android.Models;

namespace NutritionApp_Android.ViewModels
{
    public class UserViewModel : BaseViewModel
    {


        //VM gestiona los cambios que ocurren entre M y V.

        public NutritionalPlan MyUserNutritionalPlan { get; set; }
        public ExerciseRoutine MyUserExerciseRoutine { get; set; }
        public User MyUser { get; set; }
        public UserDTO MyUserDTO { get; set; }
        public Email MyEmail { get; set; }


        public UserViewModel()
        {
            MyUser = new User();
            MyUserNutritionalPlan = new NutritionalPlan();
            MyUserExerciseRoutine = new ExerciseRoutine();
            MyUserDTO = new UserDTO();
            MyEmail = new Email();

        }

        //FUNCIONALIDAD principal del VM

        public async Task<UserDTO> GetUserData(string pEmail)
        {

            if (IsBusy)
            {
                return null;
            }
            else
            {
                IsBusy = true;
            }

            try
            {
                UserDTO user = new UserDTO();

                user = await MyUserDTO.GetUserData(pEmail);

                if (user == null)
                {
                    return null;
                }
                else
                {
                    return user;
                }
            }
            catch (Exception)
            {
                return null;

                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }



        public async Task<bool> UserAccessValidation(string pEmail, string pPassword)
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
                MyUser.Email = pEmail;
                MyUser.Password = pPassword;

                bool R = await MyUser.ValidateLogin();

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



        public async Task<bool> AddUser(
                                       string pName,
                                       string pPhoneNum,
                                       string pEmailAddress,
                                       string pPass,
                                       decimal pW,
                                       decimal pH,
                                       int pAges,
                                       decimal pFat,
                                       string pGenres,
                                       //int pPlans,
                                       //int pRoutines,
                                       int pStates = 1)
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
                MyUser.FullName = pName;
                MyUser.Phone = pPhoneNum;
                MyUser.Email = pEmailAddress;
                MyUser.Password = pPass;
                MyUser.Weight = pW;
                MyUser.Hight = pH;
                MyUser.Age = pAges;
                MyUser.FatPercent = pFat;
                MyUser.Genre = pGenres;
                MyUser.IdState = pStates;
                MyUser.IdPlan = 1004;
                MyUser.IdRoutine = 1004;


                bool R = await MyUser.AddUser();

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



        public async Task<bool> AddRecoveryCode(int pId)
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
                Random rand = new Random();
                //char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
                char[] numbers = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
                string tempCode = null;

                for (int i = 0; i < 6; i++)
                {
                    tempCode += numbers[rand.Next(0, 9)];
                }
                int pCode = Convert.ToInt32(tempCode);

                //MyUser.RecoveryCode = pCode;

                bool R = await MyUser.AddRecoveryCode(pId, pCode);

                //UNA VEZ QUE SE HAYA GUARDADO CORRECTAMENTE EL RECOVERYCODE, SE ENVIA EL EMAIL
                if (R)
                {
                    MyEmail.SendTo = "robertstevenumca0@gmail.com"; // se usa ese para el testeo...pero usuario si o si debe indicar el correo que esta en la BD
                    MyEmail.Subject = "AppNutritio Password Recovery Code";
                    MyEmail.Message = string.Format("Your recovery code is: {0}", pCode);

                    R = (MyEmail.SendEmail());

                }
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



        public async Task<bool> UpdateUser(
                                        string pName,
                                        string pPhoneNum,
                                        string pEmailAddress,
                                        decimal pW,
                                        decimal pH,
                                        int pAges,
                                        decimal pFat)
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

                MyUser.FullName = pName;
                MyUser.Phone = pPhoneNum;
                MyUser.Email = pEmailAddress;
                MyUser.Weight = pW;
                MyUser.Hight = pH;
                MyUser.Age = pAges;
                MyUser.FatPercent = pFat;

                bool R = await MyUser.UpdateUser();

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


        public async Task<bool> UpdatePassword( string pPassword )
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

                MyUser.Password = pPassword;

                bool R = await MyUser.UpdatePassword();

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
        public async Task<bool> UserRecoveryCodeValidation(string pEmail, int pRecoveryCode)
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
                MyUser.Email = pEmail;
                MyUser.RecoveryCode = pRecoveryCode;

                bool R = await MyUser.ValidateRecoveryCode();

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
        public async Task<bool> ChangePassword(int pId, string pPassword)
        {
            bool R = false;
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
                R = await MyUser.ChangePassword(pId, pPassword);

                if (R)
                {
                    R = await MyUser.DeleteRecoveryCode(pId);
                }

                if (R)
                {
                    MyEmail.SendTo = "robertstevenumca0@gmail.com";
                    MyEmail.Subject = "AppNutrition Password Changed!";
                    MyEmail.Message = string.Format("Your Password has been changed successfully, try to log in." + Environment.NewLine + Environment.NewLine +
                        "In Case you did not request a change password, " +
                        "do not hesitate on contact the administrators as soon as possible." + Environment.NewLine + Environment.NewLine +
                        "Best Regards." + Environment.NewLine + Environment.NewLine +
                        "AppNutrition Team.");

                    R = (MyEmail.SendEmail());

                }
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
        public async Task<bool> ChangeNutritionalPlan(int pId, int pIdPlan)
        {
            bool R = false;
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
                R = await MyUser.ChangePlan(pId, pIdPlan);
 
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
        public async Task<bool> ChangeRoutine(int pId, int pIdRoutine)
        {
            bool R = false;
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
                R = await MyUser.ChangeRoutine(pId, pIdRoutine);

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




    }
}
