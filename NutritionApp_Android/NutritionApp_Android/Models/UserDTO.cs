using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp_Android.Models
{
    public class UserDTO
    {
        public RestRequest Request { get; set; }



        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNum { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string Pass { get; set; } = null!;
        public decimal? Code { get; set; }
        public decimal W { get; set; }
        public decimal H { get; set; }
        public int Ages { get; set; }
        public decimal Fat { get; set; }
        public string Genres { get; set; } = null!;
        public int IdStates { get; set; }
        public int? IdPlans { get; set; }
        public int? IdRoutines { get; set; }


        //Funciones
        public async Task<UserDTO> GetUserData(string email)
        {
            try
            {
                //en APIConnection definimos uin prefijo para la ruta de consumo de los end points. Aca tenemos que agregar el resto de la ruta para la funcion que queremos usar
                //dentro del controller


                string RouteSufix = string.Format("Users/GetUserData?Correo={0}", email);

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
                    var list = JsonConvert.DeserializeObject<List<UserDTO>>(response.Content);

                    var item = list[0];

                    return item;
                }
                else
                {
                    return null;
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
