using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace NutritionApp_Android.Models
{
    public class User
    {

        public RestRequest Request { get; set; }

        public User()
        {

        }

        public int IdUser { get; set; }
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public decimal? RecoveryCode { get; set; }
        public decimal Weight { get; set; }
        public decimal Hight { get; set; }
        public int Age { get; set; }
        public decimal FatPercent { get; set; }
        public string Genre { get; set; } = null!;
        public int IdState { get; set; }
        public int? IdPlan { get; set; }
        public int? IdRoutine { get; set; }

        public virtual NutritionalPlan? IdPlanNavigation { get; set; } = null!;
        public virtual ExerciseRoutine? IdRoutineNavigation { get; set; } = null!;
        public virtual State? IdStateNavigation { get; set; } = null!;



        //Funciones
        public async Task<bool> ValidateLogin()
        {
            try
            {
        

                string RouteSufix = string.Format("Users/ValidateUserLogin?pUserName={0}&pPassword={1}", this.Email, this.Password);

                //con esto obtenemos la ruta completa de consumo
                string URL = Services.APIConnection.ProductionURLPrefix + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                //Agregamos la info de la llave de seguridad (ApiKey)
                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);
                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                //ejecucion de la llamada al controlador
                RestResponse response = await client.ExecuteAsync(Request);

                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;

                //almacenar registro de errores en una bitacora para analisis posteriores
                //tambien puede ser enviarlos a un servidor de captura de errores

                throw;
            }
        }



        public async Task<bool> AddUser()
        {
            try
            {

                string RouteSufix = string.Format("Users");

                //con esto obtenemos la ruta completa de consumo
                string URL = Services.APIConnection.ProductionURLPrefix + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Post);

                //Agregamos la info de la llave de seguridad (ApiKey)

                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);
                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                //En este caso tenemos que enviar un JSON al API con la data del usuario que se quiere agregar

                string SerializedModel = JsonConvert.SerializeObject(this);



                Request.AddBody(SerializedModel, GlobalObjects.MimeType);


                //ejecucion de la llamada al controlador
                RestResponse response = await client.ExecuteAsync(Request);

                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.Created)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;

                //almacenar registro de errores en una bitacora para analisis posteriores
                //tambien puede ser enviarlos a un servidor de captura de errores

                throw;
            }
        }















    }
}
