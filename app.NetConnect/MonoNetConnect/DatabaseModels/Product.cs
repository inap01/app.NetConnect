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
using MonoNetConnect.DatabaseModels;

namespace MonoNetConnect.Models
{
    class Product : BaseProperties
    {
        public String Name { get; set; }
        public String Description { get; set; } = "";
        public Decimal Price { get; set; }
        public List<String> Attributes { get; set; } = new List<string>();
    }
}