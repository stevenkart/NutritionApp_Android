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
                //MyUser.IdPlan = pPlans;
                //MyUser.IdRoutine = pRoutines;


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




        public async Task<bool> AddRecoveryCode()
                                       // decimal pCode)
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

                int pCode = 25;
                //MyUser.RecoveryCode = pCode;
                
                bool R = await MyUser.AddRecoveryCode(1, "recoveryCode", pCode);

                //UNA VEZ QUE SE HAYA GUARDADO CORRECTAMENTE EL RECOVERYCODE, SE ENVIA EL EMAIL
                if (R)
                {
                    MyEmail.SendTo = "robertstevenumca0@gmail.com";
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














    }
}
