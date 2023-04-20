using NutritionApp_Android.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp_Android.ViewModels
{
    public class ExerciseRoutineViewModel : BaseViewModel
    {
        public ExerciseRoutine MyExerciseRoutine { get; set; }

        public ExerciseRoutineViewModel()
        {
            MyExerciseRoutine = new ExerciseRoutine();

        }

        //Funciones
        //carga lista de datos de planes
        public async Task<List<ExerciseRoutine>> GetExercisesAll()
        {
            try
            {
                List<ExerciseRoutine> exercises = new List<ExerciseRoutine>();

                exercises = await MyExerciseRoutine.GetExercises();

                if (exercises == null)
                {
                    return null;
                }
                else
                {
                    return exercises;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<ExerciseRoutine>> GetExercisesByFilter(int state)
        {
            try
            {
                List<ExerciseRoutine> exercises = new List<ExerciseRoutine>();

                exercises = await MyExerciseRoutine.GetByFilterExerciseState(state);

                if (exercises == null)
                {
                    return null;
                }
                else
                {
                    return exercises;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> AddExercise(string pRoutineName, string pDescription, string pExerciseXample, int pStates = 1)
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
                MyExerciseRoutine.RoutineName = pRoutineName;
                MyExerciseRoutine.Description = pDescription;
                MyExerciseRoutine.ExerciseXample = pExerciseXample;
                MyExerciseRoutine.IdState = pStates;

                bool R = await MyExerciseRoutine.AddExercise();

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
        public async Task<ExerciseRoutine> GetExerciseData(int id)
        {
            try
            {
                ExerciseRoutine exercises = new ExerciseRoutine();

                exercises = await MyExerciseRoutine.GetSingleExercise(id);

                if (exercises == null)
                {
                    return null;
                }
                else
                {
                    return exercises;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<ExerciseRoutine>> GetCollectionRoutineData(int id)
        {
            try
            {
                List<ExerciseRoutine> exercises = new List<ExerciseRoutine>();

                exercises = await MyExerciseRoutine.GetRoutinesFilterId(id);

                if (exercises == null)
                {
                    return null;
                }
                else
                {
                    return exercises;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> UpdateExerciseName(int id, ExerciseRoutine exerciseRoutine)
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
                MyExerciseRoutine = exerciseRoutine;

                bool R = await MyExerciseRoutine.PatchExerciseName(id, MyExerciseRoutine.RoutineName);

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
        public async Task<bool> UpdateExerciseDescription(int id, ExerciseRoutine exerciseRoutine)
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
                MyExerciseRoutine = exerciseRoutine;

                bool R = await MyExerciseRoutine.PatchExerciseDescription(id, MyExerciseRoutine.Description);

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
        public async Task<bool> UpdateExerciseXample(int id, ExerciseRoutine exerciseRoutine)
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
                MyExerciseRoutine = exerciseRoutine;

                bool R = await MyExerciseRoutine.PatchExerciseDescription(id, MyExerciseRoutine.ExerciseXample);

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
        public async Task<bool> UpdateExerciseState(int id, ExerciseRoutine exerciseRoutine)
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
                MyExerciseRoutine = exerciseRoutine;

                bool R = await MyExerciseRoutine.PatchExerciseState(id, MyExerciseRoutine.IdState);

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
