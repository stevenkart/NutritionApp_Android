using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp_Android.Models
{
    public class NutritionalPlan
    {

        public RestRequest Request { get; set; }

        public NutritionalPlan()
        {

        }


        public int IdPlan { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PlanXample { get; set; }
        public int IdState { get; set; }

        public virtual State? IdStateNavigation { get; set; } = null!;


        public async Task<bool> AddPlan()
        {
            try
            {

                string RouteSufix = string.Format("NutritionalPlans");

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


        public async Task<bool> UpdatePlan(int pId)
        {
            try
            {

                string RouteSufix = string.Format("NutritionalPlans/id{0}", pId);

                //con esto obtenemos la ruta completa de consumo
                string URL = Services.APIConnection.ProductionURLPrefix + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Put);

                //Agregamos la info de la llave de seguridad (ApiKey)

                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);
                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                //En este caso tenemos que enviar un JSON al API con la data del usuario que se quiere actualizar

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
