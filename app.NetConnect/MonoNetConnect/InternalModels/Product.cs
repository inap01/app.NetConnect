using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MonoNetConnect.Cache;
using Newtonsoft.Json;

namespace MonoNetConnect.InternalModels
{
    
    [Serializable]
    public class Product : BaseProperties, IApiModels
    {
        private static String ProductImagePath = @"Images/Product";
        private static String ProductApiPath = @"api.php/app/Product{/id}";

        [JsonProperty("")]
        public String Name { get; set; }
        [JsonProperty("")]
        public String Description { get; set; } = "";
        [JsonProperty("")]
        public Decimal Price { get; set; }
        [ApiPropertyName("Image")]
        [JsonProperty("")]
        public String ImageName { get; set; }
        [JsonProperty("")]
        public List<String> Attributes { get; set; } = new List<string>();

        public string ApiPath()
        {
            return ProductApiPath.Replace("{/id}",$"/{ID}");
        }

        public string ImageDirectoryPath()
        {
            return ProductImagePath;
        }

        public bool IsClassWithImage()
        {
            return true;
        }
    }
}