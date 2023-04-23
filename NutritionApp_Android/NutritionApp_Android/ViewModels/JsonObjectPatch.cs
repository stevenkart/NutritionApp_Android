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

        public Hashtable JsonCollector;

        public List<JsonObjectPatch> JsonList;

        public JsonObjectPatch() { 
        
            JsonCollector = new Hashtable();
            
            JsonList = new List<JsonObjectPatch>();

        }

        public List<JsonObjectPatch> PatchMethod( Hashtable HT )
        {
            foreach (DictionaryEntry item in HT)
            {
                JsonObjectPatch JSON = new JsonObjectPatch();
                JSON.path = Convert.ToString( item.Key );
                JSON.op = "add";
                JSON.value = Convert.ToString( item.Value );

                JsonList.Add( JSON );
            }

            return JsonList;
        }
    }
}
