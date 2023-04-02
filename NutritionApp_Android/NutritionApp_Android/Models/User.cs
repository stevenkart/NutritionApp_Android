using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.JsonPatch;
using System.Net.Http;
using System.Collections;
using NutritionApp_Android.ViewModels;

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
        public int? RecoveryCode { get; set; }
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
        

                string RouteSufix = string.Format("Users/ValidateUserLogin?pEmail={0}&pPassword={1}", this.Email, this.Password);

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



        public async Task<bool> UpdateUser()
        {
            try
            {

               

                this.IdUser = GlobalObjects.LocalUser.Id;

                string RouteSufix = string.Format("Users/{0}", this.IdUser);

                //con esto obtenemos la ruta completa de consumo
                string URL = Services.APIConnection.ProductionURLPrefix + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Patch);

                //Agregamos la info de la llave de seguridad (ApiKey)

                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);
                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                //En este caso tenemos que enviar un JSON al API con la data del usuario que se quiere agregar

                List<JsonObjectPatch> list = PatchMethod();

                string SerializedModel = JsonConvert.SerializeObject(list);

                Request.AddBody(SerializedModel, GlobalObjects.MimeType);


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

                
              
        public List<JsonObjectPatch> PatchMethod()
        {
            List<JsonObjectPatch> list = new List<JsonObjectPatch>();

            Hashtable HS = new Hashtable();

            HS.Add( "FullName", this.FullName );
            HS.Add( "Phone", this.Phone );
            HS.Add( "Email", this.Email );
            HS.Add( "Weight", this.Weight );
            HS.Add( "Hight", this.Hight );
            HS.Add( "Age", this.Age );
            HS.Add( "FatPercent", this.FatPercent );

            foreach (DictionaryEntry item in HS)
            {
                JsonObjectPatch JSON = new JsonObjectPatch();
                JSON.path = Convert.ToString( item.Key );
                JSON.op = "add";
                JSON.value = Convert.ToString( item.Value );

                list.Add( JSON );
            }

            return list;
        }
        


        //Funciones
        public async Task<bool> ValidateRecoveryCode()
        {
            try
            {
                string RouteSufix = string.Format("Users/ValidateCode?pEmail={0}&pCode={1}", this.Email, this.RecoveryCode);

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



        public async Task<bool> AddRecoveryCode(int id, string pPath, int pValue)
        {
            try
            {
                string RouteSufix = string.Format("Users/{0}", id);

                //con esto obtenemos la ruta completa deonsumo
                string URL = Services.APIConnection.ProductionURLPrefix + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Patch);


                //Agregamos la info de la llave de seguridad (ApiKey)

                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);
                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

    
                //usamos JsonPatch para ejecutar la sentencia de update correctamente tomando como metodo .Replace y
                //psanado parametro, el Path o ruta(nombre columna) & el valor a tomar
                var patch = new JsonPatchDocument();
                patch.Replace(pPath, pValue);

                string SerializedModel = JsonConvert.SerializeObject(patch);
                Request.AddBody(SerializedModel, GlobalObjects.PatchType);
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












    }
}
