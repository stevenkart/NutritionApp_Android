using NutritionApp_Android.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp_Android.ViewModels
{
    public class NutritionalPlanViewModel : BaseViewModel
    {
        //VM gestiona los cambios que ocurren entre M y V.
        public NutritionalPlan MyNutritionalPlan { get; set; }


        public NutritionalPlanViewModel()
        {
            MyNutritionalPlan = new NutritionalPlan();

        }

        public async Task<bool> AddPlan(
                                        string pName,
                                        string pDescription,
                                        string pPlanXample,         
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
                MyNutritionalPlan.Name = pName;
                MyNutritionalPlan.Description = pDescription;
                MyNutritionalPlan.PlanXample = pPlanXample;
                MyNutritionalPlan.IdState = pStates;

                bool R = await MyNutritionalPlan.AddPlan();

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
