﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NutritionApp_Android.Services
{
    public class APIConnection
    {
        //en esta clase definimos el Prefijo de consumo de las rutas de los controladores del API 
        //Normalmente los AI trabajan con una version de pruebas y otra en produccion 

        //ademas aca vamos a escibir la infor del API key que necesitamos para podernos validar 

        //public static string TestingnURLPrefix = "http://192.168.1.237:45455/api/";
        //public static string ProductionURLPrefix = "http://192.168.1.237:45455/api/";

        public static string TestingnURLPrefix = "http://192.168.23.1:45455/api/";
        public static string ProductionURLPrefix = "http://192.168.23.1:45455/api/";



        public static string ApiKeyName = "P6ApiKey";
        public static string ApiKeyValue = "AppNutritionJQWERTYINDSBNJDCSBNKL0001*";

    }
}
