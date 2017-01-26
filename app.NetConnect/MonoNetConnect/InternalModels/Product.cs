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

namespace MonoNetConnect.InternalModels
{
    
    [Serializable]
    public class Product : BaseProperties, IApiPath
    {
        public static String ProductImagePath = @"Images/Product";
        public static String ProductApiPath = @"app/Product{/id}";

        [ApiPropertyName("")]
        public String Name { get; set; }
        [ApiPropertyName("")]
        public String Description { get; set; } = "";
        [ApiPropertyName("")]
        public Decimal Price { get; set; }
        [ApiPropertyName("")]
        public String ImageName { get; set; }
        [ApiPropertyName("")]
        public List<String> Attributes { get; set; } = new List<string>();

        public string ApiPath()
        {
            return ProductApiPath.Replace("{/id}",$"/{ID}");
        }
    }
}