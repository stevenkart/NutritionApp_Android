using NutritionApp_Android.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp_Android.ViewModels
{
    public class NutritionalPlanViewModel : BaseViewModel
    {
        public NutritionalPlan MyNutritionalPlan { get; set; }

        public NutritionalPlanViewModel()
        {
            MyNutritionalPlan = new NutritionalPlan();
 
        }

        //Funciones
        //carga lista de datos de planes
        public async Task<List<NutritionalPlan>> GetNutritionalPlansAll()
        {
            try
            {
                List<NutritionalPlan> plans = new List<NutritionalPlan>();

                plans = await MyNutritionalPlan.GetPlans();

                if (plans == null)
                {
                    return null;
                }
                else
                {
                    return plans;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<NutritionalPlan>> GetPlansByFilter(int state)
        {
            try
            {
                List<NutritionalPlan> plans = new List<NutritionalPlan>();

                plans = await MyNutritionalPlan.GetByFilterPlanState(state);

                if (plans == null)
                {
                    return null;
                }
                else
                {
                    return plans;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> AddPlan(string pName, string pDescription, string pPlanXample, int pStates = 1)
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
        public async Task<NutritionalPlan> GetPlanData(int id)
        {
            try
            {
                NutritionalPlan plan = new NutritionalPlan();

                plan = await MyNutritionalPlan.GetSinglePlan(id);

                if (plan == null)
                {
                    return null;
                }
                else
                {
                    return plan;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> UpdatePlanName(int id, NutritionalPlan nutritionalPlan)
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
                MyNutritionalPlan = nutritionalPlan;

                bool R = await MyNutritionalPlan.PatchPlanName(id, MyNutritionalPlan.Name);

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
        public async Task<bool> UpdatePlanDescription(int id, NutritionalPlan nutritionalPlan)
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
                MyNutritionalPlan = nutritionalPlan;

                bool R = await MyNutritionalPlan.PatchPlanDescription(id, MyNutritionalPlan.Description);

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
        public async Task<bool> UpdatePlanXample(int id, NutritionalPlan nutritionalPlan)
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
                MyNutritionalPlan = nutritionalPlan;

                bool R = await MyNutritionalPlan.PatchPlanXample(id, MyNutritionalPlan.PlanXample);

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
        public async Task<bool> UpdatePlanState(int id, NutritionalPlan nutritionalPlan)
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
                MyNutritionalPlan = nutritionalPlan;

                bool R = await MyNutritionalPlan.PatchPlanState(id, MyNutritionalPlan.IdState);

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
