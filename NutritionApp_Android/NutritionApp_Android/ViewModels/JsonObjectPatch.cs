using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NutritionApp_Android.ViewModels
{
    public class JsonObjectPatch
    {
        public string path { get; set; }
        public string op { get; set; }
        public string value { get; set; }
    }
}
