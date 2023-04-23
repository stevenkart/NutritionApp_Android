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
using Xamarin.Essentials;
using NutritionApp_Android.ViewModels;
using System.Collections.ObjectModel;

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
                string RouteSufix = string.Format("Users/{0}", this.IdUser);

                string URL = Services.APIConnection.ProductionURLPrefix + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Patch);

                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);
                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);
                
                // JSON class -> build patch method
                JsonObjectPatch JSON = new JsonObjectPatch();
                JSON.JsonCollector.Add("FullName", this.FullName);
                JSON.JsonCollector.Add("Phone", this.Phone);
                JSON.JsonCollector.Add("Email", this.Email);
                JSON.JsonCollector.Add("Weight", this.Weight);
                JSON.JsonCollector.Add("Hight", this.Hight);
                JSON.JsonCollector.Add("Age", this.Age);
                JSON.JsonCollector.Add("FatPercent", this.FatPercent);
                List<JsonObjectPatch> JSONList = JSON.PatchMethod(JSON.JsonCollector);

                string SerializedModel = JsonConvert.SerializeObject(JSONList);

                Request.AddBody(SerializedModel, GlobalObjects.MimeType);

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

                throw;
            }
        }


        public async Task<bool> UpdatePassword()
        {
            try
            {
                string RouteSufix = string.Format("Users/{0}", this.IdUser);

                string URL = Services.APIConnection.ProductionURLPrefix + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Patch);

                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);
                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                JsonObjectPatch JSON = new JsonObjectPatch();
                JSON.JsonCollector.Add("Password", this.Password);
                List<JsonObjectPatch> JSONList = JSON.PatchMethod(JSON.JsonCollector);

                string SerializedModel = JsonConvert.SerializeObject(JSONList);

                Request.AddBody(SerializedModel, GlobalObjects.MimeType);

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

                throw;
            }
        }


        public async Task<bool> UpdateUserState()
        {
            try
            {
                string RouteSufix = string.Format("Users/{0}", this.IdUser);

                string URL = Services.APIConnection.ProductionURLPrefix + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Patch);

                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);
                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                JsonObjectPatch JSON = new JsonObjectPatch();
                JSON.JsonCollector.Add("IdState", this.IdState);
                List<JsonObjectPatch> JSONList = JSON.PatchMethod(JSON.JsonCollector);

                string SerializedModel = JsonConvert.SerializeObject(JSONList);

                Request.AddBody(SerializedModel, GlobalObjects.MimeType);

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

                throw;
            }
        }


        public async Task<bool> ValidateRecoveryCode()
        {
            try
            {
                string RouteSufix = string.Format("Users/ValidateRecoveryCode?pEmail={0}&pRecoveryCode={1}", this.Email, this.RecoveryCode);

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


        public async Task<bool> AddRecoveryCode(int id, int pValue)
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
                patch.Replace("recoveryCode", pValue);

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


        public async Task<bool> DeleteRecoveryCode(int id)
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
                patch.Replace("recoveryCode", 0);

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


        public async Task<bool> ChangePassword(int id, string pValue)
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
                patch.Replace("password", pValue);

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


        public async Task<bool> ChangePlan(int id, int pValue)
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
                patch.Replace("idPlan", pValue);

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
        public async Task<bool> ChangeRoutine(int id, int pValue)
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
                patch.Replace("idRoutine", pValue);

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

      


        public async Task<List<User>> GetUsersList()
        {

            try
            {
                string RouteSufix = string.Format("Users/GetUsersList?pUserStatus={0}", this.IdState);

                string URL = Services.APIConnection.ProductionURLPrefix + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);
                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                RestResponse response = await client.ExecuteAsync(Request);

                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.OK)
                {
                    var UserList = JsonConvert.DeserializeObject<List<User>>(response.Content);

                    return UserList;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;

                throw;
            }

        }



















    }
}
